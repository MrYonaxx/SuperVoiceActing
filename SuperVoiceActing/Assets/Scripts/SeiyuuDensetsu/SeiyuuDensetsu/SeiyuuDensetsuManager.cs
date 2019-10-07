/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuDensetsuManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Debug")]
        [SerializeField]
        VoiceActorData voiceActorDebug;
        [SerializeField]
        Date dateDebug;
        [SerializeField]
        Season initialSeason;
        [SerializeField]
        int initialMoney;
        [SerializeField]
        int initialBill;

        [Title("Data")]
        [SerializeField]
        SeiyuuData seiyuuData;
        [SerializeField]
        SeiyuuActorManager seiyuuActorManager;
        [SerializeField]
        SeiyuuActionManager seiyuuActionManager;
        [SerializeField]
        Animator animatorBackground;

        [Title("WeekProgress")]


        [Title("Calendar")]
        [SerializeField]
        ImageDictionnary seasonData;
        [SerializeField]
        CalendarData calendarData;

        [SerializeField]
        Image seasonSprite1;
        [SerializeField]
        Image seasonSprite2;
        [SerializeField]
        TextMeshProUGUI textDate;
        [SerializeField]
        Animator animatorCalendar;

        [SerializeField]
        TextMeshProUGUI[] textCalendarNextDays;

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
        protected void Start()
        {
            seiyuuData = new SeiyuuData(voiceActorDebug, dateDebug, initialSeason, initialMoney, initialBill);
            NewWeek();
        }

        public void NewWeek()
        {
            DrawDate();
            seiyuuActorManager.DrawActorStat(seiyuuData.VoiceActor);
        }

        public void StartWeek()
        {

        }

        // Call each day
        public void ApplyDay()
        {

        }

        // Call at the end of week
        public void ApplyWeek()
        {

        }

        public void MoveMenuCenter(bool b)
        {
            animatorBackground.SetBool("Center", b);
        }

        public void MoveMenuLeft(bool b)
        {
            animatorBackground.SetBool("Left", b);
        }

        public void DrawDate()
        {
            seasonSprite1.sprite = seasonData.GetSprite((int)seiyuuData.Season);
            seasonSprite2.sprite = seasonSprite1.sprite;
            textDate.text = calendarData.MonthName[seiyuuData.Date.month] + "/ Semaine " + seiyuuData.Date.week;
            for(int i =0; i < textCalendarNextDays.Length; i++)
            {
                int week = seiyuuData.Date.week + (i + 1);
                if(week >= 53)
                {
                    week -= 52;
                }
                textCalendarNextDays[i].text = week.ToString();
            }
        }

        public void ShowCalendar(bool b)
        {
            animatorCalendar.SetBool("Appear", b);
        }




        #endregion

    } 

} // #PROJECTNAME# namespace