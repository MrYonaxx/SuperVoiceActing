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
    [CreateAssetMenu(fileName = "CalendarData", menuName = "PlayerData/CalendarData", order = 1)]
    public class CalendarData: ScriptableObject
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [HorizontalGroup]
        [SerializeField]
        private string[] monthName;

        [HorizontalGroup]
        [SerializeField]
        private int[] monthDate;

        [SerializeField]
        private int[] seasonDate;
        public int[] SeasonDate
        {
            get { return seasonDate; }
        }

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public string GetMonthName(int i)
        {
            if (i < 0)
                i += monthName.Length;
            return monthName[i];
        }

        public int GetMonthDate(int i)
        {
            if (i < 0)
                i += monthDate.Length;
            return monthDate[i];
        }


        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */


        // return true if change Month
        public bool CheckMonth(int playerWeek)
        {
            for (int i = 0; i < monthDate.Length; i++)
            {
                if (playerWeek == monthDate[i])
                    return true;
            }
            return false;
        }

        // return true if change Month
        public bool CheckMonth(int playerWeek, int playerMonth)
        {
            return (playerWeek == monthDate[playerMonth - 1]);
        }

        public void CheckSeason(Season playerSeason, int playerWeek)
        {
            for(int i = 0; i < seasonDate.Length; i++)
            {
                if(playerWeek == seasonDate[i])
                    playerSeason = (Season) i;
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace