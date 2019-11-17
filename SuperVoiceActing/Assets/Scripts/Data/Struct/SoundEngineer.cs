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

    [System.Serializable]
    public class SoundEngineer
	{

        [SerializeField]
        private string engineerName;
        public string EngineerName
        {
            get { return engineerName; }
        }

        [SerializeField]
        string soundEngineerID;
        public string SoundEngineerID
        {
            get { return soundEngineerID; }
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
        private SkillDataSoundEngi[] skills;
        public SkillDataSoundEngi[] Skills
        {
            get { return skills; }
        }



        [SerializeField]
        private int formationSkillID;
        public int FormationSkillID
        {
            get { return formationSkillID; }
        }
        [SerializeField]
        private SkillDataSoundEngi formationSkill;
        public SkillDataSoundEngi FormationSkill
        {
            get { return formationSkill; }
        }
        [SerializeField]
        private int formationSkillTime;
        public int FormationSkillTime
        {
            get { return formationSkillTime; }
        }
        [SerializeField]
        private int formationSkillTotalTime;
        public int FormationSkillTotalTime
        {
            get { return formationSkillTotalTime; }
        }


        public SoundEngineer(SoundEngineerData soundEngineer)
        {
            engineerName = soundEngineer.SpritesSheets.GetName();
            soundEngineerID = soundEngineer.SpritesSheets.name;
            level = soundEngineer.Level;
            experience = 0;
            salary = soundEngineer.InitialSalary;
            experienceCurve = soundEngineer.ExperienceCurve;
            artificeGauge = soundEngineer.ArtificeGauge;
            mixingPower = soundEngineer.MixingPower;


            skills = new SkillDataSoundEngi[soundEngineer.InitialSkills.Length];
            for (int i = 0; i < soundEngineer.InitialSkills.Length; i++)
            {
                skills[i] = soundEngineer.InitialSkills[i];
            }
            formationSkillID = 0;
            formationSkill = null;
            formationSkillTime = 0;
            formationSkillTotalTime = 0;
        }

        public bool GainExp(int exp)
        {
            bool returnValue = false;
            experience += exp;
            if (experienceCurve == null)
                return false;
            while(experience >= experienceCurve.ExperienceCurve[level])
            {
                LevelUp();
                returnValue = true;
            }
            return returnValue;
        }

        public void LevelUp()
        {
            experience -= experienceCurve.ExperienceCurve[level];
            level += 1;
            mixingPower += 1;
        }

        public int GetMixingPower(Contract contract)
        {
            return mixingPower;
        }

        public int GetMixingPower(ContractType contractType)
        {
            return mixingPower;
        }

        public void SetSkillFormation(int slotSkill, SkillDataSoundEngi skillFormation, int formationTime)
        {
            formationSkillID = slotSkill;
            formationSkill = skillFormation;
            formationSkillTime = 0;
            formationSkillTotalTime = formationTime;
        }

        public bool Formation()
        {
            if (formationSkill == null)
                return false;
            formationSkillTime += 1;
            if(formationSkillTime >= formationSkillTotalTime)
            {
                skills[formationSkillID] = formationSkill;
                formationSkill = null;
                return true;
            }
            return false;
        }

    } // SoundEngineer class
	
}// #PROJECTNAME# namespace
