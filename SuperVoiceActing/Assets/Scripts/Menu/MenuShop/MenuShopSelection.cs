/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuShopSelection : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Data")]
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        MenuContractMoney menuContractMoney;

        [Header("Equipement")]
        [SerializeField]
        ButtonEquipement buttonPrefab;


        [Header("Category")]
        [SerializeField]
        RectTransform selectionCategories;
        [HorizontalGroup]
        [SerializeField]
        RectTransform[] transformCategories;
        [HorizontalGroup]
        [SerializeField]
        EquipementCategory[] categories;

        [SerializeField]
        List<ButtonEquipement> buttonEquipementList = new List<ButtonEquipement>();
        [SerializeField]
        List<EquipementData> equipementsShowcase;

        [SerializeField]
        int saleDivision = 4;

        bool buyMode = true;
        int indexCreateList = 0;

        int indexCategory = 0;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetEquipementsShowcase(List<EquipementData> equipements, bool isBuyMode)
        {
            buyMode = isBuyMode;
            equipementsShowcase = equipements;
            menuContractMoney.ActivateEstimate();
            indexSelected = 0;
            indexCategory = 0;

            DrawEquipementInventory(categories[indexCategory]);
            AfterSelection();
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void Validate()
        {
            if(buyMode == true) // Buy
            {
                if(playerData.Money >= buttonEquipementList[indexSelected].GetEquipement().Price)
                {
                    menuContractMoney.AuditionFeedback();
                    playerData.Money -= buttonEquipementList[indexSelected].GetEquipement().Price;
                }
            }
            else // Sell
            {
                menuContractMoney.AddSalaryFast((buttonEquipementList[indexSelected].GetEquipement().Price / saleDivision));
                playerData.Money += (buttonEquipementList[indexSelected].GetEquipement().Price / saleDivision);
                equipementsShowcase.Remove(buttonEquipementList[indexSelected].GetEquipement());
                DrawEquipementInventory(categories[indexCategory]);
            }
        }


        protected override void AfterSelection()
        {
            if (buyMode == true)
                menuContractMoney.DrawEstimate(-buttonEquipementList[indexSelected].GetEquipement().Price);
            else
                menuContractMoney.DrawEstimate((buttonEquipementList[indexSelected].GetEquipement().Price / saleDivision));
        }







        public void SelectCategoryRight()
        {
            if (transformCategories.Length == 0)
            {
                return;
            }
            indexSelected = 0;
            indexCategory += 1;
            if (indexCategory >= transformCategories.Length)
            {
                indexCategory = 0;
            }
            selectionCategories.transform.position = transformCategories[indexCategory].transform.position;
            DrawEquipementInventory(categories[indexCategory]);
        }

        public void SelectCategoryLeft()
        {
            if (transformCategories.Length == 0)
            {
                return;
            }
            indexSelected = 0;
            indexCategory -= 1;
            if (indexCategory <= -1)
            {
                indexCategory = transformCategories.Length - 1;
            }
            selectionCategories.transform.position = transformCategories[indexCategory].transform.position;
            DrawEquipementInventory(categories[indexCategory]);
        }






        private void DrawEquipementInventory(EquipementCategory eqCategory)
        {
            if (eqCategory == EquipementCategory.All)
            {
                for (int i = 0; i < equipementsShowcase.Count; i++)
                {
                    CreateButtonEquipement(equipementsShowcase[i]);
                }
            }
            else
            {
                for (int i = 0; i < equipementsShowcase.Count; i++)
                {
                    if (equipementsShowcase[i].EquipementCategory == eqCategory)
                    {
                        CreateButtonEquipement(equipementsShowcase[i]);
                    }
                }
            }
            CleanButtons();
        }

        private void CreateButtonEquipement(EquipementData eqData)
        {
            if (buttonEquipementList.Count <= indexCreateList) // On doit créer un nouveau bouton
            {
                buttonEquipementList.Add(Instantiate(buttonPrefab, buttonScrollListTransform));
                buttonEquipementList[indexCreateList].DrawEquipement(eqData);
                buttonsList.Add(buttonEquipementList[indexCreateList].GetRectTransform());
            }
            else // On peut réécrire un bouton existant
            {
                buttonEquipementList[indexCreateList].DrawEquipement(eqData);
                if (buttonsList.Count <= indexCreateList)
                    buttonsList.Add(buttonEquipementList[indexCreateList].GetRectTransform());
                else
                    buttonsList[indexCreateList] = buttonEquipementList[indexCreateList].GetRectTransform();
            }
            buttonEquipementList[indexCreateList].gameObject.SetActive(true);
            indexCreateList += 1;
        }

        private void CleanButtons()
        {
            for (int i = indexCreateList; i < buttonEquipementList.Count; i++)
            {
                buttonEquipementList[i].gameObject.SetActive(false);
                if (buttonsList.Count > indexCreateList)
                    buttonsList.RemoveAt(buttonsList.Count - 1);
            }
            indexCreateList = 0;
        }

        #endregion

    } // MenuShopSelection class
	
}// #PROJECTNAME# namespace
