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
	public class SkillEffectTradeStat : SkillEffectData
	{
        [HideLabel]
        [SerializeField]
        Emotion firstEmotion;
        [HideLabel]
        [SerializeField]
        Emotion secondEmotion;

        EmotionCard[] packA;
        EmotionCard[] packB;


        public override void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {
            Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
                if (doublageManager.ActorsManager.AddBuff(buff))
                    InvertActorStat(doublageManager.EmotionAttackManager.GetCards());
            }
            else
            {
                InvertActorStat(doublageManager.EmotionAttackManager.GetCards());
            }

        }


        private void InvertActorStat(EmotionCardTotal[] emotionCards)
        {
            packA = emotionCards[(int)firstEmotion].Cards;
            packB = emotionCards[(int)secondEmotion].Cards;

            InvertActorStat();
        }


        private void InvertActorStat()
        {
            int[] tmpBaseValue = new int[3];

            for (int i = 0; i < packB.Length; i++)
            {
                if (packA[i] == null)
                {
                    tmpBaseValue[i] = tmpBaseValue[i - 1];
                    if (packB[i] != null)
                        packB[i].DrawStat(packA[i].GetBaseValue());
                }
                else if (packB[i] != null)
                {
                    tmpBaseValue[i] = packB[i].GetBaseValue();
                    packB[i].DrawStat(packA[i].GetBaseValue());
                }
            }
            for (int i = 0; i < packA.Length; i++)
            {
                if (packB[i] != null)
                {
                    packA[i].DrawStat(tmpBaseValue[i]);
                }
            }
        }


        public override void RemoveSkillEffectActor(VoiceActor actor)
        {
            InvertActorStat();
        }


    } // SkillEffectTradeStat class
	
}// #PROJECTNAME# namespace
