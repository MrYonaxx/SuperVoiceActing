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
    [CreateAssetMenu(fileName = "Formation", menuName = "ItemData/FormationData", order = 1)]
    public class FormationData : ScriptableObject
	{

        [SerializeField]
        private string formationName;
        public string FormationName
        {
            get { return formationName; }
        }

        [SerializeField]
        private string formationDescription;
        public string FormationDescription
        {
            get { return formationDescription; }
        }

        [SerializeField]
        private int formationTime;
        public int FormationTime
        {
            get { return formationTime; }
        }


        [InfoBox("0 = skill pour Zaque, 1 = skill pour quelqu'un d'autre (Voir sound engi formation pour + d'info)")]
        [HorizontalGroup]
        [SerializeField]
        private SkillDataSoundEngi[] formationSkills;
        public SkillDataSoundEngi[] FormationSkills
        {
            get { return formationSkills; }
        }






    } // FormationData class
	
}// #PROJECTNAME# namespace
