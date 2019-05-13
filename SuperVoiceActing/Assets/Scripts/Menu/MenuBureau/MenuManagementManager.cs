﻿/*****************************************************************
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
            //moneyManager.AddSalaryDatas("Entretien", )
            contractManager.DrawDate();
            AudioManager.Instance.PlayMusic(defaultDekstopTheme);
            actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
            menuNextWeek.StartNextWeek();

        }


        // Appelé par la manip de debug pour réafficher les listes
        public void RefreshData()
        {
            //actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.DestroyButtonList();
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
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
                CheckMoneyGain();
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

            moneyManager.DrawMoneyGain();

        }

        // Appelé après l'anim des contrats
        public void EndContractDone()
        {
            menuContractEnd.EndContractDone();
            contractManager.gameObject.SetActive(true);
            menuContractEnd.gameObject.SetActive(false);
            CheckMoneyGain();
        }


        #endregion

    } // MenuManagementManager class
	
}// #PROJECTNAME# namespace
