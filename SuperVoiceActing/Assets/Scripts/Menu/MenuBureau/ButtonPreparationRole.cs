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

namespace VoiceActing
{
	public class ButtonPreparationRole : MonoBehaviour
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

        public void DrawButton(Role role, VoiceActor voiceActor)
        {
            buttonRole.color = roleUnlocked;
            textRoleName.text = role.Name;
            if(voiceActor != null)
            {
                DrawActor(role, voiceActor);
            }
            else
            {
                panelActor.SetActive(false);
                panelNoActor.SetActive(true);
            }
        }

        public void DrawActor(Role role, VoiceActor voiceActor)
        {
            if(role.CharacterLock != null)
            {
                buttonRole.color = roleLocked;
            }
            panelNoActor.SetActive(false);
            panelActor.SetActive(true);
            textActorName.text = voiceActor.Name;
            textActorLevel.text = voiceActor.Level.ToString();
            textActorHP.text = voiceActor.Hp.ToString();
            textActorCost.text = (role.Line * voiceActor.Price).ToString();
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


        #endregion

    } // ButtonPreparationRole class
	
}// #PROJECTNAME# namespace
