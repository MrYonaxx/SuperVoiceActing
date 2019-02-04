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
        private List<Contract> contractAvailableList = new List<Contract>(5);

        [SerializeField]
        private List<Contract> contractCurrentList = new List<Contract>(5);

        [Header("Prefab")]

        [SerializeField]
        GameObject[] contractAvailables;

        int indexCurrentList = 0;
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
            contractAvailableList.Add(new Contract());
            contractAvailableList.Add(new Contract());
            contractAvailableList.Add(null);
            contractAvailableList.Add(null);
            contractAvailableList.Add(null);
            DrawAvailableContract();
        }

        // Créer un Contract depuis ContractData
        public void CreateContract()
        {

        }

        public void DrawAvailableContract()
        {
            // à voir si j'instancie des prefab, mais pour 5 objets on va faire simple
            for(int i = 0; i < contractAvailableList.Count; i++)
            {
                if(contractAvailableList[i] != null)
                {
                    contractAvailables[i].SetActive(true);
                }
            }
        }



        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace