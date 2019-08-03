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
    // Un ingé son peut etre utiliser en tant qu'acteur
	public class SoundEngineer
	{

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
        private int experience;
        public int Experience
        {
            get { return experience; }
        }

        [SerializeField]
        private int salary;
        public int Salary
        {
            get { return salary; }
        }

        [SerializeField]
        private ExperienceCurveData experienceCurve;
        public ExperienceCurveData ExperienceCurve
        {
            get { return experienceCurve; }
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

        [SerializeField]
        StoryCharacterData spritesSheets;
        public StoryCharacterData SpritesSheets
        {
            get { return spritesSheets; }
        }




        public SoundEngineer(SoundEngineerData soundEngineer)
        {
            engineerName = soundEngineer.EngineerName;
            level = soundEngineer.Level;
            experience = 0;
            salary = soundEngineer.Salary;
            experienceCurve = soundEngineer.ExperienceCurve;
            artificeGauge = soundEngineer.ArtificeGauge;
            mixingPower = soundEngineer.MixingPower;
            spritesSheets = soundEngineer.SpritesSheets;
        }


    } // SoundEngineer class
	
}// #PROJECTNAME# namespace
