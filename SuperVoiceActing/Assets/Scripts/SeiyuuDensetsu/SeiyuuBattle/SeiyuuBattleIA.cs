/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuBattleIA : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        SeiyuuHandManager handEnemy;

        [HorizontalGroup("ReactionStartTurn")]
        [SerializeField]
        float timeReactionMinStartTurn = 1f;
        [HorizontalGroup("ReactionStartTurn")]
        [SerializeField]
        float timeReactionMaxStartTurn = 2f;

        [HorizontalGroup("CardScoreForAttack")]
        [SerializeField]
        float[] cardScoreForAttack;
        [HorizontalGroup("CardScoreForAttack")]
        [SerializeField]
        float[] cardScoreForAttackProbability;



        [SerializeField]
        float timeSelectionMin = 0.5f;
        [SerializeField]
        float timeSelectionMax = 1.5f;

        [SerializeField]
        int weaknessKnowedAtStartMin = 0;
        [SerializeField]
        int weaknessKnowedAtStartMax = 2;

        [SerializeField]
        int weaknessTargetLenghtMin = 1;
        [SerializeField]
        int weaknessTargetLenghtMax = 4;

        [SerializeField]
        EmotionStat weaknessKnowledge = new EmotionStat(0,0,0,0,0,0,0,0);

        [SerializeField]
        List<int> weaknessTarget = new List<int>();
        [SerializeField]
        List<int> weaknessNotKnown = new List<int>();
        [SerializeField]
        List<int> cardPositionUsable = new List<int>();

        TextData currentTextData;

        private IEnumerator coroutineIA;

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

        public void SetTextData(TextData textData)
        {
            int rand = Random.Range(weaknessKnowedAtStartMin, weaknessKnowedAtStartMax+1);
            weaknessNotKnown = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < rand; i++)
            {
                int selectWeakness = Random.Range(0, weaknessNotKnown.Count);
                weaknessKnowledge.SetValue(weaknessNotKnown[selectWeakness], textData.EnemyResistance.GetEmotion(weaknessNotKnown[selectWeakness]));
                weaknessNotKnown.RemoveAt(selectWeakness);
            }
            currentTextData = textData;
        }

        private void UpdateWeakness()
        {
            if (weaknessNotKnown.Count == 0)
                return;
            int selectWeakness = Random.Range(0, weaknessNotKnown.Count);
            weaknessKnowledge.SetValue(weaknessNotKnown[selectWeakness], currentTextData.EnemyResistance.GetEmotion(weaknessNotKnown[selectWeakness]));
            weaknessNotKnown.RemoveAt(selectWeakness);
        }

        private void OnEnable()
        {
            coroutineIA = EnemyIACoroutine();
            StartCoroutine(coroutineIA);
        }

        private void OnDisable()
        {
            UpdateWeakness();
            if (coroutineIA != null)
                StopCoroutine(coroutineIA);
        }

        private IEnumerator EnemyIACoroutine()
        {
            yield return new WaitForSeconds(Random.Range(timeReactionMinStartTurn, timeReactionMaxStartTurn));
            AnalyzeWeakness();
            if(AttackOrDiscard() == true) // Discard
            {
                AnalyzeCardsUsableDiscard();
                while (cardPositionUsable.Count > 0)
                {
                    int itinary = CheckItinary();
                    while (itinary != 0)
                    {
                        if (itinary < 0)
                        {
                            yield return new WaitForSeconds(Random.Range(timeSelectionMin, timeSelectionMax));
                            handEnemy.SelectRight();
                            itinary += 1;
                        }
                        else
                        {
                            yield return new WaitForSeconds(Random.Range(timeSelectionMin, timeSelectionMax));
                            handEnemy.SelectLeft();
                            itinary -= 1;
                        }
                    }
                    yield return new WaitForSeconds(Random.Range(timeSelectionMin, timeSelectionMax));
                    handEnemy.DiscardCard();
                    AnalyzeCardsUsableDiscard();
                }
            }
            else // Attack -----------------------------------------------------------
            {
                AnalyzeCardsUsableAttack();
                while (cardPositionUsable.Count > 0 && handEnemy.GetComboCount() != 0)
                {
                    int itinary = CheckItinary();
                    while (itinary != 0)
                    {
                        if (itinary < 0)
                        {
                            yield return new WaitForSeconds(Random.Range(timeSelectionMin, timeSelectionMax));
                            handEnemy.SelectRight();
                            itinary += 1;
                        }
                        else
                        {
                            yield return new WaitForSeconds(Random.Range(timeSelectionMin, timeSelectionMax));
                            handEnemy.SelectLeft();
                            itinary -= 1;
                        }
                    }
                    yield return new WaitForSeconds(Random.Range(timeSelectionMin, timeSelectionMax));
                    handEnemy.SelectCard();
                    AnalyzeCardsUsableAttack();
                }
            }
        }













        // A N A L Y Z E

        private void AnalyzeWeakness()
        {
            int currentStat = 0;

            weaknessTarget.Clear();
            for (int i = 0; i < weaknessKnowledge.GetLength(); i++)
            {
                currentStat = weaknessKnowledge.GetEmotion(i);
                if (currentStat >= 0)
                {
                    weaknessTarget.Add(i);
                }
            }

        }


        private int AnalyzeHand()
        {
            EmotionCard[] hand = handEnemy.GetCardsInHand();
            int handScore = 0;
            for(int i = 0; i < hand.Length; i++)
            {
                for(int j = 0; j < weaknessTarget.Count; j++)
                {
                    if((int)hand[i].GetEmotion() == weaknessTarget[j])
                    {
                        handScore += 1;
                    }
                }
            }
            return handScore;
        }

        private bool AttackOrDiscard()
        {
            int handScore = AnalyzeHand();
            bool discard = true;
            for(int i = 0; i < cardScoreForAttack.Length; i++)
            {
                if(handScore > cardScoreForAttack[i])
                {
                    if (Random.Range(0, 100) <= cardScoreForAttackProbability[i])
                    {
                        discard = false;
                        break;
                    }
                }
            }
            return discard;
        }









        // A T T A C K

        private void AnalyzeCardsUsableAttack()
        {
            cardPositionUsable.Clear();
            EmotionCard[] hand = handEnemy.GetCardsInHand();
            for (int i = 0; i < hand.Length; i++)
            {
                for (int j = 0; j < weaknessTarget.Count; j++)
                {
                    if ((int)hand[i].GetEmotion() == weaknessTarget[j])
                    {
                        cardPositionUsable.Add(i);
                    }
                }
            }
        }

        private int CheckItinary()
        {
            int rand = cardPositionUsable[Random.Range(0, cardPositionUsable.Count)];
            int index = handEnemy.GetIndex();
            return index - rand;
        }





        // D I S C A R D

        private void AnalyzeCardsUsableDiscard()
        {
            cardPositionUsable.Clear();
            EmotionCard[] hand = handEnemy.GetCardsInHand();
            bool useless = true;
            for (int i = 0; i < hand.Length; i++)
            {
                useless = true;
                for (int j = 0; j < weaknessTarget.Count; j++)
                {
                    if ((int)hand[i].GetEmotion() == weaknessTarget[j])
                    {
                        useless = false;
                    }
                }
                if (useless == true)
                    cardPositionUsable.Add(i);
            }
        }


        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace
