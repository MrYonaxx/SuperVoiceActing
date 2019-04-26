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
    public class DoublageEventSkill : DoublageEvent
    {

        [HorizontalGroup("Hey", LabelWidth = 80)]
        [SerializeField]
        private SkillData skill;
        public SkillData Skill
        {
            get { return skill; }
        }

        [HorizontalGroup("Hey")]
        [SerializeField]
        private bool showAnim;
        public bool ShowAnim
        {
            get { return showAnim; }
        }


    } // DoublageEventSkill class
	
}// #PROJECTNAME# namespace
