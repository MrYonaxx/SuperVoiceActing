/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageTutorialManager class
    /// </summary>
    public class DoublageTutorialManager : DoublageManager
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private List<int> index;
        [SerializeField]
        private List<GameObject> popups;

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

        public override void SetPhrase()
        {
            if(CheckPopupTuto() == false)
                base.SetPhrase();
            else
            {
                popups[indexPhrase].SetActive(true);
            }
        }

        private bool CheckPopupTuto()
        {
            for(int i = 0; i < index.Count; i++)
            {
                if (indexPhrase == index[i])
                    return true;
            }
            return false;
        }

        #endregion

    } // DoublageTutorialManager class

} // #PROJECTNAME# namespace