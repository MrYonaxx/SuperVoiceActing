/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace VoiceActing
{
	public class ButtonMouse : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        int buttonIndex;
        [SerializeField]
        UnityEventInt eventOnClick;
        [SerializeField]
        UnityEventInt eventOnClickEnter;
        [SerializeField]
        UnityEventInt eventOnClickExit;

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
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            eventOnClickEnter.Invoke(buttonIndex);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            eventOnClickExit.Invoke(buttonIndex);
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }
        
        #endregion
		
	} // ButtonMouse class
	
}// #PROJECTNAME# namespace
