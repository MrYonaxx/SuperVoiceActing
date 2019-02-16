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
	public class MenuContractAvailable : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("MenuManagers")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContratManager menuContractManager;

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
            animatorMenu.gameObject.SetActive(true);
            //animatorMenu.disabled = false
        }

        /*private void OnDisable()
        {
            animatorMenu.gameObject.SetActive(false);
            //animatorMenu.enabled = true;
        }*/

        public void SwitchToMenuContractManager()
        {
            menuContractManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            animatorMenu.gameObject.SetActive(false);
        }

        #endregion

    } // MenuContractAvailable class
	
}// #PROJECTNAME# namespace
