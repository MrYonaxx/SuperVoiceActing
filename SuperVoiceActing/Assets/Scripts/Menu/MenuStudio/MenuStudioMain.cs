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
	public class MenuStudioMain : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Space]
        [Title("Menu Studio Main")]
        [SerializeField]
        GameObject[] subMenus;
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
        public void OnEnable()
        {
            animatorMenu.gameObject.SetActive(true);
        }

        public void Validate()
        {
            subMenus[indexSelected].gameObject.SetActive(true);
            animatorMenu.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }


        public void DrawStudioInfo()
        {

        }

        private void DrawVoiceDirectorInfo()
        {

        }

        private void DrawEquipementInfo()
        {

        }

        private void DrawWorldMoodInfo()
        {

        }

        #endregion

    } // MenuStudioMain class
	
}// #PROJECTNAME# namespace
