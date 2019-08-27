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
using Sirenix.OdinInspector;

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


    // class qui represente toutes les cartes possédé actuellement
    [System.Serializable]
    public class EmotionCardTotal
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
            set { cards = value; }
        }

        public EmotionCardTotal(EmotionCard[] newCards)
        {
            cards = new EmotionCard[3];
            for(int i = 0; i < newCards.Length; i++)
            {
                cards[i] = newCards[i];
            }
        }
    }

    [System.Serializable]
    public class EmotionCardPosition
    {

        [LabelWidth(100)]
        [SerializeField]
        private RectTransform transformRessource;
        public RectTransform TransformRessource
        {
            get { return transformRessource; }
        }

        [SerializeField]
        private RectTransform[] cardsPosition;
        public RectTransform[] CardsPosition
        {
            get { return cardsPosition; }
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
        [Title("Cartes d'émotions")]
        [SerializeField]
        EmotionCard prefab;
        [SerializeField]
        EmotionComboData emotionComboData;
        [SerializeField]
        Sprite[] emotionSprite;

        [HorizontalGroup]
        [SerializeField]
        EmotionCardTotal[] emotionCards;
        [HorizontalGroup]
        [SerializeField]
        EmotionCardPosition[] emotionCardsPosition;

        

        [Title("Combo")]
        [SerializeField]
        RectTransform[] comboSlot;



        [Title("Transtions")]
        [SerializeField]
        float transitionSpeed = 1.1f;
        [SerializeField]
        float transitionSpeedIntro = 1.05f;
        [SerializeField]
        float transitionSpeedCardSelect = 1.1f;

        [Title("Animator & UI")]
        [SerializeField]
        Animator animatorCombo;
        [SerializeField]
        Animator animatorCardLeft;
        [SerializeField]
        Animator animatorCardRight;
        [SerializeField]
        TextMeshProUGUI textComboName;
        [SerializeField]
        TextMeshProUGUI textComboDamage;

        [Title("Feedback")]
        [SerializeField]
        Image feedbackSelectCard;
        [SerializeField]
        ParticleSystem particle;
        [SerializeField]
        Image haloEmotion;
        [SerializeField]
        Color[] colorParticle;

        EmotionStat deckEmotion;
        EmotionCard[] comboCardEmotion;
        Emotion[] comboEmotion;
        int comboCount = -1;
        int comboMax = 3;
        //private IEnumerator transitionCoroutine = null;

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

        public EmotionCardTotal[] GetCards()
        {
            return emotionCards;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public EmotionCardTotal[] SetDeck(int newComboMax, EmotionStat newDeck)
        {
            comboMax = newComboMax;
            deckEmotion = newDeck;
            comboEmotion = new Emotion[comboMax];
            comboCardEmotion = new EmotionCard[comboMax];          
            CreateComboSlot();
            return CreateDeck();
        }

        public EmotionCardTotal[] CreateDeck()
        {
            for (int i = 0; i < emotionCards.Length; i++)
            {
                int cardNumber = deckEmotion.GetEmotion(i);
                if (cardNumber > 3)
                    cardNumber = 3;
                for (int j = 0; j < cardNumber; j++)
                {
                    emotionCards[i].Cards[j] = Instantiate(prefab, emotionCardsPosition[i].TransformRessource);
                    emotionCards[i].Cards[j].SetEmotion((Emotion) i);
                    emotionCards[i].Cards[j].SetSprite(emotionSprite[(int) emotionCards[i].Emotion]);
                }
            }
            return emotionCards;
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

        public void ShowComboSlot(bool b)
        {
            for (int i = 0; i < comboSlot.Length; i++)
            {
                if (i < comboMax)
                    comboSlot[i].gameObject.SetActive(b);
            }
        }


        public EmotionCardTotal[] AddDeck(EmotionStat addDeck)
        {
            for (int i = 0; i < addDeck.GetLength(); i++)
            {
                int cardNumber = addDeck.GetEmotion(i);
                if (cardNumber > 0)
                {
                    for (int j = 0; j < emotionCards[i].Cards.Length; j++)
                    {
                        if (cardNumber == 0)
                            break;
                        if (emotionCards[i].Cards[j] == null)
                        {
                            emotionCards[i].Cards[j] = Instantiate(prefab, emotionCardsPosition[i].TransformRessource);
                            emotionCards[i].Cards[j].SetEmotion((Emotion)i);
                            emotionCards[i].Cards[j].SetSprite(emotionSprite[(int)emotionCards[i].Emotion]);
                            cardNumber -= 1;
                        }
                    }
                }
                else if (cardNumber < 0)
                {
                    for (int j = emotionCards[i].Cards.Length-1; j > -1; j--)
                    {
                        if (cardNumber == 0)
                            break;
                        if (emotionCards[i].Cards[j] != null)
                        {
                            Destroy(emotionCards[i].Cards[j]);
                            emotionCards[i].Cards[j] = null;
                            cardNumber += 1;
                        }
                    }
                }
            }
            return emotionCards;
        }

        public void AddComboMax(int addComboMax)
        {
            comboMax += addComboMax;
            if (comboMax >= 3)
            {
                comboMax = 3;
            }
            else if (comboMax <= 0)
            {
                comboMax = 1;
            }
            comboEmotion = new Emotion[comboMax];
            comboCardEmotion = new EmotionCard[comboMax];
            CreateComboSlot();
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
                        emotionCards[i].Cards[j].MoveCard(emotionCardsPosition[i].CardsPosition[j], transitionSpeedIntro);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void SwitchCardTransformToBattle(bool showCombo = true)
        {
            for (int i = 0; i < emotionCards.Length; i++)
            {
                for(int j = 0; j < emotionCards[i].Cards.Length; j++)
                {
                    if(emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        emotionCards[i].Cards[j].MoveCard(emotionCardsPosition[i].CardsPosition[j], transitionSpeed);
                }
            }
            if (showCombo == true)
            {
                animatorCombo.SetBool("Appear", true);
                animatorCombo.SetBool("Attack", false);
            }
            else
            {
                animatorCombo.SetBool("Appear", false);
            }
        }



        public void SwitchCardTransformToRessource()
        {
            for (int i = 0; i < emotionCards.Length; i++)
            {
                for (int j = 0; j < emotionCards[i].Cards.Length; j++)
                {
                    if (emotionCards[i].Cards[j] != null && emotionCards[i].Cards[j].gameObject.activeInHierarchy == true)
                        emotionCards[i].Cards[j].MoveCard(emotionCardsPosition[i].TransformRessource, transitionSpeed);
                }
            }
            animatorCombo.SetBool("Appear", false);
            textComboName.gameObject.SetActive(false);
            textComboDamage.gameObject.SetActive(false);
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
                            emotionCards[i].Cards[j].MoveCard(emotionCardsPosition[i].CardsPosition[j-1], transitionSpeedCardSelect);
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
                            emotionCards[i].Cards[j].MoveCard(emotionCardsPosition[i].CardsPosition[j + 1], transitionSpeedCardSelect);
                            emotionCards[i].Cards[j] = null;
                        }
                    }
                    emotionCards[i].Cards[0] = comboCardEmotion[comboCount];
                    card = comboCardEmotion[comboCount];
                    comboCardEmotion[comboCount].MoveCard(emotionCardsPosition[i].CardsPosition[0], transitionSpeedCardSelect);
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
            RemoveCard();
            RemoveCard();
            RemoveCard();
        }

        public void CardAttack()
        {
            animatorCombo.SetTrigger("Attack");
        }

        public void DrawComboName()
        {
            if (comboEmotion.Length == 0 || comboEmotion[0] == Emotion.Neutre)
            {
                textComboName.gameObject.SetActive(false);
                textComboDamage.gameObject.SetActive(false);
            }
            else
            {
                int[] emotions = { 0, 0, 0 };
                int totalValue = 0;
                for (int i = 0; i < comboEmotion.Length; i++)
                {
                    int emotion = (int)comboEmotion[i];
                    emotions[i] = emotion;
                    if (emotion != 0)
                        totalValue += comboCardEmotion[i].GetStat();
                }
                textComboName.gameObject.SetActive(true);
                textComboDamage.gameObject.SetActive(true);
                textComboName.text = emotionComboData.GetName(emotions[0], emotions[1], emotions[2]);
                textComboDamage.text = totalValue.ToString();
            }
            
        }


        // ================================================= //
        // =============== FEEDBACK ==================
        // ================================================= //
        public void StartTurnCardFeedback()
        {
            animatorCardLeft.SetTrigger("Feedback");
        }

        private void ParticleSelectEmotion(Emotion emotion)
        {
            if (particle == null)
                return;
            if (haloEmotion == null)
                return;

            var particleColor = particle.main;
            Color colorEmotion = colorParticle[(int) emotion];
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