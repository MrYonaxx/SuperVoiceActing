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
        PlayerData playerData;


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

        public int GetPreviousMonth()
        {
            if (playerData.Date.month - 2 == -1)
            {
                return 11;
            }
            else
                return playerData.Date.month - 2;
        }

        public int GetPreviousPreviousMonth()
        {
            if (playerData.Date.month - 3 == -1)
            {
                return 11;
            }
            else if (playerData.Date.month - 3 == -2)
            {
                return 10;
            }
            else
                return playerData.Date.month - 3;
        }



        public void StartNextWeek()
        {
            textCurrentMonth.text = playerData.MonthName[playerData.Date.month - 1];

            int weekRemaining = (playerData.MonthDate[playerData.Date.month - 1] - playerData.Date.week);

            int weekNumberPrevious = playerData.MonthDate[GetPreviousMonth()];
            if (weekNumberPrevious == 53)
                weekNumberPrevious = 1;

            int weekNumber = (playerData.MonthDate[playerData.Date.month - 1] - weekNumberPrevious);

            if(weekRemaining == weekNumber) // Nouveau mois
            {
                lastMonthWeeks.gameObject.SetActive(true);
                textCurrentPreviousMonth.gameObject.SetActive(true);
                textCurrentPreviousMonth.text = playerData.MonthName[GetPreviousMonth()];
                DrawLastMonthWeek();
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
            /*weekNumberPrevious = playerData.MonthDate[GetPreviousMonth()];
            if (weekNumberPrevious == 53)
                weekNumberPrevious = 1;*/

            for (int i = 0; i < weekNumber; i++)
            {
                textMonthWeeks[i].text = (weekNumberPrevious + i).ToString();
                if ((weekNumberPrevious + i) == playerData.Date.week)
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








        private void DrawLastMonthWeek()
        {

            int weekNumberPrevious = playerData.MonthDate[GetPreviousMonth()];
            /*if (weekNumberPrevious == 53)
                weekNumberPrevious = 1;*/

            int weekNumberPreviousPrevious = playerData.MonthDate[GetPreviousPreviousMonth()];
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


            //Debug.Log("Week number :" + weekNumber);
            for (int i = 0; i < weekNumber; i++)
            {
                textMonthLastWeeks[i].text = (weekNumberPreviousPrevious + i).ToString();
            }
        }



    } // MenuNextWeek class
	
}// #PROJECTNAME# namespace
