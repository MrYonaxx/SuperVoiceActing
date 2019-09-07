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
    public enum SkillActiveTiming
    {
        AfterStart,
        AfterAttack,
        AfterCritical,
        AfterKill
    }

    [CreateAssetMenu(fileName = "SkillActorData", menuName = "Skills/SkillActorData", order = 1)]
    public class SkillActorData : SkillData
	{



        [Title("Conditions d'Activation")]

        [SerializeField]
        private SkillActiveTiming activationTiming;
        public SkillActiveTiming ActivationTiming
        {
            get { return activationTiming; }
        }

        /*private bool afterStart;
        public bool AfterStart
        {
            get { return afterStart; }
        }

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
        }*/
        [SerializeField]
        private bool onlyWhenMain = true;
        public bool OnlyWhenMain
        {
            get { return onlyWhenMain; }
        }

        [HideLabel]
        [SerializeField]
        private EmotionStat phraseType;
        public EmotionStat PhraseType
        {
            get { return phraseType; }
        }

        [MinMaxSlider(0, 100)]
        [SerializeField]
        private Vector2Int hpInterval = new Vector2Int(0, 100);
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

        [SerializeField]
        private bool onlyOnce;
        public bool OnlyOnce
        {
            get { return onlyOnce; }
        }

        [Space]
        [Space]
        [Space]
        [Title("Animation")]
        [SerializeField]
        private bool bigAnimation = true;
        public bool BigAnimation
        {
            get { return bigAnimation; }
        }
        [SerializeField]
        private bool isMalus;
        public bool IsMalus
        {
            get { return isMalus; }
        }






    } // SkillActorData class
	
}// #PROJECTNAME# namespace
