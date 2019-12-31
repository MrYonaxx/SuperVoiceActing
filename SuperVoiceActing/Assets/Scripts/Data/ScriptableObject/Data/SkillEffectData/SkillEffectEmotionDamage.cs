/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [System.Serializable]
	public class SkillEffectEmotionDamage : SkillEffectData
    {


        [HideLabel]
        [SerializeField]
        EmotionStat emotionDamage;
        [SerializeField]
        int damageVariance;


        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            /*Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }
            if (targetAcquired == false)
                CalculateTarget();
            else
                targetAcquired = false;
            AddActorEmotionDamage(doublageManager.EmotionAttackManager.GetCards(), buff);*/

        }


        public void AddActorEmotionDamage(EmotionCardTotal[] cards, Buff buff = null)
        {
            if (buff != null)
            {
                for (int i = 0; i < cardTargetsData.Count; i++)
                {
                    if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y] != null)
                    {
                        if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddBuff(buff) == true)
                        {
                            cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddDamagePercentage(cardTargetsData[i].z);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < cardTargetsData.Count; i++)
                {
                    if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y] != null)
                        cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddDamagePercentage(cardTargetsData[i].z);
                }
            }
        }



        /*public override void RemoveSkillEffectCard(EmotionCard card)
        {
            card.AddDamagePercentage(-emotionDamage.GetEmotion((int)card.GetEmotion()));
        }




        public override void ManualTarget(Emotion[] emotion, bool selectionByPack)
        {
            cardTargetsData.Clear();
            int stat = 0;
            int[] manualIndex = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < emotion.Length; i++)
            {
                stat = emotionDamage.GetEmotion((int)(emotion[i]));
                Debug.Log(emotion[i]);
                Debug.Log(stat);
                if (stat != 0)
                {
                    if (selectionByPack == true)
                    {
                        cardTargetsData.Add(new Vector3Int((int)emotion[i], 0, stat));
                        cardTargetsData.Add(new Vector3Int((int)emotion[i], 1, stat));
                        cardTargetsData.Add(new Vector3Int((int)emotion[i], 2, stat));
                    }
                    else
                    {
                        cardTargetsData.Add(new Vector3Int((int)emotion[i], manualIndex[(int)emotion[i]], stat));
                        manualIndex[(int)emotion[i]] += 1;
                    }
                }
            }
            targetAcquired = true;
        }

        private void CalculateTarget()
        {
            cardTargetsData.Clear();
            for (int i = 0; i < 9; i++)
            {
                int stat = emotionDamage.GetEmotion(i);
                if (stat != 0)
                {
                    cardTargetsData.Add(new Vector3Int(i, 0, stat));
                    cardTargetsData.Add(new Vector3Int(i, 1, stat));
                    cardTargetsData.Add(new Vector3Int(i, 2, stat));
                }
            }
        }







        public override void PreviewTarget(DoublageManager doublageManager)
        {
            base.PreviewTarget(doublageManager);

            EmotionCardTotal[] cards = doublageManager.EmotionAttackManager.GetCards();

            for (int i = 0; i < cards.Length; i++)
            {
                if (emotionDamage.GetEmotion(i) != 0)
                {
                    for (int j = 0; j < cards[i].Cards.Length; j++)
                    {
                        if (cards[i].Cards[j] != null)
                            cards[i].Cards[j].DrawPreviewText("+" + (emotionDamage.GetEmotion(i) / 100) + "x DMG");
                    }
                }
            }
        }*/




    } // SkillEffectEmotionDamage class
	
}// #PROJECTNAME# namespace
