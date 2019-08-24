﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VoiceActing
{
    [CreateAssetMenu(fileName = "SavesDatas", menuName = "PlayerData/SavesData", order = 1)]
    public class SaveDatabase : ScriptableObject
    {
        [SerializeField]
        private SaveData[] savesDatas;
        public SaveData[] SavesDatas
        {
            get { return savesDatas; }
            set { savesDatas = value; }
        }

    }

    [System.Serializable]
    public class SaveData
    {
        [SerializeField]
        private string savePlayerName;
        public string SavePlayerName
        {
            get { return savePlayerName; }
            set { savePlayerName = value; }
        }
        [SerializeField]
        string saveStudioName;
        public string SaveStudioName
        {
            get { return saveStudioName; }
            set { saveStudioName = value; }
        }
        [SerializeField]
        string saveChapter;
        public string SaveChapter
        {
            get { return saveChapter; }
            set { saveChapter = value; }
        }



        [SerializeField]
        int saveSeason;
        public int SaveSeason
        {
            get { return saveSeason; }
            set { saveSeason = value; }
        }
        [SerializeField]
        string saveMonth;
        public string SaveMonth
        {
            get { return saveMonth; }
            set { saveMonth = value; }
        }
        [SerializeField]
        string saveWeek;
        public string SaveWeek
        {
            get { return saveWeek; }
            set { saveWeek = value; }
        }



        [SerializeField]
        string saveTime;
        public string SaveTime
        {
            get { return saveTime; }
            set { saveTime = value; }
        }
        [SerializeField]
        string saveMoney;
        public string SaveMoney
        {
            get { return saveMoney; }
            set { saveMoney = value; }
        }
        [SerializeField]
        Sprite saveBestVA;
        public Sprite SaveBestVA
        {
            get { return saveBestVA; }
            set { saveBestVA = value; }
        }

        public SaveData(PlayerData playerData)
        {

        }

        public void WriteSaveData(PlayerData playerData)
        {
            savePlayerName = playerData.PlayerName;
            saveStudioName = playerData.StudioName;

            saveMonth = playerData.MonthName[playerData.Date.month - 1];
            saveWeek = playerData.Date.week.ToString();

            //
            saveTime = playerData.GetTimeInHour();
            saveMoney = playerData.Money.ToString();
            saveChapter = playerData.CurrentChapter.ToString();

            saveSeason = (int)playerData.Season;
            saveBestVA = playerData.CurrentBestActor;
        }

    }

}// #PROJECTNAME# namespace