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
    public class SkillEffectEmotionStat : SkillEffectData
    {
        [SerializeField]
        bool inPercentage;
        [SerializeField]
        int statVariance;

        [HideLabel]
        [SerializeField]
        EmotionStat emotionStat;





        public override void ApplySkillEffect(SkillTarget skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {
            Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }
            if(skill != SkillTarget.ManualPackSelection)
                CalculateTarget();
            if (inPercentage == true)
            {
                actorsManager.AddActorStatPercentage(cardTargetsData);
            }
            else
            {
                actorsManager.AddActorStat(cardTargetsData, buff);
            }

        }



        /*public override void RemoveSkillEffect(SkillData skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            CalculateTarget();
            if (inPercentage == true)
            {
                actorsManager.RemoveActorStatPercentage(cardTargetsData);
            }
            else
            {
                actorsManager.RemoveActorStat(cardTargetsData);
            }
        }*/


        public override void RemoveSkillEffectCard(EmotionCard card)
        {
            switch (card.GetEmotion())
            {
                case Emotion.Neutre:
                    card.AddStat(-emotionStat.Neutral);
                    break;
                case Emotion.Joie:
                    card.AddStat(-emotionStat.Joy);
                    break;
                case Emotion.Tristesse:
                    card.AddStat(-emotionStat.Sadness);
                    break;
                case Emotion.Dégoût:
                    card.AddStat(-emotionStat.Disgust);
                    break;
                case Emotion.Colère:
                    card.AddStat(-emotionStat.Anger);
                    break;
                case Emotion.Surprise:
                    card.AddStat(-emotionStat.Surprise);
                    break;
                case Emotion.Douceur:
                    card.AddStat(-emotionStat.Sweetness);
                    break;
                case Emotion.Peur:
                    card.AddStat(-emotionStat.Fear);
                    break;
                case Emotion.Confiance:
                    card.AddStat(-emotionStat.Trust);
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
                    stat = emotionStat.Neutral;
                    break;
                case 1: // Joie
                    stat = emotionStat.Joy;
                    break;
                case 2: // Tristesse
                    stat = emotionStat.Sadness;
                    break;
                case 3: // Dégout
                    stat = emotionStat.Disgust;
                    break;
                case 4: // Colère
                    stat = emotionStat.Anger;
                    break;
                case 5: // Surprise
                    stat = emotionStat.Surprise;
                    break;
                case 6: // Douceur
                    stat = emotionStat.Sweetness;
                    break;
                case 7: // Peur
                    stat = emotionStat.Fear;
                    break;
                case 8: // Confiance
                    stat = emotionStat.Trust;
                    break;

            }
            if (stat != 0)
            {
                cardTargetsData.Add(new Vector3Int((int) emotion, 0, stat));
                cardTargetsData.Add(new Vector3Int((int) emotion, 1, stat));
                cardTargetsData.Add(new Vector3Int((int) emotion, 2, stat));
            }
        }

        private void CalculateTarget()
        {
            cardTargetsData.Clear();
            int stat = 0;
            for (int i = 0; i < 9; i++)
            {
                stat = emotionStat.GetEmotion(i);
                /*switch (i)
                {
                    case 0: // Neutral
                        stat = emotionStat.Neutral;
                        break;
                    case 1: // Joie
                        stat = emotionStat.Joy;
                        break;
                    case 2: // Tristesse
                        stat = emotionStat.Sadness;
                        break;
                    case 3: // Dégout
                        stat = emotionStat.Disgust;
                        break;
                    case 4: // Colère
                        stat = emotionStat.Anger;
                        break;
                    case 5: // Surprise
                        stat = emotionStat.Surprise;
                        break;
                    case 6: // Douceur
                        stat = emotionStat.Sweetness;
                        break;
                    case 7: // Peur
                        stat = emotionStat.Fear;
                        break;
                    case 8: // Confiance
                        stat = emotionStat.Trust;
                        break;

                }*/

                if (stat != 0)
                {
                    cardTargetsData.Add(new Vector3Int(i, 0, stat));
                    cardTargetsData.Add(new Vector3Int(i, 1, stat));
                    cardTargetsData.Add(new Vector3Int(i, 2, stat));
                }
            }
        }

    } // SkillEffectEmotionStat class
	
}// #PROJECTNAME# namespace
