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
	public class MinorSkillWindow : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        RectTransform rectTransform;
        [SerializeField]
        Animator animator;
        [SerializeField]
        Image actorSprite;

        [SerializeField]
        TextMeshProUGUI textSkillName;
        [SerializeField]
        TextMeshProUGUI textSkillDescription;

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void SetTransform(int i)
        {
            rectTransform.anchoredPosition += new Vector2(0, (rectTransform.rect.height/2) * i);
        }

        public void DrawSkill(Sprite actor, string skillName, string skillDescription)
        {
            this.gameObject.SetActive(true);
            actorSprite.sprite = actor;
            textSkillName.text = skillName;
            textSkillDescription.text = skillDescription;
        }

        public void ActivateFeedback()
        {
            animator.SetTrigger("Feedback");
        }

        public void HideFeedback()
        {
            animator.SetTrigger("Disappear");
        }

        #endregion

    } // MinorSkillWindow class
	
}// #PROJECTNAME# namespace
