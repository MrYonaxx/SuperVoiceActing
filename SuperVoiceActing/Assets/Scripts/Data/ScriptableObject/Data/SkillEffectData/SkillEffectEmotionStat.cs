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

        [HideLabel]
        [SerializeField]
        EmotionStat emotionStat;





        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    doublageBattleParameter.VoiceActors[0].StatModifier.Add(emotionStat);
                    break;
            }
        }
        public override void RemoveSkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    doublageBattleParameter.VoiceActors[0].StatModifier.Substract(emotionStat);
                    break;
            }
        }



        public override void PreviewSkill(DoublageBattleParameter doublageBattleParameter)
        {
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    doublageBattleParameter.VoiceActors[0].StatModifier.Add(emotionStat);
                    doublageBattleParameter.ActorsManager.DrawActorStat(doublageBattleParameter.VoiceActors[0], doublageBattleParameter.Cards);
                    break;
            }
        }
        public override void StopPreview(DoublageBattleParameter doublageBattleParameter)
        {
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    doublageBattleParameter.VoiceActors[0].StatModifier.Substract(emotionStat);
                    doublageBattleParameter.ActorsManager.DrawActorStat(doublageBattleParameter.VoiceActors[0], doublageBattleParameter.Cards);
                    break;
            }
        }


        //    A C T O R

        /*public void AddActorStat(ActorsManager actorsManager, Buff buff)
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
                card.AddStatPercentage(-emotionStat.GetEmotion((int)card.GetEmotion()));
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
        }*/

    } // SkillEffectEmotionStat class
	
}// #PROJECTNAME# namespace
