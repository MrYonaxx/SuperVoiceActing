﻿/*****************************************************************
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
	public class ButtonPreparationRole : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI textRoleName;
        [SerializeField]
        TextMeshProUGUI textActorName;
        [SerializeField]
        TextMeshProUGUI textActorLevel;
        [SerializeField]
        TextMeshProUGUI textActorHP;
        [SerializeField]
        TextMeshProUGUI textActorCost;
        [SerializeField]
        Image imageActorFace;

        [SerializeField]
        Animator animatorSelection;
        [SerializeField]
        Animator animatorOutline;

        [SerializeField]
        GameObject panelActor;
        [SerializeField]
        GameObject panelNoActor;

        [SerializeField]
        Image buttonRole;

        Color roleLocked = new Color(1, 0, 0);
        Color roleUnlocked = new Color(1, 1, 1);

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

        public void DrawButton(Role role, VoiceActor voiceActor, StoryCharacterData spriteIcon)
        {
            buttonRole.color = roleUnlocked;
            textRoleName.text = role.Name;
            if(voiceActor != null)
            {
                DrawActor(role, voiceActor, spriteIcon.SpriteIcon);
            }
            else
            {
                panelActor.SetActive(false);
                panelNoActor.SetActive(true);
            }
        }

        public void DrawActor(Role role, VoiceActor voiceActor, Sprite spriteIcon)
        {
            if(role.CharacterLock != null)
            {
                buttonRole.color = roleLocked;
            }
            panelNoActor.SetActive(false);
            panelActor.SetActive(true);
            textActorName.text = voiceActor.VoiceActorName;
            textActorLevel.text = voiceActor.Level.ToString();
            textActorHP.text = voiceActor.Hp.ToString();
            textActorCost.text = (role.Line * voiceActor.Price).ToString();
            if (spriteIcon == null)
                imageActorFace.enabled = false;
            else
                imageActorFace.enabled = true;
            imageActorFace.sprite = spriteIcon;
            if (voiceActor.Availability == false)
                imageActorFace.color = Color.black;
            else
                imageActorFace.color = new Color(1,1,1,0.4f);
        }

        public void SelectButton()
        {
            animatorSelection.SetTrigger("Feedback");
            animatorOutline.gameObject.SetActive(true);
        }

        public void UnselectButton()
        {
            animatorOutline.gameObject.SetActive(false);
        }






        public void OnPointerEnter(PointerEventData data)
        {
            eventOnSelect.Invoke(buttonIndex);
            SelectButton();
        }


        public void OnPointerClick(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }




        #endregion

    } // ButtonPreparationRole class
	
}// #PROJECTNAME# namespace
