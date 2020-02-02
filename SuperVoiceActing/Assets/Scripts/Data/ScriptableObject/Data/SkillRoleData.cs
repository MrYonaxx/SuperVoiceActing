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
        [HideLabel]
        [SerializeField]
        private EmotionStat counterSkill;
        public EmotionStat CounterSkill
        {
            get { return counterSkill; }
        }


        [SerializeField]
        private bool canCounter = true;
        public bool CanCounter
        {
            get { return canCounter; }
        }

        [SerializeField]
        private int influenceMultiplier = 1;
        public int InfluenceMultiplier
        {
            get { return influenceMultiplier; }
        }

        [SerializeField]
        private int influenceRandom = 0;
        public int InfluenceRandom
        {
            get { return influenceRandom; }
        }

        [InfoBox("Overwrite role influence value if not -1")]
        [SerializeField]
        private int influenceValue = -1;
        public int InfluenceValue
        {
            get { return influenceValue; }
        }




    } // SkillRoleData class
	
}// #PROJECTNAME# namespace
