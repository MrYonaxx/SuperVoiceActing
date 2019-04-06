﻿/*****************************************************************
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

    public enum AICondition
    {
        None,
        Turn,
        Line,
        VoiceActorHP,
        Buff,
    }

    public enum MathOperator
    {
        Equal,
        Less,
        More,
        LessEqual,
        MoreEqual,
        Not
    }

    [System.Serializable]
    public class ConditionAI
    {
        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        private AICondition condition;
        public AICondition Condition
        {
            get { return condition; }
        }
        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        private MathOperator mathOperator;
        public MathOperator MathOperator
        {
            get { return mathOperator; }
        }
        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        private int conditionValue;
        public int ConditionValue
        {
            get { return conditionValue; }
        }
    }

    [System.Serializable]
	public class EnemyAI
	{
        [Header("PATTERN")]
        [SerializeField]
        [HideLabel]
        private string memo;

        [HorizontalGroup]
        [SerializeField]
        private ConditionAI[] conditions;
        public ConditionAI[] Conditions
        {
            get { return conditions; }
        }

        [HorizontalGroup]
        [SerializeField]
        private SkillData[] skills;
        public SkillData[] Skills
        {
            get { return skills; }
        }



    } // EnemyAI class
	
}// #PROJECTNAME# namespace
