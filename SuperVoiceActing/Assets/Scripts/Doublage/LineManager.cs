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
            //textMeshTurn.text = turnCount.ToString();
            feedbackLine.anchoredPosition = new Vector2(feedbackLine.anchoredPosition.x, -50);
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
            feedbackLine.gameObject.SetActive(true);
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
        }

        public void FeedbackFinalLine(float angle)
        {
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, angle);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackUltimeLine");
        }

        #endregion

    } 

} // #PROJECTNAME# namespace