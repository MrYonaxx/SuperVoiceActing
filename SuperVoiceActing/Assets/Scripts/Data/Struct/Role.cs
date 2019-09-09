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
        Protagoniste,
        Antagoniste,
        Enfant,
        Vieux,
        VoixOff
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
        }

        [SerializeField]
        private int attack;
        public int Attack
        {
            get { return attack; }
        }
        [SerializeField]
        private int defense;
        public int Defense
        {
            get { return defense; }
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
        private int bestStat;
        public int BestStat
        {
            get { return bestStat; }
        }

        [SerializeField]
        private int secondBestStat;
        public int SecondBestStat
        {
            get { return secondBestStat; }
        }


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
        private VoiceActorData characterLock;
        public VoiceActorData CharacterLock
        {
            get { return characterLock; }
            set { characterLock = value; }
        }

        [SerializeField]
        private EnemyAI[] roleAI;
        public EnemyAI[] RoleAI
        {
            get { return roleAI; }
            set { roleAI = value; }
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
            if (data.Names.Length != 0)
                this.name = data.Names[Random.Range(0, data.Names.Length)];
            else
                this.name = "";
            this.line = Random.Range(data.LineMin, data.LineMax);
            this.fan = Random.Range(data.FanMin, data.FanMax);

            this.attack = Random.Range(data.AtkMin, data.AtkMax);
            this.defense = Random.Range(data.DefMin, data.DefMax);
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
                this.characterLock = data.ActorLocked;
            this.roleAI = data.ArtificialIntelligence;

            int[] bestValues = new int[8];

            bestValues[0] = characterStat.Joy;
            bestValues[1] = characterStat.Sadness;
            bestValues[2] = characterStat.Disgust;
            bestValues[3] = characterStat.Anger;
            bestValues[4] = characterStat.Surprise;
            bestValues[5] = characterStat.Sweetness;
            bestValues[6] = characterStat.Fear;
            bestValues[7] = characterStat.Trust;

            bestStat = 0;
            secondBestStat = 0;

            for (int i = 0; i < bestValues.Length; i++)
            {
                if (bestValues[i] > bestStat)
                {
                    secondBestStat = bestStat;
                    bestStat = bestValues[i];
                    bestStatEmotion = i + 1;
                }
                else if (bestValues[i] > secondBestStat)
                {
                    secondBestStat = bestValues[i];
                    secondBestStatEmotion = i + 1;
                }
            }
            if (bestStat == 0)
            {
                bestStat = -1;
            }
            if (secondBestStat == 0)
            {
                secondBestStat = -1;
            }


        }

        #endregion

    } // Role class
	
}// #PROJECTNAME# namespace
