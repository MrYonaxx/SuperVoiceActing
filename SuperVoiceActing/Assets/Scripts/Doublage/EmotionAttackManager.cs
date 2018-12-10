/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VoiceActing
{
    public enum Emotion
    {
        Joie,
        Tristesse,
        Dégoût,
        Colère,
        Surprise,
        Douceur,
        Peur,
        Confiance,
        Neutre
    }

    [System.Serializable]
    public class DeckEmotion
    {
        [SerializeField]
        private int joy;
        public int Joy
        {
            get { return joy; }
        }

        [SerializeField]
        private int sadness;
        public int Sadness
        {
            get { return sadness; }
        }

        [SerializeField]
        private int disgust;
        public int Disgust
        {
            get { return disgust; }
        }

        [SerializeField]
        private int anger;
        public int Anger
        {
            get { return anger; }
        }

        [SerializeField]
        private int surprise;
        public int Surprise
        {
            get { return surprise; }
        }

        [SerializeField]
        private int sweetness;
        public int Sweetness
        {
            get { return sweetness; }
        }

        [SerializeField]
        private int fear;
        public int Fear
        {
            get { return fear; }
            //set { fear = value; }
        }


        [SerializeField]
        private int trust;
        public int Trust
        {
            get { return trust; }
        }
    }


    [System.Serializable]
    public class EmotionCard
    {
        [SerializeField]
        private Emotion emotion;
        public Emotion Emotion
        {
            get { return emotion; }
        }

        [SerializeField]
        private RectTransform emotionCards;
        public RectTransform EmotionCards
        {
            get { return emotionCards; }
        }



        [SerializeField]
        private RectTransform[] cards;
        public RectTransform[] Cards
        {
            get { return cards; }
        }




        [SerializeField]
        private RectTransform transformRessource;
        public RectTransform TransformRessource
        {
            get { return transformRessource; }
        }

        [SerializeField]
        private RectTransform transformBattle;
        public RectTransform TransformBattle
        {
            get { return transformBattle; }
        }
    }










    /// <summary>
    /// Definition of the EmotionAttackManager class
    /// </summary>
    public class EmotionAttackManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Cartes d'émotions")]

        [SerializeField]
        EmotionCard[] emotionCards;

        [Header("Deck")]
        [SerializeField]
        DeckEmotion deckEmotion;

        [Header("Combo")]
        [SerializeField]
        RectTransform[] comboSlot;
        [SerializeField]
        int comboMax = 3;

        Emotion[] comboEmotion;
        int comboCount = 0;

        [Header("Transtions")]

        [SerializeField]
        float transitionSpeed = 1.1f;
        [SerializeField]
        float transitionSpeedCardSelect = 1.1f;


        private IEnumerator transitionCoroutine = null;

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

        protected void Start()
        {
            comboEmotion = new Emotion[3];
            CreateDeck();
            CreateComboSlot();
        }

        private void CreateDeck()
        {
            for(int i = 0; i < emotionCards.Length; i++)
            {
                int cardNumber = 0; 
                switch (emotionCards[i].Emotion)
                {
                    case Emotion.Joie:
                        cardNumber = deckEmotion.Joy;
                        break;
                    case Emotion.Tristesse:
                        cardNumber = deckEmotion.Sadness;
                        break;
                    case Emotion.Dégoût:
                        cardNumber = deckEmotion.Disgust;
                        break;
                    case Emotion.Colère:
                        cardNumber = deckEmotion.Anger;
                        break;
                    case Emotion.Surprise:
                        cardNumber = deckEmotion.Surprise;
                        break;
                    case Emotion.Douceur:
                        cardNumber = deckEmotion.Sweetness;
                        break;
                    case Emotion.Peur:
                        cardNumber = deckEmotion.Fear;
                        break;
                    case Emotion.Confiance:
                        cardNumber = deckEmotion.Trust;
                        break;
                }

                for (int j = 0; j < emotionCards[i].Cards.Length; j++)
                {
                    if(j < cardNumber)
                        emotionCards[i].Cards[j].gameObject.SetActive(true);
                    else
                        emotionCards[i].Cards[j].gameObject.SetActive(false);
                }
            }
        }

        private void CreateComboSlot()
        {
            for (int i = 0; i < comboSlot.Length; i++)
            {
                if (i < comboMax)
                    comboSlot[i].gameObject.SetActive(true);
                else
                    comboSlot[i].gameObject.SetActive(false);
            }
        }


        [ContextMenu("hey")]
        public void SwitchCardTransformToBattle()
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
            transitionCoroutine = ChangeAllTransformCoroutine(true);
            StartCoroutine(transitionCoroutine);
        }

        [ContextMenu("hey2")]
        public void SwitchCardTransformToRessource()
        {
            if(transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
            transitionCoroutine = ChangeAllTransformCoroutine(false);
            StartCoroutine(transitionCoroutine);
        }

        private IEnumerator ChangeAllTransformCoroutine(bool toBattle)
        {
            for(int i = 0; i < emotionCards.Length; i++)
            {
                if (toBattle == true)
                    emotionCards[i].EmotionCards.SetParent(emotionCards[i].TransformBattle);
                else
                    emotionCards[i].EmotionCards.SetParent(emotionCards[i].TransformRessource);
            }
            int time = 60;
            while (time > 0)
            {
                for (int i = 0; i < emotionCards.Length; i++)
                {
                    emotionCards[i].EmotionCards.anchoredPosition /= transitionSpeed;
                }
                time -= 1;
                yield return null;
            }
            for (int i = 0; i < emotionCards.Length; i++)
            {
                emotionCards[i].EmotionCards.anchoredPosition = new Vector2(0, 0);
            }
        }




        public void SelectCard(string emotion)
        {
            if (comboCount == comboMax)
                return;
            switch (emotion)
            {
                case "Joie":
                    SelectCard(Emotion.Joie);
                    break;
                case "Tristesse":
                    SelectCard(Emotion.Tristesse);
                    break;
                case "Dégoût":
                    SelectCard(Emotion.Dégoût);
                    break;
                case "Colère":
                    SelectCard(Emotion.Colère);
                    break;
                case "Surprise":
                    SelectCard(Emotion.Surprise);
                    break;
                case "Douceur":
                    SelectCard(Emotion.Douceur);
                    break;
                case "Peur":
                    SelectCard(Emotion.Peur);
                    break;
                case "Confiance":
                    SelectCard(Emotion.Confiance);
                    break;
            }
        }

        public void SelectCard(Emotion emotion)
        {
            if (comboCount == comboMax)
                return;
            switch(emotion)
            {
                case Emotion.Joie:
                    if (deckEmotion.Joy == 0)
                        return;
                    break;
                case Emotion.Tristesse:
                    if (deckEmotion.Sadness == 0)
                        return;
                    break;
                case Emotion.Dégoût:
                    if (deckEmotion.Disgust == 0)
                        return;
                    break;
                case Emotion.Colère:
                    if (deckEmotion.Anger == 0)
                        return;
                    break;
                case Emotion.Surprise:
                    if (deckEmotion.Surprise == 0)
                        return;
                    break;
                case Emotion.Douceur:
                    if (deckEmotion.Sweetness == 0)
                        return;
                    break;
                case Emotion.Peur:
                    if (deckEmotion.Fear == 0)
                        return;
                    break;
                case Emotion.Confiance:
                    if (deckEmotion.Trust == 0)
                        return;
                    break;
            }
            PlaceCard(emotion);
        }

        private void PlaceCard(Emotion emotion)
        {
            comboEmotion[comboCount] = emotion;
            comboCount += 1;
            for (int i = 0; i < emotionCards.Length; i++)
            {
                if (emotionCards[i].Emotion == emotion)
                {
                    StartCoroutine(ChangeTransformCoroutine(i, comboCount-1));
                    break;
                }
            }
        }

        private IEnumerator ChangeTransformCoroutine(int emotionIndex, int combo)
        {
            emotionCards[emotionIndex].Cards[combo].SetParent(comboSlot[combo]);

            int time = 60;
            while (time > 0)
            {
                emotionCards[emotionIndex].Cards[combo].anchoredPosition /= transitionSpeedCardSelect;
                time -= 1;
                yield return null;
            }
            emotionCards[emotionIndex].Cards[combo].anchoredPosition = new Vector2(0, 0);
        }




        public void RemoveCard()
        {

            comboCount -= 1;
        }

        #endregion

    } // EmotionAttackManager class

} // #PROJECTNAME# namespace