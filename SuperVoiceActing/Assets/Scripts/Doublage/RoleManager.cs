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
	public class RoleManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("FeedbacksAttack")]
        [SerializeField]
        Animator spotEnemy;
        [SerializeField]
        Animator enemyAttack;
        [SerializeField]
        Animator enemyAttackFace;

        [Header("InfoAttack")]
        [SerializeField]
        TextMeshProUGUI textSkillName;
        [SerializeField]
        TextMeshProUGUI textSkillDescription;
        [SerializeField]
        TextMeshProUGUI textSkillDescriptionBattle;

        [SerializeField]
        InputController input;

        bool firstTime = false;
        bool readyToAttack = false;

        List<Role> roles;

        int indexCurrentRole = 0;
        private SkillData currentAttack = null;
        private SkillManager skillManager = null;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetRoleAttack()
        {
            return roles[indexCurrentRole].Attack;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void SetManagers(SkillManager manager)
        {
            skillManager = manager;
        }

        public void SetRoles(List<Role> contractRoles)
        {
            roles = contractRoles;
        }

        public void AddScorePerformance(int lastAttackScore, int lastBestScore)
        {
            roles[indexCurrentRole].RolePerformance += lastAttackScore;
            roles[indexCurrentRole].RoleBestScore += lastBestScore;
        }








        public bool IsAttacking()
        {
            return readyToAttack;
        }


        public bool RoleDecision(string phase, int line, int turn, float enemyHP)
        {
            for (int i = 0; i < roles[indexCurrentRole].RoleAI.Length; i++)
            {
                if (roles[indexCurrentRole].RoleAI[i].CheckEnemyAI(phase, line, turn, enemyHP))
                {
                    currentAttack = roles[indexCurrentRole].RoleAI[i].Skills[Random.Range(0, roles[indexCurrentRole].RoleAI[i].Skills.Length)];
                    if (currentAttack != null)
                    {
                        readyToAttack = true;
                        ActivateSpot();
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











        private void ActivateSpot()
        {
            if (firstTime == false)
            {
                spotEnemy.gameObject.SetActive(true);
            }
            spotEnemy.SetBool("Appear", true);
        }

        public void EnemyAttackActivation()
        {
            readyToAttack = false;
            if (firstTime == false)
            {
                enemyAttack.gameObject.SetActive(true);
                enemyAttackFace.gameObject.SetActive(true);
                firstTime = true;
            }
            enemyAttack.SetBool("Appear", true);
            enemyAttackFace.SetBool("Appear", true);
            textSkillName.text = currentAttack.SkillName;
            textSkillDescription.text = currentAttack.Description;
            textSkillDescriptionBattle.text = currentAttack.DescriptionBattle;
            //StartCoroutine(WaitInput(60));
        }

        private IEnumerator WaitInput(int time = 30)
        {
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            ActivateInput();
        }

        // Appelé via l'animator enemyAttackFace
        public void ActivateInput()
        {
            input.gameObject.SetActive(true);
        }


        public void StopEnemyActivation()
        {

            skillManager.ApplySkill(currentAttack);
            input.gameObject.SetActive(false);
            enemyAttack.SetBool("Appear", false);
            enemyAttackFace.SetBool("Appear", false);
            spotEnemy.SetBool("Appear", false);
        }

        #endregion

    } // RoleManager class
	
}// #PROJECTNAME# namespace
