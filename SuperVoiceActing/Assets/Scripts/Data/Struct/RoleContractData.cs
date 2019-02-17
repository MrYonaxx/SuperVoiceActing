/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VoiceActing
{
    // CharacterContractData Contract ===================================================================

    [System.Serializable]
    public class RoleContractDatabase
    {
        [Header("CHARACTER")]
        [SerializeField]
        private bool mainCharacter;
        public bool MainCharacter
        {
            get { return mainCharacter; }
            set { mainCharacter = value; }
        }
        [SerializeField]
        private bool optional;
        public bool Optional
        {
            get { return optional; }
            set { optional = value; }
        }

        [SerializeField]
        private RoleContractData[] charactersProfil;
        public RoleContractData[] CharactersProfil
        {
            get { return charactersProfil; }
        }
    }

    // CharacterContractData Contract ===================================================================

    [System.Serializable]
    public class RoleContractData
    {
        [Header("CharacterData")]

        [SerializeField]
        private string[] names;
        public string[] Names
        {
            get { return names; }
            set { names = value; }
        }

        [HorizontalGroup("Line")]
        [SerializeField]
        private int lineMin;
        public int LineMin
        {
            get { return lineMin; }
        }
        [HorizontalGroup("Line")]
        [SerializeField]
        private int lineMax;
        public int LineMax
        {
            get { return lineMax; }
        }

        [HorizontalGroup("Fan")]
        [SerializeField]
        private int fanMin;
        public int FanMin
        {
            get { return fanMin; }
        }
        [HorizontalGroup("Fan")]
        [SerializeField]
        private int fanMax;
        public int FanMax
        {
            get { return fanMax; }
        }

        [MinMaxSlider(-100, 100)]
        [SerializeField]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
            set { timbre = value; }
        }

        [MinMaxSlider(-100, 100)]
        [SerializeField]
        private Vector2Int timbreRand;
        public Vector2Int TimbreRand
        {
            get { return timbre; }
            set { timbre = value; }
        }

        [SerializeField]
        private EmotionStat characterStatMin;
        public EmotionStat CharacterStatMin
        {
            get { return characterStatMin; }
            set { characterStatMin = value; }
        }
        [SerializeField]
        private EmotionStat characterStatMax;
        public EmotionStat CharacterStatMax
        {
            get { return characterStatMax; }
            set { characterStatMax = value; }
        }

    }

}// #PROJECTNAME# namespace
