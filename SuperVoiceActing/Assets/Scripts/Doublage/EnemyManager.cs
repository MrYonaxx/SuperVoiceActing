/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{



    [System.Serializable]
    public class EmotionStat
    {
        [SerializeField]
        private int joy;
        public int Joy
        {
            get { return joy; }
            set { joy = value; }
        }

        [SerializeField]
        private int sadness;
        public int Sadness
        {
            get { return sadness; }
            set { sadness = value; }
        }

        [SerializeField]
        private int disgust;
        public int Disgust
        {
            get { return disgust; }
            set { disgust = value; }
        }

        [SerializeField]
        private int anger;
        public int Anger
        {
            get { return anger; }
            set { anger = value; }
        }

        [SerializeField]
        private int surprise;
        public int Surprise
        {
            get { return surprise; }
            set { surprise = value; }
        }

        [SerializeField]
        private int sweetness;
        public int Sweetness
        {
            get { return sweetness; }
            set { sweetness = value; }
        }

        [SerializeField]
        private int fear;
        public int Fear
        {
            get { return fear; }
            set { fear = value; }
        }


        [SerializeField]
        private int trust;
        public int Trust
        {
            get { return trust; }
            set { trust = value; }
        }

    }


    [System.Serializable]
    public class WeakPoint
    {
        [SerializeField]
        private int wordIndex;
        public int WordIndex
        {
            get { return wordIndex; }
            set { wordIndex = value; }
        }
        [SerializeField]
        private EmotionStat weakPointStat;
        public EmotionStat WeakPointStat
        {
            get { return weakPointStat; }
            set { weakPointStat = value; }
        }

    }




    /// <summary>
    /// Definition of the EnemyManager class
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        string enemyText;

        [SerializeField]
        int enemyHP = 100;
        [SerializeField]
        int enemyHPMax = 100;

        [SerializeField]
        EmotionStat enemyResistance;

        [SerializeField]
        WeakPoint[] enemyWeakPoints;

        [SerializeField]
        VoiceActorData voiceActor;

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

        public float DamagePhrase(Emotion[] emotions, int word)
        {
            float totalDamage = 0;
            EmotionStat statActor = voiceActor.Statistique;
            for(int i = 0; i < emotions.Length; i++)
            {
                switch(emotions[i])
                {
                    case Emotion.Joie:
                        totalDamage += statActor.Joy * ((100f + enemyResistance.Joy) / 100f);
                        break;
                    case Emotion.Tristesse:
                        totalDamage += statActor.Sadness * ((100f + enemyResistance.Sadness) / 100f);
                        break;
                    case Emotion.Dégoût:
                        totalDamage += statActor.Disgust * ((100f + enemyResistance.Disgust) / 100f);
                        break;
                    case Emotion.Colère:
                        totalDamage += statActor.Anger * ((100f + enemyResistance.Anger) / 100f);
                        break;
                    case Emotion.Surprise:
                        totalDamage += statActor.Surprise * ((100f + enemyResistance.Surprise) / 100f);
                        break;
                    case Emotion.Douceur:
                        totalDamage += statActor.Sweetness * ((100f + enemyResistance.Sweetness) / 100f);
                        break;
                    case Emotion.Peur:
                        totalDamage += statActor.Fear * ((100f + enemyResistance.Fear) / 100f);
                        break;
                    case Emotion.Confiance:
                        totalDamage += statActor.Trust * ((100f + enemyResistance.Trust) / 100f);
                        break;
                }
            }

            totalDamage += ApplyWordBonus(word, statActor);
            enemyHP -= (int) totalDamage;
            if (enemyHP < 0)
                enemyHP = 0;

            float percentage = ((float) enemyHP / (float) enemyHPMax) * 100;
            Debug.Log(100-percentage);
            return 100 - percentage;
        }

        private float ApplyWordBonus(int word, EmotionStat statActor)
        {
            float bonusDamage = 0;
            for (int i = 0; i < enemyWeakPoints.Length; i++)
            {
                if (word == enemyWeakPoints[i].WordIndex)
                {
                    bonusDamage += statActor.Joy * enemyWeakPoints[i].WeakPointStat.Joy;
                    bonusDamage += statActor.Sadness * enemyWeakPoints[i].WeakPointStat.Sadness;
                    bonusDamage += statActor.Disgust * enemyWeakPoints[i].WeakPointStat.Disgust;
                    bonusDamage += statActor.Anger * enemyWeakPoints[i].WeakPointStat.Anger;

                    bonusDamage += statActor.Surprise * enemyWeakPoints[i].WeakPointStat.Surprise;
                    bonusDamage += statActor.Sweetness * enemyWeakPoints[i].WeakPointStat.Sweetness;
                    bonusDamage += statActor.Fear * enemyWeakPoints[i].WeakPointStat.Fear;
                    bonusDamage += statActor.Trust * enemyWeakPoints[i].WeakPointStat.Trust;
                }
            }
            return bonusDamage;
        }

        #endregion

    } // EnemyManager class

} // #PROJECTNAME# namespace