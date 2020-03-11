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
using UnityEngine.EventSystems;

namespace VoiceActing
{
    public class ButtonStoryChoice: MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        int buttonIndex;

        [SerializeField]
        UnityEventInt eventOnPointer;

        [SerializeField]
        UnityEventInt eventOnClick;

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
            eventOnPointer.Invoke(buttonIndex);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace