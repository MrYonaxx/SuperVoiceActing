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
        [SerializeField]
        bool inPercentage;
        [SerializeField]
        int damageVariance;

        [HideLabel]
        [SerializeField]
        EmotionStat emotionDamage;


        public override void ApplySkillEffect(SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {
            Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }
            if (skillTarget != SkillTarget.ManualPackSelection)
                CalculateTarget();
            if (inPercentage == true)
            {
                actorsManager.AddActorEmotionDamage(cardTargetsData, buff);
            }
            else
            {
            }
        }

        /*public override void RemoveSkillEffect(SkillData skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            if (inPercentage == true)
            {
                actorsManager.RemoveActorEmotionDamage(cardTargetsData);
            }
            else
            {
            }
        }*/


        public override void RemoveSkillEffectCard(EmotionCard card)
        {
            switch (card.GetEmotion())
            {
                case Emotion.Neutre:
                    card.AddDamagePercentage(-emotionDamage.Neutral);
                    break;
                case Emotion.Joie:
                    card.AddDamagePercentage(-emotionDamage.Joy);
                    break;
                case Emotion.Tristesse:
                    card.AddDamagePercentage(-emotionDamage.Sadness);
                    break;
                case Emotion.Dégoût:
                    card.AddDamagePercentage(-emotionDamage.Disgust);
                    break;
                case Emotion.Colère:
                    card.AddDamagePercentage(-emotionDamage.Anger);
                    break;
                case Emotion.Surprise:
                    card.AddDamagePercentage(-emotionDamage.Surprise);
                    break;
                case Emotion.Douceur:
                    card.AddDamagePercentage(-emotionDamage.Sweetness);
                    break;
                case Emotion.Peur:
                    card.AddDamagePercentage(-emotionDamage.Fear);
                    break;
                case Emotion.Confiance:
                    card.AddDamagePercentage(-emotionDamage.Trust);
                    break;

            }
        }




        public override void ManualTarget(Emotion emotion)
        {
            cardTargetsData.Clear();
            int stat = 0;
            switch ((int)emotion)
            {
                case 0: // Neutral
                    stat = emotionDamage.Neutral;
                    break;
                case 1: // Joie
                    stat = emotionDamage.Joy;
                    break;
                case 2: // Tristesse
                    stat = emotionDamage.Sadness;
                    break;
                case 3: // Dégout
                    stat = emotionDamage.Disgust;
                    break;
                case 4: // Colère
                    stat = emotionDamage.Anger;
                    break;
                case 5: // Surprise
                    stat = emotionDamage.Surprise;
                    break;
                case 6: // Douceur
                    stat = emotionDamage.Sweetness;
                    break;
                case 7: // Peur
                    stat = emotionDamage.Fear;
                    break;
                case 8: // Confiance
                    stat = emotionDamage.Trust;
                    break;

            }
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
                int stat = 0;
                switch (i)
                {
                    case 0: // Neutral
                        stat = emotionDamage.Neutral;
                        break;
                    case 1: // Joie
                        stat = emotionDamage.Joy;
                        break;
                    case 2: // Tristesse
                        stat = emotionDamage.Sadness;
                        break;
                    case 3: // Dégout
                        stat = emotionDamage.Disgust;
                        break;
                    case 4: // Colère
                        stat = emotionDamage.Anger;
                        break;
                    case 5: // Surprise
                        stat = emotionDamage.Surprise;
                        break;
                    case 6: // Douceur
                        stat = emotionDamage.Sweetness;
                        break;
                    case 7: // Peur
                        stat = emotionDamage.Fear;
                        break;
                    case 8: // Confiance
                        stat = emotionDamage.Trust;
                        break;

                }

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
