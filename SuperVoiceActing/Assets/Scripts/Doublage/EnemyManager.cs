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
        private WeakPoint[] enemyWeakPoints;
        public WeakPoint[] EnemyWeakPoints
        {
            get { return enemyWeakPoints; }
            set { enemyWeakPoints = value; }
        }

        public TextData(TextDataContract data)
        {
            interlocuteur = data.TextStats.InterlocuteurID;
            hpMax = Random.Range(data.TextStats.HPMin, data.TextStats.HPMax);
            text = data.Text;
            enemyResistance = data.EnemyResistance;
            enemyWeakPoints = data.EnemyWeakPoints;
        }

        public TextData(CustomTextData data)
        {
            interlocuteur = data.TextStats.InterlocuteurID;
            hpMax = Random.Range(data.TextStats.HPMin, data.TextStats.HPMax);
            text = data.Text;
            enemyResistance = data.EnemyResistance;
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

        public EmotionStat()
        {
            this.joy = 0;
            this.sadness = 0;
            this.disgust = 0;
            this.anger = 0;
            this.surprise = 0;
            this.sweetness = 0;
            this.fear = 0;
            this.trust = 0;
            this.neutral = 0;
        }

        public EmotionStat(int jo, int sa, int di, int an, int su, int sw, int fe, int tr)
        {
            this.joy = jo;
            this.sadness = sa;
            this.disgust = di;
            this.anger = an;
            this.surprise = su;
            this.sweetness = sw;
            this.fear = fe;
            this.trust = tr;
            this.neutral = (jo + sa + di + an + su + sw + fe + tr) / 8;
        }

        public EmotionStat(EmotionStat newStat)
        {
            this.joy = newStat.Joy;
            this.sadness = newStat.Sadness;
            this.disgust = newStat.Disgust;
            this.anger = newStat.Anger;
            this.surprise = newStat.Surprise;
            this.sweetness = newStat.Sweetness;
            this.fear = newStat.Fear;
            this.trust = newStat.Trust;
            this.neutral = (joy + sadness + disgust + anger + surprise + sweetness + fear + trust) / 8;
        }

        public void Add(EmotionStat addition)
        {
            this.joy += addition.joy;
            this.sadness += addition.sadness;
            this.disgust += addition.disgust;
            this.anger += addition.anger;
            this.surprise += addition.surprise;
            this.sweetness += addition.sweetness;
            this.fear += addition.fear;
            this.trust += addition.trust;
            this.neutral += addition.neutral;
        }

        public void Add(int emotion, int addValue)
        {
            switch (emotion)
            {
                case 0: // Neutr
                    neutral += addValue;
                    break;
                case 1: // Joie
                    joy += addValue;
                    break;
                case 2: // Tristesse
                    sadness += addValue;
                    break;
                case 3: // Degout
                    disgust += addValue;
                    break;
                case 4: // Anger
                    anger += addValue;
                    break;
                case 5: // Surprise
                    surprise += addValue;
                    break;
                case 6: // Douceur
                    sweetness += addValue;
                    break;
                case 7: // Peur
                    fear += addValue;
                    break;
                case 8: // confiance
                    trust += addValue;
                    break;
            }
        }

        public void Clamp(int minValue, int maxValue)
        {
            this.joy = Mathf.Clamp(this.joy, minValue, maxValue);
            this.sadness = Mathf.Clamp(this.sadness, minValue, maxValue);
            this.disgust = Mathf.Clamp(this.disgust, minValue, maxValue);
            this.anger = Mathf.Clamp(this.anger, minValue, maxValue);
            this.surprise = Mathf.Clamp(this.surprise, minValue, maxValue);
            this.sweetness = Mathf.Clamp(this.sweetness, minValue, maxValue);
            this.fear = Mathf.Clamp(this.fear, minValue, maxValue);
            this.trust = Mathf.Clamp(this.trust, minValue, maxValue);
            this.neutral = Mathf.Clamp(this.neutral, minValue, maxValue);
        }

        public void Substract(EmotionStat addition)
        {
            this.joy -= addition.joy;
            this.sadness -= addition.sadness;
            this.disgust -= addition.disgust;
            this.anger -= addition.anger;
            this.surprise -= addition.surprise;
            this.sweetness -= addition.sweetness;
            this.fear -= addition.fear;
            this.trust -= addition.trust;
            this.neutral -= addition.neutral;
        }

        public EmotionStat Reverse()
        {
            return new EmotionStat(-this.joy, -this.sadness, -this.disgust, -this.anger, -this.surprise, -this.sweetness, -this.fear, -this.trust);
        }

        public int GetEmotion(int emotion)
        {
            switch (emotion)
            {
                case 0: // Neutr
                    return neutral;
                case 1: // Joie
                    return joy;
                case 2: // Tristesse
                    return sadness;
                case 3: // Degout
                    return disgust;
                case 4: // Anger
                    return anger;
                case 5: // Surprise
                    return surprise;
                case 6: // Douceur
                    return sweetness;
                case 7: // Peur
                    return fear;
                case 8: // confiance
                    return trust;
            }
            return 0;
        }

        public int SetValue(int emotion, int newValue)
        {
            switch (emotion)
            {
                case 0: // Neutr
                    neutral = newValue;
                    break;
                case 1: // Joie
                    joy = newValue;
                    break;
                case 2: // Tristesse
                    sadness = newValue;
                    break;
                case 3: // Degout
                    disgust = newValue;
                    break;
                case 4: // Anger
                    anger = newValue;
                    break;
                case 5: // Surprise
                    surprise = newValue;
                    break;
                case 6: // Douceur
                    sweetness = newValue;
                    break;
                case 7: // Peur
                    fear = newValue;
                    break;
                case 8: // confiance
                    trust = newValue;
                    break;
            }
            return 0;
        }

        public int GetLength()
        {
            return 9;
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
        protected GameObject criticalFeedback;
        [SerializeField]
        protected GameObject criticalFeedback2;

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

        public float DamagePhrase(EmotionCard[] emotions, int word, int damageVariance)
        {
            int multiplier = 0;
            int comboSize = emotions.Length;
            float totalDamage = 0;
            float normalDamage = 0;

            int enemyHPMax = currentTextData.HPMax;
            EmotionStat enemyResistance = currentTextData.EnemyResistance;
            Color colorEmotion = Color.white;

            for (int i = 0; i < emotions.Length; i++)
            {
                if(emotions[i] == null)
                {
                    comboSize -= 1;
                    particleFeedbacks[i].gameObject.SetActive(false);
                    textMeshResult[i].gameObject.SetActive(false);
                    continue;
                }
                DrawTextResult(enemyResistance.GetEmotion((int)emotions[i].GetEmotion()), textMeshResult[i], (int)emotions[i].GetEmotion());
                multiplier += enemyResistance.GetEmotion((int)emotions[i].GetEmotion());
                colorEmotion = colorsEmotions[(int)emotions[i].GetEmotion()];

                normalDamage += emotions[i].GetStat();

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

            totalDamage = normalDamage * ((100 + multiplier) / 100f);
            totalDamage += ApplyWordBonus(totalDamage, word);
            totalDamage += Random.Range(-damageVariance, damageVariance+1);

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
            WeakPoint[] enemyWeakPoints = currentTextData.EnemyWeakPoints;
            lastAttackCritical = false;
            for (int i = 0; i < enemyWeakPoints.Length; i++)
            {
                if (word == enemyWeakPoints[i].WordIndex)
                {
                    bonusDamage = totalDamage * criticalMultiplier;
                    lastAttackCritical = true;
                    if (criticalFeedback != null)
                    {
                        criticalFeedback.SetActive(true);
                        criticalFeedback2.SetActive(true);
                    }
                }
            }
            return bonusDamage;
        }


        //





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
            float currentDamage = 0;
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
            /*while (time != 0)
            {
                time -= 1;
                if(timeFeedback > 0)
                {
                    timeFeedback -= 1;
                    currentDamage += speed;
                    ChangeTextColor(totalDamage, currentDamage);
                    damageText.text = ((int)currentDamage).ToString();
                }
                else if (timeFeedback == 0)
                {
                    damageTextAnimator.enabled = true;
                }
                yield return null;
            }*/
            damageText.text = "";
            damageTextAnimator.enabled = false;
            damageText.gameObject.SetActive(false);
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