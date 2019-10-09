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
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuActorManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Info Panel")]

        [SerializeField]
        private TextMeshProUGUI statNameStylishActor;
        [SerializeField]
        private TextMeshProUGUI statNameActor;
        [SerializeField]
        private TextMeshProUGUI statFanActor;
        [SerializeField]
        private TextMeshProUGUI statCurrentHpActor;
        [SerializeField]
        private TextMeshProUGUI statMaxHpActor;

        [SerializeField]
        private RectTransform statHpGauge;
        [SerializeField]
        private RectTransform statTimbre;

        [SerializeField]
        private TextMeshProUGUI[] statSkillsActor;
        [SerializeField]
        private TextMeshProUGUI statSkillActorDescription;



        [Title("Stats Panel")]
        [SerializeField]
        Image[] imageStatIcon;
        [SerializeField]
        Image[] feedbackBestStat;

        [Title("Stats Panel 1")]
        [InfoBox("Joie > Tristesse > Dégoût > Colère > Surprise > Douceur > Peur > Confiance")]
        [SerializeField]
        GameObject representation1;

        [SerializeField]
        TextMeshProUGUI[] textStatsActor;
        [SerializeField]
        RectTransform[] jaugeStatsActor;
        [SerializeField]
        TextMeshProUGUI[] textStatsRole;
        [SerializeField]
        RectTransform[] jaugeStatsRole;
        [SerializeField]
        Image[] jaugeEmptyActor;
        [SerializeField]
        Image[] jaugeEmptyRole;

        [Title("Stats Panel 2")]
        [InfoBox("Joie > Tristesse > Dégoût > Colère > Surprise > Douceur > Peur > Confiance")]
        [SerializeField]
        GameObject representation2;
        [SerializeField]
        TextMeshProUGUI[] textStatsActor2;
        [SerializeField]
        RectTransform[] jaugeStatsActor2;
        [SerializeField]
        TextMeshProUGUI[] textStatsRole2;
        [SerializeField]
        RectTransform[] jaugeStatsRole2;
        [SerializeField]
        Image[] jaugeEmpty;

        [Title("Animator")]
        [SerializeField]
        Animator animatorStatPanel;
        [SerializeField]
        Animator animatorStatMediaPanel;
        [SerializeField]
        Animator animatorSkillPanel;

        private bool auditionMode = false;
        private Role auditionRole = null;

        private bool gaugeCursorMode = false;


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

        public void DrawActorStat(VoiceActor actor)
        {
            statTimbre.anchorMin = new Vector2((actor.Timbre.x + 10) / 20f, 0);
            statTimbre.anchorMax = new Vector2((actor.Timbre.y + 10) / 20f, 1);
            statTimbre.anchoredPosition = Vector3.zero;

            statNameStylishActor.text = actor.Name;
            statNameActor.text = actor.Name;
            statFanActor.text = actor.Fan.ToString();

            statCurrentHpActor.text = actor.Hp.ToString();
            statMaxHpActor.text = actor.HpMax.ToString();
            statHpGauge.transform.localScale = new Vector3((actor.Hp / (float)actor.HpMax), statHpGauge.transform.localScale.y, statHpGauge.transform.localScale.z);


            /*for (int i = 0; i < statSkillsActor.Length; i++)
            {
                if (i < actor.Potentials.Length)
                    statSkillsActor[i].text = actor.Potentials[i].SkillName;
                else
                    statSkillsActor[i].text = " ";
            }

            if (actor.Potentials.Length != 0)
                statSkillActorDescription.text = actor.Potentials[0].Description;*/

            DrawGaugeInfo(actor);

        }


        private void DrawGaugeInfo(VoiceActor actor)
        {
            for (int i = 0; i < textStatsActor.Length; i++)
            {
                int currentStatActor = actor.Statistique.GetEmotion(i + 1);

                if (gaugeCursorMode == false)
                {
                    textStatsActor[i].text = currentStatActor.ToString();
                    jaugeStatsActor[i].transform.localScale = new Vector3(currentStatActor / 100f, 1, 0);
                    //StartCoroutine(GaugeCoroutine(jaugeStatsActor[i], currentStatActor / 100f));
                }
                else
                {
                    textStatsActor2[i].text = currentStatActor.ToString();
                    jaugeStatsActor2[i].transform.localScale = new Vector3(currentStatActor / 100f, 1, 0);
                    //StartCoroutine(GaugeCoroutine(jaugeStatsActor2[i], currentStatActor / 100f));
                }

                if (auditionMode == true)
                {
                    int currentStatRole = auditionRole.CharacterStat.GetEmotion(i + 1);

                    if (currentStatActor >= currentStatRole)
                    {
                        feedbackBestStat[i].color = new Color(1, 1, 0, 0.5f);
                        imageStatIcon[i].color = new Color(1, 1, 0, imageStatIcon[i].color.a);
                        //jaugeEmpty[i].color = new Color(0, 0, 0, 0.7f);
                        jaugeEmptyActor[i].color = jaugeEmpty[i].color;
                        jaugeEmptyRole[i].color = jaugeEmpty[i].color;
                    }
                    else
                    {
                        // actor.RoleDefense -= 1;
                        feedbackBestStat[i].color = new Color(1, 1, 1, 0.5f);
                        imageStatIcon[i].color = new Color(1, 1, 1, imageStatIcon[i].color.a);
                        //jaugeEmpty[i].color = new Color(0, 0, 0, 0.4f);
                        jaugeEmptyActor[i].color = jaugeEmpty[i].color;
                        jaugeEmptyRole[i].color = jaugeEmpty[i].color;
                    }
                }

            }
        }

        private void DrawGaugeRoleInfo()
        {
            int currentStatRole = 0;
            for (int i = 0; i < textStatsActor.Length; i++)
            {
                currentStatRole = auditionRole.CharacterStat.GetEmotion(i + 1);

                if (gaugeCursorMode == false)
                {
                    textStatsRole[i].text = currentStatRole.ToString();
                    jaugeStatsRole[i].transform.localScale = new Vector3(currentStatRole / 100f, 1, 1);
                }
                else
                {
                    jaugeStatsRole2[i].sizeDelta = new Vector2((currentStatRole / 100f) * 500, 0);
                }

                if (currentStatRole == auditionRole.BestStat || currentStatRole == auditionRole.SecondBestStat)
                {
                    textStatsActor[i].color = new Color(textStatsActor[i].color.r, textStatsActor[i].color.g, textStatsActor[i].color.b, 1f);
                    textStatsRole[i].color = new Color(textStatsRole[i].color.r, textStatsRole[i].color.g, textStatsRole[i].color.b, 1f);
                    imageStatIcon[i].color = new Color(imageStatIcon[i].color.r, imageStatIcon[i].color.g, imageStatIcon[i].color.b, 1f);
                    jaugeEmpty[i].color = new Color(0, 0, 0, 0.7f);
                    feedbackBestStat[i].gameObject.SetActive(true);
                }
                else
                {
                    textStatsActor[i].color = new Color(textStatsActor[i].color.r, textStatsActor[i].color.g, textStatsActor[i].color.b, 0.5f);
                    textStatsRole[i].color = new Color(textStatsRole[i].color.r, textStatsRole[i].color.g, textStatsRole[i].color.b, 0.5f);
                    imageStatIcon[i].color = new Color(imageStatIcon[i].color.r, imageStatIcon[i].color.g, imageStatIcon[i].color.b, 0.5f);
                    jaugeEmpty[i].color = new Color(0, 0, 0, 0.4f);
                    feedbackBestStat[i].gameObject.SetActive(false);
                }
            }
        }




        public void HideAllActorInfo()
        {
            animatorStatPanel.SetBool("Appear", false);
            animatorStatMediaPanel.SetBool("Appear", false);
            animatorSkillPanel.SetBool("Appear", false);
        }
        public void ShowAllActorInfo()
        {
            animatorStatPanel.SetBool("Appear", true);
            animatorStatMediaPanel.SetBool("Appear", true);
            animatorSkillPanel.SetBool("Appear", true);
        }

        public void ShowActorHPModification()
        {

        }

        public void ShowActorStatModification(EmotionStat emotionStat)
        {
            animatorStatPanel.SetBool("Appear", true);
        }






        #endregion

    } 

} // #PROJECTNAME# namespace