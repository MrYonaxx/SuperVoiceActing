﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{


    [System.Serializable]
    public class TextData
    {
        [SerializeField]
        private int hpMax;
        public int HPMax
        {
            get { return hpMax; }
            set { hpMax = value; }
        }

        [HorizontalGroup("Texte")]
        [SerializeField]
        [TextArea]
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }


        [TabGroup("ParentGroup", "Statistiques")]
        //[BoxGroup("Statistique")]
        [SerializeField]
        private EmotionStat enemyResistance;
        public EmotionStat EnemyResistance
        {
            get { return enemyResistance; }
            set { enemyResistance = value; }
        }

        [TabGroup("ParentGroup", "Points faibles")]
        [SerializeField]
        private WeakPoint[] enemyWeakPoints;
        public WeakPoint[] EnemyWeakPoints
        {
            get { return enemyWeakPoints; }
            set { enemyWeakPoints = value; }
        }

    }

    [System.Serializable]
    public class EmotionStat
    {
        //[Gr("Statistiques")]
        [Title("Neutr", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int neutral;
        public int Neutral
        {
            get { return neutral; }
            set { neutral = value; }
        }

        [Title("Joie", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int joy;
        public int Joy
        {
            get { return joy; }
            set { joy = value; }
        }

        [Title("Triste", " ", TitleAlignments.Centered)]
        //[Title("Tristesse", titleAlignment: TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int sadness;
        public int Sadness
        {
            get { return sadness; }
            set { sadness = value; }
        }

        [Title("Dégo", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int disgust;
        public int Disgust
        {
            get { return disgust; }
            set { disgust = value; }
        }

        [Title("Colèr", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int anger;
        public int Anger
        {
            get { return anger; }
            set { anger = value; }
        }

        [Title("Surpr", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int surprise;
        public int Surprise
        {
            get { return surprise; }
            set { surprise = value; }
        }

        [Title("Douce", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int sweetness;
        public int Sweetness
        {
            get { return sweetness; }
            set { sweetness = value; }
        }

        [Title("Peur", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int fear;
        public int Fear
        {
            get { return fear; }
            set { fear = value; }
        }

        [Title("Confi", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("Stat", PaddingLeft = 10, Width = 50)]
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
        [Header("WeakPoint")]
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

        [Header("TextData")]
        [Space]
        [SerializeField]
        TextData currentTextData;

        int enemyHP = 100;

        [Header("Current Voice actor")]
        [Space]
        [SerializeField]
        VoiceActorData voiceActor;

        [Header("Feedbacks")]
        [SerializeField]
        ParticleSystem[] particleFeedbacks;
        [SerializeField]
        Image haloCurrentEmotion;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public float GetHpPercentage()
        {
            if(currentTextData.HPMax == 0)
            {
                return 0;
            }
            return ((float)enemyHP / (float) currentTextData.HPMax) * 100;
        }

        public void SetHp(int newHP)
        {
            enemyHP = newHP;
        }

        #endregion

        #region Functions 


        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void SetTextData(TextData newTextData)
        {
            enemyHP = currentTextData.HPMax;
            currentTextData = newTextData;
        }

        public float DamagePhrase(Emotion[] emotions, int word)
        {
            float totalDamage = 0;
            EmotionStat statActor = voiceActor.Statistique;

            int enemyHPMax = currentTextData.HPMax;
            EmotionStat enemyResistance = currentTextData.EnemyResistance;

            for(int i = 0; i < emotions.Length; i++)
            {
                Debug.Log("hey");
                switch(emotions[i])
                {
                    case Emotion.Neutre:
                        totalDamage += statActor.Neutral * ((100f + enemyResistance.Neutral) / 100f);
                        break;
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

            ChangeParticleAttack(emotions);
            ChangeHaloEmotion(emotions);

            Debug.Log(totalDamage);
            float percentage = 0;
            if (currentTextData.HPMax != 0)
            {
                percentage = ((float)enemyHP / (float)enemyHPMax) * 100;
            }           
            return 100 - percentage;
        }

        private float ApplyWordBonus(int word, EmotionStat statActor)
        {
            float bonusDamage = 0;
            WeakPoint[] enemyWeakPoints = currentTextData.EnemyWeakPoints;
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

                    Debug.Log("Weakpoint");
                }
            }
            return bonusDamage;
        }





        // ======================== //
        //        Feedback          //
        // ======================== //

        private void ChangeParticleAttack(Emotion[] emotions)
        {
            Color colorEmotion;
            for (int i = 0; i < emotions.Length; i++)
            {
                switch (emotions[i])
                {
                    case Emotion.Joie:
                        colorEmotion = Color.yellow;
                        break;
                    case Emotion.Tristesse:
                        colorEmotion = Color.blue;
                        break;
                    case Emotion.Dégoût:
                        colorEmotion = Color.green;
                        break;
                    case Emotion.Colère:
                        colorEmotion = Color.red;
                        break;
                    /*case Emotion.Surprise:
                        deckEmotion.Surprise = number;
                        break;
                    case Emotion.Douceur:
                        deckEmotion.Sweetness = number;
                        break;
                    case Emotion.Peur:
                        deckEmotion.Fear = number;
                        break;
                    case Emotion.Confiance:
                        deckEmotion.Trust = number;
                        break;*/
                    default:
                        colorEmotion = Color.white;
                        break;
                }
                if (colorEmotion == Color.white && i > 0)
                    return;
                else
                {
                    for (int j = i; j < particleFeedbacks.Length; j++)
                    {
                        var particleColor = particleFeedbacks[j].main;
                        particleColor.startColor = colorEmotion;
                    }
                }
            }
        }

        private void ChangeHaloEmotion(Emotion[] emotions)
        {
            // Attention aux combo de 2 emotion quand il y a 3 slot, emotions peut retourner [emotion, emotion, neutre]

            int size = emotions.Length;
            Color colorEmotion = new Color(0, 0, 0, 0);
            for (int i = 0; i < emotions.Length; i++)
            {
                switch (emotions[i])
                {
                    case Emotion.Joie:
                        colorEmotion += Color.yellow;
                        break;
                    case Emotion.Tristesse:
                        colorEmotion += Color.blue;
                        break;
                    case Emotion.Dégoût:
                        colorEmotion += Color.green;
                        break;
                    case Emotion.Colère:
                        colorEmotion += Color.red;
                        break;
                    /*case Emotion.Surprise:
                        deckEmotion.Surprise = number;
                        break;
                    case Emotion.Douceur:
                        deckEmotion.Sweetness = number;
                        break;
                    case Emotion.Peur:
                        deckEmotion.Fear = number;
                        break;
                    case Emotion.Confiance:
                        deckEmotion.Trust = number;
                        break;*/
                    default:
                        if (i == 0)
                            colorEmotion = Color.white;
                        else
                            size -= 1;
                        break;
                }
            }
            colorEmotion = new Color(colorEmotion.r, colorEmotion.g, colorEmotion.b, 0.1f * size);
            StartCoroutine(ChangeHaloEmotionCoroutine(colorEmotion, 70));
        }

        private IEnumerator ChangeHaloEmotionCoroutine(Color colorEmotion, int time)
        {
            float speedRed = (colorEmotion.r - haloCurrentEmotion.color.r) / time;
            float speedGreen = (colorEmotion.g - haloCurrentEmotion.color.g) / time;
            float speedBlue = (colorEmotion.b - haloCurrentEmotion.color.b) / time;
            float speedAlpha = (colorEmotion.a - haloCurrentEmotion.color.a) / time;
            while(time != 0)
            {
                time -= 1;
                haloCurrentEmotion.color += new Color(speedRed, speedGreen, speedBlue, speedAlpha);
                yield return null;
            }
        }

        #endregion

    } // EnemyManager class

} // #PROJECTNAME# namespace