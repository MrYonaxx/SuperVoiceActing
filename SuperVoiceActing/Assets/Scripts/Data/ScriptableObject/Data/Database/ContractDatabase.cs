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
        public List<ContractData> contractDatas;

        [SerializeField]
        public bool priorityOrder = true;
        [SerializeField]
        public bool priorityScaling = true;
        [SerializeField]
        public bool priorityRandom = true;

        [Space]
        // Partie series
        [SerializeField]
        public bool isSeries = false;

        [ShowIf("isSeries", true)]
        [SerializeField]
        public bool canReboot = false;

        [ShowIf("canReboot", true)]
        [SerializeField]
        public string rebootSuffix = "";
        [ShowIf("canReboot", true)]
        [SerializeField]
        public bool prefix = false;
    }


    [CreateAssetMenu(fileName = "ContractDatabase", menuName = "Database/ContractDatabase", order = 1)]
    public class ContractDatabase: ScriptableObject
    {
        //Get tout les ContractData de l'onglet scriptableObject
        //[AssetList(Path = "/ScriptableObject/")]

        // Pour ajouter un bouton + sur le coté
        //[ListDrawerSettings(HideAddButton = true, OnTitleBarGUI = "DrawTitleBarGUI")]

        //[ValueDropdown("myValues")]




        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/Contrat/Francais")]
        [SerializeField]
        List<ContractData> contractDatabase;

        [SerializeField]
        List<Franchise> franchises;


        public ContractData GetContractData(string contractName)
        {
            return contractDatabase.Find(x => x.name == contractName);
        }


        public Franchise GetFranchise(string contractName)
        {
            for(int i = 0; i < franchises.Count; i++)
            {
                if(franchises[i].contractDatas[0].name == contractName)
                {
                    return franchises[i];
                }
            }
            return null;
        }


    } 

} // #PROJECTNAME# namespace