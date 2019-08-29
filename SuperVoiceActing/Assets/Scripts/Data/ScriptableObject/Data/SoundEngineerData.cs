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
        private int initialSalary;
        public int InitialSalary
        {
            get { return initialSalary; }
        }
        [SerializeField]
        private int additionSalary;
        public int AdditionSalary
        {
            get { return additionSalary; }
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
        private int bonusBuff;
        public int BonusBuff
        {
            get { return bonusBuff; }
        }
        [SerializeField]
        private int buffTurnCount;
        public int BuffTurnCount
        {
            get { return buffTurnCount; }
        }

        [SerializeField]
        private SkillDataSoundEngi[] initialSkills;
        public SkillDataSoundEngi[] InitialSkills
        {
            get { return initialSkills; }
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
