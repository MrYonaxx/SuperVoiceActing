/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace VoiceActing
{
    public enum Emotion
    {
        Neutre,
        Joie,
        Tristesse,
        Dégoût,
        Colère,
        Surprise,
        Douceur,
        Peur,
        Confiance
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
    public class EmotionCardPosition
    {
        [SerializeField]
        private Emotion emotion;
        public Emotion Emotion
        {
            get { return emotion; }
        }

        [SerializeField]
        private EmotionCard[] cards;
        public EmotionCard[] Cards
        {
            get { return cards; }
        }

        [SerializeField]
        private RectTransform[] cardsPosition;
        public RectTransform[] CardsPosition
        {
            get { return cardsPosition; }
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
        EmotionCardPosition[] emotionCards;

        [SerializeField]
        EmotionComboData emotionComboData;

        

        [Header("Combo")]
        [SerializeField]
        RectTransform[] comboSlot;
        


        [Header("Transtions")]
        [SerializeField]
        float transitionSpeed = 1.1f;
        [SerializeField]
        float transitionSpeedIntro = 1.05f;
        [SerializeField]
        float transitionSpeedCardSelect = 1.1f;

        [Header("Feedback")]
        [SerializeField]
        Animator animatorCombo;
        [SerializeField]
        TextMeshProUGUI textComboName;
        [SerializeField]
        Image feedbackSelectCard;
        [SerializeField]
        ParticleSystem particle;
        [SerializeField]
        Image haloEmotion;

        EmotionStat deckEmotion;
        EmotionCard[] comboCardEmotion;
        Emotion[] comboEmotion;
        int comboCount = -1;
        int comboMax = 3;
        private IEnumerator transitionCoroutine = null;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetComboCount()
        {
            return comboCount;
        }

        public Emotion[] GetComboEmotion()
        {
            var emoti = new Emotion[comboMax];
            for(int i = 0; i < comboEmotion.Length; i++)
            {
                emoti[i] = comboEmotion[i];
            }
            return emoti;
        }

        public EmotionCard[] GetComboEmotionCard()
        {
            var emoti = new EmotionCard[comboMax];
            for (int i = 0; i < comboEmotion.Length; i++)
            {
                emoti[i] = comboCardEmotion[i];
            }
            return emoti;
        }

        public void SetDeck(int newComboMax, EmotionStat newDeck)
        {
            comboMax = newComboMax;
            deckEmotion = newDeck;

            comboEmotion = new Emotion[comboMax];
            comboCardEmotion = new EmotionCard[comboMax];
            CreateDeck();
            CreateComboSlot();
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        // Initialization
        protected void Start()
        {
            /*comboEmotion = new Emotion[comboMax];
            comboCardEmotion = new EmotionCard[comboMax];
            CreateDeck();
            CreateComboSlot();*/
        }

        // Eventuellement déplacer la gestion du deck dans n script a part
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
                    case Emotion.Neutre:
                        cardNumber = 1;
                        break;
                }

                for (int j = 0; j < emotionCards[i].Cards.Length; j++)
                {
                    emotionCards[i].Cards[j].SetEmotion(emotionCards[i].Emotion);
                    if (j < cardNumber)
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
            if (comboSlot.Length == 2)
            {
                comboSlot[0].anchoredPosition += new Vector2(-75, 0);
                comboSlot[1].anchoredPosition += new Vector2(-75, 0);
            }
        }


        public void ModifiyDeck(Emotion emotion, int number)
        {
            switch (emotion)
            {
                case Emotion.Joie:
                    deckEmotion.Joy = number;
                    break;
                case Emotion.Tristesse:
                    deckEmotion.Sadness = number;
                    break;
                case Emotion.Dégoût:
                    deckEmotion.Disgust = number;
                    break;
                case Emotion.Colère:
                    deckEmotion.Anger = number;
                    break;
                case Emotion.Surprise:
                    deckEmotion.Surprise = number;
                    break;
                case Emotion.Douceur:
                    deckEmotion.Sweetness = number;
                    break;
                case Emotion.Peur:
                    deckEmotion.Fear = number;
                    break;
                case Emotion.Confiance:
                    deckEmotion.Trust = number;
                    break;
            }
            CreateDeck();
        }

        public void ModifiyDeck(EmotionStat newDeck, int newComboMax = -1)
        {
            //deckEmotion.Add(newDeck);
            CreateDeck();
            if(newComboMax != -1)
            {
                comboMax = newComboMax;
                comboEmotion = new Emotion[comboMax];
                comboCardEmotion = new EmotionCard[comboMax];
                CreateComboSlot();
            }

        }
        // =========================================================================================================================================

        // =========================================================================================================================================
        // DEPLACEMENT ET FEEDBACK CARTE

        // Déplace toutes les cartes
        public void SwitchCardTransformIntro()
        {
            StartCoroutine(IntroSequence());
        }

        private IEnumerator IntroSequence()
        {
            for (int i = 0; i < emotionCards.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        emotionCards[i].Cards[j].MoveCard(emotionCards[i].CardsPosition[j], transitionSpeedIntro);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void SwitchCardTransformToBattle()
        {
            for (int i = 0; i < emotionCards.Length; i++)
            {
                for(int j = 0; j < emotionCards[i].Cards.Length; j++)
                {
                    if(emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        emotionCards[i].Cards[j].MoveCard(emotionCards[i].CardsPosition[j], transitionSpeed);
                }
            }
            animatorCombo.SetBool("Appear", true);
            animatorCombo.SetBool("Attack", false);
        }


        public void SwitchCardTransformToRessource()
        {
            for (int i = 0; i < emotionCards.Length; i++)
            {
                for (int j = 0; j < emotionCards[i].Cards.Length; j++)
                {
                    if (emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        emotionCards[i].Cards[j].MoveCard(emotionCards[i].TransformRessource, transitionSpeed);
                }
            }
            animatorCombo.SetBool("Appear", false);
        }




        // sélectionne une carte avec un nom
        public EmotionCard SelectCard(string emotion)
        {
            if (comboCount == comboMax)
                return null;
            switch (emotion)
            {
                case "Joie":
                    return SelectCard(Emotion.Joie);
                case "Tristesse":
                    return SelectCard(Emotion.Tristesse);
                case "Dégoût":
                    return SelectCard(Emotion.Dégoût);
                case "Colère":
                    return SelectCard(Emotion.Colère);
                case "Surprise":
                    return SelectCard(Emotion.Surprise);
                case "Douceur":
                    return SelectCard(Emotion.Douceur);
                case "Peur":
                    return SelectCard(Emotion.Peur);
                case "Confiance":
                    return SelectCard(Emotion.Confiance);
                case "Neutre":
                    return SelectCard(Emotion.Neutre);
            }
            return null;
        }

        // selectionne une carte et décale l'ordre de 1
        public EmotionCard SelectCard(Emotion emotion)
        {
            if (comboCount == comboMax-1)
                return null;

            for (int i = 0; i < emotionCards.Length; i++)
            {
                if (emotionCards[i].Emotion == emotion)
                {
                    if (emotionCards[i].Cards.Length == 0)
                        return null;
                    if (emotionCards[i].Cards[0] == null)
                        return null;
                    if (emotionCards[i].Cards[0] != null && emotionCards[i].Cards[0].gameObject.activeInHierarchy == true)
                    {
                        emotionCards[i].Cards[0].FeedbackCardSelected(feedbackSelectCard);
                        ParticleSelectEmotion(emotion);
                        comboCount += 1;
                        comboEmotion[comboCount] = emotion;
                        comboCardEmotion[comboCount] = emotionCards[i].Cards[0];
                        emotionCards[i].Cards[0].MoveCard(comboSlot[comboCount], transitionSpeedCardSelect);
                        emotionCards[i].Cards[0] = null;
                    }
                    for(int j = 1; j < emotionCards[i].Cards.Length; j++)
                    {
                        if (emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        {
                            emotionCards[i].Cards[j - 1] = emotionCards[i].Cards[j];
                            emotionCards[i].Cards[j].MoveCard(emotionCards[i].CardsPosition[j-1], transitionSpeedCardSelect);
                            emotionCards[i].Cards[j] = null;
                        }
                    }
                    DrawComboName();
                    return comboCardEmotion[comboCount];
                }
            }
            return null;
        }




        // Retire une carte et décale l'ordre de 1 vers le haut
        public EmotionCard RemoveCard()
        {
            if (comboCount == -1)
                return null;
            Emotion emotionRemove = comboEmotion[comboCount];
            EmotionCard card = null;

            for (int i = 0; i < emotionCards.Length; i++)
            {
                if (emotionCards[i].Emotion == emotionRemove)
                {
                    for (int j = emotionCards[i].Cards.Length-1; j >= 0; j--)
                    {
                        if (emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        {
                            emotionCards[i].Cards[j + 1] = emotionCards[i].Cards[j];
                            emotionCards[i].Cards[j].MoveCard(emotionCards[i].CardsPosition[j + 1], transitionSpeedCardSelect);
                            emotionCards[i].Cards[j] = null;
                        }
                    }
                    emotionCards[i].Cards[0] = comboCardEmotion[comboCount];
                    card = comboCardEmotion[comboCount];
                    comboCardEmotion[comboCount].MoveCard(emotionCards[i].CardsPosition[0], transitionSpeedCardSelect);
                    comboCardEmotion[comboCount] = null;               
                    comboEmotion[comboCount] = Emotion.Neutre;
                    comboCount -= 1;
                    DrawComboName();
                    break;
                }
            }
            return card;
        }

        public void ResetCard()
        {

        }

        public void CardAttack()
        {
            animatorCombo.SetTrigger("Attack");
        }

        public void DrawComboName()
        {
            if (comboEmotion.Length == 0 || comboEmotion[0] == Emotion.Neutre)
                textComboName.gameObject.SetActive(false);
            else
            {
                int[] emotions = { 0, 0, 0 };
                for (int i = 0; i < comboEmotion.Length; i++)
                {
                    int emotion = 0;
                    switch (comboEmotion[i])
                    {
                        case Emotion.Joie:
                            emotion = 1;
                            break;
                        case Emotion.Tristesse:
                            emotion = 2;
                            break;
                        case Emotion.Dégoût:
                            emotion = 3;
                            break;
                        case Emotion.Colère:
                            emotion = 4;
                            break;
                        case Emotion.Surprise:
                            emotion = 5;
                            break;
                        case Emotion.Douceur:
                            emotion = 6;
                            break;
                        case Emotion.Peur:
                            emotion = 7;
                            break;
                        case Emotion.Confiance:
                            emotion = 8;
                            break;
                    }
                    emotions[i] = emotion;
                }
                textComboName.gameObject.SetActive(true);
                textComboName.text = emotionComboData.GetName(emotions[0], emotions[1], emotions[2]);
            }
            
        }

        // ================================================= //
        // =============== FEEDBACK ==================
        // ================================================= //


        private void ParticleSelectEmotion(Emotion emotion)
        {
            if (particle == null)
                return;
            if (haloEmotion == null)
                return;

            var particleColor = particle.main;
            Color colorEmotion;
            switch (emotion)
            {
                case Emotion.Joie:
                    colorEmotion = Color.yellow;
                    break;
                case Emotion.Tristesse:
                    colorEmotion = Color.blue;
                    break;
                case Emotion.Dégoût:
                    colorEmotion = Color.green;
                    break;
                case Emotion.Colère:
                    colorEmotion = Color.red;
                    break;
                case Emotion.Surprise:
                    colorEmotion = new Color(0.9f, 0.55f, 0.3f);
                    break;
                case Emotion.Douceur:
                    colorEmotion = new Color(0.88f, 0.58f, 0.9f);
                    break;
                case Emotion.Peur:
                    colorEmotion = Color.black;
                    break;
                case Emotion.Confiance:
                    colorEmotion = Color.white;
                    break;
                default:
                    colorEmotion = Color.white;
                    break;
            }
            particleColor.startColor = colorEmotion;
            StartCoroutine(HaloEmotionCoroutine(colorEmotion, 0.1f, 10, 10));
            particle.Play();
        }

        private IEnumerator HaloEmotionCoroutine(Color initialColor, float alpha, int time1, int time2)
        {
            haloEmotion.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
            float speedAlpha = alpha / time1;
            float speedAlpha2 = alpha / time2;
            while (time1 != 0)
            {
                haloEmotion.color += new Color(0, 0, 0, speedAlpha);
                time1 -= 1;
                yield return null;
            }
            while (time2 != 0)
            {
                haloEmotion.color -= new Color(0, 0, 0, speedAlpha2);
                time2 -= 1;
                yield return null;
            }
        }

        #endregion

    } // EmotionAttackManager class

} // #PROJECTNAME# namespace