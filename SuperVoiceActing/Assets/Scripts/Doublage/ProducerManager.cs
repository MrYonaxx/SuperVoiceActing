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
	public class ProducerManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Animator producerPanel;


        [SerializeField]
        InputController input;

        private bool readyToAttack = false;
        private SkillData currentAttack = null;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void ProducerDecision(EnemyAI[] producerAI, string phase)
        {
            for(int i = 0; i < producerAI.Length; i++)
            {
                if (CheckPhase(producerAI[i], phase) == true)
                {
                    if (CheckAICondition(producerAI[i].Conditions) == true)
                    {
                        currentAttack = producerAI[i].Skills[Random.Range(0, producerAI[i].Skills.Length)];
                        if (currentAttack != null)
                            readyToAttack = true;
                        return;
                    }
                }
            }

        }


        private bool CheckPhase(EnemyAI producerAI, string phase)
        {
            switch (phase)
            {
                case "STARTPHRASE":
                    if (producerAI.StartPhrase == true)
                        return true;
                    break;
                case "STARTATTACK":
                    if (producerAI.StartAttack == true)
                        return true;
                    break;
                case "ENDATTACK":
                    if (producerAI.EndAttack == true)
                        return true;
                    break;
                case "ENDPHRASE":
                    if (producerAI.EndPhrase == true)
                        return true;
                    break;
            }
            return false;
        }

        private bool CheckAICondition(ConditionAI[] conditionAI)
        {
            for(int i = 0; i < conditionAI.Length; i++)
            {
                switch(conditionAI[i].Condition)
                {
                    case AICondition.None:
                        break;
                    case AICondition.Line:
                        if (CheckVariableCondition(conditionAI[i].MathOperator, 0, 0) == false)
                            return false;
                        break;
                    case AICondition.Turn:
                        break;
                }
            }
            return true;
        }


        private bool CheckVariableCondition(MathOperator mathOperator, float variable, float value)
        {
            switch(mathOperator)
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



        public void RefuteProducer()
        {
            input.gameObject.SetActive(true);
        }

        // Appelé via l'animator enemyAttackFace
        public void ActivateInput()
        {
            input.gameObject.SetActive(true);
        }

        #endregion

    } // ProducerManager class
	
}// #PROJECTNAME# namespace
