/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{


    public enum SkillType
    {
        Instant,
        Buff,
        Special
    }

    public enum SkillTarget
    {
        VoiceActor,
        VoiceActors,
        Role,
        Roles,
        Sentence,
        Producer,
        Cards
    }

    [System.Serializable]
    public class BuffData
    {

        [HorizontalGroup("Name", LabelWidth = 100)]
        [SerializeField]
        private string buffName;
        public string BuffName
        {
            get { return buffName; }
        }

        [HorizontalGroup("Name")]
        [SerializeField]
        private bool infinite;
        public bool Infinite
        {
            get { return infinite; }
        }

        [HorizontalGroup("Name")]
        [SerializeField]
        private bool tickEffect;
        public bool TickEffect
        {
            get { return tickEffect; }
        }

        [HorizontalGroup("Name")]
        [SerializeField]
        private bool endEffect;
        public bool EndEffect
        {
            get { return endEffect; }
        }

        [HideIf("infinite")]
        [HorizontalGroup("buffType", LabelWidth = 100)]
        [SerializeField]
        private int turnActive;
        public int TurnActive
        {
            get { return turnActive; }
        }

        [HideIf("infinite")]
        [HorizontalGroup("buffType")]
        [SerializeField]
        private bool refresh;
        public bool Refresh
        {
            get { return refresh; }
        }

        [HideIf("infinite")]
        [HorizontalGroup("buffType")]
        [SerializeField]
        private bool addBuffTurn;
        public bool AddBuffTurn
        {
            get { return addBuffTurn; }
        }


        [HideIf("infinite")]
        [HorizontalGroup("buffType")]
        [SerializeField]
        private bool lineCount;
        public bool LineCount
        {
            get { return lineCount; }
        }


        [SerializeField]
        private bool canAddMultiple;
        public bool CanAddMultiple
        {
            get { return canAddMultiple; }
        }

        [SerializeField]
        private bool buffAll;
        public bool BuffAll
        {
            get { return buffAll; }
        }

        [SerializeField]
        private bool conditionRemove;
        public bool ConditionRemove
        {
            get { return conditionRemove; }
        }

    }


    /// <summary>
    /// Definition of the SkillData class
    /// </summary>
    [CreateAssetMenu(fileName = "SkillData", menuName = "Skills/SkillData", order = 1)]
    public class SkillData : ScriptableObject
    {
        [Title("Info")]
        [SerializeField]
        private string skillName;
        public string SkillName
        {
            get { return skillName; }
        }

        [TextArea]
        [SerializeField]
        private string description;
        public string Description
        {
            get { return description; }
        }

        [SerializeField]
        private string descriptionBattle;
        public string DescriptionBattle
        {
            get { return descriptionBattle; }
        }



        [Space]
        [Space]


        [Title("Type")]     
        [SerializeField]
        [HideLabel]
        private SkillType skillType;
        public SkillType SkillType
        {
            get { return skillType; }
        }

        [Space]
        [Space]
        [ShowIf("skillType", SkillType.Buff)]
        [HideLabel]
        [SerializeField]
        private BuffData buffData;
        public BuffData BuffData
        {
            get { return buffData; }
        }

        [Space]
        [Space]
        [SerializeField]
        private SkillEffectNode[] skillEffects;
        public SkillEffectNode[] SkillEffects
        {
            get { return skillEffects; }
        }
        [SerializeField]
        private SkillData[] bonusSkillEffects;
        public SkillData[] BonusSkillEffects
        {
            get { return bonusSkillEffects; }
        }



        [Space]
        [Space]

        [SerializeField]
        private int producerCost;
        public int ProducerCost
        {
            get { return producerCost; }
        }




        public void ApplySkill(DoublageBattleParameter doublageBattleParameter)
        {
            if (skillType == SkillType.Buff)
            {
                if (buffData.BuffAll)
                {
                    for (int i = 0; i < doublageBattleParameter.VoiceActors.Count; i++)
                    {
                        AddBuff(doublageBattleParameter.VoiceActors[i].Buffs, this);
                        //doublageBattleParameter.VoiceActors[i].Buffs.Add(new Buff(this));
                    }
                }
                else
                {
                    AddBuff(doublageBattleParameter.VoiceActors[doublageBattleParameter.IndexCurrentCharacter].Buffs, this);
                    //doublageBattleParameter.VoiceActors[doublageBattleParameter.IndexCurrentCharacter].Buffs.Add(new Buff(this));
                }
                if (buffData.TickEffect == false && buffData.EndEffect == false)
                    ApplySkillEffects(doublageBattleParameter);
            }
            else
            {
                ApplySkillEffects(doublageBattleParameter);
            }
        }


        public void ApplySkillEffects(DoublageBattleParameter doublageBattleParameter)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ApplySkillEffect(doublageBattleParameter);
            }
            for (int i = 0; i < bonusSkillEffects.Length; i++)
            {
                bonusSkillEffects[i].ApplySkill(doublageBattleParameter);
            }
        }

        private void AddBuff(List<Buff> listBuffs, SkillData skill)
        {
            if(skill.BuffData.CanAddMultiple == true)
            {
                listBuffs.Add(new Buff(skill));
                return;
            }
            for(int i = 0; i < listBuffs.Count; i++)
            {
                if (listBuffs[i].SkillData == skill)
                {
                    if (skill.BuffData.Refresh == true)
                    {
                        listBuffs[i].Turn = skill.BuffData.TurnActive;
                    }
                    else if (skill.BuffData.AddBuffTurn == true)
                    {
                        listBuffs[i].Turn += skill.BuffData.TurnActive;
                    }
                    return;
                }
            }
            listBuffs.Add(new Buff(skill));
        }


        public void RemoveSkill(DoublageBattleParameter doublageBattleParameter)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().RemoveSkillEffect(doublageBattleParameter);
            }
        }


        /*public void ManualTarget(Emotion[] emotion, bool selectionByPack)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ManualTarget(emotion, selectionByPack);
            }
        }*/


        public void PreviewSkill(DoublageBattleParameter doublageBattleParameter)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().PreviewSkill(doublageBattleParameter);
            }
        }

        public void StopPreview(DoublageBattleParameter doublageBattleParameter)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().StopPreview(doublageBattleParameter);
            }
        }


    } // SkillData class

} // #PROJECTNAME# namespace