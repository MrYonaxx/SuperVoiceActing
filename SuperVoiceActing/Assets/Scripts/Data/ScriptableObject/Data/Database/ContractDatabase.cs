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

    [System.Serializable]
    public class Franchise
    {
        [SerializeField]
        public bool Enabled;

        [SerializeField]
        public bool canScale = true;

        [Space]
        [HorizontalGroup("FranchiseSeries")]
        [SerializeField]
        public bool isSeries = false;

        [Space]
        [HorizontalGroup("FranchiseSeries")]
        [ShowIf("isSeries", true)]
        [SerializeField]
        public bool canReboot = false;

        [ShowIf("canReboot", true)]
        [SerializeField]
        public string rebootSuffix = "";
        [ShowIf("canReboot", true)]
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



        private bool GetFranchiseOnly(ContractData obj)
        {
            return obj.Franchise.Enabled;
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