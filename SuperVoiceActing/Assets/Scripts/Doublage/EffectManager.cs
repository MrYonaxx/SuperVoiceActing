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

namespace VoiceActing
{
	public class EffectManager : MonoBehaviour
	{

        [SerializeField]
        GameObject negativeEffect;

        [SerializeField]
        GameObject blurredEffect;

        [SerializeField]
        Image flashEffect;

        [SerializeField]
        Image fadeUI;
        [SerializeField]
        Image fadeScreen;


        public void StartEffect(DoublageEventEffect effectData)
        {
            switch(effectData.EventEffect)
            {
                case EventEffect.Negative:
                    negativeEffect.SetActive(effectData.Active);
                    break;
                case EventEffect.BlurredVision:
                    blurredEffect.SetActive(effectData.Active);
                    break;
                case EventEffect.FadeTotal:
                    TotalFade(effectData.Active, effectData.EventTime);
                    break;
            }
        }



        public void TotalFade(bool active, int time)
        {
            fadeUI.gameObject.SetActive(true);
            fadeScreen.gameObject.SetActive(true);
            StartCoroutine(TotalFadeCoroutine(active, time));
        }

        private IEnumerator TotalFadeCoroutine(bool active, int time)
        {
            Color colorSpeed;
            if (active == true)
            {
                fadeUI.color = new Color(0, 0, 0, 0);
                colorSpeed = new Color(0, 0, 0, 1f / time);
            }
            else
            {
                fadeUI.color = new Color(0, 0, 0, 1);
                colorSpeed = new Color(0, 0, 0, -1f / time);
            }

            while (time != 0)
            {
                time -= 1;
                fadeUI.color += colorSpeed;
                fadeScreen.color = fadeUI.color;
                yield return null;
            }

            if (active == true)
            {
                fadeUI.color = new Color(0, 0, 0, 1);
            }
            else
            {
                fadeUI.color = new Color(0, 0, 0, 0);
                fadeUI.gameObject.SetActive(false);
                fadeScreen.gameObject.SetActive(false);
            }
        }


    } // EffectManager class
	
}// #PROJECTNAME# namespace
