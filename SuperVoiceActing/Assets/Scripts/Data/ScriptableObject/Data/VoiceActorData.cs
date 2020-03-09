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
            get { return this.name; }
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

        [SerializeField]
        [HorizontalGroup("Skill")]
        [VerticalGroup("Skill/Potential", PaddingBottom = 12)]
        private SkillActorData[] potentials;
        public SkillActorData[] Potentials
        {
            get { return potentials; }
        }

        [SerializeField]
        [VerticalGroup("Skill/Cost", PaddingBottom = 12)]
        private int[] friendshipCost;
        public int[] FriendshipCost
        {
            get { return friendshipCost; }
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
        [ReadOnly]
        [OnValueChanged("CalculateExperienceCurve")]
        [TabGroup("Croissance")]
        [SerializeField]
        private float totalExperience = 0;

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





        private void CalculateExperienceCurve()
        {
            experienceCurve[0] = baseStat;
            totalExperience = 0;
            for (int i = 1; i < experienceCurve.Length; i++)
            {
                experienceCurve[i] = (int)(experienceCurve[i - 1] * multiplier);
                totalExperience += experienceCurve[i];
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





        [TabGroup("Sprites")]
        [SerializeField]
        [HideLabel]
        private StoryCharacterData spriteSheets;
        public StoryCharacterData SpriteSheets
        {
            get { return spriteSheets; }
        }

        [TabGroup("Sprites")]
        [SerializeField]
        private Vector3 skillOffset;
        public Vector3 SkillOffset
        {
            get { return skillOffset; }
        }

        #endregion



    } // VoiceActorData class

} // #PROJECTNAME# namespace