/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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

        [SerializeField]
        TextMeshProUGUI textStat;
        [SerializeField]
        Image StatBonus;
        [SerializeField]
        Image StatMalus;


        private Emotion emotion;

        private int value = 0;
        private int valueBonus = 0;

        private float damagePercentage = 1;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetStat()
        {
            return value + valueBonus;
        }

        public float GetDamagePercentage()
        {
            return damagePercentage;
        }

        public void SetEmotion(Emotion emo)
        {
            emotion = emo;
        }

        public Emotion GetEmotion()
        {
            return emotion;
        }

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
            rectTransform.localScale = new Vector3(1, 1, 1);
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = MoveToOrigin(speed);
            StartCoroutine(coroutine);
        }

        private IEnumerator MoveToOrigin(float speed)
        {
            while (rectTransform.anchoredPosition != Vector2.zero)
            {
                rectTransform.anchoredPosition /= speed;
                yield return null;
            }
            rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
            coroutine = null;
        }

        public void FeedbackCardSelected(Image feedback)
        {
            if (feedback != null)
            {
                feedback.transform.position = this.transform.position;//.anchoredPosition;
                feedback.rectTransform.localScale = this.rectTransform.localScale;
                feedback.sprite = image.sprite;
                feedback.color = image.color;
                StartCoroutine(FeedbackCoroutine(feedback, 20));
            }
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





        public void DrawStat(int baseStat, int newStat, Color bonusStat, Color malusStat)
        {
            value = baseStat;
            valueBonus = newStat;
            if (textStat == null)
                return;
            if(newStat > 0)
            {
                textStat.color = bonusStat;
            }
            else if (newStat < 0)
            {
                textStat.color = malusStat;
            }
            else
            {
                textStat.color = Color.white;
            }
            textStat.text = (baseStat + newStat).ToString();
        }

        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace