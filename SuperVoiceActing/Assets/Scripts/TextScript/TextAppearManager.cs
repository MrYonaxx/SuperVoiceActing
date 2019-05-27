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
        CharacterDialogueController mouth;

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

        [SerializeField]
        TextPerformanceAppear textNewPhrase;

        [Header("Particle")]
        [SerializeField]
        ParticleSystem particleEnd;
        [SerializeField]
        ParticleSystem particleLineDead1;
        [SerializeField]
        ParticleSystem particleLineDead2;



        TextPerformanceAppear currentText = null;

        private IEnumerator coroutineWaitEndLine = null;

        private Vector3 initialScale = Vector3.zero;

        int wordID = 0;

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

        public void SelectWord(int index)
        {
            currentText.SelectWord(index);
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

        public bool GetEndDamage()
        {
            return currentText.GetEndDamage();
        }

        public void TextPop(int time = 60)
        {
            currentText.TextPop(time);
        }

        public void SetPauseText(bool b)
        {
            if (currentText == null)
                return;
            currentText.SetPauseText(b);
        }

        public void SetLetterSpeed(int newValue)
        {
            currentText.SetLetterSpeed(newValue);
        }

        public void HideText()
        {
            if (initialScale != Vector3.zero)
                return;
            initialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = new Vector3(0, 0, 0);
        }

        public void ShowText()
        {
            if (initialScale == Vector3.zero)
                return;
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
            initialScale = Vector3.zero;
        }


        public void NewPhrase(string newText, Emotion emotion = Emotion.Neutre, bool startPhrase = false)
        {

            if (currentText != null)
            {
                if (currentText.GetEndLine() != true)
                    return;
                currentText.Stop();
            }

            if(startPhrase == true)
            {
                currentText = textNewPhrase;
                currentText.NewMouthAnim(mouth);
                //currentText.SetParticle(particleEnd, particleLineDead1, particleLineDead2);
                currentText.NewPhrase(newText, newText.Length);
            }
            else
            {
                currentText = SelectTextEffect(emotion);
                currentText.NewMouthAnim(mouth);
                currentText.SetParticle(particleEnd, particleLineDead1, particleLineDead2);
                currentText.NewPhrase(newText);
            }
        }

        // Attack
        public void ExplodeLetter(float damage, Emotion[] emotions)
        {
            if (coroutineWaitEndLine != null)
            {
                StopCoroutine(coroutineWaitEndLine);
            }
            wordID = GetWordSelected();
            currentText.ExplodeLetter(damage, 60);
            currentText = SelectTextEffect(emotions[0]);

            StartCoroutine(WaitFrame(60, damage));

        }

        public void ApplyDamage(float damage)
        {
            currentText.ApplyDamage(damage);
            //ShowUIButton(damage);

        }

        public void ReprintText()
        {
            currentText.ReprintText();
        }

        // C'est pas clair
        public void CalculateDamageColor(float damage)
        {
            currentText.CalculateDamageColor(damage);
        }

        private IEnumerator WaitFrame(float time, float damage)
        {
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            currentText.NewMouthAnim(mouth);
            currentText.SetParticle(particleEnd, particleLineDead1, particleLineDead2);
            currentText.CalculateDamageColor(damage);
            currentText.ReprintText();
            currentText.SetWordFeedback(wordID);
            ApplyDamage(damage);
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
                case Emotion.Surprise:
                    return textSurprise;
                case Emotion.Douceur:
                    return textSweetness;
                case Emotion.Peur:
                    return textFear;
                case Emotion.Confiance:
                    return textTrust;
            }
            return textNeutral;
        }

        #endregion

    } // TextAppearManager class

} // #PROJECTNAME# namespace