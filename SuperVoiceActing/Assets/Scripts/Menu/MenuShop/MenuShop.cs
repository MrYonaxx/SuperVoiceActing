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
        int exitOptionIndex = 2;
        [SerializeField]
        Animator animatorTransition;
        [SerializeField]
        CameraBureau cameraDekstop;
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

        public void Validate()
        {
            
            if (indexSelected == buyOptionIndex)
            {
                return;
            }
            else if(indexSelected == sellOptionIndex)
            {
                return;
                /*menuShopSelection.SetEquipementsShowcase(playerData.InventoryEquipement, false);
                menuShopSelection.gameObject.SetActive(true);*/
            }
            else if (indexSelected == exitOptionIndex)
            {
                this.gameObject.SetActive(false);
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
