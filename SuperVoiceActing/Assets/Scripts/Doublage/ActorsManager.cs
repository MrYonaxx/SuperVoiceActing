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
	public class ActorsManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Debug")]
        [SerializeField]
        VoiceActorData debug;

        [SerializeField]
        private List<VoiceActor> actors = new List<VoiceActor>();

        [SerializeField]
        TextMeshProUGUI[] textCardStats;

        [Header("Feedbacks Damage")]
        [SerializeField]
        Animator animatorHealthBar;
        [SerializeField]
        RectTransform healthBar;
        [SerializeField]
        Image healthContent;
        [SerializeField]
        Image healthContentProgression;
        [SerializeField]
        TextMeshProUGUI textCurrentHp;
        [SerializeField]
        TextMeshProUGUI textMaxHp;

        private int indexCurrentActor = 0;
        
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
        public VoiceActor GetCurrentActor()
        {
            return actors[indexCurrentActor];
        }

        public void SetActors(List<VoiceActor> actorsContract)
        {
            if (actorsContract[0] == null)
                actors.Add(new VoiceActor(debug));
            else
                actors = actorsContract;
            DrawActorStat();
        }

        private void DrawActorStat()
        {
            // Joie > Tristesse > Dégout > Colère > Surprise > Douceur > Peur > Confiance

            textCardStats[0].text = actors[indexCurrentActor].Statistique.Joy.ToString();
            textCardStats[1].text = actors[indexCurrentActor].Statistique.Sadness.ToString();
            textCardStats[2].text = actors[indexCurrentActor].Statistique.Disgust.ToString();
            textCardStats[3].text = actors[indexCurrentActor].Statistique.Anger.ToString();
            textCardStats[4].text = actors[indexCurrentActor].Statistique.Surprise.ToString();
            textCardStats[5].text = actors[indexCurrentActor].Statistique.Sweetness.ToString();
            textCardStats[6].text = actors[indexCurrentActor].Statistique.Fear.ToString();
            textCardStats[7].text = actors[indexCurrentActor].Statistique.Trust.ToString();
            textCardStats[8].text = actors[indexCurrentActor].Statistique.Neutral.ToString();

            textCurrentHp.text = actors[indexCurrentActor].Hp.ToString();
            textMaxHp.text = actors[indexCurrentActor].HpMax.ToString();

        }

        public void ActorTakeDamage(int damage)
        {
            
            actors[indexCurrentActor].Hp -= damage;
            textCurrentHp.text = actors[indexCurrentActor].Hp.ToString();

            float ratioHP = (float) actors[indexCurrentActor].Hp / actors[indexCurrentActor].HpMax;
            healthContent.transform.localScale = new Vector3(ratioHP, 1, 1);
            StartCoroutine(FeedbackHealthBar());
        }

        private IEnumerator FeedbackHealthBar(int timeShake = 30, int timeGauge = 180)
        {
            float intensity = 100;
            Vector2 origin = healthBar.anchoredPosition;
            while (timeShake != 0)
            {
                timeShake -= 1;
                healthBar.anchoredPosition = new Vector2(origin.x + Random.Range(-intensity, intensity), origin.y);
                intensity *= 0.9f;
                yield return null;
            }
            healthBar.anchoredPosition = origin;

            Vector3 speed = new Vector3((healthContent.transform.localScale.x - healthContentProgression.transform.localScale.x) / timeGauge, 0, 0);
            while (timeGauge != 0)
            {
                timeGauge -= 1;
                healthContentProgression.transform.localScale += speed;
                yield return null;
            }
            //animatorHealthBar.SetBool("Appear", false);
        }

        public void HideHealthBar()
        {
            animatorHealthBar.SetBool("Appear", false);
        }
        public void ShowHealthBar()
        {
            animatorHealthBar.SetBool("Appear", true);
        }

        #endregion

    } // ActorsManager class
	
}// #PROJECTNAME# namespace
