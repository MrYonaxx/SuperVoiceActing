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
    [CreateAssetMenu(fileName = "SkillDatabase", menuName = "Database/SkillDatabase", order = 1)]
    public class SkillDatabase: ScriptableObject
    {
        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/Skill")]
        [SerializeField]
        List<SkillData> skillDatabase;

        public SkillData GetSkillData(string skillName)
        {
            return skillDatabase.Find(x => x.name == skillName);
        }
    } 

} // #PROJECTNAME# namespace