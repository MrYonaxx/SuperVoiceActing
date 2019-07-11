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
        int soundEngineerMenu = 0;
        [SerializeField]
        int translatorMenu = 1;
        [SerializeField]
        int equipementMenu = 2;
        [SerializeField]
        int researchMenu = 3;
        [SerializeField]
        int otherMenu = 4;

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
            if(indexSelected == soundEngineerMenu)
            {

            }
            else if (indexSelected == translatorMenu)
            {

            }
            else if (indexSelected == equipementMenu)
            {

            }
            else if (indexSelected == researchMenu)
            {

            }
            else if (indexSelected == otherMenu)
            {

            }
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
