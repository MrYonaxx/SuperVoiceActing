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

namespace VoiceActing
{
	public class ResultScreen : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

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
        TextMeshProUGUI[] textsStats;
        [SerializeField]
        RectTransform[] gaugeStats;
        [SerializeField]
        RectTransform[] newGaugeStats;




        private int[] oldLevel;
        private EmotionStat[] oldStats;

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
        public void SetContract(Contract con)
        {
            contract = con;
        }


        public void DrawResult(int numberTurn, int lineDefeated)
        {
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
                va.Experience -= va.NextEXP;
                va.LevelUp();
                va.NextEXP = experience.ExperienceCurve[va.Level];

                textsLevel[i].text = va.Level.ToString();
                textsNext[i].text = va.NextEXP.ToString();

                yield return DrawExpGauge(expGauge, textsNext[i], va.NextEXP, va.Experience);
            }

            textsLevel[i].text = va.Level.ToString();
            textsNext[i].text = (va.NextEXP- va.Experience).ToString();
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





        private void DrawOldLevelStat(int id)
        {
            int stat = 0;
            for(int i = 0; i < gaugeStats.Length; i++)
            {
                switch(i)
                {
                    case 0:
                        stat = oldStats[i].Joy;
                        break;
                    case 1:
                        stat = oldStats[i].Sadness;
                        break;
                    case 2:
                        stat = oldStats[i].Disgust;
                        break;
                    case 3:
                        stat = oldStats[i].Anger;
                        break;
                    case 4:
                        stat = oldStats[i].Surprise;
                        break;
                    case 5:
                        stat = oldStats[i].Sweetness;
                        break;
                    case 6:
                        stat = oldStats[i].Fear;
                        break;
                    case 7:
                        stat = oldStats[i].Trust;
                        break;

                }
                gaugeStats[i].transform.localScale = new Vector3(stat / 100f, gaugeStats[i].transform.localScale.y, gaugeStats[i].transform.localScale.z);
                newGaugeStats[i].sizeDelta = new Vector2((stat / 100f) * 500, 0);
                textsStats[i].text = stat.ToString();
            }
        }


        private void DrawNewLevelStat(int id)
        {
            int stat = 0;
            for (int i = 0; i < gaugeStats.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        stat = oldStats[i].Joy;
                        break;
                    case 1:
                        stat = oldStats[i].Sadness;
                        break;
                    case 2:
                        stat = oldStats[i].Disgust;
                        break;
                    case 3:
                        stat = oldStats[i].Anger;
                        break;
                    case 4:
                        stat = oldStats[i].Surprise;
                        break;
                    case 5:
                        stat = oldStats[i].Sweetness;
                        break;
                    case 6:
                        stat = oldStats[i].Fear;
                        break;
                    case 7:
                        stat = oldStats[i].Trust;
                        break;

                }
                gaugeStats[i].transform.localScale = new Vector3(stat / 100f, gaugeStats[i].transform.localScale.y, gaugeStats[i].transform.localScale.z);
                textsStats[i].text = stat.ToString();
            }
        }





        #endregion

    } // ResultScreen class
	
}// #PROJECTNAME# namespace
