/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
    [System.Serializable]
    public class DoublageEventSkill : DoublageEvent
    {
        [SerializeField]
        private SkillData skill;
        public SkillData Skill
        {
            get { return skill; }
        }


    } // DoublageEventSkill class
	
}// #PROJECTNAME# namespace
