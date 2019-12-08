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
    public class MenuContractDifficultyCurve: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Difficulty HP")] //  ======================================== ======================================== ========================================
        [SerializeField]
        [OnValueChanged("CalculateDebugHP")]
        int baseHP;

        [HorizontalGroup]
        [SerializeField]
        [ReadOnly]
        int[] level;
        [HorizontalGroup]
        [SerializeField]
        int[] hpBonus;
        [HorizontalGroup]
        [SerializeField]
        int[] hpDebug;

        // + Point de recherche bonus par tour restant
        [Title("Experience")] //  ======================================== ======================================== ========================================
        [SerializeField]
        [OnValueChanged("CalculateDebugEXP")]
        float expRandomValueRatio;

        [HorizontalGroup("EXP")]
        [SerializeField]
        [ReadOnly]
        int[] expLevel;
        [HorizontalGroup("EXP")]
        [SerializeField]
        int[] expBonus;
        [HorizontalGroup("EXP")]
        [SerializeField]
        int[] expRandomValue;

        [Title("Atk")]
        [SerializeField]
        [OnValueChanged("CalculateDebugAtk")]
        int baseAtk;

        [HorizontalGroup("Atk")]
        [SerializeField]
        [ReadOnly]
        int[] atkLevel;
        [HorizontalGroup("Atk")]
        [SerializeField]
        int[] atkBonus;
        [HorizontalGroup("Atk")]
        [SerializeField]
        int[] atkBonusDebug;

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


        private void CalculateDebugHP()
        {
            hpDebug = new int[hpBonus.Length];
            level = new int[hpBonus.Length];
            int bonus = baseHP;
            for (int i = 0; i < hpBonus.Length; i++)
            {
                bonus += hpBonus[i];
                hpDebug[i] = bonus;
                level[i] = i;
            }
        }

        private void CalculateDebugEXP()
        {
            expRandomValue = new int[expBonus.Length];
            expLevel = new int[expBonus.Length];
            for (int i = 0; i < expBonus.Length; i++)
            {
                expRandomValue[i] = (int)(expBonus[i] * expRandomValueRatio);
                expLevel[i] = i;
            }
        }

        private void CalculateDebugAtk()
        {
            atkBonusDebug = new int[atkBonus.Length];
            atkLevel = new int[atkBonus.Length];
            int bonus = baseAtk;
            for (int i = 0; i < atkBonus.Length; i++)
            {
                bonus += atkBonus[i];
                atkBonusDebug[i] = bonus;
                atkLevel[i] = i;
            }
        }





        public void EquilibrateContract(Contract c, int targetLevel)
        {
            int oldLevel = c.Level;
            c.Level = targetLevel;


            // Equilibrate Exp ==========================================
            c.ExpGain = expBonus[targetLevel] + Random.Range(-expRandomValue[targetLevel], expRandomValue[targetLevel]);



            // Equilibrate Role ==========================================
            for (int i = 0; i < c.Characters.Count; i++)
            {
                for (int j = oldLevel; j < targetLevel; j++)
                    c.Characters[i].Attack += atkBonus[j];
            }


            // Equilibrate Text HP ==========================================
            for (int i = 0; i < c.TextData.Count; i++)
            {
                for (int j = oldLevel; j < targetLevel; j++)
                    c.TextData[i].HPMax += hpBonus[j];
            }
        }


        #endregion

    } 

} // #PROJECTNAME# namespace