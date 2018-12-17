/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the SkillData class
    /// </summary>
    [CreateAssetMenu(fileName = "SkillData", menuName = "SkillData", order = 1)]
    public class SkillData : ScriptableObject
    {

        [SerializeField]
        private string skillName;
        public string SkillName
        {
            get { return skillName; }
        }



    } // SkillData class

} // #PROJECTNAME# namespace