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
        AfterKill,
        AfterCardSelection,
        AfterSwitch
    }

    [CreateAssetMenu(fileName = "SkillActorData", menuName = "Skills/SkillActorData", order = 1)]
    public class SkillActorData : SkillData
	{


        [Title("SkillType")]
        [SerializeField]
        private bool isPassive = false;
        public bool IsPassive
        {
            get { return isPassive; }
        }

        [HideIf("isPassive")]
        [HideLabel]
        [SerializeField]
        private EmotionStat movelist;
        public EmotionStat Movelist
        {
            get { return movelist; }
        }

        /*[ShowIf("isPassive")]
        [SerializeField]
        private SkillActiveTiming activationTiming;
        public SkillActiveTiming ActivationTiming
        {
            get { return activationTiming; }
        }*/

        [ShowIf("isPassive")]
        [HorizontalGroup("Group1")]
        [SerializeField]
        private bool onlyWhenMain = true;
        public bool OnlyWhenMain
        {
            get { return onlyWhenMain; }
        }
        [ShowIf("isPassive")]
        [HorizontalGroup("Group1")]
        [SerializeField]
        private bool onlyWhenSupport = true;
        public bool OnlyWhenSupport
        {
            get { return onlyWhenSupport; }
        }

        [ShowIf("isPassive")]
        [SerializeField]
        private bool onlyOnce;
        public bool OnlyOnce
        {
            get { return onlyOnce; }
        }
        [ShowIf("onlyOnce")]
        [HorizontalGroup("Group2")]
        [SerializeField]
        private bool onlyOncePerLine;
        public bool OnlyOncePerLine
        {
            get { return onlyOncePerLine; }
        }
        [ShowIf("onlyOnce")]
        [HorizontalGroup("Group2")]
        [SerializeField]
        private bool onlyOncePerSwitch;
        public bool OnlyOncePerSwitch
        {
            get { return onlyOncePerSwitch; }
        }

        [ShowIf("isPassive")]
        [SerializeField]
        private SkillConditionNode[] skillConditions;
        public SkillConditionNode[] SkillConditions
        {
            get { return skillConditions; }
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
