﻿/*****************************************************************
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
    [CreateAssetMenu(fileName = "InitialPlayerData", menuName = "PlayerData/InitialPlayerData", order = 1)]
    public class InitialPlayerData : ScriptableObject
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


        [Space]
        [Space]
        [Space]
        [Title("Initial Ressources")]
        [SerializeField]
        private int playerLevel;
        public int PlayerLevel
        {
            get { return playerLevel; }
        }

        [SerializeField]
        private int startMoney;
        public int StartMoney
        {
            get { return startMoney; }
        }

        [SerializeField]
        private int startMaintenance;
        public int StartMaintenance
        {
            get { return startMaintenance; }
        }

        [SerializeField]
        private int startResearchPoint;
        public int StartResearchPoint
        {
            get { return startResearchPoint; }
        }




        [SerializeField]
        private ResearchDatabase researchDatabase;
        public ResearchDatabase ResearchDatabase
        {
            get { return researchDatabase; }
        }

        [SerializeField]
        private ResearchDungeonData[] researchDungeonData;
        public ResearchDungeonData[] ResearchDungeonData
        {
            get { return researchDungeonData; }
        }
        [SerializeField]
        private ResearchEventDatabase researchEventDatabase;
        public ResearchEventDatabase ResearchEventDatabase
        {
            get { return researchEventDatabase; }
        }




        [SerializeField]
        private RecapDatabase gameManualDatabase;
        public RecapDatabase GameManualDatabase
        {
            get { return gameManualDatabase; }
        }
        [SerializeField]
        private RecapDatabase storyResumeDatabase;
        public RecapDatabase StoryResumeDatabase
        {
            get { return storyResumeDatabase; }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///
        [Space]
        [Space]
        [Space]
        [Title("Initial Date")]
        [SerializeField]
        private Season startSeason;
        public Season StartSeason
        {
            get { return startSeason; }
        }

        [HorizontalGroup("Date", LabelWidth = 50, Width = 50)]
        [SerializeField]
        private int week;
        public int Week
        {
            get { return week; }
        }

        [HorizontalGroup("Date", LabelWidth = 50, Width = 50)]
        [SerializeField]
        private int month;
        public int Month
        {
            get { return month; }
        }

        [HorizontalGroup("Date", LabelWidth = 50, Width = 50)]
        [SerializeField]
        private int year;
        public int Year
        {
            get { return year; }
        }

        /////////////////////////////////////////////////////////////////////////////////// 
        [Space]
        [Space]
        [Space]
        [Title("Initial Battle Data")]
        [SerializeField]
        private int initialComboMax;
        public int InitialComboMax
        {
            get { return initialComboMax; }
        }

        [SerializeField]
        [HideLabel]
        private EmotionStat initialDeck;
        public EmotionStat InitialDeck
        {
            get { return initialDeck; }
        }

        [SerializeField]
        private EmotionStat initialAtkBonus;
        public EmotionStat InitialAtkBonus
        {
            get { return initialAtkBonus; }
        }

        [SerializeField]
        private EmotionStat initialDefBonus;
        public EmotionStat InitialDefBonus
        {
            get { return initialDefBonus; }
        }

        [SerializeField]
        private int initialCriticalRate;
        public int InitialCriticalRate
        {
            get { return initialCriticalRate; }
        }

        [SerializeField]
        private int initialTemperature;
        public int InitialTemperature
        {
            get { return initialTemperature; }
        }

        [SerializeField]
        private int initialTurn;
        public int InitialTurn
        {
            get { return initialTurn; }
        }

        [SerializeField]
        private bool menuStudioUnlocked = true;
        public bool MenuStudioUnlocked
        {
            get { return menuStudioUnlocked; }
        }

        [HorizontalGroup("Initial Equipement")]
        [SerializeField]
        private EquipementCategory[] equipementCategories;
        public EquipementCategory[] EquipementCategories
        {
            get { return equipementCategories; }
        }

        [HorizontalGroup("Initial Equipement")]
        [SerializeField]
        private EquipementData[] initialEquipement;
        public EquipementData[] InitialEquipement
        {
            get { return initialEquipement; }
        }

        /////////////////////////////////////////////////////////////////////////////////// 
        [Space]
        [Space]
        [Space]
        [Title("Management Data")]
        [SerializeField]
        private List<ContractData> contractAvailableDebug = new List<ContractData>();
        public List<ContractData> ContractAvailableDebug
        {
            get { return contractAvailableDebug; }
        }

        [SerializeField]
        private List<VoiceActorData> voiceActorsDebug = new List<VoiceActorData>();
        public List<VoiceActorData> VoiceActorsDebug
        {
            get { return voiceActorsDebug; }
        }

        [SerializeField]
        private List<VoiceActorData> voiceActorsGachaDebug = new List<VoiceActorData>();
        public List<VoiceActorData> VoiceActorsGachaDebug
        {
            get { return voiceActorsGachaDebug; }
        }

        [SerializeField]
        private List<SoundEngineerData> soundEngineerDebug = new List<SoundEngineerData>();
        public List<SoundEngineerData> SoundEngineerDebug
        {
            get { return soundEngineerDebug; }
        }

        [SerializeField]
        private List<int> initialTutoEvent = new List<int>();
        public List<int> InitialTutoEvent
        {
            get { return initialTutoEvent; }
        }

        [SerializeField]
        private List<EquipementData> inventoryDebug = new List<EquipementData>();
        public List<EquipementData> InventoryDebug
        {
            get { return inventoryDebug; }
        }

        [SerializeField]
        private List<FormationData> formationDebug = new List<FormationData>();
        public List<FormationData> FormationDebug
        {
            get { return formationDebug; }
        }

        #endregion

		
	} // InitialPlayerData class
	
}// #PROJECTNAME# namespace
