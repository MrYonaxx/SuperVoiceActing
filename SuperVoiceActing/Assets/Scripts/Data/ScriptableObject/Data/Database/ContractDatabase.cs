/*****************************************************************
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