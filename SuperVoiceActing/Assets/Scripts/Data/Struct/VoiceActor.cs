﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VoiceActing
{

    public enum VoiceActorState
    {
        Fine,
        Tired,
        Dead,
        Absent
    }

    [System.Serializable]
    public class Voxography
    {
        [SerializeField]
        private string contractName;
        public string ContractName
        {
            get { return contractName; }
            set { contractName = value; }
        }

        [SerializeField]
        private List<Role> roles;
        public List<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        [SerializeField]
        private List<Emotion> emotionGained;
        public List<Emotion> EmotionGained
        {
            get { return emotionGained; }
            set { emotionGained = value; }
        }

        public Voxography(string cName, List<Role> cRole, List<Emotion> cEmotions)
        {
            contractName = cName;
            this.roles = cRole;
            this.emotionGained = cEmotions;
        }

        public Voxography(string cName)
        {
            contractName = cName;
            this.roles = new List<Role>(3);
            this.emotionGained = new List<Emotion>(3);
        }

        public void AddVoxography(Role role, Emotion emotion)
        {
            roles.Add(role);
            emotionGained.Add(emotion);
        }

    }


    /// <summary>
    /// Definition of the VoiceActor class
    /// A VoiceActor is created from a VoiceActorData
    /// VoiceActorData contains all possibilities for a VoiceActor
    /// A VoiceActor object is a possibility of VoiceActorData
    /// </summary>

    [System.Serializable]
    public class VoiceActor
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private string voiceActorName;
        public string VoiceActorName
        {
            get { return voiceActorName; }
        }

        [SerializeField]
        private string voiceActorID;
        public string VoiceActorID
        {
            get { return voiceActorID; }
            set { voiceActorID = value; }
        }

        [Header(" Informations générales")]
        [SerializeField]
        private int fan;
        public int Fan
        {
            get { return fan; }
            set { fan = value; }
        }

        [SerializeField]
        private int fanGain;
        public int FanGain
        {
            get { return fanGain; }
            set { fanGain = value; }
        }


        [SerializeField]
        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        [Header(" Statistiques")]
        [SerializeField]
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

        [SerializeField]
        private int hpMax;
        public int HpMax
        {
            get { return hpMax; }
            set { hpMax = value; }
        }



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

        [SerializeField]
        private EmotionStat statModifier;
        public EmotionStat StatModifier
        {
            get { return statModifier; }
            set { statModifier = value; }
        }

        [SerializeField]
        private EmotionStat statVoxography;
        public EmotionStat StatVoxography
        {
            get { return statVoxography; }
            set { statVoxography = value; }
        }

        [SerializeField]
        private EmotionStat growth;
        public EmotionStat Growth
        {
            get { return growth; }
            set { growth = value; }
        }

        [SerializeField]
        private EmotionStat currentGrowth;
        public EmotionStat CurrentGrowth
        {
            get { return currentGrowth; }
            set { currentGrowth = value; }
        }



        [SerializeField]
        private int experience = 0;
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        [SerializeField]
        private int nextEXP = 120;
        public int NextEXP
        {
            get { return nextEXP; }
            set { nextEXP = value; }
        }


        [SerializeField]
        private int relation;
        public int Relation
        {
            get { return relation; }
            set { relation = value; }
        }

        [SerializeField]
        private Vector3 skillOffset;
        public Vector3 SkillOffset
        {
            get { return skillOffset; }
        }

        [SerializeField]
        private int damageVariance;
        public int DamageVariance
        {
            get { return damageVariance; }
            set { damageVariance = value; }
        }

        [SerializeField]
        private int roleDefense;
        public int RoleDefense
        {
            get { return roleDefense; }
            set { roleDefense = value; }
        }


        [SerializeField]
        private string[] potentials;
        public string[] Potentials
        {
            get { return potentials; }
            set { potentials = value; }
        }

        [SerializeField]
        private int[] friendshipLevel;
        public int[] FriendshipLevel
        {
            get { return friendshipLevel; }
            set { friendshipLevel = value; }
        }

        [SerializeField]
        private List<Buff> buffs;
        public List<Buff> Buffs
        {
            get { return buffs; }
            set { buffs = value; }
        }

        [SerializeField]
        private List<Voxography> voxography;
        public List<Voxography> Voxography
        {
            get { return voxography; }
            set { voxography = value; }
        }


        [SerializeField]
        private VoiceActorState actorMentalState;
        public VoiceActorState ActorMentalState
        {
            get { return actorMentalState; }
            set { actorMentalState = value; }
        }

        [SerializeField]
        private bool availability;
        public bool Availability
        {
            get { return availability; }
            set { availability = value; }
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

        // Rappel : les classes ne sont pas dupliqué par défaut, à la place les deux variable auront la meme reference donc bug potentiel
        public VoiceActor(VoiceActorData actorData)
        {
            voiceActorName = actorData.SpriteSheets.GetName();
            voiceActorID = actorData.SpriteSheets.name;
            level = actorData.Level;
            fanGain = 0;
            fan = actorData.Fan;
            price = actorData.Price;
            hp = actorData.Hp;
            hpMax = actorData.Hp;
            damageVariance = actorData.FourchetteMax;
            roleDefense = 0;
            timbre = actorData.Timbre;

            relation = 0;
            potentials = new string[actorData.Potentials.Length];
            friendshipLevel = new int[actorData.FriendshipCost.Length];
            for(int i = 0; i < actorData.Potentials.Length; i++)
            {
                potentials[i] = actorData.Potentials[i].name;
                friendshipLevel[i] = actorData.FriendshipCost[i];
            }


            skillOffset = actorData.SkillOffset;

            statistique = new EmotionStat(actorData.Statistique);
            statModifier = new EmotionStat();
            statVoxography = new EmotionStat();

            growth = new EmotionStat(actorData.Growth);
            currentGrowth = new EmotionStat(actorData.Growth);

            voxography = new List<Voxography>(actorData.Voxography.Length);
            for (int i = 0; i < actorData.Voxography.Length; i++)
            {
                voxography.Add(new Voxography(actorData.Voxography[i].ContractName, actorData.Voxography[i].Roles, actorData.Voxography[i].EmotionGained));
            }

            buffs = new List<Buff>();
            availability = true;


            int point = 0;
            int pointCumul = 0;

            for (int i = 1; i < 9; i++)
            {
                point = Random.Range(-actorData.GrowthRandom.GetEmotion(i), actorData.GrowthRandom.GetEmotion(i)+1);
                currentGrowth.Add(i, point);
                pointCumul += point;
            }


            int result = 0;

            List<int> possibilities = new List<int>(8) { 1, 2, 3, 4, 5, 6, 7, 8 };
            int choiceEmotion = 0;
            int emotionSelected = 0;
            choiceEmotion = Random.Range(0, possibilities.Count);

            while (pointCumul != 0)
            {
                emotionSelected = possibilities[choiceEmotion];
                result = EquilibrateStat(currentGrowth.GetEmotion(emotionSelected), actorData.Growth.GetEmotion(emotionSelected), actorData.GrowthRandom.GetEmotion(emotionSelected), pointCumul);
                currentGrowth.Add(emotionSelected, result);

                if (result == 0)
                {
                    possibilities.RemoveAt(choiceEmotion);
                    choiceEmotion += 1;
                    if(choiceEmotion >= possibilities.Count)
                    {
                        choiceEmotion = 0;
                    }
                }
                else if (result == -1)
                {
                    pointCumul -= 1;
                    choiceEmotion = Random.Range(0, possibilities.Count);
                    result = 0;
                }
                else if (result == 1)
                {
                    pointCumul += 1;
                    choiceEmotion = Random.Range(0, possibilities.Count);
                    result = 0;
                }
            }

            growth = new EmotionStat(currentGrowth);
            currentGrowth = new EmotionStat(0, 0, 0, 0, 0, 0, 0, 0);
        }








        private int EquilibrateStat(int growthToEquilibrate, int growthToCompare, int growthRandom, int pointCumul)
        {
            if(pointCumul < 0)
            {
                if (growthToEquilibrate == growthToCompare + growthRandom)
                {
                    return 0;
                }
                else
                {
                    growthToEquilibrate += 1;
                    return 1;
                }
            }
            else if (pointCumul > 0)
            {
                if (growthToEquilibrate == growthToCompare - growthRandom)
                {
                    return 0;
                }
                else
                {
                    growthToEquilibrate -= 1;
                    return -1;
                }
            }
            return 0;
        }





        public void LevelUp()
        {
            for(int i = 1; i < 9; i++)
            {
                currentGrowth.Add(i, growth.GetEmotion(i));
                while (currentGrowth.GetEmotion(i) >= 100)
                {
                    currentGrowth.Add(i, -100);
                    statistique.Add(i, 1);
                }
            }
            statistique.Neutral = (statistique.Joy + statistique.Sadness + statistique.Disgust + statistique.Anger + 
                                   statistique.Surprise + statistique.Sweetness + statistique.Fear + statistique.Trust) / 8;
            hp += (growth.Neutral / 8);
            hpMax += (growth.Neutral / 8);
            level += 1;

        }







        public void CreateVoxography(string contractName)
        {
            voxography.Add(new Voxography(contractName));
        }

        public void AddVoxography(Role role, Emotion emotion)
        {
            voxography[voxography.Count-1].AddVoxography(role, emotion);
            statVoxography.Add((int)emotion, 1);
        }

        public int AddRelation(Contract c)
        {
            int reward = 0;
            if (c.CurrentLine == c.TotalLine)
                reward = c.Level + Random.Range(0, 3);
            relation += reward;
            return reward;
        }

        #endregion

    } // VoiceActor class

} // #PROJECTNAME# namespace