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
        Sentence,
        Role,
        Producer,
        ManualPackSelection
    }

    [System.Serializable]
    public class BuffData
    {
        [HorizontalGroup("buffType", LabelWidth = 100)]
        [SerializeField]
        private int turnActive;
        public int TurnActive
        {
            get { return turnActive; }
        }

        [HorizontalGroup("buffType")]
        [SerializeField]
        private bool refresh;
        public bool Refresh
        {
            get { return refresh; }
        }

        [HorizontalGroup("buffType")]
        [SerializeField]
        private bool addBuffTurn;
        public bool AddBuffTurn
        {
            get { return addBuffTurn; }
        }

        [HorizontalGroup("buffType")]
        [SerializeField]
        private bool canAddMultiple;
        public bool CanAddMultiple
        {
            get { return canAddMultiple; }
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
        [HorizontalGroup]
        [HideLabel]
        private SkillType skillType;
        public SkillType SkillType
        {
            get { return skillType; }
        }

        [Space]
        [Space]
        [Title("Target")]
        [SerializeField]
        [HorizontalGroup]
        [HideLabel]
        private SkillTarget skillTarget;
        public SkillTarget SkillTarget
        {
            get { return skillTarget; }
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



        [Space]
        [Space]

        [SerializeField]
        private int producerCost;
        public int ProducerCost
        {
            get { return producerCost; }
        }


        public void ApplySkillsEffects(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            for(int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ApplySkillEffect(this.skillTarget, actorsManager, enemyManager, doublageManager);
            }
        }

        public void ApplyBuffsEffects(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ApplySkillEffect(this.skillTarget, actorsManager, enemyManager, doublageManager, buffData);
            }
        }

        public void ManualTarget(Emotion emotion)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().ManualTarget(emotion);
            }
        }
        /*public void RemoveSkillsEffects(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                skillEffects[i].GetSkillEffectNode().RemoveSkillEffect(this.skillTarget, actorsManager, enemyManager, doublageManager);
            }
        }*/


    } // SkillData class

} // #PROJECTNAME# namespace