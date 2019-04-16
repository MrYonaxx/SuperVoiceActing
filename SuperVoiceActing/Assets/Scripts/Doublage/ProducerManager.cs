/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        TextMeshProUGUI skillName;


        [SerializeField]
        InputController input;

        private bool readyToAttack = false;
        private SkillData currentAttack = null;
        private SkillManager skillManager;

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

        public void ProducerAttackActivation()
        {
            //producerPanel.gameObject.SetActive(true);
            producerPanel.SetTrigger("Appear");
            input.gameObject.SetActive(true);
            skillName.text = currentAttack.SkillName;
        }

        public void ProducerAttackCounter()
        {

        }

        public void ProducerAttackDisappear()
        {
            //skillManager.ApplySkill(currentAttack);
            producerPanel.SetTrigger("Cancel");
            input.gameObject.SetActive(false);
        }

        public bool ProducerDecision(EnemyAI[] producerAI, string phase, int line, int turn, float enemyHP)
        {
            for(int i = 0; i < producerAI.Length; i++)
            {
                if (CheckPhase(producerAI[i], phase) == true)
                {
                    if (CheckAICondition(producerAI[i].Conditions, line, turn, enemyHP) == true)
                    {
                        currentAttack = producerAI[i].Skills[Random.Range(0, producerAI[i].Skills.Length)];
                        if (currentAttack != null)
                            readyToAttack = true;
                        else
                            readyToAttack = false;
                        return readyToAttack;
                    }
                }
            }
            return false;
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

        private bool CheckAICondition(ConditionAI[] conditionAI, int line, int turn, float enemyHP)
        {
            for(int i = 0; i < conditionAI.Length; i++)
            {
                switch(conditionAI[i].Condition)
                {
                    case AICondition.None:
                        break;
                    case AICondition.Line:
                        if (CheckVariableCondition(conditionAI[i].MathOperator, line, conditionAI[i].ConditionValue) == false)
                            return false;
                        break;
                    case AICondition.Turn:
                        if (CheckVariableCondition(conditionAI[i].MathOperator, turn, conditionAI[i].ConditionValue) == false)
                            return false;
                        break;
                    case AICondition.EnemyHP:
                        if (CheckVariableCondition(conditionAI[i].MathOperator, enemyHP, conditionAI[i].ConditionValue) == false)
                            return false;
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
