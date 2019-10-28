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
	public class MenuContractInfo : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("MenuInfo")]
        [SerializeField]
        private CalendarData calendarData;
        [SerializeField]
        private ImageDictionnary seasonData;

        [Header("MenuDate")]
        [SerializeField]
        TextMeshProUGUI textWeek;
        [SerializeField]
        TextMeshProUGUI textMonth;
        [SerializeField]
        TextMeshProUGUI textNextMonth;
        [SerializeField]
        TextMeshProUGUI textYear;
        [SerializeField]
        Image imageSeasonOutline;
        [SerializeField]
        Image imageSeason;
        [SerializeField]
        MenuRessourceResearch menuResearch;

        [Header("MenuInfo")]
        [SerializeField]
        TextMeshProUGUI[] textsInfo;

        [SerializeField]
        string[] placeholderTextDatabase;

        int textInfoCount = 0;
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


        public void DrawDate(Date date, int season)
        {
            textWeek.text = date.week.ToString();
            textMonth.text = calendarData.GetMonthName(date.month-1);

            textYear.text = date.year.ToString();
            textNextMonth.text = (calendarData.GetMonthDate(date.month - 1) - date.week).ToString();

            imageSeason.sprite = seasonData.GetSprite(season);
            imageSeasonOutline.sprite = imageSeason.sprite;
        }

        public void DrawResearch(int researchPoint)
        {
            menuResearch.DrawResearchPoint(researchPoint);
        }


        public void DrawInfo(string info)
        {
            textsInfo[textInfoCount].text = info;
            textInfoCount += 1;
        }

        public void DrawObjective(int currentChapter, string objective)
        {
            if (currentChapter != 0)
            {
                textsInfo[textInfoCount].text = "Chapitre " + currentChapter + "\n" + objective;
                textInfoCount += 1;
            }
        }

        public void DrawInfo(string info, int value)
        {
            if (value == 0)
                return;
            textsInfo[textInfoCount].text = string.Format(info, value);
            textInfoCount += 1;
        }

        public void DrawInfo(int infoID, int value)
        {
            if (value == 0)
                return;
            textsInfo[textInfoCount].text = string.Format(placeholderTextDatabase[infoID], value);
            textInfoCount += 1;
        }



        #endregion

    } // MenuContractInfo class
	
}// #PROJECTNAME# namespace
