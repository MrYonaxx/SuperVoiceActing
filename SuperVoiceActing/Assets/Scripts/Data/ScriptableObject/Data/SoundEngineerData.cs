/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
    [CreateAssetMenu(fileName = "SoundEngineerData", menuName = "SoundEngineer", order = 1)]
    public class SoundEngineerData : ScriptableObject
	{

        [Header("Info")]
        [SerializeField]
        private string engineerName;
        public string EngineerName
        {
            get { return engineerName; }
        }

        [SerializeField]
        private int level;
        public int Level
        {
            get { return level; }
        }

        [SerializeField]
        private ExperienceCurveData experienceCurve;
        public ExperienceCurveData ExperienceCurve
        {
            get { return experienceCurve; }
        }

        [SerializeField]
        private int salary;
        public int Salary
        {
            get { return salary; }
        }

        [SerializeField]
        private int artificeGauge;
        public int ArtificeGauge
        {
            get { return artificeGauge; }
        }

        [SerializeField]
        private int mixingPower;
        public int MixingPower
        {
            get { return mixingPower; }
        }

        [SerializeField]
        private SkillData[] skills;
        public SkillData[] Skills
        {
            get { return skills; }
        }

        [Header("Sprites")]
        [SerializeField]
        StoryCharacterData spritesSheets;
        public StoryCharacterData SpritesSheets
        {
            get { return spritesSheets; }
        }

    } // SoundEngineerData class
	
}// #PROJECTNAME# namespace
