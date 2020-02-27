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



        [Title("Stat")]
        [SerializeField]
        [OnValueChanged("CalculateStatModifier")]
        float baseStatMultiplier;

        [HorizontalGroup("Stat")]
        [SerializeField]
        [ReadOnly]
        int[] statLevel;
        [HorizontalGroup("Stat")]
        [SerializeField]
        float[] statBonusMultiplier;
        [HorizontalGroup("Stat")]
        [SerializeField]
        float[] statTotalMultiplier;

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

        private void CalculateStatModifier()
        {
            statTotalMultiplier = new float[statBonusMultiplier.Length];
            statLevel = new int[statBonusMultiplier.Length];
            float bonus = baseStatMultiplier;
            for (int i = 0; i < statBonusMultiplier.Length; i++)
            {
                bonus += statBonusMultiplier[i];
                statTotalMultiplier[i] = bonus;
                statLevel[i] = i;
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

            // Equilibrate Stat ==========================================
            float bonus = 0;
            float oldBonus = 0;
            for (int j = oldLevel; j < targetLevel; j++)
            {
                oldBonus = bonus;
                bonus += statBonusMultiplier[j];
            }
            
            for (int i = 0; i < c.Characters.Count; i++)
            {
                bonus = baseStatMultiplier + Random.Range(oldBonus, bonus);
                c.Characters[i].CharacterStat.Multiply(bonus);
            }
        }


        #endregion

    } 

} // #PROJECTNAME# namespace