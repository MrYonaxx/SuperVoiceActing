/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.Events;
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

        [SerializeField]
        UnityEvent eventWin;

        float currentTimer = 0;
        private IEnumerator timeCoroutine;

        int second = 0;
        int frame = 0;
        int frameMytho = 0;

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
            DrawTimer();
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
            while(currentTimer > 0)
            {
                currentTimer -= Time.deltaTime * speed;

                DrawTimer();

                yield return null;
            }
            textTimer.text = "00:00:00";
            eventWin.Invoke();
        }


        private void DrawTimer()
        {
            second = Mathf.FloorToInt(currentTimer);
            frame = (int)((currentTimer - Mathf.FloorToInt(currentTimer)) * 100);
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