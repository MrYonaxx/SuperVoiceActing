/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuButtonAction: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        RectTransform rectTransformButton;
        public RectTransform RectTransform
        {
            get { return rectTransformButton; }
        }
        [SerializeField]
        Animator animatorButton;

        [SerializeField]
        TextMeshProUGUI textActionName;
        [SerializeField]
        TextMeshProUGUI textActionCost;

        [SerializeField]
        UnityEventInt eventEnter;
        [SerializeField]
        UnityEventInt eventClick;

        private int indexButton;
        public int IndexButton
        {
            get { return indexButton; }
            set { indexButton = value; }
        }

        private IEnumerator coroutine;
        bool isClickable = false;

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

        public void DrawSeiyuuAction(SeiyuuAction seiyuuAction, int index = -1)
        {
            textActionName.text = seiyuuAction.ActionName;

            if (seiyuuAction.Cost == 0)
                textActionCost.text = "";
            else
                textActionCost.text = seiyuuAction.Cost + "<sprite=9>";

            if (index != -1)
                indexButton = index;

        }

        public void ButtonFeedback()
        {
            animatorButton.SetTrigger("Feedback");
        }

        public void ButtonAppear(bool b, float time)
        {
            isClickable = b;

            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoroutineAppear(b, time);
            StartCoroutine(coroutine);
        }

        public void ButtonSelected(bool b)
        {
            animatorButton.SetBool("Selected", b);
        }

        private IEnumerator CoroutineAppear(bool b, float time)
        {
            yield return new WaitForSeconds(time);
            animatorButton.SetBool("Appear", b);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (isClickable == false)
                return;
            eventEnter.Invoke(indexButton);
            ButtonSelected(true);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            ButtonSelected(false);
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (isClickable == false)
                return;
            eventClick.Invoke(indexButton);
            ButtonFeedback();
        }

        #endregion

    } 

} // #PROJECTNAME# namespace