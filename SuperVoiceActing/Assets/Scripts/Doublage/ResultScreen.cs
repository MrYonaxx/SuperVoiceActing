/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace VoiceActing
{
	public class ResultScreen : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("NextScene")]
        [SerializeField]
        private string nextScene = "Bureau";


        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI textName;
        [SerializeField]
        private TextMeshProUGUI textSessionNumber;

        [SerializeField]
        private TextMeshProUGUI textCurrentLine;
        [SerializeField]
        private TextMeshProUGUI textMaxLine;

        [SerializeField]
        private TextMeshProUGUI textTurn;
        [SerializeField]
        private TextMeshProUGUI textEXP;
        [SerializeField]
        private TextMeshProUGUI textExpBonus;

        [Header("UI")]
        [SerializeField]
        private RectTransform lineGauge;
        [SerializeField]
        private RectTransform lineNewGauge;

        [SerializeField]
        private Contract contract;




        [Header("Actors")]
        [SerializeField]
        private ExperienceCurveData experience;

        [SerializeField]
        private Image[] actorsImage;
        [SerializeField]
        private TextMeshProUGUI[] textsLevel;
        [SerializeField]
        private TextMeshProUGUI[] textsNext;
        [SerializeField]
        private RectTransform[] expGauge;



        [Header("LevelUp")]
        [SerializeField]
        private TextMeshProUGUI textOldLevel;
        [SerializeField]
        private TextMeshProUGUI textNewLevel;
        [SerializeField]
        private TextMeshProUGUI[] textsStats;
        [SerializeField]
        private RectTransform[] gaugeStats;
        [SerializeField]
        private RectTransform[] newGaugeStats;
        [SerializeField]
        private TextMeshProUGUI[] textsGain;


        private bool[] actorsLevelUp;
        private int[] actorsOldLevel;
        private EmotionStat[] actorsOldStats;

        [Header("Feedback")]
        [SerializeField]
        private Animator resultScreen;
        [SerializeField]
        private Animator[] animatorLevelUpFeedback;
        [SerializeField]
        private Animator[] animatorStats;
        [SerializeField]
        private Animator animatorEndStatScreen;
        [SerializeField]
        private AudioSource yokaiDisco;

        private bool inAnimation = true;
        private int lineDefeated = 0;

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
        public void ChangeEndScene(string newEndScene)
        {
            nextScene = newEndScene;
        }

        public void SetContract(Contract con)
        {
            contract = con;

            actorsLevelUp = new bool[contract.VoiceActors.Count];
            for (int i = 0; i < actorsLevelUp.Length; i++)
            {
                actorsLevelUp[i] = false;
            }
            actorsOldLevel = new int[contract.VoiceActors.Count];
            actorsOldStats = new EmotionStat[contract.VoiceActors.Count];
        }


        public void DrawResult(int numberTurn, int lineDefeated)
        {
            this.lineDefeated = lineDefeated;
            // Draw
            textName.text = contract.Name;
            textSessionNumber.text = contract.SessionNumber.ToString();
            textCurrentLine.text = contract.CurrentLine.ToString();
            textMaxLine.text = contract.TotalLine.ToString();

            textTurn.text = (15-numberTurn).ToString();

            textEXP.text = "0";
            textExpBonus.text = "0";

            lineGauge.localScale = new Vector3((contract.CurrentLine / contract.TotalLine), lineGauge.localScale.y, lineGauge.localScale.z);
            lineNewGauge.localScale = new Vector3((contract.CurrentLine / contract.TotalLine), lineNewGauge.localScale.y, lineNewGauge.localScale.z);

            // Update Contract
            contract.CurrentLine += lineDefeated;

            // Coroutine
            StartCoroutine(LineGainCoroutine());
            StartCoroutine(ExpGainCoroutine(lineDefeated));

            DrawActors(contract.ExpGain * lineDefeated);

        }



        private IEnumerator LineGainCoroutine()
        {
            float ratio = contract.CurrentLine / (float)contract.TotalLine;
            int time = 180;
            Vector3 speed = new Vector3((ratio - lineNewGauge.localScale.x) / time, 0, 0);
            while (time != 0)
            {
                time -= 1;
                lineNewGauge.localScale += speed;
                yield return null;
            }
            textCurrentLine.text = contract.CurrentLine.ToString();
        }

        private IEnumerator ExpGainCoroutine(int lineDefeated)
        {
            float time = 180f;
            float speed = (contract.ExpGain * lineDefeated) / time;
            float digit = 0;
            while (time != 0)
            {
                time -= 1;
                digit += speed;
                textEXP.text = ((int)digit).ToString();
                yield return null;
            }
            textEXP.text = (contract.ExpGain * lineDefeated).ToString();
        }










        private void DrawActors(int expGain)
        {
            for(int i = 0; i < contract.VoiceActors.Count; i++)
            {
                actorsImage[i].sprite = contract.VoiceActors[i].ActorSprite;
                textsLevel[i].text = contract.VoiceActors[i].Level.ToString();
                textsNext[i].text = (contract.VoiceActors[i].NextEXP - contract.VoiceActors[i].Experience).ToString();
                expGauge[i].transform.localScale = new Vector3((contract.VoiceActors[i].Experience / contract.VoiceActors[i].NextEXP),
                                                                expGauge[i].transform.localScale.y,
                                                                expGauge[i].transform.localScale.z);

                contract.VoiceActors[i].Experience += expGain;

                StartCoroutine(CalculateExp(contract.VoiceActors[i], expGauge[i], i));
            }
        }

        private IEnumerator CalculateExp(VoiceActor va, RectTransform expGauge, int i)
        {
            int time = 180;
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            yield return DrawExpGauge(expGauge, textsNext[i], va.NextEXP, va.Experience);
            while (va.Experience > va.NextEXP)
            {
                // Level Up
                if (actorsLevelUp[i] == false)
                {
                    actorsOldLevel[i] = va.Level;
                    actorsOldStats[i] = new EmotionStat(va.Statistique);
                }
                actorsLevelUp[i] = true;

                va.Experience -= va.NextEXP;
                va.LevelUp();
                va.NextEXP = experience.ExperienceCurve[va.Level];

                textsLevel[i].text = va.Level.ToString();
                textsNext[i].text = va.NextEXP.ToString();

                animatorLevelUpFeedback[i].SetTrigger("LevelUp");

                yield return DrawExpGauge(expGauge, textsNext[i], va.NextEXP, va.Experience);
            }

            textsLevel[i].text = va.Level.ToString();
            textsNext[i].text = (va.NextEXP- va.Experience).ToString();
            inAnimation = false;
        }

        private IEnumerator DrawExpGauge(RectTransform expGauge, TextMeshProUGUI textNext, float nextExp, float exp)
        {
            int time = 100;
            Vector3 speed = new Vector3((exp / nextExp) / time, 0, 0);
            while(time != 0)
            {
                time -= 1;
                textNext.text = ((int)(nextExp * (1 - expGauge.transform.localScale.x))).ToString();
                expGauge.transform.localScale += speed;
                if (expGauge.transform.localScale.x >= 1)
                {
                    expGauge.transform.localScale = new Vector3(0, expGauge.transform.localScale.y, expGauge.transform.localScale.z);
                    yield break;
                }
                yield return null;
            }
        }



        public void ValidateNext()
        {
            if(inAnimation == true)
            {
                SkipAnimation();
                return;
            }
            else
            {
                for(int i = 0; i < actorsLevelUp.Length; i++)
                {
                    if (actorsLevelUp[i] == true)
                    {
                        ActivateLevelUp(i);
                        return;
                    }
                }
            }
            EndScreenResult();
        }

        private void SkipAnimation()
        {
            StopAllCoroutines();
            VoiceActor va;
            for (int i = 0; i < contract.VoiceActors.Count; i++)
            {
                va = contract.VoiceActors[i];
                while (va.Experience > va.NextEXP)
                {
                    // Level Up
                    if (actorsLevelUp[i] == false)
                    {
                        actorsOldLevel[i] = va.Level;
                        actorsOldStats[i] = new EmotionStat(va.Statistique);
                    }
                    actorsLevelUp[i] = true;

                    va.Experience -= va.NextEXP;
                    va.LevelUp();
                    va.NextEXP = experience.ExperienceCurve[va.Level];

                    animatorLevelUpFeedback[i].SetTrigger("LevelUp");
                }
                textsLevel[i].text = va.Level.ToString();
                textsNext[i].text = (va.NextEXP- va.Experience).ToString();
            }
            textCurrentLine.text = contract.CurrentLine.ToString();
            textEXP.text = (contract.ExpGain * lineDefeated).ToString();
            inAnimation = false;
        }



        private void ActivateLevelUp(int id)
        {
            resultScreen.SetTrigger("LevelUp");
            actorsLevelUp[id] = false;
            DrawOldLevelStat(id);
            StartCoroutine(NewstatCoroutine(id));
        }


        private IEnumerator NewstatCoroutine(int id)
        {
            yield return new WaitForSeconds(1.2f);
            DrawNewLevelStat(id);
        }

        private void DrawOldLevelStat(int id)
        {
            textOldLevel.text = actorsOldLevel[id].ToString();
            int stat = 0;
            for(int i = 0; i < gaugeStats.Length; i++)
            {
                switch(i)
                {
                    case 0:
                        stat = actorsOldStats[id].Joy;
                        break;
                    case 1:
                        stat = actorsOldStats[id].Sadness;
                        break;
                    case 2:
                        stat = actorsOldStats[id].Disgust;
                        break;
                    case 3:
                        stat = actorsOldStats[id].Anger;
                        break;
                    case 4:
                        stat = actorsOldStats[id].Surprise;
                        break;
                    case 5:
                        stat = actorsOldStats[id].Sweetness;
                        break;
                    case 6:
                        stat = actorsOldStats[id].Fear;
                        break;
                    case 7:
                        stat = actorsOldStats[id].Trust;
                        break;

                }
                gaugeStats[i].transform.localScale = new Vector3(stat / 100f, gaugeStats[i].transform.localScale.y, gaugeStats[i].transform.localScale.z);
                newGaugeStats[i].sizeDelta = new Vector2((stat / 100f) * 500, newGaugeStats[i].sizeDelta.y);
                textsStats[i].text = stat.ToString();
                textsGain[i].transform.localScale = new Vector3(1, 0, 1);
            }
        }


        private void DrawNewLevelStat(int id)
        {
            VoiceActor va = contract.VoiceActors[id];
            textNewLevel.text = va.Level.ToString();
            int stat = 0;
            int oldStat = 0;
            for (int i = 0; i < gaugeStats.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        oldStat = actorsOldStats[id].Joy;
                        stat = va.Statistique.Joy;
                        break;
                    case 1:
                        oldStat = actorsOldStats[id].Sadness;
                        stat = va.Statistique.Sadness;
                        break;
                    case 2:
                        oldStat = actorsOldStats[id].Disgust;
                        stat = va.Statistique.Disgust;
                        break;
                    case 3:
                        oldStat = actorsOldStats[id].Anger;
                        stat = va.Statistique.Anger;
                        break;
                    case 4:
                        oldStat = actorsOldStats[id].Surprise;
                        stat = va.Statistique.Surprise;
                        break;
                    case 5:
                        oldStat = actorsOldStats[id].Sweetness;
                        stat = va.Statistique.Sweetness;
                        break;
                    case 6:
                        oldStat = actorsOldStats[id].Fear;
                        stat = va.Statistique.Fear;
                        break;
                    case 7:
                        oldStat = actorsOldStats[id].Trust;
                        stat = va.Statistique.Trust;
                        break;

                }
                if(oldStat != stat)
                    StartCoroutine(NewStatDrawCoroutine(stat, oldStat, i));
            }
        }

        private IEnumerator NewStatDrawCoroutine(int stat, int oldStat, int i)
        {
            yield return new WaitForSeconds(0.1f * i);
            int time = 20;
            Vector2 speed = new Vector2((((stat / 100f) * 500) - newGaugeStats[i].sizeDelta.x) / time, 0);
            while (time != 0)
            {
                time -= 1;
                newGaugeStats[i].sizeDelta += speed;
                yield return null;
            }
            textsStats[i].text = stat.ToString();
            animatorStats[i].SetTrigger("Feedback");
            textsGain[i].transform.localScale = new Vector3(1, 1, 1);
            textsGain[i].text = (stat - oldStat).ToString();
        }







        private void EndScreenResult()
        {
            animatorEndStatScreen.gameObject.SetActive(true);
            animatorEndStatScreen.SetTrigger("End");
            StartCoroutine(StopYokaiDisco());


        }

        private IEnumerator StopYokaiDisco()
        {
            int time = 90;
            float speed = yokaiDisco.volume / time;
            while(time != 0)
            {
                time -= 1;
                yokaiDisco.volume -= speed;
                yield return null;
            }
            yokaiDisco.volume = 0;
            SceneManager.LoadScene(nextScene);

        }



    #endregion

} // ResultScreen class
	
}// #PROJECTNAME# namespace
