﻿/*****************************************************************
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

    public class DebugSave
    {
        private bool boolean = true;
        public DebugSave(bool b)
        {
            boolean = b;
        }
    }

    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    [System.Serializable]
    public struct Date
    {
        [SerializeField]
        public int week;
        [SerializeField]
        public int month;
        [SerializeField]
        public int year;

        public Date(int w, int m, int y)
        {
            week = w;
            month = m;
            year = y;
        }
    }

    [System.Serializable]
    public class ContractCooldown
    {
        public ContractData contract;
        public int cooldown;

        public ContractCooldown(ContractData cd, int c)
        {
            contract = cd;
            cooldown = c;
        }
    }

    /// <summary>
    /// Definition of the PlayerData class
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {

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


        [SerializeField]
        private ExperienceCurveData experienceCurve;
        public ExperienceCurveData ExperienceCurve
        {
            get { return experienceCurve; }
        }

        [Space]
        [Space]
        [Space]
        [Title("Calendar")]


        [SerializeField]
        private GameTimelineData gameTimeline;
        public GameTimelineData GameTimeline
        {
            get { return gameTimeline; }
        }

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
        private int initialComboMax;


        [SerializeField]
        [HideLabel]
        private EmotionStat initialDeck;

        [SerializeField]
        private EmotionStat initialAtkBonus;

        [SerializeField]
        private EmotionStat initialDefBonus;

        [SerializeField]
        private int initialCriticalRate;

        [SerializeField]
        private int initialTemperature;

        [SerializeField]
        private int initialTurn;

        [HorizontalGroup("InitialEquipement")]
        [SerializeField]
        private EquipementCategory[] equipementCategories;
        public EquipementCategory[] EquipementCategories
        {
            get { return equipementCategories; }
        }

        [HorizontalGroup("InitialEquipement")]
        [SerializeField]
        private EquipementData[] initialEquipement;

        /////////////////////////////////////////////////////////////////////////////////// 
        [Space]
        [Space]
        [Space]
        [Title("Management Data")]
        [SerializeField]
        private List<ContractData> contractAvailableDebug = new List<ContractData>();

        [SerializeField]
        private List<VoiceActorData> voiceActorsDebug = new List<VoiceActorData>();

        [SerializeField]
        private List<VoiceActorData> voiceActorsGachaDebug = new List<VoiceActorData>();

        [SerializeField]
        private List<SoundEngineerData> soundEngineerDebug = new List<SoundEngineerData>();

        [SerializeField]
        private List<StoryEventData> initialTutoEvent = new List<StoryEventData>();

        [SerializeField]
        private List<EquipementData> inventoryDebug = new List<EquipementData>();

        [SerializeField]
        private List<FormationData> formationDebug = new List<FormationData>();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Title("Debug")]
        [SerializeField]
        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        [SerializeField]
        private string studioName;
        public string StudioName
        {
            get { return studioName; }
            set { studioName = value; }
        }


        // Contrat en cours à doubler
        [SerializeField]
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



        // Contrat Gacha
        [SerializeField]
        private List<ContractData> contractGacha;
        public List<ContractData> ContractGacha
        {
            get { return contractGacha; }
            set { contractGacha = value; }
        }

        // Contrat Gacha qui sont en standby et qui ne peuvent pas être tiré
        [SerializeField]
        private List<ContractCooldown> contractGachaCooldown;
        public List<ContractCooldown> ContractGachaCooldown
        {
            get { return contractGachaCooldown; }
            set { contractGachaCooldown = value; }
        }



        // Liste des Acteurs
        [SerializeField]
        private List<VoiceActor> voiceActors;
        public List<VoiceActor> VoiceActors
        {
            get { return voiceActors; }
            set { voiceActors = value; }
        }

        [SerializeField]
        private List<VoiceActor> voiceActorsGacha;
        public List<VoiceActor> VoiceActorsGacha
        {
            get { return voiceActorsGacha; }
            set { voiceActorsGacha = value; }
        }


        // Liste des IngéSon
        [SerializeField]
        private List<SoundEngineer> soundEngineers;
        public List<SoundEngineer> SoundEngineers
        {
            get { return soundEngineers; }
            set { soundEngineers = value; }
        }


        [SerializeField]
        private List<StoryEventData> nextStoryEvents;
        public List<StoryEventData> NextStoryEvents
        {
            get { return nextStoryEvents; }
            set { nextStoryEvents = value; }
        }

        [SerializeField]
        private List<StoryEventData> nextStoryEventsStartWeek;
        public List<StoryEventData> NextStoryEventsStartWeek
        {
            get { return nextStoryEventsStartWeek; }
            set { nextStoryEventsStartWeek = value; }
        }

        [SerializeField]
        private List<StoryEventData> phoneStoryEvents;
        public List<StoryEventData> PhoneStoryEvents
        {
            get { return phoneStoryEvents; }
            set { phoneStoryEvents = value; }
        }

        [SerializeField]
        private List<StoryEventData> tutoEvent;
        public List<StoryEventData> TutoEvent
        {
            get { return tutoEvent; }
            set { tutoEvent = value; }
        }



        [SerializeField]
        private List<StoryEventData> randomEventsAvailable;
        public List<StoryEventData> RandomEventsAvailable
        {
            get { return randomEventsAvailable; }
            set { randomEventsAvailable = value; }
        }

        [SerializeField]
        private List<StoryEventData> randomEventsStandby;
        public List<StoryEventData> RandomEventsStandby
        {
            get { return randomEventsStandby; }
            set { randomEventsStandby = value; }
        }




        [SerializeField]
        private List<EquipementData> inventoryEquipement;
        public List<EquipementData> InventoryEquipement
        {
            get { return inventoryEquipement; }
        }
        [SerializeField]
        private EquipementData[] currentEquipement;
        public EquipementData[] CurrentEquipement
        {
            get { return currentEquipement; }
        }



        [SerializeField]
        private List<FormationData> inventoryFormation;
        public List<FormationData> InventoryFormation
        {
            get { return inventoryFormation; }
        }






        [SerializeField]
        private int currentChapter;
        public int CurrentChapter
        {
            get { return currentChapter; }
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
        private float timer;
        public float Timer
        {
            get { return timer; }
        }

        [SerializeField]
        private Season season;
        public Season Season
        {
            get { return season; }
        }




        [SerializeField]
        private int comboMax;
        public int ComboMax
        {
            get { return comboMax; }
            set { comboMax = value; }
        }

        [SerializeField]
        private EmotionStat deck;
        public EmotionStat Deck
        {
            get { return deck; }
            set { deck = value; }
        }

        [SerializeField]
        private EmotionStat atkBonus;
        public EmotionStat AtkBonus
        {
            get { return atkBonus; }
            set { atkBonus = value; }
        }
        [SerializeField]
        private EmotionStat defBonus;
        public EmotionStat DefBonus
        {
            get { return defBonus; }
            set { defBonus = value; }
        }

        [SerializeField]
        private int criticalRate;
        public int CriticalRate
        {
            get { return criticalRate; }
            set { criticalRate = value; }
        }

        [SerializeField]
        private int enemyHealthMalus;
        public int EnemyHealthMalus
        {
            get { return enemyHealthMalus; }
            set { enemyHealthMalus = value; }
        }

        [SerializeField]
        private int healthActorsBonus;
        public int HealthActorsBonus
        {
            get { return healthActorsBonus; }
            set { healthActorsBonus = value; }
        }

        [SerializeField]
        private int turnLimit;
        public int TurnLimit
        {
            get { return turnLimit; }
        }

        private DebugSave bla = null;

        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CreateList()
        {
            if (bla == null)
            {
                bla = new DebugSave(true);
                currentContract = null;

                voiceActors = new List<VoiceActor>(voiceActorsDebug.Count);
                for (int i = 0; i < voiceActorsDebug.Count; i++)
                {
                    voiceActors.Add(new VoiceActor(voiceActorsDebug[i]));
                }

                voiceActorsGacha = new List<VoiceActor>(voiceActorsGachaDebug.Count);
                for (int i = 0; i < voiceActorsGachaDebug.Count; i++)
                {
                    voiceActorsGacha.Add(new VoiceActor(voiceActorsGachaDebug[i]));
                }

                soundEngineers = new List<SoundEngineer>(soundEngineerDebug.Count);
                for (int i = 0; i < soundEngineerDebug.Count; i++)
                {
                    soundEngineers.Add(new SoundEngineer(soundEngineerDebug[i]));
                }


                contractAvailable = new List<Contract>(contractAvailableDebug.Count);
                for (int i = 0; i < contractAvailableDebug.Count; i++)
                {
                    contractAvailable.Add(new Contract(contractAvailableDebug[i]));
                }


                tutoEvent = new List<StoryEventData>(initialTutoEvent.Count);
                for (int i = 0; i < initialTutoEvent.Count; i++)
                {
                    tutoEvent.Add(initialTutoEvent[i]);
                }

                inventoryEquipement = new List<EquipementData>(inventoryDebug.Count);
                for (int i = 0; i < inventoryDebug.Count; i++)
                {
                    inventoryEquipement.Add(inventoryDebug[i]);
                }


                inventoryFormation = new List<FormationData>(formationDebug.Count);
                for (int i = 0; i < formationDebug.Count; i++)
                {
                    inventoryFormation.Add(formationDebug[i]);
                }

                contractAccepted = new List<Contract>(3);


                currentEquipement = new EquipementData[equipementCategories.Length];
                for (int i = 0; i < initialEquipement.Length; i++)
                {
                    currentEquipement[i] = initialEquipement[i];
                }

                CreateNewData();
                CheckContractTimeline();
                CheckEventsTimeline();
                GachaContract();
            }
            else
            {
                NextWeek();
            }

        }


        public void CreateNewData()
        {
            contractGacha = new List<ContractData>();
            contractGachaCooldown = new List<ContractCooldown>();
            nextStoryEvents = new List<StoryEventData>();
            nextStoryEventsStartWeek = new List<StoryEventData>();
            phoneStoryEvents = new List<StoryEventData>();

            date = new Date(week, month, year);

            season = StartSeason;
            money = startMoney;
            maintenance = startMaintenance;
            timer = 0;

            deck = new EmotionStat(initialDeck);
            comboMax = initialComboMax;
            atkBonus = new EmotionStat(initialAtkBonus);
            defBonus = new EmotionStat(initialDefBonus);

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
            CheckContractTimeline();
            CheckEventsTimeline();
            CheckContractCooldown();
            GachaContract();
        }


        public void CheckMonth()
        {
            if (date.week == monthDate[date.month - 1])
            {
                date.month += 1;
                PayMaintenance();
            }
        }

        public void CheckSeason()
        {
            switch (season)
            {
                case Season.Spring:
                    if (date.week == summerDate)
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
            for (int i = 0; i < contractAccepted.Count; i++)
            {
                if (contractAccepted[i] != null)
                {
                    contractAccepted[i].WeekRemaining -= 1;
                    if(contractAccepted[i].CurrentLine == contractAccepted[i].TotalLine)
                    {
                        continue;
                    }
                    if (contractAccepted[i].WeekRemaining <= 0)
                    {
                        contractAccepted[i] = null;
                    }
                }
            }
            for (int i = 0; i < contractAvailable.Count; i++)
            {
                contractAvailable[i].WeekRemaining -= 1;
                if (contractAvailable[i].WeekRemaining <= 0)
                {
                    contractAvailable.RemoveAt(i);
                }
            }
        }

        public void VoiceActorWork()
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                voiceActors[i].WorkForWeek(experienceCurve);
            }
        }


        public void CheckContractTimeline()
        {
            for (int i = 0; i < gameTimeline.ContractTimeline[date.week].contractsData.addContracts.Length; i++)
            {
                contractAvailable.Add(new Contract(gameTimeline.ContractTimeline[date.week].contractsData.addContracts[i]));
            }

            for (int i = 0; i < gameTimeline.ContractRandomTimeline[date.week].contractsData.addContracts.Length; i++)
            {
                contractGacha.Add(gameTimeline.ContractRandomTimeline[date.week].contractsData.addContracts[i]);
            }

        }

        public void CheckEventsTimeline()
        {
            for (int i = 0; i < gameTimeline.EventsTimeline[date.week].eventData.eventsPhone.Length; i++)
            {
                phoneStoryEvents.Add(gameTimeline.EventsTimeline[date.week].eventData.eventsPhone[i]);
            }

            for (int i = 0; i < gameTimeline.EventsTimeline[date.week].eventData.eventStartWeek.Length; i++)
            {
                nextStoryEventsStartWeek.Add(gameTimeline.EventsTimeline[date.week].eventData.eventStartWeek[i]);
            }

            for (int i = 0; i < gameTimeline.EventsTimeline[date.week].eventData.eventEndWeek.Length; i++)
            {
                nextStoryEvents.Add(gameTimeline.EventsTimeline[date.week].eventData.eventEndWeek[i]);
            }
        }


        public void GachaContract()
        {
            int nbContract = 0;
            if (contractAvailable.Count == 0)
            {
                nbContract = Random.Range(2, 4);
            }
            else if (contractAvailable.Count <= 3)
            {
                nbContract = Random.Range(1, 4);
            }
            else if (contractAvailable.Count <= 6)
            {
                nbContract = Random.Range(0, 3);
            }
            else if (contractAvailable.Count > 6)
            {
                nbContract = Random.Range(0, 2);
            }


            if (contractGacha.Count != 0)
            {
                nbContract = Mathf.Clamp(nbContract, 0, contractGacha.Count);
                for (int i = 0; i < nbContract; i++)
                {
                    int rand = Random.Range(0, contractGacha.Count);
                    Contract contractSelected = new Contract(contractGacha[rand]);
                    contractAvailable.Add(contractSelected);
                    contractGachaCooldown.Add(new ContractCooldown(contractGacha[rand], contractGacha[rand].WeekMax));
                    contractGacha.Remove(contractGacha[rand]);
                }
            }
        }

        public void CheckContractCooldown()
        {
            for (int i = 0; i < contractGachaCooldown.Count; i++)
            {
                contractGachaCooldown[i].cooldown -= 1;
                if (contractGachaCooldown[i].cooldown <= 0)
                {
                    contractGacha.Add(contractGachaCooldown[i].contract);
                    contractGachaCooldown.RemoveAt(i);
                }
            }
        }



        public VoiceActor GachaVoiceActors(Role role)
        {
            int playerRank = 1;
            int[] randomDraw = new int[voiceActorsGacha.Count + voiceActors.Count];
            int currentMaxValue = 0;

            // Calculate Actor Draw Chances
            for(int i = 0; i < voiceActorsGacha.Count; i++)
            {
                if(voiceActorsGacha[i].Level > playerRank + 3)
                {
                    randomDraw[i] = -1;
                }
                else
                {
                    currentMaxValue += CalculateVoiceActorGachaScore(role, voiceActorsGacha[i]);
                    randomDraw[i] = currentMaxValue;
                }
            }

            // Calculate Actor Draw Chances
            for (int i = 0; i < voiceActors.Count; i++)
            {
                if (voiceActors[i].Availability == false)
                    currentMaxValue = -1;
                else
                {
                    currentMaxValue += CalculateVoiceActorGachaScore(role, voiceActors[i], 0.5f);
                    randomDraw[voiceActorsGacha.Count + i] = currentMaxValue;
                }
            }

            // Draw
            int draw = Random.Range(0, currentMaxValue);
            VoiceActor result;
            Debug.Log(draw);
            for (int i = 0; i < randomDraw.Length; i++)
            {
                if(draw < randomDraw[i])
                {
                    if (i < voiceActorsGacha.Count)
                    {
                        Debug.Log(voiceActorsGacha[i].Name);
                        voiceActors.Add(voiceActorsGacha[i]);
                        result = voiceActorsGacha[i];
                        voiceActorsGacha.RemoveAt(i);
                        return result;
                    }
                    else
                    {
                        result = voiceActors[i - voiceActorsGacha.Count];
                        return result;
                    }
                }
            }
            return null;
        }

        private int CalculateVoiceActorGachaScore(Role role, VoiceActor va, float finalMultiplier = 1)
        {
            int finalScore = 1;

            int statRole = 0;
            int statActor = 0;
            int multiplier = 1;

            int statGain = 0;

            for (int i = 0; i < 8; i++)
            {
                multiplier = 1;
                statGain = 0;
                switch (i)
                {
                    case 0:
                        statRole = role.CharacterStat.Joy;
                        statActor = va.Statistique.Joy;
                        break;
                    case 1:
                        statRole = role.CharacterStat.Sadness;
                        statActor = va.Statistique.Sadness;
                        break;
                    case 2:
                        statRole = role.CharacterStat.Disgust;
                        statActor = va.Statistique.Disgust;
                        break;
                    case 3:
                        statRole = role.CharacterStat.Anger;
                        statActor = va.Statistique.Anger;
                        break;
                    case 4:
                        statRole = role.CharacterStat.Surprise;
                        statActor = va.Statistique.Surprise;
                        break;
                    case 5:
                        statRole = role.CharacterStat.Sweetness;
                        statActor = va.Statistique.Sweetness;
                        break;
                    case 6:
                        statRole = role.CharacterStat.Fear;
                        statActor = va.Statistique.Fear;
                        break;
                    case 7:
                        statRole = role.CharacterStat.Trust;
                        statActor = va.Statistique.Trust;
                        break;

                }

                if(statRole == role.BestStat)
                {
                    multiplier = 100;
                }
                else if(statRole == role.SecondBestStat)
                {
                    multiplier = 80;
                }

                if (statRole == statActor) // Si stat équivalente c'est la folie
                {
                    statGain = 20;
                }
                else if (statRole < statActor)
                {
                    statGain = 20 - Mathf.Abs(statActor - statRole);
                    if (statGain < 0)
                        statGain = 0;
                    statGain += 1;
                    if (statGain == 1 && multiplier != 1)
                        multiplier /= 2;
                }
                else if (statRole > statActor)
                {
                    statGain = 5 - Mathf.Abs(statRole - statActor);
                    if (statGain < 0)
                        statGain = 0;
                    if (multiplier != 1)
                        multiplier /= 2;
                }
                statGain *= multiplier;
                finalScore += statGain;
            }
            Debug.Log(va.Name + " | " + finalScore);
            return (int) (finalScore * finalMultiplier);
        }




        public void AddTimer()
        {
            timer += Time.time;
        }
        public void SubstractTimer()
        {
            timer -= Time.time;
        }

        public string GetTimeInHour()
        {
            float tmpTimer = timer + Time.time;
            string hourDecade = "0";
            string minuteDecade = "0";
            int hour = 0;
            int minute = 0;

            hour = (int)(tmpTimer / 3600);
            minute = (int)((tmpTimer / 60) % 60);

            if(hour >= 10)
            {
                hourDecade = "";
            }

            if (minute >= 10)
            {
                minuteDecade = "";
            }
            return hourDecade + hour + ":" + minuteDecade + minute;
        }


        // PlayerData class
    }
} // #PROJECTNAME# namespace