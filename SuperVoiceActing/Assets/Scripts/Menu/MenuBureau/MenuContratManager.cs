/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        // [AssetList(Path = "Plugins/Sirenix/")] pour chopper tout les asset d'un repertoire et remplir automatiquement un truc
        [SerializeField]
        private ContractData[] contractDatabase;

        [SerializeField]
        private List<Contract> contractAcceptedList = new List<Contract>(3);

        /*[SerializeField]
        private List<Contract> contractAvailableList = new List<Contract>(5);*/

        [Header("Prefab")]

        [SerializeField]
        ButtonContratAccepted[] buttonContractAccepted;

        int indexAcceptedList = 0;


        [Header("MenuManagers")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContractAvailable menuContractAvailable;


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
            //animatorMenu.disabled = false
        }


        private void Start()
        {
            contractAcceptedList.Add(new Contract("Salut"));
            contractAcceptedList.Add(new Contract("Hello"));
            contractAcceptedList.Add(null);
            DrawAvailableContract();
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

        public void Validate()
        {
            if(contractAcceptedList[indexAcceptedList] == null)
            {
                SwitchToMenuContractAvailable();
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
            //animatorMenu.SetBool("Appear", false);
            //animatorMenu.gameObject.SetActive(false);
        }


        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace