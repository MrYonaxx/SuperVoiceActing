/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [Header("Managers")]
        [SerializeField]
        private MenuContratManager contractManager;
        [SerializeField]
        private MenuContractAvailable contractAvailable;
        [SerializeField]
        private MenuActorsManager actorsManagers;
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
            moneyManager.DrawMoney(playerData.Money);
            playerData.CreateList();
            contractManager.DrawDate();
            actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);


            if(playerData.NextStoryEventsStartWeek.Count == 0)
            {
                AudioManager.Instance.PlayMusic(defaultDekstopTheme);
            }
            else
            {
                storyEventPrefab.SetActive(true);
                storyEventTexture.SetActive(true);
                storyEventStartWeek.CreateScene(playerData.NextStoryEventsStartWeek[0]);
            }
            menuNextWeek.StartNextWeek();



            //menuNextWeek.gameObject.SetActive(true);
            //menuNextWeek.StartNextWeek();
                //menuNextWeek.SkipTransition();


        }


        // Appelé par la manip de debug pour réafficher les listes
        public void RefreshData()
        {
            //actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.DestroyButtonList();
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
        }



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




        public void CheckNextStartEvent()
        {
            if (playerData.NextStoryEventsStartWeek.Count == 0)
            {
                storyEventPrefab.SetActive(false);
                storyEventTexture.SetActive(false);
                menuNextWeek.SkipTransition();
            }
            else
            {
                storyEventStartWeek.StartStoryEventDataWithScene(playerData.NextStoryEventsStartWeek[0]);
                playerData.NextStoryEventsStartWeek.RemoveAt(0);
            }
        }




        // Appelé après l'anim de next Week
        public void CheckContractDone()
        {
            if (menuContractEnd.CheckContractDone(playerData) == true)
            {
                // Stop Input
                menuContractEnd.gameObject.SetActive(true);
                menuContractEnd.DrawAllContracts();
            }
            else
            {
                contractManager.gameObject.SetActive(true);
                CheckPhoneEvents();
                //CheckMoneyGain();
            }
            contractManager.SetContractList(playerData.ContractAccepted);
        }

        public void CheckMoneyGain()
        {
            if (playerData.Date.week == playerData.MonthDate[playerData.Date.month - 2]) // Nouveau mois
            {
                Debug.Log(playerData.Date.week);
                moneyManager.AddSalaryDatas("Entretien", -playerData.Maintenance);
            }
            //CheckPhoneEvents();
            moneyManager.DrawMoneyGain();

        }

        // Appelé après l'anim des contrats
        public void EndContractDone()
        {
            menuContractEnd.EndContractDone();
            contractManager.gameObject.SetActive(true);
            menuContractEnd.gameObject.SetActive(false);
            CheckPhoneEvents();
            //CheckMoneyGain();
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


        #endregion

    } // MenuManagementManager class
	
}// #PROJECTNAME# namespace
