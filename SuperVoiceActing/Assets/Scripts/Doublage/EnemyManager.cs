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
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{


    [System.Serializable]
    public class TextData
    {
        [SerializeField]
        private int interlocuteur;
        public int Interlocuteur
        {
            get { return interlocuteur; }
            set { interlocuteur = value; }
        }

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
        [SerializeField]
        private EmotionStat enemyResistance;
        public EmotionStat EnemyResistance
        {
            get { return enemyResistance; }
            set { enemyResistance = value; }
        }

        [TabGroup("ParentGroup", "Points faibles")]
        [SerializeField]
        private List<int> enemyWeakPoints;
        public List<int> EnemyWeakPoints
        {
            get { return enemyWeakPoints; }
            set { enemyWeakPoints = value; }
        }

        [TabGroup("ParentGroup", "Event")]
        [SerializeField]
        private List<DoublageEventData> eventDatas;
        public List<DoublageEventData> EventDatas
        {
            get { return eventDatas; }
            set { eventDatas = value; }
        }

        public TextData(TextDataContract data)
        {
            interlocuteur = data.InterlocuteurID;
            hpMax = Random.Range(data.HPMin, data.HPMax);
            if (hpMax <= 0)
                hpMax = 1;
            text = data.Text;
            enemyResistance = data.EnemyResistance;
            enemyWeakPoints = new List<int>(data.EnemyWeakPoints.Length);
            for (int i = 0; i < data.EnemyWeakPoints.Length; i++)
            {
                enemyWeakPoints.Add(data.EnemyWeakPoints[i].WordIndex);
            }
            if (data.EventData != null)
            {
                eventDatas = new List<DoublageEventData>(data.EventData.Length);
                for (int i = 0; i < data.EventData.Length; i++)
                {
                    eventDatas.Add(data.EventData[i]);
                }
            }
            /*int word = 0;
            enemyWeakPoints = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    word += 1;
                else if (text[i] == '#')
                {
                    enemyWeakPoints.Add(word);
                    text[i] = ' ';
                }

            }*/
            //enemyWeakPoints = data.EnemyWeakPoints;
        }

        public TextData(CustomTextData data)
        {
            interlocuteur = data.InterlocuteurID;
            hpMax = Random.Range(data.HPMin, data.HPMax);
            if (hpMax <= 0)
                hpMax = 1;
            text = data.Text;
            enemyResistance = data.EnemyResistance;
            enemyWeakPoints = new List<int>(data.EnemyWeakPoints.Length);
            for (int i = 0; i < data.EnemyWeakPoints.Length; i++)
            {
                enemyWeakPoints.Add(data.EnemyWeakPoints[i].WordIndex);
            }
            if (data.EventData != null)
            {
                eventDatas = new List<DoublageEventData>(data.EventData.Length);
                for (int i = 0; i < data.EventData.Length; i++)
                {
                    eventDatas.Add(data.EventData[i]);
                }
            }
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
        /*[SerializeField]
        private EmotionStat weakPointStat;
        public EmotionStat WeakPointStat
        {
            get { return weakPointStat; }
            set { weakPointStat = value; }
        }*/

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
        protected TextData currentTextData;

        protected int enemyHP = 100;
        //int enemyHPMax = 100;

        [Title("Info")]
        [SerializeField]
        protected Animator animatorEnemyHP;
        [SerializeField]
        protected TextMeshProUGUI textEnemyHP;
        [SerializeField]
        protected RectTransform transformEnemyHP;
        [SerializeField]
        protected Animator[] animatorEnemyCritical;

        [Title("Feedbacks")]
        [SerializeField]
        protected ParticleSystem[] particleFeedbacks;
        [SerializeField]
        protected Color[] colorsEmotions;
        [SerializeField]
        protected Image haloCurrentEmotion;
        [SerializeField]
        protected TextMeshPro damageText;
        [SerializeField]
        protected TextMeshPro[] damageTextCritical;
        [SerializeField]
        protected ParticleSystem[] particleCritical;

        [HorizontalGroup]
        [SerializeField]
        protected float[] damageLevel;
        [HorizontalGroup]
        [SerializeField]
        protected Color[] damageColorLevel;

        [SerializeField]
        protected Animator animatorDamageTextResult;
        [SerializeField]
        protected Animator animatorLightFeedback;
        [HorizontalGroup("textResult")]
        [SerializeField]
        protected float[] damageLevelText;
        [HorizontalGroup("textResult")]
        [SerializeField]
        protected string[] textResult;
        [HorizontalGroup("textResult")]
        [SerializeField]
        protected TextMeshProUGUI[] textMeshResult;


        [SerializeField]
        protected Animator criticalFeedback;

        [Title("Feedbacks Guard")]
        [SerializeField]
        protected ParticleSystem guardFeedback;

        protected Animator damageTextAnimator;


        protected int lastAttackScore = 0;
        protected float criticalMultiplier = 0.25f;

        protected bool lastAttackCritical = false;
        protected bool damageOnlyCritical = false;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        public int GetHp()
        {
            return enemyHP;
        }

        public int GetHpMax()
        {
            return currentTextData.HPMax;
        }

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
            if (enemyHP < 0)
                enemyHP = 0;
            else if (enemyHP > currentTextData.HPMax)
                enemyHP = currentTextData.HPMax;
        }

        public int GetInterlocutor()
        {
            return currentTextData.Interlocuteur;
        }

        public bool CheckInterlocutor(int currentInterlocutor)
        {
            return (currentInterlocutor == currentTextData.Interlocuteur);
        }

        public int GetLastAttackScore()
        {
            return lastAttackScore;
        }

        public bool GetLastAttackCritical()
        {
            return lastAttackCritical;
        }

        public void SetDamageOnlyCritical(bool b)
        {
            damageOnlyCritical = b;
        }

        #endregion

        #region Functions 


        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        private void Start()
        {
            if(damageText != null)
                damageTextAnimator = damageText.GetComponent<Animator>();
        }

        public virtual void SetTextData(TextData newTextData)
        {
            currentTextData = newTextData;
            enemyHP = currentTextData.HPMax;
            DrawTextHP();
        }

        public void ShowHPEnemy(bool b)
        {
            animatorEnemyHP.SetBool("Appear", b);
        }

        public void DrawTextHP()
        {
            textEnemyHP.text = enemyHP.ToString();
            transformEnemyHP.localScale = new Vector3(enemyHP / currentTextData.HPMax, transformEnemyHP.localScale.y, transformEnemyHP.localScale.z);
            for(int i = 0; i < currentTextData.EnemyWeakPoints.Count; i++)
            {
                animatorEnemyCritical[i].gameObject.SetActive(true);
            }
            for(int i = currentTextData.EnemyWeakPoints.Count; i < animatorEnemyCritical.Length; i++)
            {
                animatorEnemyCritical[i].gameObject.SetActive(false);
            }
        }






        public float DamagePhrase()
        {
            for (int i = 0; i < particleFeedbacks.Length; i++)
            {
                particleFeedbacks[i].gameObject.SetActive(true);
                particleFeedbacks[i].Play();
            }
            return 0;
        }

        public float DamagePhrase(int damage, Emotion[] emotions, int word)
        {
            int multiplier = 0;
            int comboSize = emotions.Length;
            float totalDamage = 0;

            int enemyHPMax = currentTextData.HPMax;
            EmotionStat enemyResistance = currentTextData.EnemyResistance;
            Color colorEmotion = Color.white;

            for (int i = 0; i < emotions.Length; i++)
            {
                if(emotions[i] == Emotion.Neutre)
                {
                    comboSize -= 1;
                    particleFeedbacks[i].gameObject.SetActive(false);
                    textMeshResult[i].gameObject.SetActive(false);
                    continue;
                }
                DrawTextResult(enemyResistance.GetEmotion((int)emotions[i]), textMeshResult[i], (int)emotions[i]);
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

            animatorDamageTextResult.gameObject.SetActive(true);
            animatorLightFeedback.gameObject.SetActive(true);



            multiplier = multiplier / comboSize;

            totalDamage = damage * ((100 + multiplier) / 100f);
            totalDamage += ApplyWordBonus(totalDamage, word);

            if (damageOnlyCritical == true && lastAttackCritical == false)
            {
                NullifyFeedback();
                return 100 - ((float)enemyHP / (float)enemyHPMax) * 100;
            }

            if (totalDamage <= 0)
                totalDamage = 1;

            lastAttackScore = multiplier + (int)(totalDamage / 10);

            enemyHP -= (int) totalDamage;
            if (enemyHP < 0)
                enemyHP = 0;

            ChangeParticleAttack();
            ChangeHaloEmotion(colorEmotion, comboSize);

            PrintDamage(totalDamage);

            float percentage = 0;
            if (currentTextData.HPMax != 0)
            {
                percentage = ((float)enemyHP / (float)enemyHPMax) * 100;
            }           
            return 100 - percentage;
        }


        private float ApplyWordBonus(float totalDamage, int word)
        {
            float bonusDamage = 0;
            lastAttackCritical = false;
            for (int i = 0; i < currentTextData.EnemyWeakPoints.Count; i++)
            {
                if (word == currentTextData.EnemyWeakPoints[i])
                {
                    bonusDamage = totalDamage * criticalMultiplier;
                    lastAttackCritical = true;
                    if (criticalFeedback != null)
                    {
                        criticalFeedback.gameObject.SetActive(true);
                        criticalFeedback.SetTrigger("Feedback");
                    }
                    currentTextData.EnemyWeakPoints[i] = -1;
                    animatorEnemyCritical[i].SetTrigger("Feedback");
                }
            }
            return bonusDamage;
        }







        public bool CheckIfMiss(int actorStat)
        {
            int rand = Random.Range(0, 100);
            if(rand < actorStat)
                return true;
            return false;
        }











        public int GetBestMultiplier()
        {
            int bestStat = 0;
            int currentStat = 0;
            for (int i = 0; i < currentTextData.EnemyResistance.GetLength(); i++)
            {
                currentStat = currentTextData.EnemyResistance.GetEmotion(i);
                if (currentStat > bestStat)
                    bestStat = currentStat;
            }
            return bestStat;
        }










        // Section Feedback ===============================================================================



        public void StartDamageCriticalFeedback(float[] allDamages)
        {
            int count = 0;
            for(int i = 0; i < allDamages.Length; i++)
            {
                if(allDamages[i] > 0)
                {
                    damageTextCritical[i].text = ((int)allDamages[i]).ToString();
                    StartCoroutine(BonusDamageCoroutine(damageTextCritical[i], count, particleCritical[i]));
                    
                    count += 1;
                }
            }
        }

        private IEnumerator BonusDamageCoroutine(TextMeshPro textMesh, int count, ParticleSystem particleCritical)
        {
            yield return new WaitForSeconds(count/8f);
            textMesh.color = new Color(1, 1, 1, 1);
            textMesh.rectTransform.anchoredPosition3D = new Vector3(Random.Range(-6f, 6f), Random.Range(-4f, 1f), -0.1f);
            particleCritical.transform.position = textMesh.transform.position;
            particleCritical.Play();
            float speed = 0.2f;
            int time = 80;
            bool stop = false;
            while(time > 0)
            {
                if(speed <= -0.3f)
                {
                    speed = 0;
                    stop = true;
                }
                else if (stop == false)
                {
                    speed -= 0.005f;
                }
                textMesh.rectTransform.anchoredPosition3D += new Vector3(0, speed, 0);
                if(time < 20)
                    textMesh.color -= new Color(0, 0, 0, 0.1f);
                time -= 1;
                yield return null;
            }
        }


        // ======================== //
        //        Feedback          //
        // ======================== //

        protected void ChangeParticleAttack()
        {
            for (int i = 0; i < particleFeedbacks.Length-2; i++)
            {
                particleFeedbacks[i].Play();
            }
        }

        protected void ChangeHaloEmotion(Color colorEmotion, int size)
        {
            // Attention aux combo de 2 emotion quand il y a 3 slot, emotions peut retourner [emotion, emotion, neutre]
            if (haloCurrentEmotion == null)
                return;
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

        public void ResetHalo()
        {
            if(haloCurrentEmotion != null)
                haloCurrentEmotion.color = new Color(0, 0, 0, 0);
        }


        protected void DrawTextResult(int multiplier, TextMeshProUGUI textMesh, int emotion)
        {
            textMesh.gameObject.SetActive(false);
            for (int i = 0; i < damageLevelText.Length; i++)
            {
                if (multiplier >= damageLevelText[i])
                {
                    textMesh.gameObject.SetActive(true);
                    textMesh.color = colorsEmotions[emotion];
                    textMesh.text = textResult[i];
                }
            }
        }


        protected void PrintDamage(float totalDamage)
        {
            if (damageText == null)
                return;
            damageText.gameObject.SetActive(true);
            damageText.text = totalDamage.ToString();
            damageText.transform.localScale = new Vector3(1, 1, 1);
            float timeFeedback = 40 / 60f;
            float speed = (totalDamage / timeFeedback);
            StartCoroutine(DamageTextCoroutine(totalDamage, speed, timeFeedback, 120/60f));
        }

        private IEnumerator DamageTextCoroutine(float totalDamage, float speed, float timeFeedback, float time)
        {
            float t = 0f;       
            while (t < 1f)
            {
                t += (Time.deltaTime / time);
                if (t < (timeFeedback / time))
                {
                    damageText.text = ((int)(totalDamage * (t / (timeFeedback / time)))).ToString();
                }
                else
                {
                    damageText.text = ((int)totalDamage).ToString();
                    damageTextAnimator.enabled = true;
                }
                yield return null;
            }
            damageText.text = "";
            damageTextAnimator.enabled = false;
            damageText.gameObject.SetActive(false);
            //DrawTextHP();
        }


        private void ChangeTextColor(float normalDamage, float currentDamage)
        {
            for(int i = 0; i < damageLevel.Length; i++)
            {
                if((currentDamage / normalDamage) <= damageLevel[i])
                {
                    damageText.color = damageColorLevel[i];
                    return;
                }
            }
        }


        public void NullifyFeedback()
        {
            guardFeedback.gameObject.SetActive(true);
            guardFeedback.Play();
        }


        #endregion

    } // EnemyManager class

} // #PROJECTNAME# namespace