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
                    if (targetAcquired == false)
                        CalculateTarget();
                    else
                        targetAcquired = false;
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








        public override void ManualTarget(Emotion[] emotion, bool selectionByPack)
        {
            cardTargetsData.Clear();
            int stat = 0;
            int[] manualIndex = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < emotion.Length; i++)
            {
                stat = emotionStat.GetEmotion((int)(emotion[i]));
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



        public override void PreviewTarget(DoublageManager doublageManager)
        {
            base.PreviewTarget(doublageManager);

            EmotionCardTotal[] cards = doublageManager.EmotionAttackManager.GetCards();

            for (int i = 0; i < cards.Length; i++)
            {
                if (emotionStat.GetEmotion(i) != 0)
                {
                    for (int j = 0; j < cards[i].Cards.Length; j++)
                    {
                        if (cards[i].Cards[j] != null)
                            cards[i].Cards[j].DrawPreviewStat(emotionStat.GetEmotion(i), inPercentage);
                    }
                }
            }
        }

    } // SkillEffectEmotionStat class
	
}// #PROJECTNAME# namespace
