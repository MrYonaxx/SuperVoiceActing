/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the EmotionCard class
    /// </summary>
    public class EmotionCard : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        private IEnumerator coroutine = null;
        private RectTransform rectTransform;
        private Image image;

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

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        public void MoveCard(RectTransform newTransform, float speed)
        {
            this.transform.SetParent(newTransform);

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = MoveToOrigin(speed);
            StartCoroutine(coroutine);
        }

        private IEnumerator MoveToOrigin(float speed)
        {
            while (this.rectTransform.anchoredPosition != Vector2.zero)
            {
                this.rectTransform.anchoredPosition /= speed;
                yield return null;
            }
            coroutine = null;
        }

        public void FeedbackCardSelected(Image feedback)
        {
            feedback.transform.position = this.transform.position;//.anchoredPosition;
            feedback.rectTransform.localScale = this.rectTransform.localScale;
            feedback.sprite = image.sprite;
            feedback.color = image.color;
            StartCoroutine(FeedbackCoroutine(feedback, 20));
        }

        private IEnumerator FeedbackCoroutine(Image feedback, int time)
        {
            //feedback.color = new Color(1, 1, 1, 1);
            while (time != 0)
            {
                feedback.color -= new Color(0, 0, 0, 0.05f);
                feedback.rectTransform.localScale += new Vector3(0.075f, 0.075f, 0);
                time -= 1;
                yield return null;
            }
        }

        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace