/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [System.Serializable]
    public class SeiyuuBattleCard
    {
        [SerializeField]
        private Emotion emotion;
        public Emotion Emotion
        {
            get { return emotion; }
        }

        [SerializeField]
        SkillData skillCard = null;
    }

    [System.Serializable]
    public class SeiyuuBattleDeck
    {
        [SerializeField]
        private int handSize = 3;
        public int HandSize
        {
            get { return handSize; }
        }

        [SerializeField]
        private List<SeiyuuBattleCard> seiyuuBattleCards;
        public List<SeiyuuBattleCard> SeiyuuBattleCards
        {
            get { return seiyuuBattleCards; }
        }
    }

    public class SeiyuuHandManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Data")]
        [SerializeField]
        SeiyuuBattleDeck seiyuuDeck;
        [SerializeField]
        Sprite[] emotionSprite;
        [SerializeField]
        VoiceActorData voiceActorData;

        SeiyuuBattleCard seiyuuHandNextCard;
        List<SeiyuuBattleCard> seiyuuHand = new List<SeiyuuBattleCard>();
        List<SeiyuuBattleCard> seiyuuGraveyard = new List<SeiyuuBattleCard>();

        [Title("CardPosition")]
        [SerializeField]
        RectTransform cardSelection;
        [SerializeField]
        RectTransform nextCardPosition;
        [SerializeField]
        RectTransform comboPosition;
        [SerializeField]
        RectTransform discardPosition;
        [SerializeField]
        RectTransform[] cardsPositions;


        [SerializeField]
        EmotionCard cardPrefab;
        EmotionCard[] handCards;
        EmotionCard nextCard;
        List<EmotionCard> comboCards = new List<EmotionCard>();



        [Title("Timer")]
        [SerializeField]
        TextMeshProUGUI textTimer;
        [SerializeField]
        float timerValue = 3f;
        float timer = 0f;

        [Title("ATB")]
        [SerializeField]
        SeiyuuATBManager atbManager;
        [SerializeField]
        int atbAttackValue;
        [SerializeField]
        int atbDiscardValue;

        [Title("Event")]
        [SerializeField]
        InputController inputTurnActive;
        [SerializeField]
        UnityEvent eventATBEndDiscard;
        [SerializeField]
        UnityEvent eventATBEndBattle;


        bool isDiscarding = false;
        bool isAttacking = false;


        int indexSelection = 0;
        VoiceActor voiceActor;


        [Title("Combo")]
        [SerializeField]
        TextMeshProUGUI textComboCount;
        [SerializeField]
        TextMeshProUGUI textComboDamage;
        [SerializeField]
        int maxCombo;

        Emotion[] comboEmotion;
        int comboCount = 0;
        int comboDamage = 0;

        [Title("Feedback")]
        [SerializeField]
        Animator handActiveFace;
        [SerializeField]
        Image feedbackSelectCard;
        [SerializeField]
        Animator feedbackSelectCardAnimator;
        [SerializeField]
        ParticleSystem particle;
        [SerializeField]
        Image haloEmotion;
        [SerializeField]
        Animator haloEmotionAnimator;
        [SerializeField]
        Color[] colorParticle;

        [SerializeField]
        AudioClip audioClipStartTurn;
        [SerializeField]
        AudioClip audioClipSelection;


        [SerializeField]
        protected int timeBeforeRepeat = 10;
        [SerializeField]
        protected int repeatInterval = 10;

        protected int currentTimeBeforeRepeat = -1;
        protected int currentRepeatInterval = -1;
        protected int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private IEnumerator coroutineSelection;
        private IEnumerator coroutineTimerSelection;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetComboCount()
        {
            return maxCombo-(comboCount+1);
        }

        public int GetIndex()
        {
            return indexSelection;
        }

        public EmotionCard[] GetCardsInHand()
        {
            return handCards;
        }

        public Emotion[] GetEmotions()
        {
            return comboEmotion;
        }

        public int GetDamage()
        {
            return comboDamage;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void Start()
        {
            voiceActor = new VoiceActor(voiceActorData);
            handCards = new EmotionCard[seiyuuDeck.HandSize];
            for (int i = 0; i < seiyuuDeck.HandSize; i++)
            {
                seiyuuHand.Add(null);
                handCards[i] = null;
            }

            DrawNextCard();
            for (int i = 0; i < seiyuuDeck.HandSize; i++)
            {
                DrawCard();
            }

            comboEmotion = new Emotion[maxCombo];
            for (int i = 0; i < maxCombo; i++)
                comboEmotion[i] = Emotion.Neutre;
        }

        public void DrawNextCard()
        {
            int draw = Random.Range(0, seiyuuDeck.SeiyuuBattleCards.Count);
            seiyuuHandNextCard = seiyuuDeck.SeiyuuBattleCards[draw];
            seiyuuDeck.SeiyuuBattleCards.RemoveAt(draw);

            nextCard = Instantiate(cardPrefab, nextCardPosition);
            nextCard.SetEmotion(seiyuuHandNextCard.Emotion);
            nextCard.SetSprite(emotionSprite[(int)nextCard.GetEmotion()]);
            nextCard.DrawStat(voiceActor.Statistique.GetEmotion((int)nextCard.GetEmotion()));
        }

        public void DrawCard()
        {
            ShiftHand();

            seiyuuHand[0] = seiyuuHandNextCard;

            handCards[0] = nextCard;
            handCards[0].MoveCard(cardsPositions[0], 1.1f);

            DrawNextCard();
        }

        public void DrawCardUntilHandFull()
        {
            for(int i = 0; i < seiyuuHand.Count; i++)
            {
                if(seiyuuHand[i] == null)
                    DrawCard();
            }
        }

        private void ShiftHand()
        {
            for(int i = seiyuuDeck.HandSize-1; i >= 0; i--)
            {
                if(seiyuuHand[i] != null)
                {
                    if (i + 1 < seiyuuDeck.HandSize)
                    {
                        if (seiyuuHand[i + 1] == null)
                        {
                            seiyuuHand[i + 1] = seiyuuHand[i];
                            seiyuuHand[i] = null;

                            handCards[i + 1] = handCards[i];
                            handCards[i].MoveCard(cardsPositions[i + 1], 1.1f);
                            handCards[i] = null;
                        }
                    }
                }
            }
        }

        public void DestroyComboCards()
        {
            for(int i = 0; i < comboCards.Count; i++)
            {
                comboCards[i].DestroyCard();
            }
            comboCards.Clear();
        }













        public void CheckComboCardPreview()
        {
            for (int i = 0; i < handCards.Length; i++)
            {
                if (handCards[i].GetEmotion() == comboEmotion[comboCount])
                    handCards[i].DrawPreview();
                else
                    handCards[i].StopPreview();
            }
        }

        public void StopComboCardPreview()
        {
            for (int i = 0; i < handCards.Length; i++)
            {
                handCards[i].StopPreview();
            }
        }















        public void SelectCard()
        {
            if (isDiscarding == true)
                return;


            if (isAttacking == true)
            {
                if (comboEmotion[comboCount] != handCards[indexSelection].GetEmotion())
                {
                    comboCount += 1;
                    if (comboCount >= maxCombo)
                        return;
                    comboEmotion[comboCount] = handCards[indexSelection].GetEmotion();
                    textComboCount.text = (maxCombo - (comboCount+1)).ToString();
                }
            }
            else
            {
                isAttacking = true;
                comboEmotion[comboCount] = handCards[indexSelection].GetEmotion();
                textComboCount.text = (maxCombo - (comboCount+1)).ToString();
            }
            comboDamage += handCards[indexSelection].GetBaseValue();
            textComboDamage.text = comboDamage.ToString();
            CheckComboCardPreview();
            handCards[indexSelection].StopPreview();

            handCards[indexSelection].FeedbackCardSelected(feedbackSelectCard);
            feedbackSelectCardAnimator.SetTrigger("Feedback");
            ParticleSelectEmotion(handCards[indexSelection].GetEmotion());

            seiyuuGraveyard.Add(seiyuuHand[indexSelection]);
            seiyuuHand[indexSelection] = null;
            handCards[indexSelection].MoveCard(comboPosition, 1.1f);
            comboCards.Add(handCards[indexSelection]);
            handCards[indexSelection] = null;

            StartTimer();
            DrawCard();
            atbManager.AddATB(atbAttackValue);
        }

        public void DiscardCard()
        {
            if (isAttacking == true)
                return;
            if (seiyuuHand[indexSelection] == null)
                return;
            isDiscarding = true;
            seiyuuGraveyard.Add(seiyuuHand[indexSelection]);
            seiyuuHand[indexSelection] = null;

            handCards[indexSelection].MoveCard(discardPosition, 1.1f);
            handCards[indexSelection].DestroyCard();
            handCards[indexSelection] = null;

            StartTimer();
            DrawCard();
            atbManager.AddATB(atbDiscardValue);

            // modifyTone
        }



        public void StartTimer()
        {
            textTimer.gameObject.SetActive(true);
            timer = timerValue;

            if (coroutineTimerSelection != null)
                StopCoroutine(coroutineTimerSelection);
            coroutineTimerSelection = TimerSelectionCoroutine();
            StartCoroutine(coroutineTimerSelection);
        }

        public void SkipTimer()
        {
            if (timer == 0)
                return;
            if (coroutineTimerSelection != null)
                StopCoroutine(coroutineTimerSelection);
            ATBEnd();
        }

        private IEnumerator TimerSelectionCoroutine()
        {
            int second = 0;
            int frame = 0;
            while(timer > 0)
            {
                timer -= Time.deltaTime;
                second = Mathf.FloorToInt(timer);
                frame = (int)((timer - Mathf.FloorToInt(timer)) * 100);
                if(frame < 10)
                    textTimer.text = second + " : 0" + frame;
                else 
                    textTimer.text = second + " : " + frame;
                yield return null;
            }
            ATBEnd();
        }

        public void ATBEnd()
        {
            timer = 0;
            textTimer.gameObject.SetActive(false);
            SelectionActivate(false);
            StopComboCardPreview();
            if (isDiscarding == true)
            {
                atbManager.StartATB();
                isDiscarding = false;
                eventATBEndDiscard.Invoke();
            }
            if (isAttacking == true)
            {
                isAttacking = false;
                textComboCount.text = "";
                textComboDamage.text = "";
                eventATBEndBattle.Invoke();
            }
        }





        public void StopRound()
        {
            DestroyComboCards();
            if (coroutineTimerSelection != null)
                StopCoroutine(coroutineTimerSelection);
            timer = 0;
            textTimer.gameObject.SetActive(false);
            StopComboCardPreview();
            isAttacking = false;
            isDiscarding = false;
            textComboCount.text = "";
            textComboDamage.text = "";

            SelectionActivate(false);
        }



        public void SelectionActivate(bool b)
        {
            inputTurnActive.gameObject.SetActive(b);
            textComboCount.gameObject.SetActive(b);
            if(b == true)
            {
                comboCount = 0;
                comboDamage = 0;
                for (int i = 0; i < maxCombo; i++)
                    comboEmotion[i] = Emotion.Neutre;
                handActiveFace.gameObject.SetActive(true);
                handActiveFace.SetTrigger("Feedback");

                textComboCount.text = (maxCombo - comboCount).ToString();
                textComboDamage.text = "";
                int size = seiyuuGraveyard.Count;
                for (int i = 0; i < size; i++)
                {
                    seiyuuDeck.SeiyuuBattleCards.Add(seiyuuGraveyard[0]);
                    seiyuuGraveyard.RemoveAt(0);
                }
                AudioManager.Instance.PlaySound(audioClipStartTurn, 1f);
            }
            else
            {
                handActiveFace.SetTrigger("Disappear");
            }
        }







        private void ParticleSelectEmotion(Emotion emotion)
        {
            AudioManager.Instance.PlaySound(audioClipSelection, 0.8f);
            if (particle == null)
                return;
            if (haloEmotion == null)
                return;

            var particleColor = particle.main;
            Color colorEmotion = colorParticle[(int)emotion];
            particleColor.startColor = colorEmotion;
            haloEmotion.color = new Color(colorEmotion.r, colorEmotion.g, colorEmotion.b, 0);
            haloEmotionAnimator.SetTrigger("Feedback");
            particle.Play();
        }




        public void SelectCardLeft()
        {
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }
            if (CheckRepeat() == false)
                return;

            SelectLeft();
        }

        public void SelectCardRight()
        {
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }
            if (CheckRepeat() == false)
                return;

            SelectRight();
        }



        public void SelectRight()
        {
            indexSelection += 1;
            if (indexSelection == seiyuuDeck.HandSize)
            {
                indexSelection -= 1;
                return;
            }
            if (coroutineSelection != null)
                StopCoroutine(coroutineSelection);
            coroutineSelection = HandSelectionCoroutine();
            StartCoroutine(coroutineSelection);
        }

        public void SelectLeft()
        {
            indexSelection -= 1;
            if (indexSelection == -1)
            {
                indexSelection += 1;
                return;
            }
            if (coroutineSelection != null)
                StopCoroutine(coroutineSelection);
            coroutineSelection = HandSelectionCoroutine();
            StartCoroutine(coroutineSelection);
        }



        private IEnumerator HandSelectionCoroutine()
        {
            float speed = 0.1f;
            float blend;
            int time = 1;
            float t = 0f;
            cardSelection.SetParent(cardsPositions[indexSelection]);
            Vector2 finalPosition = Vector2.zero;
            while (t < 1f)
            {
                t += Time.deltaTime / time;
                blend = 1f - Mathf.Pow(1f - speed, Time.deltaTime * 60);
                cardSelection.anchoredPosition = Vector2.Lerp(cardSelection.anchoredPosition, finalPosition, blend);
                yield return null;
            }
        }


        // Check si on peut repeter l'input
        protected bool CheckRepeat()
        {
            if (currentRepeatInterval == -1)
            {
                if (currentTimeBeforeRepeat == -1)
                {
                    currentTimeBeforeRepeat = timeBeforeRepeat;
                    return true;
                }
                else if (currentTimeBeforeRepeat == 0)
                {
                    currentRepeatInterval = repeatInterval;
                }
                else
                {
                    currentTimeBeforeRepeat -= 1;
                }
            }
            else if (currentRepeatInterval == 0)
            {
                currentRepeatInterval = repeatInterval;
                return true;
            }
            else
            {
                currentRepeatInterval -= 1;
            }
            return false;
        }

        public void StopRepeat()
        {
            currentRepeatInterval = -1;
            currentTimeBeforeRepeat = -1;
        }


        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace
