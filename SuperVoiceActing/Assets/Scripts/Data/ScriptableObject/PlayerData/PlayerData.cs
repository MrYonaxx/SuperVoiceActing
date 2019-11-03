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



        public void NextWeek()
        {
            week += 1;
            if (week > 52)
            {
                week = 1;
                month = 1;
                year += 1;
            }
        }

        public void NextWeek(CalendarData calendar)
        {
            NextWeek();
            if(calendar.CheckMonth(week, month))
            {
                month += 1;
            }
        }
    }


    [System.Serializable]
    public class ContractCooldown
    {
        public string contract;
        public int cooldown;

        public ContractCooldown(string cd, int c)
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
        private ExperienceCurveData experienceCurve;
        public ExperienceCurveData ExperienceCurve
        {
            get { return experienceCurve; }
        }



        [HorizontalGroup("InitialEquipement")]
        [SerializeField]
        private EquipementCategory[] equipementCategories;
        public EquipementCategory[] EquipementCategories
        {
            get { return equipementCategories; }
        }

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


        // Contrat en cours à doubler  ------------------------------------------------------------------------------------------
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

        // Contrat Realise 
        [SerializeField]
        private List<Contract> contractHistoric;
        public List<Contract> ContractHistoric
        {
            get { return contractHistoric; }
            set { contractHistoric = value; }
        }

        // x = contractID, y = franchise series
        [SerializeField]
        private List<Vector2> contractFranchise;
        public List<Vector2> ContractFranchise
        {
            get { return contractFranchise; }
            set { contractFranchise = value; }
        }




        // Contrat Gacha
        [SerializeField]
        private List<string> contractGacha;
        public List<string> ContractGacha
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



        // Liste des Acteurs ------------------------------------------------------------------------------------------
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


        // Liste des IngéSon  ------------------------------------------------------------------------------------------
        [SerializeField]
        private List<SoundEngineer> soundEngineers;
        public List<SoundEngineer> SoundEngineers
        {
            get { return soundEngineers; }
            set { soundEngineers = value; }
        }


        // List Story  ------------------------------------------------------------------------------------------
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
        private List<int> tutoEvent;
        public List<int> TutoEvent
        {
            get { return tutoEvent; }
            set { tutoEvent = value; }
        }



        [SerializeField]
        private List<int> nextRandomEvent;
        public List<int> NextRandomEvent
        {
            get { return nextRandomEvent; }
            set { nextRandomEvent = value; }
        }

        /*[SerializeField]
        private List<int> randomEventsStandby;
        public List<int> RandomEventsStandby
        {
            get { return randomEventsStandby; }
            set { randomEventsStandby = value; }
        }*/




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
        private ResearchPlayerLevel[] researchesLevels;
        public ResearchPlayerLevel[] ResearchesLevels
        {
            get { return researchesLevels; }
        }

        [SerializeField]
        private bool[] gameManualUnlocked;
        public bool[] GameManualUnlocked
        {
            get { return gameManualUnlocked; }
        }

        [SerializeField]
        private bool[] storyResumeUnlocked;
        public bool[] StoryResumeUnlocked
        {
            get { return storyResumeUnlocked; }
        }



        private Sprite currentBestActor;
        public Sprite CurrentBestActor
        {
            get { return currentBestActor; }
            set { currentBestActor = value; }
        }


        [SerializeField]
        private int currentChapter;
        public int CurrentChapter
        {
            get { return currentChapter; }
        }
        [SerializeField]
        private string currentObjective;
        public string CurrentObjective
        {
            get { return currentObjective; }
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
        private int researchPoint;
        public int ResearchPoint
        {
            get { return researchPoint; }
            set { researchPoint = value; }
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
            set { turnLimit = value; }
        }




        [SerializeField]
        private bool menuStudioUnlocked = false;
        public bool MenuStudioUnlocked
        {
            get { return menuStudioUnlocked; }
            set { menuStudioUnlocked = value; }
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


        public void CreatePlayerData(InitialPlayerData initialPlayerData)
        {
            voiceActors.Clear();
            voiceActorsGacha.Clear();
            contractAccepted.Clear();
            soundEngineers.Clear();
            tutoEvent.Clear();
            inventoryEquipement.Clear();
            inventoryFormation.Clear();
            contractGacha.Clear();
            contractGachaCooldown.Clear();
            nextStoryEvents.Clear();
            nextStoryEventsStartWeek.Clear();
            phoneStoryEvents.Clear();

            currentContract = new Contract();
            contractAccepted = new List<Contract>(3);

            voiceActors = new List<VoiceActor>(initialPlayerData.VoiceActorsDebug.Count);
            for (int i = 0; i < initialPlayerData.VoiceActorsDebug.Count; i++)
            {
                voiceActors.Add(new VoiceActor(initialPlayerData.VoiceActorsDebug[i]));
            }

            voiceActorsGacha = new List<VoiceActor>(initialPlayerData.VoiceActorsGachaDebug.Count);
            for (int i = 0; i < initialPlayerData.VoiceActorsGachaDebug.Count; i++)
            {
                voiceActorsGacha.Add(new VoiceActor(initialPlayerData.VoiceActorsGachaDebug[i]));
            }

            soundEngineers = new List<SoundEngineer>(initialPlayerData.SoundEngineerDebug.Count);
            for (int i = 0; i < initialPlayerData.SoundEngineerDebug.Count; i++)
            {
                soundEngineers.Add(new SoundEngineer(initialPlayerData.SoundEngineerDebug[i]));
            }

            contractAvailable = new List<Contract>(initialPlayerData.ContractAvailableDebug.Count);
            for (int i = 0; i < initialPlayerData.ContractAvailableDebug.Count; i++)
            {
                contractAvailable.Add(new Contract(initialPlayerData.ContractAvailableDebug[i]));
            }

            tutoEvent = new List<int>(initialPlayerData.InitialTutoEvent.Count);
            for (int i = 0; i < initialPlayerData.InitialTutoEvent.Count; i++)
            {
                tutoEvent.Add(initialPlayerData.InitialTutoEvent[i]);
            }

            inventoryEquipement = new List<EquipementData>(initialPlayerData.InventoryDebug.Count);
            for (int i = 0; i < initialPlayerData.InventoryDebug.Count; i++)
            {
                inventoryEquipement.Add(initialPlayerData.InventoryDebug[i]);
            }

            inventoryFormation = new List<FormationData>(initialPlayerData.FormationDebug.Count);
            for (int i = 0; i < initialPlayerData.FormationDebug.Count; i++)
            {
                inventoryFormation.Add(initialPlayerData.FormationDebug[i]);
            }

            currentEquipement = new EquipementData[initialPlayerData.InitialEquipement.Length];
            for (int i = 0; i < initialPlayerData.InitialEquipement.Length; i++)
            {
                currentEquipement[i] = initialPlayerData.InitialEquipement[i];
            }

            equipementCategories = new EquipementCategory[initialPlayerData.EquipementCategories.Length];
            for (int i = 0; i < initialPlayerData.EquipementCategories.Length; i++)
            {
                equipementCategories[i] = initialPlayerData.EquipementCategories[i];
            }



            contractGacha = new List<string>();
            contractGachaCooldown = new List<ContractCooldown>();
            nextStoryEvents = new List<StoryEventData>();
            nextStoryEventsStartWeek = new List<StoryEventData>();
            phoneStoryEvents = new List<StoryEventData>();
            nextRandomEvent = new List<int>();

            date = new Date(initialPlayerData.Week, initialPlayerData.Month, initialPlayerData.Year);

            season = initialPlayerData.StartSeason;
            money = initialPlayerData.StartMoney;
            maintenance = initialPlayerData.StartMaintenance;

            comboMax = initialPlayerData.InitialComboMax;
            deck = new EmotionStat(initialPlayerData.InitialDeck);
            turnLimit = initialPlayerData.InitialTurn;
            atkBonus = new EmotionStat(initialPlayerData.InitialAtkBonus);
            defBonus = new EmotionStat(initialPlayerData.InitialDefBonus);

            timer = 0;
            menuStudioUnlocked = initialPlayerData.MenuStudioUnlocked;

            researchPoint = initialPlayerData.StartResearchPoint;
            researchesLevels = new ResearchPlayerLevel[initialPlayerData.ResearchDatabase.ResearchesData.Length];
            for(int i = 0; i < initialPlayerData.ResearchDatabase.ResearchesData.Length; i++)
            {
                researchesLevels[i] = new ResearchPlayerLevel(initialPlayerData.ResearchDatabase.ResearchesData[i].Researches.Length);
                for(int j = 0; j < initialPlayerData.ResearchDatabase.ResearchesData[i].Researches.Length; j++)
                {
                    researchesLevels[i].ResearchLevel[j] = 0;
                }
            }

            gameManualUnlocked = new bool[initialPlayerData.GameManualDatabase.RecapDatas.Length];
            for (int i = 0; i < initialPlayerData.GameManualDatabase.RecapDatas.Length; i++)
            {
                gameManualUnlocked[i] = false;
            }
            storyResumeUnlocked = new bool[initialPlayerData.StoryResumeDatabase.RecapDatas.Length];
            for (int i = 0; i < initialPlayerData.StoryResumeDatabase.RecapDatas.Length; i++)
            {
                storyResumeUnlocked[i] = false;
            }
        }

        public void CreateList(InitialPlayerData initialPlayerData)
        {
            if (bla == null)
            {
                bla = new DebugSave(true);
                CreatePlayerData(initialPlayerData);
            }
        }
        public void CreateList(InitialPlayerData initialPlayerData, CalendarData calendarData)
        {
            if (bla == null)
            {
                bla = new DebugSave(true);
                CreatePlayerData(initialPlayerData);
            }
            else
            {
                NextWeek(calendarData);
            }

        }

        public void ResetDebugSave()
        {
            bla = null;
        }

        public bool GetPlayerDebugSave()
        {
            return (bla == null);
        }

        public void SetLoading()
        {
            isLoading = true;
            bla = new DebugSave(true);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void PayMaintenance()
        {
            money -= maintenance;
        }

        public void NextWeek(CalendarData calendar)
        {
            date.NextWeek(calendar);
            if (calendar.CheckMonth(date.week) == true)
                PayMaintenance();
            calendar.CheckSeason(season, date.week);
        }



        public void VoiceActorWork()
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                voiceActors[i].WorkForWeek(experienceCurve);
            }
        }






        public VoiceActor GachaVoiceActors(Role role)
        {
            int playerRank = 20;
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






        public void SetBestActor()
        {
            if(currentContract.IsNull == false)
                currentBestActor = currentContract.VoiceActors[Random.Range(0, CurrentContract.VoiceActors.Count)].SpriteSheets.SpriteIcon;
        }

        public void SetNewChapter(int newChapter, string newObjective)
        {
            currentChapter = newChapter;
            currentObjective = newObjective;
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