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
    public class SkillConditionContractType: SkillCondition
    {
        [SerializeField]
        ContractType contractType;

        public override bool CheckCondition(DoublageBattleParameter battleParameter)
        {
            if(battleParameter.Contract.ContractType == contractType)
            {
                return true;
            }
            return false;
        }

    } 

} // #PROJECTNAME# namespace