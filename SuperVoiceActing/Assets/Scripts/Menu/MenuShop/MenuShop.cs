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
	public class MenuShop : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("Menu Shop")]
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        GameObject menuShopWindow;
        [SerializeField]
        MenuShopSelection menuShopSelection;
        [SerializeField]
        int buyOptionIndex = 0;
        [SerializeField]
        int sellOptionIndex = 1;
        [SerializeField]
        int talkOptionIndex = 2;
        [SerializeField]
        int exitOptionIndex = 3;
        [SerializeField]
        Animator animatorTransition;
        [SerializeField]
        CameraBureau cameraDekstop;

        [Title("Menu Shop")]
        [SerializeField]
        List<EquipementData> shopCurrentStockDebug;

        [Title("Dialogue")]
        [SerializeField]
        StoryEventData storyEventData;


        List<Equipement> shopCurrentStock = new List<Equipement>();

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

        protected override void Start()
        {
            base.Start();
            if(shopCurrentStockDebug != null)
            {
                for(int i = 0; i < shopCurrentStockDebug.Count; i++)
                {
                    shopCurrentStock.Add(new Equipement(shopCurrentStockDebug[i]));
                }          
            }
        }

        public void Validate()
        {
            
            if (indexSelected == buyOptionIndex)
            {
                menuShopSelection.SetEquipementsShowcase(shopCurrentStock, true);
                menuShopSelection.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else if(indexSelected == sellOptionIndex)
            {
                menuShopSelection.SetEquipementsShowcase(playerData.InventoryEquipement, false);
                menuShopSelection.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else if (indexSelected == talkOptionIndex)
            {
                return;
            }
            else if (indexSelected == exitOptionIndex)
            {
                ExitMenuShop();
            }
        }

        public void ExitMenuShop()
        {
            this.gameObject.SetActive(false);
            animatorTransition.gameObject.SetActive(true);
            animatorTransition.SetTrigger("Disappear");
        }

        public void ShowWindowShop()
        {
            if(menuShopWindow.activeInHierarchy == true)
            {
                menuShopWindow.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                cameraDekstop.MoveToCameraInstant(2, true);
            }
            else
            {
                menuShopWindow.gameObject.SetActive(true);
                this.gameObject.SetActive(true);
            }
        }

        #endregion

    } // MenuShop class
	
}// #PROJECTNAME# namespace
