/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace VoiceActing
{
	public class ButtonContratAvailable : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI contractTitle;
        [SerializeField]
        Image iconSprite;

        int buttonIndex;

        [SerializeField]
        UnityEventInt eventOnClick;
        [SerializeField]
        UnityEventInt eventOnSelect;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public Sprite GetIconContract()
        {
            return iconSprite.sprite;
        }

        public void SetButtonIndex(int index)
        {
            buttonIndex = index;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void DrawButton(string name, Sprite icon)
        {
            contractTitle.text = name;
            iconSprite.sprite = icon;
        }

        public void OnPointerEnter(PointerEventData data)
        {
            eventOnSelect.Invoke(buttonIndex);
        }

        public void OnPointerClick(PointerEventData data)
        {
            eventOnClick.Invoke(buttonIndex);
        }

        #endregion

    } // ButtonContratAvailable class
	
}// #PROJECTNAME# namespace
