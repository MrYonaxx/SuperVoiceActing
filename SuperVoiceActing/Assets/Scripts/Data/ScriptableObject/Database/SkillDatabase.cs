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
    /// Definition of the SkillDatabase class
    /// </summary>
    [CreateAssetMenu(fileName = "SkillDatabase", menuName = "Database/SkillDatabase", order = 1)]
    public class SkillDatabase : ScriptableObject
    {
        [SerializeField]
        List<SkillData> database;

    } // SkillDatabase class

} // #PROJECTNAME# namespace