﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the VoiceActorData class
    /// </summary>
    [CreateAssetMenu(fileName = "VoiceActorData", menuName = "VoiceActor", order = 1)]
    public class VoiceActorData : ScriptableObject
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Informations générales")]
        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private int fan;
        public int Fan
        {
            get { return fan; }
            set { fan = value; }
        }

        [SerializeField]
        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }




        [Space]
        [Header("Statistiques")]
        [SerializeField]
        [MinValue(1), MaxValue(99)]
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField]
        private int hp;
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        [MinMaxSlider(-10, 10)]
        [SerializeField]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
            set { timbre = value; }
        }

        [SerializeField]
        private EmotionStat statistique;
        public EmotionStat Statistique
        {
            get { return statistique; }
            set { statistique = value; }
        }

        [Space]
        [SerializeField]
        private EmotionStat growth;
        public EmotionStat Growth
        {
            get { return growth; }
            set { growth = value; }
        }

        [Space]
        [SerializeField]
        private EmotionStat growthRandom;
        public EmotionStat GrowthRandom
        {
            get { return growthRandom; }
            set { growthRandom = value; }
        }



        [Space]
        [Space]
        [HorizontalGroup("Fourchette")]
        [SerializeField]
        private int fourchetteMin;
        public int FourchetteMin
        {
            get { return fourchetteMin; }
            set { fourchetteMin = value; }
        }

        [Space]
        [Space]
        [HorizontalGroup("Fourchette")]
        [SerializeField]
        private int fourchetteMax;
        public int FourchetteMax
        {
            get { return fourchetteMax; }
            set { fourchetteMax = value; }
        }

        [SerializeField]
        private int relation;
        public int Relation
        {
            get { return relation; }
            set { relation = value; }
        }

        [SerializeField]
        private SkillData[] potentials;
        public SkillData[] Potentials
        {
            get { return potentials; }
            set { potentials = value; }
        }













        [Space]
        [Header("Stat Variation")]
        [SerializeField]
        bool hasVariation = false;

        [SerializeField]
        [ShowIf("hasVariation", true)]
        private int levelVariation;
        public int LevelVariation
        {
            get { return levelVariation; }
            set { levelVariation = value; }
        }


        [SerializeField]
        [ShowIf("hasVariation", true)]
        private int hpVariation;
        public int HpVariation
        {
            get { return hpVariation; }
            set { hpVariation = value; }
        }

        [SerializeField]
        [ShowIf("hasVariation", true)]
        private EmotionStat statistiqueVariation;
        public EmotionStat StatistiqueVariation
        {
            get { return statistiqueVariation; }
            set { statistiqueVariation = value; }
        }
        /*[SerializeField]
        private int experience;
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }*/


        [Space]
        [Header("Graphics")]

        [SerializeField]
        private AudioClip voice;
        public AudioClip Voice
        {
            get { return voice; }
            set { voice = value; }
        }

        [SerializeField]
        private Sprite actorSprite;
        public Sprite ActorSprite
        {
            get { return actorSprite; }
            set { actorSprite = value; }
        }

        [SerializeField]
        private Sprite actorIcon;
        public Sprite ActorIcon
        {
            get { return actorIcon; }
            set { actorIcon = value; }
        }

        [SerializeField]
        private Sprite actorContractIcon;
        public Sprite ActorContractIcon
        {
            get { return actorContractIcon; }
            set { actorContractIcon = value; }
        }

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void GainExp(float exp)
        {

        }
        
        #endregion

    } // VoiceActorData class

} // #PROJECTNAME# namespace