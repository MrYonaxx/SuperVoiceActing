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
	public class ButtonVoiceActor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI actorName;

        [SerializeField]
        TextMeshProUGUI actorLevel;

        [SerializeField]
        RectTransform actorHealth;
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        Image actorImage;

        [SerializeField]
        Image buttonImage;
        [SerializeField]
        Color colorSelected;
        [SerializeField]
        Color colorUnSelected;

        [Header("HealthColor")]
        [SerializeField]
        float thresholdHealth = 0.5f;
        [SerializeField]
        Color colorNameNormal;
        [SerializeField]
        Color colorNameTired;
        [SerializeField]
        Color colorNameUnavailable;
        [SerializeField]
        Color colorActorNormal;
        [SerializeField]
        Color colorActorDead;

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

        public void SetButtonIndex(int index)
        {
            buttonIndex = index;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */


        public void DrawActor(string name, int level, float hpPercentage, bool availability, Sprite actorSprite = null)
        {
            actorName.text = name;
            actorLevel.text = "Lv." + level;
            actorHealth.transform.localScale = new Vector3(hpPercentage, 1, 1);
            if(actorSprite != null)
                actorImage.sprite = actorSprite;
            if (availability == false)
            {
                actorName.color = colorNameUnavailable;
                actorImage.color = colorActorDead;
            }
            else if (hpPercentage <= thresholdHealth)
            {
                actorName.color = colorNameTired;
                actorImage.color = colorActorNormal;
            }
            else
            {
                actorName.color = colorNameNormal;
                actorImage.color = colorActorNormal;
            }

    }

        public void SelectButton()
        {
            actorImage.enabled = false;
            buttonImage.color = colorSelected;
        }

        public void UnSelectButton()
        {
            actorImage.enabled = true;
            buttonImage.color = colorUnSelected;
        }

        public void DrawAudition()
        {
            actorName.text = "Audition";
            actorImage.gameObject.SetActive(false);
            actorLevel.gameObject.SetActive(false);
        }

        public Vector2 GetAnchoredPosition()
        {
            return rectTransform.anchoredPosition;
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if (!Input.GetMouseButton(0))
            {
                eventOnSelect.Invoke(buttonIndex);
                SelectButton();
            }
        }
        public void OnPointerExit(PointerEventData data)
        {
            UnSelectButton();
        }
        public void OnPointerClick(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }

        #endregion

    } // ButtonVoiceActor class
	
}// #PROJECTNAME# namespace
