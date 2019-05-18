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
	public class StoryEventChoiceManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Animator[] animatorsZoom;

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

        [ContextMenu("Dezoom")]
        public void ChoiceDezoomOn()
        {
            for(int i = 0; i < animatorsZoom.Length; i++)
            {
                animatorsZoom[i].SetTrigger("Dezoom");
            }
        }

        [ContextMenu("DezoomOff")]
        public void ChoiceDezoomOff()
        {
            for (int i = 0; i < animatorsZoom.Length; i++)
            {
                animatorsZoom[i].SetTrigger("Zoom");
            }
        }
        
        #endregion
		
	} // StoryEventChoiceManager class
	
}// #PROJECTNAME# namespace
