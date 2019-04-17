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

        public TextData(TextDataContract data)
        {
            interlocuteur = data.TextStats.InterlocuteurID;
            hpMax = Random.Range(data.TextStats.HPMin, data.TextStats.HPMax);
            text = data.Text;
            enemyResistance = data.EnemyResistance;
            enemyWeakPoints = data.EnemyWeakPoints;
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
        VoiceActor voiceActor;


        [Header("Feedbacks")]
        [SerializeField]
        ParticleSystem[] particleFeedbacks;
        [SerializeField]
        Image haloCurrentEmotion;
        [SerializeField]
        TextMeshPro damageText;
        [SerializeField]
        TextMeshPro[] damageTextCritical;
        [SerializeField]
        ParticleSystem[] particleCritical;

        [SerializeField]
        GameObject criticalFeedback;
        [SerializeField]
        GameObject criticalFeedback2;

        Animator damageTextAnimator;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        public int GetHp()
        {
            return enemyHP;
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
        }

        public void SetVoiceActor(VoiceActor va)
        {
            voiceActor = va;
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

        public void SetTextData(TextData newTextData)
        {
            currentTextData = newTextData;
            enemyHP = currentTextData.HPMax;
        }

        public float DamagePhrase()
        {

            for (int i = 0; i < particleFeedbacks.Length; i++)
            {
                particleFeedbacks[i].Play();
            }
            return 0;
        }

        public float DamagePhrase(Emotion[] emotions, int word)
        {
            float totalDamage = 0;
            EmotionStat statActor = voiceActor.Statistique;

            int enemyHPMax = currentTextData.HPMax;
            EmotionStat enemyResistance = currentTextData.EnemyResistance;

            for(int i = 0; i < emotions.Length; i++)
            {

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

            totalDamage += ApplyWordBonus(totalDamage, word, statActor);
            totalDamage += Random.Range(-voiceActor.DamageVariance, voiceActor.DamageVariance+1);
            if(totalDamage <= 0)
            {
                totalDamage = 1;
            }
            enemyHP -= (int) totalDamage;
            if (enemyHP < 0)
                enemyHP = 0;

            ChangeParticleAttack(emotions);
            ChangeHaloEmotion(emotions);

            PrintDamage(totalDamage);

            Debug.Log(totalDamage);
            float percentage = 0;
            if (currentTextData.HPMax != 0)
            {
                percentage = ((float)enemyHP / (float)enemyHPMax) * 100;
            }           
            return 100 - percentage;
        }

        private float ApplyWordBonus(float totalDamage, int word, EmotionStat statActor)
        {
            float bonusDamage = 0;
            float[] allDamages = new float[9];
            allDamages[0] = totalDamage;
            WeakPoint[] enemyWeakPoints = currentTextData.EnemyWeakPoints;
            for (int i = 0; i < enemyWeakPoints.Length; i++)
            {
                if (word == enemyWeakPoints[i].WordIndex)
                {
                    bonusDamage += statActor.Joy * (enemyWeakPoints[i].WeakPointStat.Joy / 100f);
                    allDamages[1] = statActor.Joy * (enemyWeakPoints[i].WeakPointStat.Joy / 100f);
                    bonusDamage += statActor.Sadness * (enemyWeakPoints[i].WeakPointStat.Sadness / 100f);
                    allDamages[2] = statActor.Sadness * (enemyWeakPoints[i].WeakPointStat.Sadness / 100f);
                    bonusDamage += statActor.Disgust * (enemyWeakPoints[i].WeakPointStat.Disgust / 100f);
                    allDamages[3] = statActor.Disgust * (enemyWeakPoints[i].WeakPointStat.Disgust / 100f);
                    bonusDamage += statActor.Anger * (enemyWeakPoints[i].WeakPointStat.Anger / 100f);
                    allDamages[4] = statActor.Anger * (enemyWeakPoints[i].WeakPointStat.Anger / 100f);

                    bonusDamage += statActor.Surprise * (enemyWeakPoints[i].WeakPointStat.Surprise / 100f);
                    allDamages[5] = statActor.Surprise * (enemyWeakPoints[i].WeakPointStat.Surprise / 100f);
                    bonusDamage += statActor.Sweetness * (enemyWeakPoints[i].WeakPointStat.Sweetness / 100f);
                    allDamages[6] = statActor.Sweetness * (enemyWeakPoints[i].WeakPointStat.Sweetness / 100f);
                    bonusDamage += statActor.Fear * (enemyWeakPoints[i].WeakPointStat.Fear / 100f);
                    allDamages[7] = statActor.Fear * (enemyWeakPoints[i].WeakPointStat.Fear / 100f);
                    bonusDamage += statActor.Trust * (enemyWeakPoints[i].WeakPointStat.Trust / 100f);
                    allDamages[8] = statActor.Trust * (enemyWeakPoints[i].WeakPointStat.Trust / 100f);

                    Debug.Log("Weakpoint");
                    if (criticalFeedback != null)
                    {
                        criticalFeedback.SetActive(true);
                        criticalFeedback2.SetActive(true);
                        //StartDamageCriticalFeedback(allDamages);
                    }
                }
            }
            return bonusDamage;
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
        /*public void SetParticlePosition(Vector2 pos)
        {
            particleFeedbacks[2].transform.localPosition = new Vector3(pos.x, pos.y, 0);
        }*/

        private void ChangeParticleAttack(Emotion[] emotions)
        {
            if (particleFeedbacks == null)
                return;
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
                    case Emotion.Surprise:
                        colorEmotion = new Color(0.9f,0.55f,0.3f);
                        break;
                    case Emotion.Douceur:
                        colorEmotion = new Color(0.88f, 0.58f, 0.9f);
                        break;
                    case Emotion.Peur:
                        colorEmotion = Color.black;
                        break;
                    case Emotion.Confiance:
                        colorEmotion = Color.white;
                        break;
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

            for (int i = 0; i < particleFeedbacks.Length-1; i++)
            {
                particleFeedbacks[i].Play();
            }
        }

        private void ChangeHaloEmotion(Emotion[] emotions)
        {
            // Attention aux combo de 2 emotion quand il y a 3 slot, emotions peut retourner [emotion, emotion, neutre]
            if (haloCurrentEmotion == null)
                return;

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
                    case Emotion.Surprise:
                        colorEmotion = new Color(0.9f, 0.55f, 0.3f);
                        break;
                    case Emotion.Douceur:
                        colorEmotion = new Color(0.88f, 0.58f, 0.9f);
                        break;
                    case Emotion.Peur:
                        colorEmotion = Color.black;
                        break;
                    case Emotion.Confiance:
                        colorEmotion = Color.white;
                        break;
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


        public void ResetHalo()
        {
            haloCurrentEmotion.color = new Color(0, 0, 0, 0);
        }

        private void PrintDamage(float totalDamage)
        {
            if (damageText == null)
                return;
            damageText.gameObject.SetActive(true);
            damageText.text = totalDamage.ToString();
            damageText.transform.localScale = new Vector3(1, 1, 1);
            int timeFeedback = 40;
            float speed = (totalDamage / timeFeedback);
            //Debug.Log(speed);
            StartCoroutine(DamageTextCoroutine(speed, timeFeedback, 120));
        }

        private IEnumerator DamageTextCoroutine(float speed, int timeFeedback, int time)
        {
            float currentDamage = 0;
            while (time != 0)
            {
                time -= 1;
                if(timeFeedback > 0)
                {
                    timeFeedback -= 1;
                    currentDamage += speed;
                    damageText.text = ((int)currentDamage).ToString();
                }
                else if (timeFeedback == 0)
                {
                    damageTextAnimator.enabled = true;
                }
                yield return null;
            }
            damageText.text = "";
            damageTextAnimator.enabled = false;
            damageText.gameObject.SetActive(false);
        }

        #endregion

    } // EnemyManager class

} // #PROJECTNAME# namespace