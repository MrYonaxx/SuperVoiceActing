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
        public string NameID
        {
            get { return spriteSheets.name; }
        }

        [Title("Informations générales")]
        [SerializeField]
        private string actorName;
        public string ActorName
        {
            get { return actorName; }
        }

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
        [Title("Statistiques initiales")]
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

        // ========== C R O I S S A N C E =====================================
        
        [TabGroup("Croissance")]
        [SerializeField]
        [Header("Croissance")]
        [HideLabel]
        private EmotionStat growth;
        public EmotionStat Growth
        {
            get { return growth; }
        }

        [TabGroup("Croissance")]
        [SerializeField]
        [Header("Croissance Random")]
        [HideLabel]
        private EmotionStat growthRandom;
        public EmotionStat GrowthRandom
        {
            get { return growthRandom; }
        }

        [OnValueChanged("CalculateExperienceCurve")]
        [TabGroup("Croissance")]
        [SerializeField]
        private int baseStat = 100;

        [OnValueChanged("CalculateExperienceCurve")]
        [TabGroup("Croissance")]
        [SerializeField]
        private float multiplier = 1.2f;

        [ReadOnly]
        [TabGroup("Croissance")]
        [SerializeField]
        private int[] experienceCurve = new int[50];
        public int[] ExperienceCurve
        {
            get { return experienceCurve; }
        }


        [Space]
        [Space]
        [SerializeField]
        private Voxography[] voxography;
        public Voxography[] Voxography
        {
            get { return voxography; }
        }


        [Space]
        [Space]
        [SerializeField]
        private int fourchetteMax = 2;
        public int FourchetteMax
        {
            get { return fourchetteMax; }
        }

        [SerializeField]
        [HorizontalGroup("Skills")]
        private SkillActorData[] potentials;
        public SkillActorData[] Potentials
        {
            get { return potentials; }
        }

        [SerializeField]
        [HorizontalGroup("Skills")]
        private int[] friendshipCost;
        public int[] FriendshipCost
        {
            get { return friendshipCost; }
        }



        private void CalculateExperienceCurve()
        {
            experienceCurve[0] = baseStat;
            for (int i = 1; i < experienceCurve.Length; i++)
            {
                experienceCurve[i] = (int)(experienceCurve[i - 1] * multiplier);
            }
        }




        [Space]
        [TabGroup("SeiyuuDensetsu")]
        [SerializeField]
        private EmotionStat humorAffinity;
        public EmotionStat HumorAffinity
        {
            get { return humorAffinity; }
        }




        [Space]
        [Header("Graphics")]
        [TabGroup("Sprites")]
        [SerializeField]
        private Vector3 skillOffset;
        public Vector3 SkillOffset
        {
            get { return skillOffset; }
        }
        [TabGroup("Sprites")]
        [SerializeField]
        private StoryCharacterData spriteSheets;
        public StoryCharacterData SpriteSheets
        {
            get { return spriteSheets; }
        }

        #endregion



    } // VoiceActorData class

} // #PROJECTNAME# namespace