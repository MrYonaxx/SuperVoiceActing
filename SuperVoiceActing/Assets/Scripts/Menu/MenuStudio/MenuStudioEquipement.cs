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

        [Space]
        [Space]

        [HorizontalGroup("Def Recap")]
        [SerializeField]
        TextMeshProUGUI[] textDefRecap;

        [Space]
        [Space]
        [HorizontalGroup("Def Recap")]
        [SerializeField]
        TextMeshProUGUI[] textNewDefRecap;

        [SerializeField]
        ButtonEquipement[] buttonEquipements;

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
            /*animatorMenu.SetBool("Appear", true);
            animatorMenu.gameObject.SetActive(true);*/
            DrawPlayerStat();
            DrawEquipements();
            
        }

        private void DrawPlayerStat()
        {
            for(int i = 0; i < textAtkRecap.Length; i++)
            {
                textAtkRecap[i].text = playerData.AtkBonus.GetEmotion(i).ToString();
                textAtkRecap[i].color = Color.white;
                /*textDefRecap[i].text = playerData.DefBonus.GetEmotion(i).ToString();
                textDefRecap[i].color = Color.white;*/
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

        }

        private void DrawEquipementInventory(EquipementCategory eqCategory)
        {

        }

        public void Validate()
        {

        }

        #endregion

    } // MenuStudioEquipement class
	
}// #PROJECTNAME# namespace
