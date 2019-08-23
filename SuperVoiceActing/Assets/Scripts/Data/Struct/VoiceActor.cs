/*****************************************************************
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
        //private string name;
        public string Name
        {
            get { return spriteSheets.GetName(); }
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
        private Sprite actorSprite;
        public Sprite ActorSprite
        {
            get { return actorSprite; }
            set { actorSprite = value; }
        }

        [SerializeField]
        private StoryCharacterData spriteSheets;
        public StoryCharacterData SpriteSheets
        {
            get { return spriteSheets; }
            set { spriteSheets = value; }
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
        private SkillActorData[] potentials;
        public SkillActorData[] Potentials
        {
            get { return potentials; }
            set { potentials = value; }
        }

        [SerializeField]
        private List<Buff> buffs;
        public List<Buff> Buffs
        {
            get { return buffs; }
            set { buffs = value; }
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

        [SerializeField]
        private bool isNull;
        public bool IsNull
        {
            get { return isNull; }
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

        public VoiceActor()
        {
            isNull = true;
        }

        // Rappel : les classes ne sont pas dupliqué par défaut, à la place les deux variable auront la meme reference donc bug potentiel
        public VoiceActor(VoiceActorData actorData)
        {
            isNull = false;
            //name = actorData.Name;
            level = actorData.Level;
            fan = actorData.Fan;
            price = actorData.Price;
            hp = actorData.Hp;
            hpMax = actorData.Hp;
            actorSprite = actorData.ActorSprite;
            damageVariance = actorData.FourchetteMax;
            roleDefense = 0;
            timbre = actorData.Timbre;

            potentials = new SkillActorData[actorData.Potentials.Length];
            for(int i = 0; i < actorData.Potentials.Length; i++)
            {
                potentials[i] = actorData.Potentials[i];
            }


            skillOffset = actorData.SkillOffset;
            spriteSheets = actorData.SpriteSheets;

            statistique = new EmotionStat( actorData.Statistique);
            statModifier = new EmotionStat();
            growth = new EmotionStat(actorData.Growth);
            currentGrowth = new EmotionStat(actorData.Growth);

            buffs = new List<Buff>();
            availability = true;

            // Repartition aléatoire
            /*int totalPoint = actorData.GrowthRandom.Joy + actorData.GrowthRandom.Sadness + actorData.GrowthRandom.Disgust +
                             actorData.GrowthRandom.Anger + actorData.GrowthRandom.Surprise + actorData.GrowthRandom.Sweetness +
                             actorData.GrowthRandom.Fear + actorData.GrowthRandom.Trust;*/

            int point = 0;
            int pointCumul = 0;

            for (int i = 0; i < 8; i++)
            {             
                switch(i)
                {
                    case 0:
                        point = Random.Range(-actorData.GrowthRandom.Joy, actorData.GrowthRandom.Joy+1);
                        currentGrowth.Joy += point;
                        break;
                    case 1:
                        point = Random.Range(-actorData.GrowthRandom.Sadness, actorData.GrowthRandom.Sadness+1);
                        currentGrowth.Sadness += point;
                        break;
                    case 2:
                        point = Random.Range(-actorData.GrowthRandom.Disgust, actorData.GrowthRandom.Disgust+1);
                        currentGrowth.Disgust += point;
                        break;
                    case 3:
                        point = Random.Range(-actorData.GrowthRandom.Anger, actorData.GrowthRandom.Anger+1);
                        currentGrowth.Anger += point;
                        break;
                    case 4:
                        point = Random.Range(-actorData.GrowthRandom.Surprise, actorData.GrowthRandom.Surprise+1);
                        currentGrowth.Surprise += point;
                        break;
                    case 5:
                        point = Random.Range(-actorData.GrowthRandom.Sweetness, actorData.GrowthRandom.Sweetness+1);
                        currentGrowth.Sweetness += point;
                        break;
                    case 6:
                        point = Random.Range(-actorData.GrowthRandom.Fear, actorData.GrowthRandom.Fear+1);
                        currentGrowth.Fear += point;
                        break;
                    case 7:
                        point = Random.Range(-actorData.GrowthRandom.Trust, actorData.GrowthRandom.Trust+1);
                        currentGrowth.Trust += point;
                        break;
                }
                pointCumul += point;
            }


            int result = 0;

            List<int> possibilities = new List<int>(8) { 0, 1, 2, 3, 4, 5, 6, 7 };
            int choiceEmotion = 0;
            choiceEmotion = Random.Range(0, possibilities.Count);

            while (pointCumul != 0)
            {
                //Debug.Log(possibilities[choiceEmotion]);
                switch (possibilities[choiceEmotion])
                {
                    case 0:
                        result = EquilibrateStat(currentGrowth.Joy, actorData.Growth.Joy, actorData.GrowthRandom.Joy, pointCumul);
                        currentGrowth.Joy += result;
                        break;
                    case 1:
                        result = EquilibrateStat(currentGrowth.Sadness, actorData.Growth.Sadness, actorData.GrowthRandom.Sadness, pointCumul);
                        currentGrowth.Sadness += result;
                        break;
                    case 2:
                        result = EquilibrateStat(currentGrowth.Disgust, actorData.Growth.Disgust, actorData.GrowthRandom.Disgust, pointCumul);
                        currentGrowth.Disgust += result;
                        break;
                    case 3:
                        result = EquilibrateStat(currentGrowth.Anger, actorData.Growth.Anger, actorData.GrowthRandom.Anger, pointCumul);
                        currentGrowth.Anger += result;
                        break;
                    case 4:
                        result = EquilibrateStat(currentGrowth.Surprise, actorData.Growth.Surprise, actorData.GrowthRandom.Surprise, pointCumul);
                        currentGrowth.Surprise += result;
                        break;
                    case 5:
                        result = EquilibrateStat(currentGrowth.Sweetness, actorData.Growth.Sweetness, actorData.GrowthRandom.Sweetness, pointCumul);
                        currentGrowth.Sweetness += result;
                        break;
                    case 6:
                        result = EquilibrateStat(currentGrowth.Fear, actorData.Growth.Fear, actorData.GrowthRandom.Fear, pointCumul);
                        currentGrowth.Fear += result;
                        break;
                    case 7:
                        result = EquilibrateStat(currentGrowth.Trust, actorData.Growth.Trust, actorData.GrowthRandom.Trust, pointCumul);
                        currentGrowth.Trust += result;
                        break;
                }

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
            /*Debug.Log(growth.Joy);
            Debug.Log(growth.Sadness);
            Debug.Log(growth.Disgust);
            Debug.Log(growth.Anger);
            Debug.Log(growth.Surprise);
            Debug.Log(growth.Sweetness);
            Debug.Log(growth.Fear);
            Debug.Log(growth.Trust);
            Debug.Log(growth.Joy + growth.Sadness + growth.Disgust + growth.Anger + growth.Surprise + growth.Sweetness + growth.Fear + growth.Trust);*/
            

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
            currentGrowth.Joy += growth.Joy;
            while(currentGrowth.Joy >= 100)
            {
                currentGrowth.Joy -= 100;
                statistique.Joy += 1;
            }

            currentGrowth.Sadness += growth.Sadness;
            while (currentGrowth.Sadness >= 100)
            {
                currentGrowth.Sadness -= 100;
                statistique.Sadness += 1;
            }

            currentGrowth.Disgust += growth.Disgust;
            while (currentGrowth.Disgust >= 100)
            {
                currentGrowth.Disgust -= 100;
                statistique.Disgust += 1;
            }

            currentGrowth.Anger += growth.Anger;
            while (currentGrowth.Anger >= 100)
            {
                currentGrowth.Anger -= 100;
                statistique.Anger += 1;
            }

            currentGrowth.Surprise += growth.Surprise;
            while (currentGrowth.Surprise >= 100)
            {
                currentGrowth.Surprise -= 100;
                statistique.Surprise += 1;
            }

            currentGrowth.Sweetness += growth.Sweetness;
            while (currentGrowth.Sweetness >= 100)
            {
                currentGrowth.Sweetness -= 100;
                statistique.Sweetness += 1;
            }

            currentGrowth.Fear += growth.Fear;
            while (currentGrowth.Fear >= 100)
            {
                currentGrowth.Fear -= 100;
                statistique.Fear += 1;
            }

            currentGrowth.Trust += growth.Trust;
            while (currentGrowth.Trust >= 100)
            {
                currentGrowth.Trust -= 100;
                statistique.Trust += 1;
            }
            statistique.Neutral = (statistique.Joy + statistique.Sadness + statistique.Disgust + statistique.Anger + 
                                   statistique.Surprise + statistique.Sweetness + statistique.Fear + statistique.Trust) / 8;
            hp += (growth.Neutral / 8);
            hpMax += (growth.Neutral / 8);
            level += 1;

        }


        public void WorkForWeek(ExperienceCurveData experienceCurve)
        {
            if (actorMentalState == VoiceActorState.Absent)
            {
                return;
            }



            // Si le comédien était mort il passe en fatigué
            if (actorMentalState == VoiceActorState.Dead)
            {
                hp = 1;
                actorMentalState = VoiceActorState.Tired;
                availability = false;
                return;
            }

            // Si le comédien a 0 hp il est mort
            if (hp == 0)
            {
                actorMentalState = VoiceActorState.Dead;
                availability = false;
                return;
            }

            // Si le comédien était indisponible il revient full
            if (actorMentalState == VoiceActorState.Tired)
            {
                hp = hpMax;
                availability = true;
                actorMentalState = VoiceActorState.Fine;
                return;
            }

            //Chance de trouver du travail dépendant du nombre de fan
            int chanceWork = 15;
            int random = Random.Range(0, 100);

            // Le comédien travaille
            if(random <= chanceWork)
            {
                hp -= (int) (hpMax * Random.Range(0.1f,0.2f));
                //experience = experience + (int) (experienceCurve.ExperienceCurve[level] * Random.Range(0.1f, 0.3f));
                nextEXP -= (int)(experienceCurve.ExperienceCurve[level] * Random.Range(0.1f, 0.3f));//experienceCurve.ExperienceCurve[level] - experience;
                if (nextEXP <= 0)
                {
                    LevelUp();
                    nextEXP += experienceCurve.ExperienceCurve[level];
                }
            }
            else // le comédien fais des trucs
            {
                hp += (int)(hpMax * Random.Range(0.05f, 0.2f));
            }
            hp = Mathf.Clamp(hp, 0, hpMax);

            // Si le comédien est dans le mal il passe indisponible
            if (hp <= (hpMax * 0.25f))
            {
                actorMentalState = VoiceActorState.Tired;
                availability = false;
            }
        }



        #endregion

    } // VoiceActor class

} // #PROJECTNAME# namespace