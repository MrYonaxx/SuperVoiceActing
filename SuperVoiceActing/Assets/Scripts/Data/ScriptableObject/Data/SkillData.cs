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
    [CreateAssetMenu(fileName = "SkillData", menuName = "Skills/SkillData", order = 1)]
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
        private bool addBuffTurn;
        public bool AddBuffTurn
        {
            get { return addBuffTurn; }
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