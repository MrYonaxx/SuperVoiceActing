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
        private List<Contract> contractAcceptedList = new List<Contract>(5);

        [SerializeField]
        private List<Contract> contractAvailableList = new List<Contract>(5);

        [Header("Prefab")]

        [SerializeField]
        ButtonContratAccepted[] buttonContractAccepted;

        int indexAcceptedList = 0;
        int indexAvailableList = 0;



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
            contractAcceptedList.Add(new Contract("Salut"));
            contractAcceptedList.Add(null);
            contractAcceptedList.Add(null);
            contractAcceptedList.Add(null);
            contractAcceptedList.Add(null);
            DrawAvailableContract();
        }

        // Créer un Contract depuis ContractData
        public void CreateContract()
        {

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

        [ContextMenu("SelectionUp")]
        public void SelectContractAcceptedUp()
        {
            buttonContractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList -= 1;
            if(indexAcceptedList == -1)
            {
                indexAcceptedList = buttonContractAccepted.Length-1;
            }
            buttonContractAccepted[indexAcceptedList].SelectContract();
        }

        [ContextMenu("SelectionDown")]
        public void SelectContractAcceptedDown()
        {
            buttonContractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList += 1;
            if (buttonContractAccepted[indexAcceptedList] == null || buttonContractAccepted[indexAcceptedList].gameObject.activeInHierarchy == false)
            {
                indexAcceptedList = 0;
            }
            buttonContractAccepted[indexAcceptedList].SelectContract();
        }



        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace