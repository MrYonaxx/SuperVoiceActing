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
        GameObject spotEnemy;
        [SerializeField]
        GameObject enemyAttack;

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

        public void ActivateSpot()
        {
            spotEnemy.SetActive(true);
        }

        public void EnemyAttackActivation()
        {
            enemyAttack.SetActive(true);
        }

        #endregion

    } // RoleManager class
	
}// #PROJECTNAME# namespace
