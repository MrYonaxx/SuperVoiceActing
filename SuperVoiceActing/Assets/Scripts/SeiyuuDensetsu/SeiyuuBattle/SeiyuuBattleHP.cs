/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuBattleHP : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI textTimer;
        [SerializeField]
        float timer = 20f;
        [SerializeField]
        float speed = 1;

        [SerializeField]
        Color colorInactive;
        [SerializeField]
        Color colorActive;

        float currentTimer = 0;
        private IEnumerator timeCoroutine;

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
        public void ReinitializeTimer()
        {
            currentTimer = timer;
        }


        public void ActiveTimer(float momentum)
        {
            textTimer.color = colorActive;

            if (timeCoroutine != null)
                StopCoroutine(timeCoroutine);

            timeCoroutine = TimerCoroutine(speed * ((momentum - 50) / 50f));
            StartCoroutine(timeCoroutine);
        }


        private IEnumerator TimerCoroutine(float speed)
        {
            int second = 0;
            int frame = 0;
            int frameMytho = 0;
            while(timer > 0)
            {
                timer -= Time.deltaTime * speed;

                second = Mathf.FloorToInt(timer);
                frame = (int)((timer - Mathf.FloorToInt(timer)) * 100);
                frameMytho -= Random.Range(20, 60);
                if (second < 10)
                    textTimer.text = "0" + second;
                else
                    textTimer.text = second.ToString();

                if (frame < 10)
                    textTimer.text += ":0" + frame;
                else
                    textTimer.text += ":" + frame;

                if (frameMytho < 0)
                    frameMytho += 100;
                if (frameMytho < 10)
                    textTimer.text += ":0" + frameMytho;
                else
                    textTimer.text += ":" + frameMytho;

                yield return null;
            }
            textTimer.text = "00:00:00";
        }

        public void StopTimer()
        {
            textTimer.color = colorInactive;

            if(timeCoroutine != null)
                StopCoroutine(timeCoroutine);
        }

        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace