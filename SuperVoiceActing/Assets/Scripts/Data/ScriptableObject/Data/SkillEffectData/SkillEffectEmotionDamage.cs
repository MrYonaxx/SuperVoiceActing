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


        public override void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {
            Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }
            CalculateTarget();
            AddActorEmotionDamage(doublageManager.EmotionAttackManager.GetCards(), buff);

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



        public override void RemoveSkillEffectCard(EmotionCard card)
        {
            card.AddDamagePercentage(-emotionDamage.GetEmotion((int)card.GetEmotion()));
        }




        public override void ManualTarget(Emotion emotion)
        {
            cardTargetsData.Clear();
            int stat = emotionDamage.GetEmotion((int)emotion);
            if (stat != 0)
            {
                cardTargetsData.Add(new Vector3Int((int)emotion, 0, stat));
                cardTargetsData.Add(new Vector3Int((int)emotion, 1, stat));
                cardTargetsData.Add(new Vector3Int((int)emotion, 2, stat));
            }
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












    } // SkillEffectEmotionDamage class
	
}// #PROJECTNAME# namespace
