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
        [SerializeField]
        private ImageDictionnary typeContractData;

        /*[SerializeField]*/
        private List<Contract> contractAcceptedList = new List<Contract>(3);

        [Header("Prefab")]

        [SerializeField]
        ButtonContratAccepted[] buttonContractAccepted;

        int indexAcceptedList = 0;

        [SerializeField]
        InputController inputController;

        [Header("MenuManagers")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContractAvailable menuContractAvailable;
        [SerializeField]
        MenuContractPreparation menuContractPreparation;
        [SerializeField]
        MenuManagementManager menuManagementManager;
        [SerializeField]
        MenuActorsManager menuActorsManager;


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




        public void DrawAvailableContract()
        {          
            for(int i = contractAcceptedList.Count; i < buttonContractAccepted.Length; i++ )
            {
                buttonContractAccepted[i].gameObject.SetActive(false);
            }

            // à voir si j'instancie des prefab, mais pour 5 objets on va faire simple
            for (int i = 0; i < contractAcceptedList.Count; i++)
            {
                buttonContractAccepted[i].DrawContract(contractAcceptedList[i], typeContractData.GetSprite((int)contractAcceptedList[i].ContractType));
                buttonContractAccepted[i].SetButtonIndex(i);
            }
            if (contractAcceptedList.Count < 3)
            {
                buttonContractAccepted[contractAcceptedList.Count].gameObject.SetActive(true);
                buttonContractAccepted[contractAcceptedList.Count].SetButtonToAddContract();
            }
        }

        public bool AddContractToList(Contract newContract)
        {
            contractAcceptedList.Add(newContract);
            DrawAvailableContract();
            CheckEventWhenAccepted(newContract);
            if(newContract.CheckCharacterLock(playerData.VoiceActors) == true) // true == acteurs à instancier;
                menuActorsManager.DestroyButtonList();
            return true;
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

        // ``A déplacer




        public IEnumerator ProgressMixingContract()
        {
            for (int i = 0; i < contractAcceptedList.Count; i++)
            {
                contractAcceptedList[i].ProgressMixing();
                if(contractAcceptedList[i].SoundEngineer != null)
                    yield return buttonContractAccepted[i].CoroutineProgress(contractAcceptedList[i].CurrentMixing, contractAcceptedList[i].TotalMixing);
                yield return new WaitForSeconds(0.2f);
            }
        }



        public void ActivateInput(bool b)
        {
            inputController.gameObject.SetActive(b);
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
            if (eventPhone == true)
            {
                this.gameObject.SetActive(false);
                eventPhone = false;
            }
        }














        // SELECTION UI
        // =================================================================================================================================


        public void Validate()
        {
            if(indexAcceptedList == contractAcceptedList.Count)
            {
                SwitchToMenuContractAvailable();
            }
            else
            {
                SwitchToMenuContractPreparation();
                menuContractPreparation.SetContract(contractAcceptedList[indexAcceptedList]);
            }
        }

        public void Validate(int index)
        {
            indexAcceptedList = index;
            if (indexAcceptedList == contractAcceptedList.Count)
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
                if (contractAcceptedList.Count == 3)
                {
                    indexAcceptedList = contractAcceptedList.Count-1;
                }
                else
                {
                    indexAcceptedList = contractAcceptedList.Count;
                }
            }
            buttonContractAccepted[indexAcceptedList].SelectContract();
        }

        [ContextMenu("SelectionDown")]
        public void SelectContractAcceptedDown()
        {
            buttonContractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList += 1;
            if (indexAcceptedList == 3)
            {
                indexAcceptedList = 0;
            }
            else if (indexAcceptedList == contractAcceptedList.Count+1)
            {
                indexAcceptedList = 0;
            }
            buttonContractAccepted[indexAcceptedList].SelectContract();
        }











        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace