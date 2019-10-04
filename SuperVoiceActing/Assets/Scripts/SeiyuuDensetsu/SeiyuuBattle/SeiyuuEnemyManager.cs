/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuEnemyManager : EnemyManager
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshPro enemyTextDamage;
        [SerializeField]
        Animator enemyTextDamageAnimator;


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

        public override void SetTextData(TextData newTextData)
        {
            currentTextData = newTextData;
            currentTextData.HPMax *= 10;
            enemyHP = currentTextData.HPMax / 2;
        }


        public float DamagePhrase(Emotion[] emotions, int damage, int damageVariance)
        {
            int multiplier = 0;
            int comboSize = emotions.Length;

            int enemyHPMax = currentTextData.HPMax;
            EmotionStat enemyResistance = currentTextData.EnemyResistance;

            Color colorEmotion = Color.white;

            for (int i = 0; i < emotions.Length; i++)
            {
                if (emotions[i] == Emotion.Neutre)
                {
                    comboSize -= 1;
                    particleFeedbacks[i].gameObject.SetActive(false);
                    continue;
                }
                multiplier += enemyResistance.GetEmotion((int)emotions[i]);

                colorEmotion = colorsEmotions[(int)emotions[i]];
                if (particleFeedbacks.Length != 0)
                {
                    particleFeedbacks[i].gameObject.SetActive(true);
                    for (int j = i; j < particleFeedbacks.Length; j++)
                    {
                        var particleColor = particleFeedbacks[j].main;
                        particleColor.startColor = colorEmotion;
                    }
                }
            }

            multiplier = multiplier / comboSize;
            if (damage > 0)
            {
                damage = (int)(damage * ((100 + multiplier) / 100f));
                damage += Random.Range(-damageVariance, damageVariance + 1);
                if (damage <= 0)
                    damage = 1;
            }
            else
            {
                damage = (int)(damage * ((100 + multiplier) / 100f));
                damage += Random.Range(-damageVariance, damageVariance + 1);
                if (damage >= 0)
                    damage = -1;
            }



            enemyHP -= (int)damage;
            if (enemyHP < 0)
                enemyHP = 0;
            else if (enemyHP > enemyHPMax)
                enemyHP = enemyHPMax;

            ChangeParticleAttack();
            ChangeHaloEmotion(colorEmotion, comboSize);

            if (damage < 0)
                PrintDamageEnemy(damage);
            else
                PrintDamage(damage);

            float percentage = 0;
            if (currentTextData.HPMax != 0)
            {
                percentage = ((float)enemyHP / (float)enemyHPMax) * 100;
            }
            return 100 - percentage;
        }

         

        // return la meilleur ou la deuxième meilleur faiblesse
        public Emotion GetEmotionHint()
        {
            int bestStat = 0;
            int secondBestStat = 0;

            int bestStatID = 0;
            int secondBestStatID = 0;

            int currentStat = 0;
            for (int i = 0; i < currentTextData.EnemyResistance.GetLength(); i++)
            {
                currentStat = currentTextData.EnemyResistance.GetEmotion(i);
                if (currentStat > bestStat)
                {
                    secondBestStat = bestStat;
                    secondBestStatID = bestStatID;
                    bestStat = currentStat;
                    bestStatID = i;
                }
                else if (currentStat > secondBestStat)
                {
                    secondBestStat = currentStat;
                    secondBestStatID = i;
                }
            }
            int rand = Random.Range(0, bestStat + secondBestStat);
            if(rand <= bestStat)
            {
                return (Emotion)bestStatID;
            }
            else
            {
                return (Emotion)secondBestStatID;
            }

            //return bestStat;
        }




        protected void PrintDamageEnemy(float totalDamage)
        {
            if (damageText == null)
                return;

            StartCoroutine(EnemyDamageTextCoroutine(totalDamage, 1, 2));
        }

        private IEnumerator EnemyDamageTextCoroutine(float totalDamage, int timeFeedback, int time)
        {
            enemyTextDamage.gameObject.SetActive(true);
            enemyTextDamage.transform.localScale = new Vector3(1, 1, 1);
            enemyTextDamage.text = "0";

            float t = 0f;
            while (t < time)
            {
                t += Time.deltaTime;
                if (t < timeFeedback)
                {
                    enemyTextDamage.text = ((int)(totalDamage * (t /timeFeedback))).ToString();
                }
                else if (timeFeedback == 0)
                {
                    enemyTextDamageAnimator.enabled = true;
                }
                yield return null;
            }
            enemyTextDamage.text = "";
            enemyTextDamageAnimator.enabled = false;
            enemyTextDamage.gameObject.SetActive(false);
        }




        #endregion
    }
}
