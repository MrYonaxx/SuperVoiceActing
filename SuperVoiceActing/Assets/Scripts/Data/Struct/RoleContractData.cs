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

        [GUIColor(1f, 1f, 1f, 1f)]
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
        [HorizontalGroup("Data")]
        [VerticalGroup("Data/Left")]
        [SerializeField]
        private string[] names;
        public string[] Names
        {
            get { return names; }
            set { names = value; }
        }

        [VerticalGroup("Data/Middle")]
        [SerializeField]
        private int lineMin;
        public int LineMin
        {
            get { return lineMin; }
        }
        [VerticalGroup("Data/Right")]
        [HideLabel]
        [SerializeField]
        private int lineMax;
        public int LineMax
        {
            get { return lineMax; }
        }

        [VerticalGroup("Data/Middle")]
        [SerializeField]
        private int fanMin;
        public int FanMin
        {
            get { return fanMin; }
        }
        [VerticalGroup("Data/Right")]
        [HideLabel]
        [SerializeField]
        private int fanMax;
        public int FanMax
        {
            get { return fanMax; }
        }

        [VerticalGroup("Data/Middle")]
        [SerializeField]
        private int atkMin;
        public int AtkMin
        {
            get { return atkMin; }
        }
        [VerticalGroup("Data/Right")]
        [HideLabel]
        [SerializeField]
        private int atkMax;
        public int AtkMax
        {
            get { return atkMax; }
        }

        [MinMaxSlider(-10, 10)]
        [SerializeField]
        [HorizontalGroup("Timbre")]
        [HideLabel]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
        }

        [MinMaxSlider(-10, 10)]
        [SerializeField]
        [HorizontalGroup("Timbre")]
        [HideLabel]
        private Vector2Int timbreRand;
        public Vector2Int TimbreRand
        {
            get { return timbre; }
        }



        [TabGroup("Stat")]
        [HideLabel]
        [SerializeField]
        private EmotionStat characterStatMin;
        public EmotionStat CharacterStatMin
        {
            get { return characterStatMin; }
        }
        [TabGroup("Stat")]
        [HideLabel]
        [SerializeField]
        private EmotionStat characterStatMax;
        public EmotionStat CharacterStatMax
        {
            get { return characterStatMax; }
        }


        [TabGroup("VoiceActors")]
        [SerializeField]
        private VoiceActorData actorLocked;
        public VoiceActorData ActorLocked
        {
            get { return actorLocked; }
        }

    }

}// #PROJECTNAME# namespace
