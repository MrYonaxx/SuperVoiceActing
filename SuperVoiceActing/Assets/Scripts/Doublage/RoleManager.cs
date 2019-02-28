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

        [SerializeField]
        InputController input;

        bool firstTime = false;
        bool attack = false;

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

        public bool IsAttacking()
        {
            return attack;
        }

        public void SelectAttack()
        {
            attack = true;
            ActivateSpot();
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
            attack = false;
            if (firstTime == false)
            {
                enemyAttack.gameObject.SetActive(true);
                enemyAttackFace.gameObject.SetActive(true);
                firstTime = true;
            }
            enemyAttack.SetBool("Appear", true);
            enemyAttackFace.SetBool("Appear", true);
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
            input.gameObject.SetActive(false);
            enemyAttack.SetBool("Appear", false);
            enemyAttackFace.SetBool("Appear", false);
            spotEnemy.SetBool("Appear", false);
        }

        #endregion

    } // RoleManager class
	
}// #PROJECTNAME# namespace
