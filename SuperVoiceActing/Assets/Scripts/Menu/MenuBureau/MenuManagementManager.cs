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
        PlayerData playerData;
        [SerializeField]
        private RandomEventDatabase randomEventDatabase;

        [Header("Managers")]
        [SerializeField]
        private MenuContratManager contractManager;
        [SerializeField]
        private MenuContractAvailable contractAvailable;
        [SerializeField]
        private MenuActorsManager actorsManagers;
        [SerializeField]
        private MenuStudioSoundEngiManager soundEngiManager;
        [SerializeField]
        private MenuStudioSoundEngiFormation soundEngiFormation;
        [SerializeField]
        private MenuContractMoney moneyManager;

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
                playerData.CreateList();
                AddRandomEvents();
            }
            moneyManager.DrawMoney(playerData.Money);
            contractManager.DrawDate();


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
            menuNextWeek.StartNextWeek();
        }

        private void InitialiazeManagers()
        {
            AudioManager.Instance.PlayMusic(defaultDekstopTheme);
            actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
            soundEngiManager.SetSoundEngiList(playerData.SoundEngineers);
            soundEngiFormation.CreateButtonFormation(playerData.InventoryFormation);
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
            if (menuContractEnd.CheckContractDone(playerData) == false)
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
            if (playerData.Date.week == playerData.MonthDate[playerData.Date.month - 2]) // Nouveau mois
            {
                moneyManager.AddSalaryDatas("Entretien", -playerData.Maintenance);
            }
            moneyManager.DrawMoneyGain();
        }



        private void AddRandomEvents()
        {
            StoryEventData randomEvent = randomEventDatabase.GetStoryRandomEvent(playerData);
            if (randomEvent != null)
                playerData.NextStoryEvents.Add(randomEvent);
        }













        public void ReloadScene()
        {
            if(playerData.NextStoryEvents.Count == 0)
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
