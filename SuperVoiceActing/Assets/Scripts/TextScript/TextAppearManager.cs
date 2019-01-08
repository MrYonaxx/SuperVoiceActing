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
    /// Definition of the TextAppearManager class
    /// </summary>
    public class TextAppearManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Mouth")]
        [SerializeField]
        MouthAnimation mouth;

        [Header("Text Emotion")]
        [SerializeField]
        TextPerformanceAppear textNeutral;
        [SerializeField]
        TextPerformanceAppear textJoy;
        [SerializeField]
        TextPerformanceAppear textSadness;
        [SerializeField]
        TextPerformanceAppear textAnger;
        [SerializeField]
        TextPerformanceAppear textDisgust;
        [SerializeField]
        TextPerformanceAppear textSurprise;
        [SerializeField]
        TextPerformanceAppear textSweetness;
        [SerializeField]
        TextPerformanceAppear textFear;
        [SerializeField]
        TextPerformanceAppear textTrust;

        [Header("Particle")]
        [SerializeField]
        ParticleSystem particleEnd;

        [Header("UI")]
        [SerializeField]
        Image buttonUIY;
        [SerializeField]
        Image buttonUIA;


        TextPerformanceAppear currentText = null;

        private IEnumerator coroutineWaitEndLine = null;

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
        public bool PrintAllText()
        {
            if (currentText == null)
                return true;
            return currentText.PrintAllText();
        }

        public void SelectWordRight()
        {
            currentText.SelectWordRight();
        }

        public void SelectWordLeft()
        {
            currentText.SelectWordLeft();
        }

        public int GetWordSelected()
        {
            return currentText.GetWordSelected();
        }

        public bool GetEndLine()
        {
            return currentText.GetEndLine();
        }

        public void NewPhrase(string newText, Emotion emotion = Emotion.Neutre)
        {
            if(currentText != null)
                currentText.Stop();
            currentText = SelectTextEffect(emotion);
            currentText.NewMouthAnim(mouth);
            currentText.SetParticle(particleEnd);
            currentText.NewPhrase(newText);
        }

        public void ExplodeLetter(float damage, Emotion[] emotions)
        {
            if (coroutineWaitEndLine != null)
            {
                StopCoroutine(coroutineWaitEndLine);
            }
            currentText.ExplodeLetter(damage, 60);
            currentText = SelectTextEffect(emotions[0]);
            StartCoroutine(WaitFrame(60, damage));

        }

        public void ApplyDamage(float damage)
        {
            currentText.ApplyDamage(damage);
            if (damage >= 100)
            {
                if (coroutineWaitEndLine != null)
                {
                    StopCoroutine(coroutineWaitEndLine);
                }
                coroutineWaitEndLine = WaitEndLine();
                StartCoroutine(coroutineWaitEndLine);
            }
        }

        public void HideUIButton()
        {
            StartCoroutine(MoveUIButton(-600));
        }

        private IEnumerator WaitEndLine()
        {
            yield return null;
            while (currentText.GetEndLine() == false)
            {
                yield return null;
            }
            StartCoroutine(MoveUIButton(-500));
        }

        private IEnumerator WaitFrame(float time, float damage)
        {
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            currentText.NewMouthAnim(mouth);
            currentText.SetParticle(particleEnd);
            currentText.ReprintText();
            ApplyDamage(damage);
        }

        private IEnumerator MoveUIButton(float targetY)
        {

            int time = 20;
            float speedY = (targetY - buttonUIY.rectTransform.anchoredPosition.y) / time;
            while (time != 0)
            {
                buttonUIY.rectTransform.anchoredPosition += new Vector2(0, speedY);
                //buttonUIA.rectTransform.anchoredPosition += new Vector2(0, speedY);
                time -= 1;
                yield return null;
            }
        }

        private TextPerformanceAppear SelectTextEffect(Emotion emotion)
        {
            switch (emotion)
            {
                case Emotion.Joie:
                    return textJoy;
                case Emotion.Tristesse:
                    return textSadness;
                case Emotion.Colère:
                    return textAnger;
                case Emotion.Dégoût:
                    return textDisgust;
            }
            return textNeutral;
        }

        #endregion

    } // TextAppearManager class

} // #PROJECTNAME# namespace