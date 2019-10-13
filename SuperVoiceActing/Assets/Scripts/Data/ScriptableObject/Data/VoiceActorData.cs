/*****************************************************************
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

        /*[SerializeField]
        private string name;*/
        public string Name
        {
            get { return spriteSheets.GetName(); }
        }

        [Header("Informations générales")]
        [SerializeField]
        private int fan;
        public int Fan
        {
            get { return fan; }
        }

        [SerializeField]
        private int price;
        public int Price
        {
            get { return price; }
        }




        [Space]
        [Header("Statistiques")]
        [SerializeField]
        [MinValue(1), MaxValue(99)]
        private int level;
        public int Level
        {
            get { return level; }
        }

        [SerializeField]
        private int hp;
        public int Hp
        {
            get { return hp; }
        }

        [MinMaxSlider(-10, 10)]
        [SerializeField]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
        }

        [SerializeField]
        [HideLabel]
        private EmotionStat statistique;
        public EmotionStat Statistique
        {
            get { return statistique; }
        }

        [Space]
        [SerializeField]
        [HideLabel]
        private EmotionStat growth;
        public EmotionStat Growth
        {
            get { return growth; }
        }

        [Space]
        [SerializeField]
        [HideLabel]
        private EmotionStat growthRandom;
        public EmotionStat GrowthRandom
        {
            get { return growthRandom; }
        }

        [SerializeField]
        private Voxography[] voxography;
        public Voxography[] Voxography
        {
            get { return voxography; }
        }


        [Space]
        [Space]
        [HorizontalGroup("Fourchette")]
        [SerializeField]
        private int fourchetteMin = -2;
        public int FourchetteMin
        {
            get { return fourchetteMin; }
        }

        [Space]
        [Space]
        [HorizontalGroup("Fourchette")]
        [SerializeField]
        private int fourchetteMax = 2;
        public int FourchetteMax
        {
            get { return fourchetteMax; }
        }

        [SerializeField]
        private int relation;
        public int Relation
        {
            get { return relation; }
        }

        [SerializeField]
        private SkillActorData[] potentials;
        public SkillActorData[] Potentials
        {
            get { return potentials; }
        }







        [Space]
        [Header("SeiyuuDensetsu")]
        [SerializeField]
        private EmotionStat humorAffinity;
        public EmotionStat HumorAffinity
        {
            get { return humorAffinity; }
        }




        [Space]
        [Header("Graphics")]

        [SerializeField]
        private Vector3 skillOffset;
        public Vector3 SkillOffset
        {
            get { return skillOffset; }
        }

        [SerializeField]
        private StoryCharacterData spriteSheets;
        public StoryCharacterData SpriteSheets
        {
            get { return spriteSheets; }
        }

        #endregion



    } // VoiceActorData class

} // #PROJECTNAME# namespace