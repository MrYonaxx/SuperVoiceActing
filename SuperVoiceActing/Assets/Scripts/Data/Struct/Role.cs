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

    public enum RoleType
    {
        None,
        Protagoniste,
        Antagoniste,
        VoixOff,
        NPC,
        Boss,
        PersonnageSecondaire,
        Mentor
    }
    public enum RolePersonality
    {
        None,
        Timide,
        SangChaud,
        Leader,
        Courageux,
        Intelligent,
        Charismatique,
        Joyeux,
        Pervers,
        Déprimé
    }

    [System.Serializable]
	public class Role
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("CharacterData")]

        [SerializeField]
        private string nameID;
        public string NameID
        {
            get { return nameID; }
            set { nameID = value; }
        }

        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private Sprite roleSprite;
        public Sprite RoleSprite
        {
            get { return roleSprite; }
            set { roleSprite = value; }
        }

        [SerializeField]
        private int line;
        public int Line
        {
            get { return line; }
        }

        [SerializeField]
        private int fan;
        public int Fan
        {
            get { return fan; }
            set { fan = value; }
        }

        [SerializeField]
        private int attack;
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        [SerializeField]
        private int defense;
        public int Defense
        {
            get { return defense; }
        }

        [SerializeField]
        private RoleType roleType;
        public RoleType RoleType
        {
            get { return roleType; }
        }

        [SerializeField]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
            set { timbre = value; }
        }


        [SerializeField]
        private EmotionStat characterStat;
        public EmotionStat CharacterStat
        {
            get { return characterStat; }
            set { characterStat = value; }
        }

        [SerializeField]
        private EmotionStat characterTone;
        public EmotionStat CharacterTone
        {
            get { return characterTone; }
            set { characterTone = value; }
        }

        /// <summary>
        /// Emotion ID of the best stat the role have
        /// </summary>
        [SerializeField]
        private int bestStatEmotion;
        public int BestStatEmotion
        {
            get { return bestStatEmotion; }
        }

        [SerializeField]
        private int secondBestStatEmotion;
        public int SecondBestStatEmotion
        {
            get { return secondBestStatEmotion; }
        }


        [SerializeField]
        private int roleScore;
        public int RoleScore
        {
            get { return roleScore; }
            set { roleScore = value; }
        }

        [SerializeField]
        private int rolePerformance;
        public int RolePerformance
        {
            get { return rolePerformance; }
            set { rolePerformance = value; }
        }

        [SerializeField]
        private int roleBestScore;
        public int RoleBestScore
        {
            get { return roleBestScore; }
            set { roleBestScore = value; }
        }


        [SerializeField]
        private string characterLock;
        public string CharacterLock
        {
            get { return characterLock; }
            set { characterLock = value; }
        }

        [SerializeField]
        private string previousVoiceActor;
        public string PreviousVoiceActor
        {
            get { return previousVoiceActor; }
            set { previousVoiceActor = value; }
        }

        [SerializeField]
        private string[] skillRoles;
        public string[] SkillRoles
        {
            get { return skillRoles; }
            set { skillRoles = value; }
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

        public Role(RoleContractData data)
        {
            this.nameID = data.RoleName;
            int r = -1;
            if (data.RoleNames != null)
                r = Random.Range(0, data.RoleNames.Length+1)-1;
            if(r == -1)
                this.name = data.RoleName;
            else
                this.name = data.RoleNames[r];

            //this.rolePersonality = data.RolePersonality;
            this.roleType = data.RoleType;

            this.line = Random.Range(data.LineMin, data.LineMax);
            this.fan = Random.Range(data.FanMin, data.FanMax);

            this.attack = Random.Range(data.AtkMin, data.AtkMax);
            //this.defense = Random.Range(data.DefMin, data.DefMax);
            //!\ à tester 
            this.timbre = new Vector2Int(Random.Range(data.Timbre.x, data.TimbreRand.x), Random.Range(data.Timbre.y, data.TimbreRand.y));
            //!\ à tester 


            this.characterStat = new EmotionStat
                                 (
                                    Random.Range(data.CharacterStatMin.Joy, data.CharacterStatMax.Joy),
                                    Random.Range(data.CharacterStatMin.Sadness, data.CharacterStatMax.Sadness),
                                    Random.Range(data.CharacterStatMin.Disgust, data.CharacterStatMax.Disgust),
                                    Random.Range(data.CharacterStatMin.Anger, data.CharacterStatMax.Anger),
                                    Random.Range(data.CharacterStatMin.Surprise, data.CharacterStatMax.Surprise),
                                    Random.Range(data.CharacterStatMin.Sweetness, data.CharacterStatMax.Sweetness),
                                    Random.Range(data.CharacterStatMin.Fear, data.CharacterStatMax.Fear),
                                    Random.Range(data.CharacterStatMin.Trust, data.CharacterStatMax.Trust)
                                 );

            this.roleScore = 0;
            this.rolePerformance = 0;
            this.roleSprite = data.RoleSprite;

            if(data.ActorLocked != null)
                this.characterLock = data.ActorLocked.NameID;

            // ================================== 
            if(data.SkillRoleSelection != null)
            {
                if (data.SkillRoleSelection.Length != 0)
                {
                    skillRoles = data.SkillRoleSelection[Random.Range(0, data.SkillRoleSelection.Length)].CreateSkillList();
                }
            }
            // ==================================

            SelectBestStat();
            characterTone = new EmotionStat();
            characterTone.Add(bestStatEmotion, Random.Range(1, 2) + 1);
            characterTone.Add(secondBestStatEmotion, Random.Range(1, 2));
        }


        private void SelectBestStat()
        {
            int bestStat = 0;
            int secondBestStat = 0;

            for (int i = 1; i < characterStat.GetLength(); i++)
            {
                int currentStat = characterStat.GetEmotion(i);
                if (currentStat > bestStat)
                {
                    secondBestStat = bestStat;
                    bestStat = currentStat;
                    bestStatEmotion = i;
                }
                else if (currentStat > secondBestStat)
                {
                    secondBestStat = currentStat;
                    secondBestStatEmotion = i;
                }
            }
        }


        public int CheckTimbre(Vector2Int timbreActor)
        {
            if (timbre.x <= timbreActor.x && timbreActor.x <= timbre.y && timbre.x <= timbreActor.y && timbreActor.y <= timbre.y)
            {
                return (timbre.y - timbre.x);
            }
            else if (timbre.x <= timbreActor.x && timbreActor.x <= timbre.y)
            {
                return (timbre.y - timbreActor.x);
            }
            else if (timbre.x <= timbreActor.y && timbreActor.y <= timbre.y)
            {
                return (timbreActor.y - timbre.x);
            }
            else
                return 0;
        }


        #endregion

    } // Role class
	
}// #PROJECTNAME# namespace
