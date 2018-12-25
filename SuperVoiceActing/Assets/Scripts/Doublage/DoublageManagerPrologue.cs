﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageManagerPrologue class
    /// </summary>
    public class DoublageManagerPrologue : DoublageManager
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Changement d'ambiance")]
        [SerializeField]
        Light light;
        [SerializeField]
        RectTransform blackRectUpper;
        [SerializeField]
        RectTransform blackRectLower;

        [SerializeField]
        UnityEvent newAmbiance;
        [SerializeField]
        UnityEvent newAmbiance2;

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

        public override void SetPhrase()
        {
            if(indexPhrase == 0)
            {
                ShiftAmbiance();
            }
            else if (indexPhrase == 1)
            {
                textPerformanceAppear.NewPhrase(contrat.TextData[indexPhrase].Text);
                textPerformanceAppear.ApplyDamage(100);
                inputController.enabled = true;
            }
            else
            {
                textPerformanceAppear.NewPhrase(contrat.TextData[indexPhrase].Text);
                inputController.enabled = true;
            }

        }

        private void ShiftAmbiance()
        {
            StartCoroutine(ShiftAmbianceCoroutine());
        }

        private IEnumerator ShiftAmbianceCoroutine()
        {
            float time = 600;
            float speedLight = 1 / time;
            float speedColor = 0.75f / time;
            Color color = new Color(1, 1, 1, 1);
            while (time != 0)
            {
                if(time == 300)
                {
                    cameraController.SetNoCameraEffect(true);
                    cameraController.MoveCamera(-6.5f,2.9f,0,400);
                }
                if(time < 300)
                {
                    blackRectUpper.anchoredPosition += new Vector2(0,0.25f);
                    blackRectLower.anchoredPosition -= new Vector2(0,0.25f);
                }
                color -= new Color(speedColor, speedColor, speedColor, 0);
                characters[0].ChangeTint(color);
                light.intensity -= speedLight;
                time -= 1;
                yield return null;

            }
            yield return new WaitForSeconds(2);
            color = new Color(1, 1, 1, 1);
            characters[0].ChangeTint(color);
            newAmbiance.Invoke();
            yield return new WaitForSeconds(3);
            newAmbiance2.Invoke();
            //emotionAttackManager.SwitchCardTransformIntro();
            //textPerformanceAppear.ApplyDamage(100);
            textPerformanceAppear.NewPhrase(contrat.TextData[indexPhrase].Text);
            textPerformanceAppear.ApplyDamage(100);
            inputController.enabled = true;
            cameraController.SetNoCameraEffect(false);
            cameraController.MoveToInitialPosition();
        }

        #endregion

    } // DoublageManagerPrologue class

} // #PROJECTNAME# namespace