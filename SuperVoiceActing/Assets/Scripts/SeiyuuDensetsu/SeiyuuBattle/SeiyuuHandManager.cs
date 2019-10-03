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


        [Title("Card (Debug)")]
        [SerializeField]
        SeiyuuBattleCard seiyuuHandNextCard;
        [SerializeField]
        List<SeiyuuBattleCard> seiyuuHand = new List<SeiyuuBattleCard>();
        [SerializeField]
        List<SeiyuuBattleCard> seiyuuGraveyard;

        [Title("CardPosition")]
        [SerializeField]
        RectTransform cardSelection;
        [SerializeField]
        RectTransform[] cardsPositions;
        [SerializeField]
        RectTransform nextCardPosition;



        [SerializeField]
        EmotionCard cardPrefab;

        EmotionCard[] handCards;
        EmotionCard nextCard;



        [Title("Timer")]
        [SerializeField]
        TextMeshProUGUI textTimer;
        [SerializeField]
        float timerValue = 3f;
        float timer = 0f;

        int indexSelection = 0;
        VoiceActor voiceActor;





        [SerializeField]
        protected int timeBeforeRepeat = 10;
        [SerializeField]
        protected int repeatInterval = 10;

        protected int currentTimeBeforeRepeat = -1;
        protected int currentRepeatInterval = -1;
        protected int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private IEnumerator coroutineSelection;

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




        public void SelectCard()
        {

        }

        public void DiscardCard()
        {
            seiyuuGraveyard.Add(seiyuuHand[indexSelection]);
            seiyuuHand[indexSelection] = null;

            Destroy(handCards[indexSelection].gameObject);
            handCards[indexSelection] = null;

            StartTimer();

            DrawCard();
        }



        public void StartTimer()
        {
            textTimer.gameObject.SetActive(true);
            timer = timerValue;
            StartCoroutine(TimerSelectionCoroutine());
        }

        public void RefreshTimer()
        {

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

        public void SelectCardRight()
        {
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }
            if (CheckRepeat() == false)
                return;

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
