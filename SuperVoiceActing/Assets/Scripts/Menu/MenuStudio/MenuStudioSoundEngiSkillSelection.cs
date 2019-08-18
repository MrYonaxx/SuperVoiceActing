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
	public class MenuStudioSoundEngiSkillSelection : MenuScrollList
	{
		#region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        
        
        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */
        public int GetIndexSelected()
        {
            return indexSelected;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        private void OnEnable()
        {
            animatorSelection.gameObject.SetActive(true);
        }
        private void OnDisable()
        {
            animatorSelection.gameObject.SetActive(false);
        }

        public void ScrollLeft()
        {
            if (buttonsList.Count == 0)
            {
                return;
            }
            if (lastDirection != 4)
            {
                StopRepeat();
                lastDirection = 4;
            }

            if (CheckRepeat() == false)
                return;

            BeforeSelection();
            indexSelected -= 4;
            if (indexSelected <= -1)
            {
                indexSelected += buttonsList.Count;
            }
            AfterSelection();
            MoveScrollRect();
        }
        
        #endregion
		
	} // MenuStudioSoundEngiSkillSelection class
	
}// #PROJECTNAME# namespace
