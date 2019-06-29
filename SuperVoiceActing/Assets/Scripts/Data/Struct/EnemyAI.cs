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

    public enum AICondition
    {
        None,
        Turn,
        Line,
        VoiceActorHP,
        EnemyHP,
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
        [Title("PATTERN")]
        [SerializeField]
        [HideLabel]
        private string memo;

        [HorizontalGroup("Attack Timing", LabelWidth = 80)]
        [SerializeField]
        private bool startPhrase;
        public bool StartPhrase
        {
            get { return startPhrase; }
        }
        [HorizontalGroup("Attack Timing")]
        [SerializeField]
        private bool startAttack;
        public bool StartAttack
        {
            get { return startAttack; }
        }
        [HorizontalGroup("Attack Timing")]
        [SerializeField]
        private bool endAttack;
        public bool EndAttack
        {
            get { return endAttack; }
        }
        [HorizontalGroup("Attack Timing")]
        [SerializeField]
        private bool endPhrase;
        public bool EndPhrase
        {
            get { return endPhrase; }
        }

        [HorizontalGroup("Condition")]
        [SerializeField]
        private ConditionAI[] conditions;
        public ConditionAI[] Conditions
        {
            get { return conditions; }
        }

        [HorizontalGroup("Condition")]
        [SerializeField]
        private SkillData[] skills;
        public SkillData[] Skills
        {
            get { return skills; }
        }




        public bool CheckEnemyAI(string phase, int line, int turn, float enemyHP)
        {
            if (CheckPhase(phase) == true)
            {
                if (CheckAICondition(line, turn, enemyHP) == true)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckPhase(string phase)
        {
            switch (phase)
            {
                case "STARTPHRASE":
                    if (startPhrase == true)
                        return true;
                    break;
                case "STARTATTACK":
                    if (startAttack == true)
                        return true;
                    break;
                case "ENDATTACK":
                    if (endAttack == true)
                        return true;
                    break;
                case "ENDPHRASE":
                    if (endPhrase == true)
                        return true;
                    break;
            }
            return false;
        }

        private bool CheckAICondition(int line, int turn, float enemyHP)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                switch (conditions[i].Condition)
                {
                    case AICondition.None:
                        break;
                    case AICondition.Line:
                        if (CheckVariableCondition(conditions[i].MathOperator, line, conditions[i].ConditionValue) == false)
                            return false;
                        break;
                    case AICondition.Turn:
                        if (CheckVariableCondition(conditions[i].MathOperator, turn, conditions[i].ConditionValue) == false)
                            return false;
                        break;
                    case AICondition.EnemyHP:
                        if (CheckVariableCondition(conditions[i].MathOperator, enemyHP, conditions[i].ConditionValue) == false)
                            return false;
                        break;
                }
            }
            return true;
        }


        private bool CheckVariableCondition(MathOperator mathOperator, float variable, float value)
        {
            switch (mathOperator)
            {
                case MathOperator.Equal:
                    return (variable == value);
                case MathOperator.Less:
                    return (variable < value);
                case MathOperator.LessEqual:
                    return (variable <= value);
                case MathOperator.More:
                    return (variable > value);
                case MathOperator.MoreEqual:
                    return (variable >= value);
                case MathOperator.Not:
                    return (variable != value);
            }
            return false;
        }





    } // EnemyAI class
	
}// #PROJECTNAME# namespace
