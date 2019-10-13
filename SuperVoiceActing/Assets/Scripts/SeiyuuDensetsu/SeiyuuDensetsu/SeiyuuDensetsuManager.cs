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
        GameTimelineData gameTimeline;
        [SerializeField]
        SeiyuuActorManager seiyuuActorManager;
        [SerializeField]
        SeiyuuActionManager seiyuuActionManager;
        [SerializeField]
        MenuContractMoney menuContractMoney;
        [SerializeField]
        Animator animatorBackground;

        [Title("WeekProgress")]
        [SerializeField]
        Animator[] animatorsWeek;
        [SerializeField]
        TextMeshProUGUI textCurrentAction;
        [SerializeField]
        InputController inputWeekProgress;
        [SerializeField]
        Animator animatorButtons;

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
        TextMeshProUGUI textMoneyGainTMP;
        [SerializeField]
        Animator animatorCalendar;

        [SerializeField]
        TextMeshProUGUI[] textCalendarNextDays;

        SeiyuuAction seiyuuActionDay;
        SeiyuuAction seiyuuActionNight;

        int day = 0;

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
            seiyuuData.CreateSeiyuuData(voiceActorDebug, dateDebug, initialSeason, initialMoney, initialBill);
            //seiyuuData = new SeiyuuData(voiceActorDebug, dateDebug, initialSeason, initialMoney, initialBill);
            NewWeek();
        }

        public void NewWeek()
        {
            seiyuuData.NextWeek(calendarData);
            DrawDate();
            seiyuuActorManager.DrawActorStat(seiyuuData.VoiceActor);
            seiyuuActorManager.ShowAllActorInfo();
            ShowCalendar(true);
            animatorButtons.SetBool("Appear", true);
            menuContractMoney.DrawMoneyGain();
        }

        public void StartWeek()
        {
            day = 0;
            animatorBackground.SetTrigger("Week");
            seiyuuActionDay = seiyuuActionManager.GetSeiyuuActionDay();
            seiyuuActionNight = seiyuuActionManager.GetSeiyuuActionNight();
            textCurrentAction.text = seiyuuActionDay.ActionName;
            seiyuuActorManager.HideAllActorInfo();
            ShowCalendar(false);
            animatorButtons.SetBool("Appear", false);
        }

        // Call each day
        public void ApplyDay()
        {
            animatorsWeek[day].SetTrigger("Feedback");
            if (seiyuuActionDay != null)
            {
                seiyuuActionDay.ApplyDay(seiyuuData);
            }
            else
            {
                seiyuuActionNight.ApplyDay(seiyuuData);
            }
            textMoneyGainTMP.gameObject.SetActive(true);
            textMoneyGainTMP.text = "<sprite=9>   +" + seiyuuData.MoneyGain;
            seiyuuActorManager.DrawActorStat(seiyuuData.VoiceActor);
            day += 1;
        }

        // Call at the end of week
        public void ApplyWeek()
        {
            if (seiyuuActionDay != null)
            {
                seiyuuActionDay.ApplyWeek(seiyuuData, seiyuuActorManager);
                menuContractMoney.AddSalaryDatas(seiyuuActionDay.ActionName, seiyuuData.MoneyGain);
            }
            else
            {
                seiyuuActionNight.ApplyWeek(seiyuuData, seiyuuActorManager);
                menuContractMoney.AddSalaryDatas(seiyuuActionNight.ActionName, seiyuuData.MoneyGain);
            }
            seiyuuData.MoneyGain = 0;
            textMoneyGainTMP.gameObject.SetActive(false);
            inputWeekProgress.gameObject.SetActive(true);


        }

        // Call by inputWeekProgress
        public void ContinueWeek()
        {
            inputWeekProgress.gameObject.SetActive(false);
            if (seiyuuActionDay != null)
            {
                seiyuuActorManager.HideAllActorInfo();
                animatorBackground.SetTrigger("WeekNight");
                seiyuuActionDay = null;
                seiyuuActionManager.RemoveAction(false);
                textCurrentAction.text = seiyuuActionNight.ActionName;
                menuContractMoney.DrawMoneyGain();
            }
            else
            {
                NewWeek();
                seiyuuActionNight = null;
                seiyuuActionManager.RemoveAction(true);
            }
            day = 0;
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
            textDate.text = calendarData.MonthName[seiyuuData.Date.month] + " / Semaine " + seiyuuData.Date.week;
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