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

        private int producerMPMax = 0;
        private int producerMP = 0;
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

        public void SetManagers(SkillManager manager, int mp)
        {
            skillManager = manager;
            producerMPMax = mp;
            producerMP = mp;
        }

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
            skillManager.ApplySkill(currentAttack);
            producerPanel.SetTrigger("Cancel");
            input.gameObject.SetActive(false);
        }

        public bool ProducerDecision(EnemyAI[] producerAI, string phase, int line, int turn, float enemyHP)
        {
            GainMP(1);
            for (int i = 0; i < producerAI.Length; i++)
            {
                if (producerAI[i].CheckEnemyAI(phase, line, turn, enemyHP))
                {
                    currentAttack = producerAI[i].Skills[Random.Range(0, producerAI[i].Skills.Length)];
                    if (currentAttack != null)
                    {
                        readyToAttack = true;
                        if (CheckMP(currentAttack.ProducerCost) == false)
                        {
                            readyToAttack = false;
                        }
                    }
                    else
                    {
                        readyToAttack = false;
                    }

                    return readyToAttack;
                }
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





        public void GainMP(int mpGain)
        {
            producerMP += mpGain;
            if(producerMP < 0)
            {
                producerMP = 0;
            }
            else if (producerMP > producerMPMax)
            {
                producerMP = producerMPMax;
            }
        }

        public bool CheckMP(int mpCost)
        {
            if(mpCost > producerMP)
            {
                return false;
            }
            producerMP -= mpCost;
            return true;
        }


        #endregion

    } // ProducerManager class
	
}// #PROJECTNAME# namespace
