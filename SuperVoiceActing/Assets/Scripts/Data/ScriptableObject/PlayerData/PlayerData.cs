/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{

    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }


    public struct Date
    {
        public int week;
        public int month;
        public int year;

        public Date(int w, int m, int y)
        {
            week = w;
            month = m;
            year = y;
        }
    }

    /// <summary>
    /// Definition of the PlayerData class
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {

        [SerializeField]
        private bool reset = true;
        public bool Reset
        {
            get { return reset; }
        }

        [Space]

        [SerializeField]
        private int language;

        public int Language
        {
            get { return language; }
        }

        [Space]
        [Space]
        [Space]
        [Title("Ressources")]

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

        [Space]
        [Space]
        [Space]
        [Title("Calendar")]



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


        [Space]
        [FoldoutGroup("Configuration")]
        [SerializeField]
        private int springDate;
        public int SpringDate
        {
            get { return springDate; }
        }
        [FoldoutGroup("Configuration")]
        [SerializeField]
        private int summerDate;
        public int SummerDate
        {
            get { return summerDate; }
        }
        [FoldoutGroup("Configuration")]
        [SerializeField]
        private int autumnDate;
        public int AutumnDate
        {
            get { return autumnDate; }
        }
        [FoldoutGroup("Configuration")]
        [SerializeField]
        private int winterDate;
        public int WinterDate
        {
            get { return winterDate; }
        }
        //[FoldoutGroup("Configuration")]
        [HorizontalGroup("Configuration/hey")]
        [SerializeField]
        private string[] monthName;
        public string[] MonthName
        {
            get { return monthName; }
        }
        //[FoldoutGroup("Configuration")]
        [HorizontalGroup("Configuration/hey")]
        [SerializeField]
        private int[] monthDate;
        public int[] MonthDate
        {
            get { return monthDate; }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        [Space]
        [Space]
        [Space]
        [Title("Battle Data")]
        [SerializeField]
        private int comboMax;
        public int ComboMax
        {
            get { return comboMax; }
            set { comboMax = value; }
        }

        [SerializeField]
        [HideLabel]
        private EmotionStat deck;
        public EmotionStat Deck
        {
            get { return deck; }
            set { deck = value; }
        }


        /////////////////////////////////////////////////////////////////////////////////// 
        [Space]
        [Space]
        [Space]
        [Title("Management Data")]
        [SerializeField]
        private List<ContractData> contractAvailableDebug = new List<ContractData>();

        [SerializeField]
        private List<VoiceActorData> voiceActorsDebug = new List<VoiceActorData>();







        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Title("Debug")]

        // Contrat en cours à doubler
        //[SerializeField]
        private Contract currentContract;
        public Contract CurrentContract
        {
            get { return currentContract; }
            set { currentContract = value; }
        }

        // Contrat accepté
        [SerializeField]
        private List<Contract> contractAccepted;
        public List<Contract> ContractAccepted
        {
            get { return contractAccepted; }
            set { contractAccepted = value; }
        }

        // Contrat Disponible 
        [SerializeField]
        private List<Contract> contractAvailable;
        public List<Contract> ContractAvailable
        {
            get { return contractAvailable; }
            set { contractAvailable = value; }
        }

        // Liste des Acteurs
        [SerializeField]
        private List<VoiceActor> voiceActors;
        public List<VoiceActor> VoiceActors
        {
            get { return voiceActors; }
            set { voiceActors = value; }
        }

        [Title("Debug")]
        [SerializeField]
        private List<StoryEventData> nextStoryEvents;
        public List<StoryEventData> NextStoryEvents
        {
            get { return nextStoryEvents; }
            set { nextStoryEvents = value; }
        }

        [SerializeField]
        private List<StoryEventData> nextDekstopStoryEvents;
        public List<StoryEventData> NextDekstopStoryEvents
        {
            get { return nextDekstopStoryEvents; }
            set { nextDekstopStoryEvents = value; }
        }

        [SerializeField]
        private int maintenance;
        public int Maintenance
        {
            get { return maintenance; }
            set { maintenance = value; }
        }

        [SerializeField]
        private int money;
        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        [SerializeField]
        private Date date;
        public Date Date
        {
            get { return date; }
        }

        [SerializeField]
        private Season season;
        public Season Season
        {
            get { return season; }
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CreateList()
        {
            if(voiceActors == null && contractAvailable == null)
            {
                currentContract = null;

                voiceActors = new List<VoiceActor>(voiceActorsDebug.Count);
                for (int i = 0; i < voiceActorsDebug.Count; i++)
                {
                    voiceActors.Add(new VoiceActor(voiceActorsDebug[i]));
                }

                contractAvailable = new List<Contract>(contractAvailableDebug.Count);
                for (int i = 0; i < contractAvailableDebug.Count; i++)
                {
                    contractAvailable.Add(new Contract(contractAvailableDebug[i]));
                }

                contractAccepted = new List<Contract>(3);
                contractAccepted.Add(null);
                contractAccepted.Add(null);
                contractAccepted.Add(null);
                CreateNewData();
            }
            else
            {
                NextWeek();
            }

            //Debug.Log(voiceActors[0].Name);

        }


        public void CreateNewData()
        {
            nextStoryEvents = new List<StoryEventData>();
            date = new Date(week, month, year);
            season = StartSeason;
            money = startMoney;
            maintenance = startMaintenance;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void PayMaintenance()
        {
            money -= maintenance;
        }

        public void NextWeek()
        {
            date.week += 1;
            if (date.week > 52)
            {
                date.week = 1;
                date.month = 1;
                date.year += 1;
            }
            CheckMonth();
            CheckSeason();
            ContractNextWeek();
            VoiceActorWork();
        }


        public void CheckMonth()
        {
            if(date.week == monthDate[date.month-1])
            {
                date.month += 1;
                PayMaintenance();
            }
        }

        public void CheckSeason()
        {
            switch(season)
            {
                case Season.Spring:
                    if(date.week == summerDate)
                        season = Season.Summer;
                    break;
                case Season.Summer:
                    if (date.week == autumnDate)
                        season = Season.Autumn;
                    break;
                case Season.Autumn:
                    if (date.week == winterDate)
                        season = Season.Winter;
                    break;
                case Season.Winter:
                    if (date.week == springDate)
                        season = Season.Spring;
                    break;
            }
        }

        public void ContractNextWeek()
        {
            for(int i = 0; i < contractAccepted.Count; i++)
            {
                if (contractAccepted[i] != null)
                {
                    contractAccepted[i].WeekRemaining -= 1;
                    if (contractAccepted[i].WeekRemaining == 0)
                    {
                        contractAccepted.RemoveAt(i);
                    }
                }
            }
        }

        public void VoiceActorWork()
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                voiceActors[i].WorkForWeek();
            }
        }






    } // PlayerData class

} // #PROJECTNAME# namespace