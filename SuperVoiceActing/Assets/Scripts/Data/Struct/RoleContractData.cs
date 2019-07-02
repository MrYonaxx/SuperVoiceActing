﻿/*****************************************************************
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
        [FoldoutGroup("RoleStat")]
        [HorizontalGroup("RoleStat/Data")]
        [VerticalGroup("RoleStat/Data/Left")]
        [SerializeField]
        private string[] names;
        public string[] Names
        {
            get { return names; }
            set { names = value; }
        }


        [VerticalGroup("RoleStat/Data/Middle")]
        [SerializeField]
        private int lineMin;
        public int LineMin
        {
            get { return lineMin; }
        }
        [VerticalGroup("RoleStat/Data/Right")]
        [HideLabel]
        [SerializeField]
        private int lineMax;
        public int LineMax
        {
            get { return lineMax; }
        }

        [VerticalGroup("RoleStat/Data/Middle")]
        [SerializeField]
        private int fanMin;
        public int FanMin
        {
            get { return fanMin; }
        }
        [VerticalGroup("RoleStat/Data/Right")]
        [HideLabel]
        [SerializeField]
        private int fanMax;
        public int FanMax
        {
            get { return fanMax; }
        }

        [VerticalGroup("RoleStat/Data/Middle")]
        [SerializeField]
        private int atkMin;
        public int AtkMin
        {
            get { return atkMin; }
        }
        [VerticalGroup("RoleStat/Data/Right")]
        [HideLabel]
        [SerializeField]
        private int atkMax;
        public int AtkMax
        {
            get { return atkMax; }
        }

        [MinMaxSlider(-10, 10)]
        [SerializeField]
        [HorizontalGroup("RoleStat/Timbre")]
        [HideLabel]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
        }

        [MinMaxSlider(-10, 10)]
        [SerializeField]
        [HorizontalGroup("RoleStat/Timbre")]
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

        [TabGroup("IA")]
        [GUIColor(1f, 0.9f, 0.9f, 1f)]
        [SerializeField]
        private EnemyAI[] artificialIntelligence;
        public EnemyAI[] ArtificialIntelligence
        {
            get { return artificialIntelligence; }
        }

    }

}// #PROJECTNAME# namespace
