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
        SkillTarget skillTarget;

        [SerializeField]
        bool inPercentage;
        [HideLabel]
        [SerializeField]
        EmotionStat emotionStat;
        [SerializeField]
        int statVariance;





        public override void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {
            Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }

            switch(skillTarget)
            {
                case SkillTarget.VoiceActor:
                    if (inPercentage == true)
                    {
                        AddActorStatPercentage(doublageManager.ActorsManager, buff);
                    }
                    else
                    {
                        AddActorStat(doublageManager.ActorsManager, buff);
                    }
                    break;
                case SkillTarget.Cards:
                    CalculateTarget();
                    if (inPercentage == true)
                    {
                        AddCardStatPercentage(doublageManager.EmotionAttackManager.GetCards(), buff);
                    }
                    else
                    {
                        AddCardStat(doublageManager.EmotionAttackManager.GetCards(), buff);
                    }
                    break;
            }

        }




        //    A C T O R

        public void AddActorStat(ActorsManager actorsManager, Buff buff)
        {
            if (buff != null)
            {
                if (actorsManager.AddBuff(buff) == true) // on peut ajouter un buff au voice actor
                {
                    actorsManager.GetCurrentActor().StatModifier.Add(emotionStat);
                    actorsManager.DrawActorStat();
                }
            }
            else
            {
                actorsManager.GetCurrentActor().StatModifier.Add(emotionStat);
                actorsManager.DrawActorStat();
            }
        }

        public void AddActorStatPercentage(ActorsManager actorsManager, Buff buff)
        {
            if (buff != null)
            {
                if (actorsManager.AddBuff(buff) == true) // on peut ajouter un buff au voice actor
                {
                    actorsManager.GetCurrentActor().StatModifier.Add(emotionStat);
                    actorsManager.DrawActorStat();
                }
            }
            else
            {
                actorsManager.GetCurrentActor().StatModifier.Add(emotionStat);
                actorsManager.DrawActorStat();
            }
        }


        public override void RemoveSkillEffectActor(VoiceActor actor)
        {
            actor.StatModifier.Add(emotionStat.Reverse());

        }







        //   C A R D S


        public void AddCardStat(EmotionCardTotal[] cards, Buff buff)
        {
            if (buff != null)
            {
                for (int i = 0; i < cardTargetsData.Count; i++)
                {
                    if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y] != null)
                    {
                        if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddBuff(buff) == true)
                        {
                            cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddStat(cardTargetsData[i].z);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < cardTargetsData.Count; i++)
                {
                    if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y] != null)
                    {
                        cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddStat(cardTargetsData[i].z);
                    }
                }
            }
        }


        public void AddCardStatPercentage(EmotionCardTotal[] cards, Buff buff)
        {
            if (buff != null)
            {
                for (int i = 0; i < cardTargetsData.Count; i++)
                {
                    if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y] != null)
                    {
                        if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddBuff(buff) == true)
                            cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddStatPercentage(cardTargetsData[i].z);
                    }
                }
            }
            else
            {
                for (int i = 0; i < cardTargetsData.Count; i++)
                {
                    if (cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y] != null)
                    {
                        cards[cardTargetsData[i].x].Cards[cardTargetsData[i].y].AddStatPercentage(cardTargetsData[i].z);
                    }
                }
            }
        }

        public override void RemoveSkillEffectCard(EmotionCard card)
        {
            if(inPercentage == true)
            {

            }
            else
            {
                card.AddStat(-emotionStat.GetEmotion((int)card.GetEmotion()));
            }

        }








        public override void ManualTarget(Emotion emotion)
        {
            cardTargetsData.Clear();
            int stat = emotionStat.GetEmotion((int)(emotion));
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
