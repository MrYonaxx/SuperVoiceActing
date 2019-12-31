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
    public class SkillConditionPercentage: SkillCondition
    {

        [SerializeField]
        int percentage;

        public override bool CheckCondition(DoublageBattleParameter battleParameter)
        {
            return (Random.Range(0, 100) <= percentage);
        }
    } 

} // #PROJECTNAME# namespace