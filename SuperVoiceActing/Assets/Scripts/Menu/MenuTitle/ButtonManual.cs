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
using TMPro;

namespace VoiceActing
{
	public class ButtonManual : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        RectTransform rectTransform;
        [SerializeField]
        TextMeshProUGUI textEntryTitle;
        [SerializeField]
        UnityEventInt eventOnClick;

        int buttonIndex = 0;

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

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }

        public void SetID(int id)
        {
            buttonIndex = id;
        }

        public void DrawEntry(string title)
        {
            textEntryTitle.text = title;
        }

        public void OnPointerEnter(PointerEventData data)
        {

        }

        public void OnPointerClick(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }

        public void OnPointerExit(PointerEventData data)
        {

        }

        #endregion

    } // ButtonManual class
	
}// #PROJECTNAME# namespace
