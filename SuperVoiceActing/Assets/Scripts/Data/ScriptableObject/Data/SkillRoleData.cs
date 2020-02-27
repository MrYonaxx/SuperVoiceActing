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
    [CreateAssetMenu(fileName = "SkillData", menuName = "Skills/SkillRoleData", order = 1)]
    public class SkillRoleData : SkillData
	{
        [Title("Influence Data")]
        [SerializeField]
        private bool canCounter = true;
        public bool CanCounter
        {
            get { return canCounter; }
        }
        [HideLabel]
        [SerializeField]
        private EmotionStat counterSkill;
        public EmotionStat CounterSkill
        {
            get { return counterSkill; }
        }


    } // SkillRoleData class
	
}// #PROJECTNAME# namespace
