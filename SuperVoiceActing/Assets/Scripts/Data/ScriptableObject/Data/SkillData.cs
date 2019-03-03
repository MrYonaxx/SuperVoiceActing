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
    /// <summary>
    /// Definition of the SkillData class
    /// </summary>
    [CreateAssetMenu(fileName = "SkillData", menuName = "SkillData", order = 1)]
    public class SkillData : ScriptableObject
    {
        [Header("Info")]
        [SerializeField]
        private string skillName;
        public string SkillName
        {
            get { return skillName; }
        }

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

        [Header("Conditions d'Activation")]

        [SerializeField]
        private bool afterAttack;
        public bool AfterAttack
        {
            get { return afterAttack; }
        }

        [SerializeField]
        private bool afterCritical;
        public bool AfterCritical
        {
            get { return afterCritical; }
        }

        [SerializeField]
        private bool afterKill;
        public bool AfterKill
        {
            get { return afterKill; }
        }

        [SerializeField]
        private EmotionStat phraseType;
        public EmotionStat PhraseType
        {
            get { return phraseType; }
        }

        [MinMaxSlider(0, 100)]
        [SerializeField]
        private Vector2Int hpInterval = new Vector2Int(0,100);
        public Vector2Int HpInterval
        {
            get { return hpInterval; }
        }

        [SerializeField]
        private float percentageActivation;
        public float PercentageActivation
        {
            get { return percentageActivation; }
        }

        [Header("Effet")]
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
        private bool addTurn;
        public bool AddTurn
        {
            get { return addTurn; }
        }

        [SerializeField]
        private EmotionStat cardGain;
        public EmotionStat CardGain
        {
            get { return cardGain; }
        }

        [SerializeField]
        private EmotionStat emotionStatGainPercentage;
        public EmotionStat EmotionStatGainPercentage
        {
            get { return emotionStatGainPercentage; }
        }

        [SerializeField]
        private EmotionStat emotionStatGainFlat;
        public EmotionStat EmotionStatGainFlat
        {
            get { return emotionStatGainFlat; }
        }

        [SerializeField]
        private float emotionStatVariance;
        public float EmotionStatVariance
        {
            get { return emotionStatVariance; }
        }

        [HorizontalGroup("hpPercentage")]
        [SerializeField]
        private float hpGainPercentage;
        public float HpGainPercentage
        {
            get { return hpGainPercentage; }
        }

        [HorizontalGroup("hpPercentage")]
        [SerializeField]
        private float hpPercentageVariance;
        public float HpPercentageVariance
        {
            get { return hpPercentageVariance; }
        }

        [HorizontalGroup("hpFlat")]
        [SerializeField]
        private float hpGainFlat;
        public float HpGainFlat
        {
            get { return hpGainFlat; }
        }

        [HorizontalGroup("hpFlat")]
        [SerializeField]
        private float hpFlatVariance;
        public float HpFlatVariance
        {
            get { return hpFlatVariance; }
        }

        [HorizontalGroup("turnGain")]
        [SerializeField]
        private float turnGain;
        public float TurnGain
        {
            get { return turnGain; }
        }

        [HorizontalGroup("turnGain")]
        [SerializeField]
        private float turnGainVariance;
        public float TurnGainVariance
        {
            get { return turnGainVariance; }
        }



    } // SkillData class

} // #PROJECTNAME# namespace