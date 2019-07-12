/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuStudioEquipementSelection : MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Menu Equipement Selection")]
        [SerializeField]
        ButtonEquipement buttonPrefab;

        [SerializeField]
        List<ButtonEquipement> buttonEquipementList = new List<ButtonEquipement>();

        [Space]
        [SerializeField]
        TextMeshProUGUI atkDetailLabel;
        [HorizontalGroup("EquipementAtkDetail")]
        [SerializeField]
        Image[] imageAtkDetail;
        [HorizontalGroup("EquipementAtkDetail")]
        [SerializeField]
        TextMeshProUGUI[] textAtkDetail;

        [Space]
        [SerializeField]
        TextMeshProUGUI defDetailLabel;
        [HorizontalGroup("EquipementDefDetail")]
        [SerializeField]
        Image[] imageDefDetail;
        [HorizontalGroup("EquipementDefDetail")]
        [SerializeField]
        TextMeshProUGUI[] textDefDetail;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        Color colorZero;
        [SerializeField]
        Color colorPositive;
        [SerializeField]
        Color colorNegative;

        int indexCreateList = 0;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void SelectionOn(bool b)
        {
            animatorSelection.gameObject.SetActive(b);
            indexSelected = 0;
            if (b == false)
                DrawEquipementSelectedDetail(null);
            else
                AfterSelection();
        }


        public void CreateButtonEquipement(EquipementData eqData)
        {
            if(buttonEquipementList.Count <= indexCreateList) // On doit créer un nouveau bouton
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

        public void CleanButtons()
        {
            for(int i = indexCreateList; i < buttonEquipementList.Count; i++)
            {
                buttonEquipementList[i].gameObject.SetActive(false);
                if(buttonsList.Count > indexCreateList)
                    buttonsList.RemoveAt(buttonsList.Count-1);
            }
            indexCreateList = 0;
        }

















        private void DrawEquipementSelectedDetail(EquipementData eqData)
        {
            atkDetailLabel.color = colorZero;
            defDetailLabel.color = colorZero;
            if (eqData != null)
            {
                for (int i = 0; i < textAtkDetail.Length; i++)
                {
                    textAtkDetail[i].text = eqData.AtkBonus.GetEmotion(i + 1).ToString();
                    if (eqData.AtkBonus.GetEmotion(i + 1) == 0)
                    {
                        textAtkDetail[i].color = colorZero;
                    }
                    else if (eqData.AtkBonus.GetEmotion(i + 1) > 0)
                    {
                        textAtkDetail[i].color = colorPositive;
                        atkDetailLabel.color = Color.white;
                    }
                    else if (eqData.AtkBonus.GetEmotion(i + 1) < 0)
                    {
                        textAtkDetail[i].color = colorNegative;
                        atkDetailLabel.color = Color.white;
                    }
                    imageAtkDetail[i].color = new Color(imageAtkDetail[i].color.r, imageAtkDetail[i].color.g, imageAtkDetail[i].color.b, textAtkDetail[i].color.a);


                    textDefDetail[i].text = eqData.DefBonus.GetEmotion(i + 1).ToString();
                    if (eqData.DefBonus.GetEmotion(i + 1) == 0)
                    {
                        textDefDetail[i].color = colorZero;
                    }
                    else if (eqData.DefBonus.GetEmotion(i + 1) > 0)
                    {
                        textDefDetail[i].color = colorPositive;
                        defDetailLabel.color = Color.white;
                    }
                    else if (eqData.DefBonus.GetEmotion(i + 1) < 0)
                    {
                        textDefDetail[i].color = colorNegative;
                        defDetailLabel.color = Color.white;
                    }
                    imageDefDetail[i].color = new Color(imageDefDetail[i].color.r, imageDefDetail[i].color.g, imageDefDetail[i].color.b, textDefDetail[i].color.a);
                }
            }
            else
            {
                for (int i = 0; i < textAtkDetail.Length; i++)
                {
                    textAtkDetail[i].text = "0";
                    textDefDetail[i].text = "0";
                    textAtkDetail[i].color = colorZero;
                    textDefDetail[i].color = colorZero;
                    imageAtkDetail[i].color = new Color(imageAtkDetail[i].color.r, imageAtkDetail[i].color.g, imageAtkDetail[i].color.b, textAtkDetail[i].color.a);
                }
            }
        }

        protected override void AfterSelection()
        {
            DrawEquipementSelectedDetail(buttonEquipementList[indexSelected].GetEquipement());
        }






        #endregion

    } // MenuStudioEquipementSelection class
	
}// #PROJECTNAME# namespace
