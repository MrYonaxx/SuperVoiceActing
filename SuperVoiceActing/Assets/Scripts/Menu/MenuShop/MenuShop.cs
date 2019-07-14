/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
	public class MenuShop : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        MenuShopSelection menuShopSelection;
        [SerializeField]
        int buyOptionIndex;
        [SerializeField]
        int sellOptionIndex;

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
            if(indexSelected == buyOptionIndex)
            {

            }
            else if(indexSelected == sellOptionIndex)
            {
                menuShopSelection.SetEquipementsShowcase(playerData.InventoryEquipement, false);
                menuShopSelection.gameObject.SetActive(true);
            }
        }

        #endregion

    } // MenuShop class
	
}// #PROJECTNAME# namespace
