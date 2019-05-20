/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace VoiceActing
{


    /// <summary>
    /// Definition of the MenuContratManager class
    /// </summary>
    public class MenuContratManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private PlayerData playerData;

        // [AssetList(Path = "Plugins/Sirenix/")] pour chopper tout les asset d'un repertoire et remplir automatiquement un truc
        //[SerializeField]
        //private ContractData[] contractDatabase;

        [SerializeField]
        private List<Contract> contractAcceptedList = new List<Contract>(3);

        [Header("Prefab")]

        [SerializeField]
        ButtonContratAccepted[] buttonContractAccepted;

        int indexAcceptedList = 0;



        [Header("MenuManagers")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContractAvailable menuContractAvailable;
        [SerializeField]
        MenuContractPreparation menuContractPreparation;
        [SerializeField]
        MenuManagementManager menuManagementManager;

        [Header("MenuInfo")]

        [SerializeField]
        TextMeshProUGUI textWeek;
        [SerializeField]
        TextMeshProUGUI textMonth;
        [SerializeField]
        TextMeshProUGUI textNextMonth;
        [SerializeField]
        TextMeshProUGUI textYear;
        [SerializeField]
        Image imageSeasonOutline;
        [SerializeField]
        Image imageSeason;

        [SerializeField]
        Sprite[] spriteSeason;

        [Header("EventPhone")]
        [SerializeField]
        GameObject phoneObject;
        [SerializeField]
        StoryEventManager storyEventManager;


        bool eventPhone = false;


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

        private void OnEnable()
        {
            animatorMenu.SetBool("Appear", true);
            animatorMenu.gameObject.SetActive(true);
        }

        public void StartPhoneEvent()
        {
            phoneObject.SetActive(true);
            storyEventManager.StartStoryEventData(playerData.PhoneStoryEvents[0]);
            playerData.PhoneStoryEvents.RemoveAt(0);
            this.gameObject.SetActive(false);
        }

        public void SetContractList(List<Contract> contracts)
        {
            contractAcceptedList = contracts;
            DrawAvailableContract();
        }


        public void DrawDate()
        {
            textWeek.text = playerData.Date.week.ToString();
            textMonth.text = playerData.MonthName[playerData.Date.month - 1];
            textYear.text = playerData.Date.year.ToString();
            textNextMonth.text = (playerData.MonthDate[playerData.Date.month - 1] - playerData.Date.week).ToString();
            switch (playerData.Season)
            {
                case Season.Spring:
                    imageSeason.sprite = spriteSeason[0];
                    break;
                case Season.Summer:
                    imageSeason.sprite = spriteSeason[1];
                    break;
                case Season.Autumn:
                    imageSeason.sprite = spriteSeason[2];
                    break;
                case Season.Winter:
                    imageSeason.sprite = spriteSeason[3];
                    break;
            }
            imageSeasonOutline.sprite = imageSeason.sprite;
        }

        public void DrawAvailableContract()
        {
            buttonContractAccepted[0].gameObject.SetActive(true);
            buttonContractAccepted[0].SetButtonToAddContract();

            // à voir si j'instancie des prefab, mais pour 5 objets on va faire simple
            for (int i = 0; i < contractAcceptedList.Count; i++)
            {
                if(contractAcceptedList[i] != null)
                {
                    //contractAccepted[i].gameObject.SetActive(true);
                    buttonContractAccepted[i].DrawContract(contractAcceptedList[i]);
                    if (i + 1 < buttonContractAccepted.Length)
                    {
                        buttonContractAccepted[i + 1].gameObject.SetActive(true);
                        buttonContractAccepted[i + 1].SetButtonToAddContract();
                    }

                }
            }
        }

        public bool AddContractToList(Contract newContract)
        {
            for(int i = 0; i < contractAcceptedList.Count; i++)
            {
                if (contractAcceptedList[i] == null)
                {
                    contractAcceptedList[i] = newContract;
                    DrawAvailableContract();
                    CheckEventWhenAccepted(newContract);
                    CheckCharacterLock(newContract);
                    return true;
                }
            }
            return false;
        }


        public void CheckEventWhenAccepted(Contract newContract)
        {
            if(newContract.StoryEventWhenAccepted != null)
            {
                phoneObject.SetActive(true);
                storyEventManager.StartStoryEventData(newContract.StoryEventWhenAccepted);
                eventPhone = true;
            }
        }

        public void CheckCharacterLock(Contract newContract)
        {
            for(int i = 0; i < newContract.Characters.Count; i++)
            {
                if(newContract.Characters[i].CharacterLock != null)
                {
                    // On cherche l'acteur dans le monde
                    for(int j = 0; j < playerData.VoiceActors.Count; j++)
                    {
                        if(newContract.Characters[i].CharacterLock == playerData.VoiceActors[j].Name)
                        {
                            newContract.VoiceActors[i] = playerData.VoiceActors[j];
                            continue;
                        }
                    }
                }
            }
        }








        // SELECTION UI
        // =================================================================================================================================


        public void Validate()
        {
            if(contractAcceptedList[indexAcceptedList] == null)
            {
                SwitchToMenuContractAvailable();
            }
            else
            {
                SwitchToMenuContractPreparation();
                menuContractPreparation.SetContract(contractAcceptedList[indexAcceptedList]);
            }
        }

        [ContextMenu("SelectionUp")]
        public void SelectContractAcceptedUp()
        {
            buttonContractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList -= 1;
            if(indexAcceptedList == -1)
            {
                indexAcceptedList = contractAcceptedList.Count-1;
                while (buttonContractAccepted[indexAcceptedList].gameObject.activeInHierarchy == false)
                {
                    indexAcceptedList -= 1;
                }
            }
            buttonContractAccepted[indexAcceptedList].SelectContract();
        }

        [ContextMenu("SelectionDown")]
        public void SelectContractAcceptedDown()
        {
            buttonContractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList += 1;
            if(indexAcceptedList == contractAcceptedList.Count)
            {
                indexAcceptedList = 0;
            }
            else if (buttonContractAccepted[indexAcceptedList].gameObject.activeInHierarchy == false)
            {
                indexAcceptedList = 0;
            }
            buttonContractAccepted[indexAcceptedList].SelectContract();
        }









        private void SwitchToMenuContractAvailable()
        {
            menuContractAvailable.gameObject.SetActive(true);
            animatorMenu.SetBool("Appear", false);
            this.gameObject.SetActive(false);
        }

        private void SwitchToMenuContractPreparation()
        {
            menuContractPreparation.gameObject.SetActive(true);
            animatorMenu.SetBool("Appear", false);
            this.gameObject.SetActive(false);
        }

        public void CheckPhoneEvent()
        {
            if(eventPhone == true)
            {
                this.gameObject.SetActive(false);
                eventPhone = false;
            }
        }


        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace