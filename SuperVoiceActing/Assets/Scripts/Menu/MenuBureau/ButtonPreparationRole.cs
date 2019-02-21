/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            textRoleName.text = role.Name;
            if(voiceActor != null)
            {

            }
            else
            {
                textActorName.text = "";
                textActorLevel.text = "   [ AUDITION ]";
                textActorHP.text = "";
            }
        }

        public void DrawActor(VoiceActor voiceActor)
        {

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
