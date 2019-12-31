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

        [ShowIf("isPassive")]
        [SerializeField]
        private SkillActiveTiming activationTiming;
        public SkillActiveTiming ActivationTiming
        {
            get { return activationTiming; }
        }

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
        private bool onlyOnce;
        public bool OnlyOnce
        {
            get { return onlyOnce; }
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


        public bool CheckConditions(DoublageBattleParameter battleParameter)
        {
            for(int i = 0; i < skillConditions.Length; i++)
            {
                if (skillConditions[i].GetSkillConditionNode().CheckCondition(battleParameter) == false)
                    return false;
            }
            return true;
        }



    } // SkillActorData class
	
}// #PROJECTNAME# namespace
