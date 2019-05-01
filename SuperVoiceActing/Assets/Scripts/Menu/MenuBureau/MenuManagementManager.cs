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
            actorsManagers.SetListActors(playerData.VoiceActors);
            contractManager.SetContractList(playerData.ContractAccepted);
            contractAvailable.SetContractAvailable(playerData.ContractAvailable);
            AudioManager.Instance.PlayMusic(defaultDekstopTheme);
            menuNextWeek.StartNextWeek();
        }


        /*public void SaveToPlayerData()
        {

        }*/

        #endregion

    } // MenuManagementManager class
	
}// #PROJECTNAME# namespace
