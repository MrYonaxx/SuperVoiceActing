﻿/*****************************************************************
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
	public class MenuPhone : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        GameObject mainMenu;
        [SerializeField]
        Animator phone;

        [HorizontalGroup]
        [SerializeField]
        GameObject[] menus;

        [HorizontalGroup]
        [SerializeField]
        bool[] horizontal;

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
            if (menus[indexSelected] != null)
            {
                this.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(false);
                menus[indexSelected].gameObject.SetActive(true);
            }
            if(horizontal[indexSelected] == true)
            {
                phone.SetTrigger("Horizontal");
            }
        }

        public void ShowMenu()
        {
            this.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);
            if (horizontal[indexSelected] == true)
            {
                phone.SetTrigger("Vertical");
            }
        }
        
        #endregion
		
	} // MenuPhone class
	
}// #PROJECTNAME# namespace