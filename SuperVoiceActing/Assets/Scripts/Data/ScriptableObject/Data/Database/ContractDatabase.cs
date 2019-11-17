﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{

    [System.Serializable]
    public class Franchise
    {
        [SerializeField]
        public bool Enabled;

        [Space]
        [HorizontalGroup("FranchiseSeries")]
        [SerializeField]
        public bool isSeries = false;
        [Space]
        [HorizontalGroup("FranchiseSeries")]
        [SerializeField]
        public bool canScale = true;



        [SerializeField]
        [HorizontalGroup("FranchiseRandomStart")]
        public int canStartFranchiseAtMin = 0;
        [SerializeField][HideLabel][HorizontalGroup("FranchiseRandomStart")]
        public int canStartAtFranchiseAtMax = 0;


        [Space]
        [HorizontalGroup("FranchiseReboot")]
        [ShowIf("isSeries", true)]
        [VerticalGroup("FranchiseReboot/Left")]
        [SerializeField]
        public bool canReboot = false;
        [ShowIf("canReboot", true)]
        [VerticalGroup("FranchiseReboot/Left")]
        [SerializeField]
        public int chanceOfRebootAtEachIteration = 0;
        [ShowIf("canReboot", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Left")]
        public bool alwaysRebootAtEnd = true;

        [Space]
        [ShowIf("isSeries", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Right")]
        public bool canRemaster = false;
        [ShowIf("canRemaster", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Right")]
        public int chanceOfRemasterAtEachIteration = 100;
        [ShowIf("canRemaster", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Right")]
        public bool alwaysRemasterAtEnd = true;


        [ShowIf("canRemaster", true)]
        [VerticalGroup("FranchiseReboot/Right")]
        [SerializeField]
        public string remasterSuffix = "";
        [ShowIf("canRemaster", true)]
        [VerticalGroup("FranchiseReboot/Right")]
        [SerializeField]
        public bool prefix = false;


        [Space]
        [HorizontalGroup("Franchise")]
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityScaling = false;
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityOrder = false;
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityRandom = false;

        [Space]
        [VerticalGroup("Franchise/Right")]
        [SerializeField]
        public List<ContractData> contractDatas;





        public bool FranchiseDirection(FranchiseSave franchiseSave)
        {
            int chanceReboot = Random.Range(0,100);
            int chanceRemaster = Random.Range(0, 100);
            /*if (canReboot == true && rebootAsRemaster == false) // Reboot
            {
            }
            else if (canReboot == true && rebootAsRemaster == true) // Remaster
            {
            }

            if(alwaysRebootAtEnd == true && (franchiseSave.CurrentFranchiseNumber % (contractDatas.Count+1)) == 0)
            {

            }*/

            return false;
        }


    }


    [CreateAssetMenu(fileName = "ContractDatabase", menuName = "Database/ContractDatabase", order = 1)]
    public class ContractDatabase: ScriptableObject
    {
        //Get tout les ContractData de l'onglet scriptableObject
        //[AssetList(Path = "/ScriptableObject/")]

        // Pour ajouter un bouton + sur le coté
        //[ListDrawerSettings(HideAddButton = true, OnTitleBarGUI = "DrawTitleBarGUI")]

        //[ValueDropdown("myValues")]




        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/Contrat/Francais", CustomFilterMethod = "GetFranchiseOnly")]
        [SerializeField]
        private List<ContractData> contracts;
        public List<ContractData> Contracts
        {
            get { return contracts; }
        }

        /*[SerializeField]
        <Franchise> franchises;*/

        [Title("Debug Data")]
        public int numberOfPub = 0;
        public int numberOfSeries = 0;
        public int numberOfFilm = 0;
        public int numberOfVideoGame = 0;
        public int numberOfOther = 0;

        public int[] curveContractLevel = new int[10];


        private bool GetFranchiseOnly(ContractData obj)
        {
            return obj.Franchise.Enabled;
        }

        [Button]
        private void CalculateDebugData()
        {
            numberOfPub = 0;
            numberOfSeries = 0;
            numberOfFilm = 0;
            numberOfVideoGame = 0;
            numberOfOther = 0;
            for(int i = 0; i < contracts.Count; i++)
            {
                curveContractLevel[contracts[i].Level] += 1;
                switch (contracts[i].ContractType)
                {
                    case ContractType.Film:
                        numberOfFilm+=1;
                        break;
                    case ContractType.JeuVideo:
                        numberOfVideoGame += 1;
                        break;
                    case ContractType.Publicite:
                        numberOfPub += 1;
                        break;
                    case ContractType.Serie:
                        numberOfSeries += 1;
                        break;
                    case ContractType.Autre:
                        numberOfOther += 1;
                        break;
                }
            }
        }


        public ContractData GetContractData(string contractName)
        {
            return contracts.Find(x => x.name == contractName);
        }


        /*public Franchise GetFranchise(string contractName)
        {
            for(int i = 0; i < franchises.Count; i++)
            {
                if(franchises[i].contractDatas[0].name == contractName)
                {
                    return franchises[i];
                }
            }
            return null;
        }*/


    } 

} // #PROJECTNAME# namespace