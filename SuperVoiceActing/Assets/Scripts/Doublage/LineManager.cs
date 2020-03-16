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
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class LineManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        protected Animator animatorFeedback;
        [SerializeField]
        protected RectTransform feedbackLine;
        [SerializeField]
        protected RectTransform feedbackLineTransform;
        [SerializeField]
        protected TextMeshProUGUI textMeshLine;
        [SerializeField]
        protected TextMeshProUGUI textMeshTurn;
        [SerializeField]
        protected TextMeshProUGUI textCurrentLineNumber;
        [SerializeField]
        protected TextMeshProUGUI textMaxLineNumber;

        [Title("LineHealthBar")]
        [SerializeField]
        RectTransform healthGaugesTransform;
        [SerializeField]
        RectTransform healtBarPrefab;

        List<RectTransform> listHealthBar = new List<RectTransform>();

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

        public void DrawLineNumber(int lineNumber)
        {
            textCurrentLineNumber.text = lineNumber.ToString();
        }

        public void DrawMaxLineNumber(int lineNumber)
        {
            textMaxLineNumber.text = lineNumber.ToString();
        }

        public void FeedbackNewLine(int lineRemaining)
        {
            if (feedbackLine == null)
                return;
            textMeshLine.text = lineRemaining.ToString();
            feedbackLine.anchoredPosition = new Vector2(feedbackLine.anchoredPosition.x, -50);
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
            feedbackLine.gameObject.SetActive(true);
            animatorFeedback.SetTrigger("Feedback");
        }

        public void FeedbackNewLineSwitch(bool goLeft)
        {
            if (feedbackLine == null)
                return;
            feedbackLine.anchoredPosition = new Vector2(feedbackLine.anchoredPosition.x, -125);
            if (goLeft == true)
            {
                feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, -20);
            }
            else
            {
                feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, 20);
            }
            feedbackLine.gameObject.SetActive(true);
            animatorFeedback.SetTrigger("Feedback");
        }

        public void FeedbackFinalLine(float angle)
        {
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, angle);
            animatorFeedback.SetTrigger("FeedbackUltimeLine");
        }



        public void DrawLineHealthBar(List<TextData> textDatas)
        {
            float maxSpace = textDatas.Count * 0.01f;
            float barSize = (1.01f - maxSpace) / (float)textDatas.Count;
            float start = 0f;
            for(int i = 0; i < textDatas.Count; i++)
            {
                listHealthBar.Add(Instantiate(healtBarPrefab, healthGaugesTransform));
                listHealthBar[listHealthBar.Count - 1].anchorMin = new Vector2(start, 0);
                listHealthBar[listHealthBar.Count - 1].anchorMax = new Vector2(start + barSize, 1);
                listHealthBar[listHealthBar.Count - 1].anchoredPosition = Vector2.zero;
                start += barSize + 0.01f;
            }
        }

        public void ShowCurrentLineHealthBar(int currentLine)
        {
            for (int i = 0; i < currentLine; i++)
            {
                listHealthBar[i].gameObject.SetActive(false);
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace