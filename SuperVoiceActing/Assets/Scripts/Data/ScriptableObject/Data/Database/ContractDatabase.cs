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




        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/Contrat/Francais")]
        [SerializeField]
        List<ContractData> contractDatabase;




        public ContractData GetContractData(string contractName)
        {
            return contractDatabase.Find(x => x.name == contractName);
        }

    } 

} // #PROJECTNAME# namespace