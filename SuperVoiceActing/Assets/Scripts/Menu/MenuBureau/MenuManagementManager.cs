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
            playerData.CreateList();
            contractManager.DrawDate();
            contractManager.DrawMoney();
            AudioManager.Instance.PlayMusic(defaultDekstopTheme);
            actorsManagers.SetListActors(playerData.VoiceActors);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
            menuNextWeek.StartNextWeek();




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

        }

        // Appelé après l'anim des contrats
        public void EndContractDone()
        {
            menuContractEnd.EndContractDone();
            contractManager.gameObject.SetActive(true);
            menuContractEnd.gameObject.SetActive(false);
            CheckMoneyGain();
        }

        /*public void SaveToPlayerData()
        {

        }*/

        #endregion

    } // MenuManagementManager class
	
}// #PROJECTNAME# namespace
