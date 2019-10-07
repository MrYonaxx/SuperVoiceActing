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
        public string[] MonthName
        {
            get { return monthName; }
        }
        [HorizontalGroup]
        [SerializeField]
        private int[] monthDate;
        public int[] MonthDate
        {
            get { return monthDate; }
        }

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

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */


        #endregion

    } 

} // #PROJECTNAME# namespace