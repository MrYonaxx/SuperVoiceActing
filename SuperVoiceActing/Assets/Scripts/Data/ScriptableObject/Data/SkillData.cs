/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
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
                //AddBuff(ITarget);
            }
            if(buffData.TickEffect == false && buffData.EndEffect == false)
            {
                ApplySkillEffects(doublageBattleParameter);
            }
            for(int i = 0; i < bonusSkillEffects.Length; i++)
            {
                bonusSkillEffects[i].ApplySkill(doublageBattleParameter);
            }
        }


        private void ApplySkillEffects(DoublageBattleParameter doublageBattleParameter)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ApplySkillEffect(doublageBattleParameter);
            }
        }


        /*public void RemoveSkill(DoublageManager doublageManager)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().RemoveSkillEffect(doublageManager);
            }
        }


        public void ManualTarget(Emotion[] emotion, bool selectionByPack)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ManualTarget(emotion, selectionByPack);
            }
        }


        public void PreviewTarget(DoublageManager doublageManager)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().PreviewTarget(doublageManager);
            }
        }

        public void StopPreview(DoublageManager doublageManager)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().StopPreview(doublageManager);
            }
        }*/


    } // SkillData class

} // #PROJECTNAME# namespace