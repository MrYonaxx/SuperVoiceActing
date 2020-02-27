/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VoiceActing
{

    public class MenuManagementManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        InitialPlayerData playerDataDebug;
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        CalendarData calendarData;
        [SerializeField]
        private RandomEventDatabase randomEventDatabase;
        [SerializeField]
        private GameTimelineData gameTimelineData;
        [SerializeField]
        private int debugActorLevel = 20;

        [Header("Managers")]
        [SerializeField]
        private MenuActorsLifeManager menuActorsLifeManager;
        [SerializeField]
        private MenuManagementContract managementContract;

        [Header("Managers")]
        [SerializeField]
        private MenuContratManager contractManager;
        [SerializeField]
        private MenuContractInfo infoManager;
        [SerializeField]
        private MenuContractAvailable contractAvailable;
        [SerializeField]
        private MenuActorsManager actorsManagers;
        [SerializeField]
        private MenuStudioSoundEngiManager soundEngiManager;
        [SerializeField]
        private MenuStudioSoundEngiFormation soundEngiFormation;
        [SerializeField]
        private MenuStudioResearch researchManager;
        [SerializeField]
        private MenuContractMoney moneyManager;

        [Header("Managers")]
        [SerializeField]
        private GameObject menuStudioBlackscreen;
        [SerializeField]
        private MenuStudioMain menuStudioMain;

        [Header("a")]
        [SerializeField]
        private MenuNextWeek menuNextWeek;
        [SerializeField]
        private MenuContractEnd menuContractEnd;


        [Header("Event Start Week")]
        [SerializeField]
        StoryEventManager storyEventStartWeek;
        [SerializeField]
        GameObject storyEventTexture;
        [SerializeField]
        GameObject storyEventPrefab;

        [Header("Sound")]
        [SerializeField]
        private AudioClip defaultDekstopTheme;



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

        private void Start()
        {
            if(playerData.IsLoading == true)
            {
                playerData.IsLoading = false;
            }
            else
            {
                NextWeek();
            }

            if (playerData.NextStoryEventsStartWeek.Count == 0)
            {
                InitialiazeManagers();
            }
            else
            {
                storyEventPrefab.SetActive(true);
                storyEventTexture.SetActive(true);
                storyEventStartWeek.CreateScene(playerData.NextStoryEventsStartWeek[0]);
            }
            menuStudioMain.gameObject.SetActive(playerData.MenuStudioUnlocked);
            menuStudioBlackscreen.SetActive(!playerData.MenuStudioUnlocked);
            menuNextWeek.StartNextWeek(playerData.Date, playerData.Season);
        }

        // Initialise la phase de bureau
        private void InitialiazeManagers()
        {
            AudioManager.Instance.PlayMusic(defaultDekstopTheme);
            actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
            soundEngiManager.SetSoundEngiList(playerData.SoundEngineers);
            soundEngiFormation.CreateButtonFormation(playerData.InventoryFormation);
            researchManager.SetResearchPlayerLevels(playerData);
            moneyManager.DrawMoney(playerData.Money);

            infoManager.DrawDate(playerData.Date, (int)playerData.Season);
            infoManager.DrawObjective(playerData.CurrentChapter, playerData.CurrentObjective);
            infoManager.DrawInfo(0, actorsManagers.GetActorUnavailable());
            infoManager.DrawInfo(1, soundEngiManager.GetFormationComplete());
            infoManager.DrawResearch(playerData.ResearchPoint);
        }

        private void NextWeek()
        {
            // PlayerData.NextWeek
            if (playerData.CreateList(playerDataDebug, calendarData) == true)
            {
                // Si création de la liste
                managementContract.AddGachaContractsUnderLevel(playerData);
                playerData.LevelUpCharacters(debugActorLevel);
            }

            // Sinon nextWeek
            AddRandomEvents();
            gameTimelineData.CheckEventsTimeline(playerData);
            menuActorsLifeManager.VoiceActorWork(playerData.VoiceActors);

            managementContract.GachaContract(playerData);
            managementContract.ContractNextWeek(playerData);
            managementContract.CheckContractCooldown(playerData);


            playerData.SetBestActor();
        }


        // Appelé après l'anim de next week
        public void NextWeekEnd()
        {
            if (playerData.NextStoryEventsStartWeek.Count == 0)
            {
                CheckContractDone();
            }
            else
            {
                storyEventStartWeek.StartStoryEventDataWithScene(playerData.NextStoryEventsStartWeek[0]);
                playerData.NextStoryEventsStartWeek.RemoveAt(0);
            }
        }



        // Appelé après un event, soit on en fait un autre soit on part en jeu
        public void CheckNextStartEvent()
        {
            if (playerData.NextStoryEventsStartWeek.Count == 0)
            {
                InitialiazeManagers();
                storyEventPrefab.SetActive(false);
                storyEventTexture.SetActive(false);
                menuNextWeek.SkipTransition(); // Joue l'anim de next week
            }
            else
            {
                storyEventStartWeek.StartStoryEventDataWithScene(playerData.NextStoryEventsStartWeek[0]);
                playerData.NextStoryEventsStartWeek.RemoveAt(0);
            }
        }



        private void CheckContractDone()
        {
            // Bug fonction quand meme appelé quand on load
            StartCoroutine(CoroutineProgressAnimation());


        }

        private IEnumerator CoroutineProgressAnimation()
        {
            contractManager.SetContractList(playerData.ContractAccepted);
            contractManager.gameObject.SetActive(true);
            contractManager.ActivateInput(false);
            yield return new WaitForSeconds(1);
            yield return contractManager.ProgressMixingContract();
            yield return null;
            if (menuContractEnd.CheckContractDone(playerData) == false) // S'il y a des contrat terminés on check contract done sinon go phone
            {
                CheckPhoneEvents();
            }
        }



        // Appelé après l'anim des contrats
        public void EndContractDone()
        {
            menuContractEnd.EndContractDone();
            contractManager.DrawAvailableContract();
            contractManager.gameObject.SetActive(true);
            menuContractEnd.gameObject.SetActive(false);
            CheckPhoneEvents();
        }


        public void CheckPhoneEvents()
        {
            if (playerData.PhoneStoryEvents.Count != 0)
            {
                contractManager.StartPhoneEvent();
            }
            else
            {
                CheckMoneyGain();
            }
        }


        public void CheckMoneyGain()
        {
            contractManager.ActivateInput(true);
            //if (playerData.Date.week == playerData.MonthDate[playerData.Date.month - 2]) // Nouveau mois
            if (playerData.Date.week == calendarData.GetMonthDate(playerData.Date.month - 2)) // Nouveau mois
            {
                moneyManager.AddSalaryDatas("Entretien", -playerData.Maintenance);
            }
            moneyManager.DrawMoneyGain();
            infoManager.DrawResearch(playerData.ResearchPoint);
        }




        [ContextMenu("Draw Contract")]
        public void GachaContract()
        {
            managementContract.GachaContract(playerData);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);

        }


        [ContextMenu("Draw Contract")]
        public void GachaContractDebug(string contractName)
        {
            playerData.ContractGacha.Clear();
            playerData.ContractGacha.Add(contractName);
            managementContract.GachaContract(playerData);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);

        }















        private void AddRandomEvents()
        {
            randomEventDatabase.AddRandomEvent(playerData);
        }













        public void ReloadScene()
        {
            if(playerData.NextStoryEvents.Count == 0 && playerData.NextRandomEvent.Count == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else
                SceneManager.LoadScene("EventScene");
        }












        public void PlayDekstopTheme()
        {
            AudioManager.Instance.PlayMusic(defaultDekstopTheme);
        }


        #endregion

    } // MenuManagementManager class
	
}// #PROJECTNAME# namespace
