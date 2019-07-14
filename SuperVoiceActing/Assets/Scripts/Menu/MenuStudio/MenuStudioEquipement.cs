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
	public class MenuStudioEquipement : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


        [Title("Menu Studio Equipement")]
        [InfoBox("Joie > Tristesse > Dégoût > Colère > Surprise > Douceur > Peur > Confiance")]
        // The one who own all stats
        [SerializeField]
        PlayerData playerData;

        [HorizontalGroup("Atk Recap")]
        [SerializeField]
        TextMeshProUGUI[] textAtkRecap;
        [HorizontalGroup("Atk Recap")]
        [SerializeField]
        TextMeshProUGUI[] textNewAtkRecap;
        [HorizontalGroup("Def Recap")]
        [SerializeField]
        TextMeshProUGUI[] textDefRecap;
        [HorizontalGroup("Def Recap")]
        [SerializeField]
        TextMeshProUGUI[] textNewDefRecap;

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

        [Space]
        [Space]
        [Space]
        [SerializeField]
        ButtonEquipement[] buttonEquipements;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        MenuStudioEquipementSelection menuEquipementSelection;

        [SerializeField]
        Animator animatorMenu;


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


        private void OnEnable()
        {
            DrawPlayerStat();
            DrawEquipements();
            animatorMenu.gameObject.SetActive(true);

        }

        private void OnDisable()
        {
            animatorMenu.gameObject.SetActive(false);

        }

        private void DrawPlayerStat()
        {
            for(int i = 0; i < textAtkRecap.Length; i++)
            {
                textAtkRecap[i].text = playerData.AtkBonus.GetEmotion(i).ToString();
                textAtkRecap[i].color = Color.white;
                textDefRecap[i].text = playerData.DefBonus.GetEmotion(i).ToString();
                textDefRecap[i].color = Color.white;
            }
        }

        private void DrawEquipements()
        {
            for(int i = 0; i < playerData.CurrentEquipement.Length; i++)
            {
                buttonEquipements[i].DrawEquipement(playerData.CurrentEquipement[i]);
            }
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

        private void DrawEquipementInventory(EquipementCategory eqCategory)
        {
            for(int i = 0; i < playerData.InventoryEquipement.Count; i++)
            {
                if(playerData.InventoryEquipement[i].EquipementCategory == eqCategory)
                {
                    menuEquipementSelection.CreateButtonEquipement(playerData.InventoryEquipement[i]);
                }
            }
            menuEquipementSelection.CleanButtons();
        }

        protected override void AfterSelection()
        {
            DrawEquipementSelectedDetail(playerData.CurrentEquipement[indexSelected]);
            DrawEquipementInventory(playerData.EquipementCategories[indexSelected]);
        }

        public void Validate()
        {
            this.gameObject.SetActive(false);
            menuEquipementSelection.gameObject.SetActive(true);
            menuEquipementSelection.SelectionOn(true);
        }


        #endregion

    } // MenuStudioEquipement class
	
}// #PROJECTNAME# namespace
