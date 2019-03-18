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
        private string name;
        public string Name
        {
            get { return name; }
        }

        [SerializeField]
        private int level;
        public int Level
        {
            get { return level; }
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



        [Header("Dialogues")]

        [SerializeField]
        private string[] lineStart;
        public string[] LineStart
        {
            get { return lineStart; }
        }


        [Header("Sprites")]
        [SerializeField]
        StoryCharacterData spritesSheets;

    } // SoundEngineerData class
	
}// #PROJECTNAME# namespace
