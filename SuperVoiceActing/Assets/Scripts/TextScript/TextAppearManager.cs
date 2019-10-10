﻿/*****************************************************************
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

        public void SetMouth(CharacterDialogueController newCharacterMouth)
        {
            mouth = newCharacterMouth;
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
            particleLineDead1.Stop();
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

            if(startPhrase == true) // Premiere Apparition d'une phrase
            {
                currentText = textNewPhrase;
                currentText.NewMouthAnim(mouth);
                currentText.SetParticle(null, particleLineDead1, particleLineDead2); // Pas de fin de ligne en première phrase
                currentText.NewPhrase(newText, newText.Length);
            }
            else // Apparition de la phrase après une prise
            {
                currentText = SelectTextEffect(emotion);
                currentText.NewMouthAnim(mouth);
                currentText.SetParticle(particleEnd, particleLineDead1, particleLineDead2);
                currentText.NewPhrase(newText);
            }
        }

        // Attack
        public void ExplodeLetter(float damage, EmotionCard[] emotions)
        {
            if (coroutineWaitEndLine != null)
            {
                StopCoroutine(coroutineWaitEndLine);
            }
            wordID = GetWordSelected();
            currentText.ExplodeLetter(damage, 60);
            currentText = SelectTextEffect(emotions[0].GetEmotion());

            StartCoroutine(WaitFrame(60, damage));

        }

        public void ExplodeLetter(float damage, Emotion emotion)
        {
            if (coroutineWaitEndLine != null)
            {
                StopCoroutine(coroutineWaitEndLine);
            }
            wordID = GetWordSelected();
            currentText.ExplodeLetter(damage, 60);
            currentText = SelectTextEffect(emotion);

            StartCoroutine(WaitFrame(60, damage));
        }

        private IEnumerator WaitFrame(float time, float damage)
        {
            /*while(time != 0)
            {
                time -= 1;
                yield return null;
            }*/
            yield return new WaitForSeconds(time / 60f);
            currentText.NewMouthAnim(mouth);
            currentText.SetParticle(particleEnd, particleLineDead1, particleLineDead2);
            currentText.CalculateDamageColor(damage);
            currentText.ReprintText();
            currentText.SetWordFeedback(wordID);
            ApplyDamage(damage);
        }

        public void ApplyDamage(float damage)
        {
            currentText.ApplyDamage(damage);
        }




        public void ReprintText()
        {
            particleLineDead1.Stop();
            currentText.ReprintText();
        }

        // C'est pas clair
        public void CalculateDamageColor(float damage)
        {
            currentText.CalculateDamageColor(damage);
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