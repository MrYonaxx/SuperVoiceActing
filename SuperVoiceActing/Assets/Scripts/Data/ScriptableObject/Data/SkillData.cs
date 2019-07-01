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

        /*[SerializeField]
        [Title("Card Gain")]
        private EmotionStat cardGain;
        public EmotionStat CardGain
        {
            get { return cardGain; }
        }

        [Space]

        [Title("Emotion Stat Gain")]

        [SerializeField]
        private EmotionStat emotionStatGainPercentage;
        public EmotionStat EmotionStatGainPercentage
        {
            get { return emotionStatGainPercentage; }
        }

        [Space]
        [SerializeField]
        private EmotionStat emotionStatGainFlat;
        public EmotionStat EmotionStatGainFlat
        {
            get { return emotionStatGainFlat; }
        }



        [Space]
        [SerializeField]
        private float emotionStatVariance;
        public float EmotionStatVariance
        {
            get { return emotionStatVariance; }
        }


        [Space]

        [Title("Emotion Damage")]

        [SerializeField]
        private EmotionStat emotionDamagePercentage;
        public EmotionStat EmotionDamagePercentage
        {
            get { return emotionDamagePercentage; }
        }

        [Space]
        [SerializeField]
        private EmotionStat emotionDamageFlat;
        public EmotionStat EmotionDamageFlat
        {
            get { return emotionDamageFlat; }
        }

        [Space]
        [SerializeField]
        private float emotionDamageVariance;
        public float EmotionDamageVariance
        {
            get { return emotionStatVariance; }
        }






        [Space]
        [Space]

        [Title("HP Damage")]

        [HorizontalGroup("HPPercentage")]
        [SerializeField]
        private float hpGainPercentage;
        public float HpGainPercentage
        {
            get { return hpGainPercentage; }
        }

        [Space]
        [Space]

        [Title("Variance")]
        [HorizontalGroup("HPPercentage")]
        [SerializeField]
        private float hpPercentageVariance;
        public float HpPercentageVariance
        {
            get { return hpPercentageVariance; }
        }

        [HorizontalGroup("HPFlat")]
        [SerializeField]
        private float hpGainFlat;
        public float HpGainFlat
        {
            get { return hpGainFlat; }
        }
        [HorizontalGroup("HPFlat")]
        [SerializeField]
        private float hpFlatVariance;
        public float HpFlatVariance
        {
            get { return hpFlatVariance; }
        }


        [Title("Other")]
        [SerializeField]
        private int turnGain;
        public int TurnGain
        {
            get { return turnGain; }
        }

        [SerializeField]
        private float turnGainVariance;
        public float TurnGainVariance
        {
            get { return turnGainVariance; }
        }*/

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