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
        [FoldoutGroup("RoleStat")]
        [HorizontalGroup("RoleStat/Data")]
        [VerticalGroup("RoleStat/Data/Left")]
        [HideLabel]
        [SerializeField]
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        [VerticalGroup("RoleStat/Data/Left")]
        [SerializeField]
        private string[] roleNames;
        public string[] RoleNames
        {
            get { return roleNames; }
            set { roleNames = value; }
        }

        /*[VerticalGroup("RoleStat/Data/Left")]
        [LabelWidth(80)]
        [HideLabel]
        [SerializeField]
        private Sprite roleSprite;
        public Sprite RoleSprite
        {
            get { return roleSprite; }
        }*/

        [VerticalGroup("RoleStat/Data/Middle")]
        [HideLabel]
        [SerializeField]
        private RoleType roleType;
        public RoleType RoleType
        {
            get { return roleType; }
        }
        [VerticalGroup("RoleStat/Data/Right")]
        [HideLabel]
        [SerializeField]
        private Sprite roleSprite;
        public Sprite RoleSprite
        {
            get { return roleSprite; }
        }
        /*[VerticalGroup("RoleStat/Data/Right")]
        [HideLabel]
        [SerializeField]
        private RolePersonality rolePersonality;
        public RolePersonality RolePersonality
        {
            get { return rolePersonality; }
        }*/

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

        /*[VerticalGroup("RoleStat/Data/Middle")]
        [SerializeField]
        private int defMin;
        public int DefMin
        {
            get { return defMin; }
        }
        [VerticalGroup("RoleStat/Data/Right")]
        [HideLabel]
        [SerializeField]
        private int defMax;
        public int DefMax
        {
            get { return defMax; }
        }*/

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


        [TabGroup("Skills")]


        [TabGroup("Skills")]
        [GUIColor(0.9f, 0.9f, 1f, 1f)]
        [SerializeField]
        private SkillRoleSelection[] skillRoleSelection;
        public SkillRoleSelection[] SkillRoleSelection
        {
            get { return skillRoleSelection; }
        }

    }


    [System.Serializable]
    public class SkillRoleSelection
    {
        [HorizontalGroup("skillRandom")]
        [SerializeField]
        private int randomDrawMin = 0;

        [HorizontalGroup("skillRandom")]
        [HideLabel]
        [SerializeField]
        private int randomDrawMax = 0;


        [HorizontalGroup("SkillRole")]
        [SerializeField]
        private SkillRoleData[] skills;


        [HorizontalGroup("SkillRole")]
        [SerializeField]
        private SkillRoleData[] skillsRandom;

        /*[HorizontalGroup("SkillPattern")]
        [SerializeField]
        private bool patternRandom = true;
        public bool PatternRandom
        {
            get { return patternRandom; }
        }
        [HorizontalGroup("SkillPattern")]
        [SerializeField]
        private bool patternList = false;
        public bool PatternList
        {
            get { return patternList; }
        }

        [SerializeField]
        private EnemyAI roleAI;
        public EnemyAI RoleAI
        {
            get { return roleAI; }
        }*/


        public string[] CreateSkillList()
        {
            if (skillsRandom == null)
                return null;
            List<string> res = new List<string>();
            List<SkillRoleData> randSkills = new List<SkillRoleData>(skillsRandom.Length);
            int randomDraw = Random.Range(randomDrawMin, randomDrawMax+1);

            // On créer une liste de skill sélectionnable
            for(int i = 0; i < skillsRandom.Length; i++)
            {
                randSkills.Add(skillsRandom[i]);
            }

            // On sélectionne les skills obligatoires
            for(int i = 0; i < skills.Length; i++)
            {
                if (skills[i] == null)
                    res.Add(" ");
                else
                    res.Add(skills[i].name);
            }

            // on comble les trous
            for (int i = 0; i < res.Count; i++)
            {
                if (res[i] == " " && randomDraw != 0)
                {
                    randomDraw -= 1;              
                    res[i] = SelectRandSkill(randSkills);
                }
            }

            // On rajoute des skills random s'il en reste
            while(randomDraw > 0)
            {
                if (randSkills == null)
                {
                    randomDraw = 0;
                }
                else if (randSkills.Count == 0)
                {
                    randomDraw = 0;
                }
                else
                {
                    randomDraw -= 1;
                    res.Add(SelectRandSkill(randSkills));
                }
            }

            return res.ToArray();
        }


        private string SelectRandSkill(List<SkillRoleData> randList)
        {
            string res = " ";
            if (randList == null)
                return res;
            if (randList.Count == 0)
                return res;
            int i = Random.Range(0, randList.Count);
            res = randList[i].name;
            randList.RemoveAt(i);
            return res;
        }

    }


}// #PROJECTNAME# namespace
