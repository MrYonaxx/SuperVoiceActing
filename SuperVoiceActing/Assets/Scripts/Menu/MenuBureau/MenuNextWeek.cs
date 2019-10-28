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

namespace VoiceActing
{
	public class MenuNextWeek : MonoBehaviour
	{

        [SerializeField]
        private CalendarData calendarData;

        [SerializeField]
        float buttonSize = 140;


        [SerializeField]
        TextMeshProUGUI textCurrentMonth;
        [SerializeField]
        TextMeshProUGUI textCurrentPreviousMonth;

        [SerializeField]
        Color colorWeekSelected;
        [SerializeField]
        Color colorWeekUnSelected;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        Animator skipAnimator;
        [SerializeField]
        Animator selection;
        [SerializeField]
        RectTransform weeks;
        [SerializeField]
        RectTransform[] rectTransformsMonthWeeks;
        [SerializeField]
        TextMeshProUGUI[] textMonthWeeks;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        RectTransform lastMonthWeeks;
        [SerializeField]
        RectTransform[] rectTransformsLastMonthWeeks;
        [SerializeField]
        TextMeshProUGUI[] textMonthLastWeeks;




        public void SkipTransition()
        {
            skipAnimator.SetTrigger("Skip");
        }


        public void StartNextWeek(Date currentDate)
        {
            textCurrentMonth.text = calendarData.GetMonthName(currentDate.month - 1); //playerData.MonthName[playerData.Date.month - 1];

            int weekRemaining = calendarData.GetMonthDate(currentDate.month - 1) - currentDate.week;  //(playerData.MonthDate[playerData.Date.month - 1] - playerData.Date.week);

            int weekNumberPrevious = calendarData.GetMonthDate(currentDate.month - 2); // playerData.MonthDate[GetPreviousMonth()];
            if (weekNumberPrevious == 53)
                weekNumberPrevious = 1;

            int weekNumber = calendarData.GetMonthDate(currentDate.month - 1) - weekNumberPrevious; // (playerData.MonthDate[playerData.Date.month - 1] - weekNumberPrevious);

            if(weekRemaining == weekNumber) // Nouveau mois
            {
                lastMonthWeeks.gameObject.SetActive(true);
                textCurrentPreviousMonth.gameObject.SetActive(true);
                textCurrentPreviousMonth.text = calendarData.GetMonthName(currentDate.month - 2);  //playerData.MonthName[GetPreviousMonth()];
                DrawLastMonthWeek(currentDate.month);
            }
            else
            {
                lastMonthWeeks.gameObject.SetActive(false);
                textCurrentPreviousMonth.gameObject.SetActive(false);
            }

            if (weekNumber == 4)
            {
                rectTransformsMonthWeeks[4].gameObject.SetActive(false);
            }
            else if (weekNumber == 5)
            {
                rectTransformsMonthWeeks[4].gameObject.SetActive(true);
            }




            // Draw Weeks
            weeks.anchoredPosition -= new Vector2(buttonSize * (weekNumber-weekRemaining), 0);

            for (int i = 0; i < weekNumber; i++)
            {
                textMonthWeeks[i].text = (weekNumberPrevious + i).ToString();
                if ((weekNumberPrevious + i) == currentDate.week)
                {
                    textMonthWeeks[i].color = colorWeekSelected;
                    selection.transform.SetParent(textMonthWeeks[i].transform);
                    selection.gameObject.SetActive(true);
                }
                else
                {
                    textMonthWeeks[i].color = colorWeekUnSelected;
                }
            }
        }








        private void DrawLastMonthWeek(int month)
        {

            int weekNumberPrevious = calendarData.GetMonthDate(month - 2); // playerData.MonthDate[GetPreviousMonth()];

            int weekNumberPreviousPrevious = calendarData.GetMonthDate(month - 3);  //playerData.MonthDate[GetPreviousPreviousMonth()];
            if (weekNumberPreviousPrevious == 53)
                weekNumberPreviousPrevious = 1;

            int weekNumber = (weekNumberPrevious - weekNumberPreviousPrevious);
            if (weekNumber == 4)
            {
                lastMonthWeeks.anchoredPosition += new Vector2(buttonSize, 0);
                rectTransformsLastMonthWeeks[4].gameObject.SetActive(false);
            }
            else if (weekNumber == 5)
            {
                rectTransformsLastMonthWeeks[4].gameObject.SetActive(true);
            }


            for (int i = 0; i < weekNumber; i++)
            {
                textMonthLastWeeks[i].text = (weekNumberPreviousPrevious + i).ToString();
            }
        }



    } // MenuNextWeek class
	
}// #PROJECTNAME# namespace
