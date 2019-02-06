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

        [SerializeField]
        private ContractData[] contractDatabase;

        [SerializeField]
        private List<Contract> contractAcceptedList = new List<Contract>(5);

        [SerializeField]
        private List<Contract> contractAvailableList = new List<Contract>(5);

        [Header("Prefab")]

        [SerializeField]
        ButtonContratAccepted[] contractAccepted;

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
            contractAcceptedList.Add(new Contract("C'est moi"));
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
            // à voir si j'instancie des prefab, mais pour 5 objets on va faire simple
            for(int i = 0; i < contractAcceptedList.Count; i++)
            {
                if(contractAcceptedList[i] != null)
                {
                    contractAccepted[i].gameObject.SetActive(true);
                    contractAccepted[i].DrawContract(contractAcceptedList[i]);
                }
            }
        }

        [ContextMenu("SelectionUp")]
        public void SelectContractAcceptedUp()
        {
            contractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList -= 1;
            if(indexAcceptedList == -1)
            {
                indexAcceptedList = contractAccepted.Length-1;
            }
            contractAccepted[indexAcceptedList].SelectContract();
        }

        [ContextMenu("SelectionDown")]
        public void SelectContractAcceptedDown()
        {
            contractAccepted[indexAcceptedList].UnSelectContract();
            indexAcceptedList += 1;
            if (contractAcceptedList[indexAcceptedList] == null)
            {
                indexAcceptedList = 0;
            }
            contractAccepted[indexAcceptedList].SelectContract();
        }



        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace