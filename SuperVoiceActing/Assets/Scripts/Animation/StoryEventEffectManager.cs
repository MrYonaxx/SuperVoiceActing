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
	public class StoryEventEffectManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Shake[] shakesPlayer;

        [SerializeField]
        Shake[] shakes;

        [SerializeField]
        Shake[] bigShakes;

        [SerializeField]
        Image flashImage;

        [SerializeField]
        Image tintImage;

        [SerializeField]
        Image fadeImage;


        private IEnumerator fadeCoroutine;

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

        [ContextMenu("ShakePlayer")]
        public void ShakePlayerEffect()
        {
            for (int i = 0; i < shakesPlayer.Length; i++)
            {
                shakesPlayer[i].ShakeEffect();
            }
        }

        [ContextMenu("BigShake")]
        public void BigShakeEffect()
        {
            for (int i = 0; i < bigShakes.Length; i++)
            {
                bigShakes[i].ShakeEffect();
            }
        }

        [ContextMenu("Shake")]
        public void ShakeEffect()
        {
            for(int i = 0; i < shakes.Length; i++)
            {
                shakes[i].ShakeEffect();
            }
        }

        [ContextMenu("Flash")]
        public void FlashEffect()
        {
            StartCoroutine(FlashCoroutine(25));
        }

        private IEnumerator FlashCoroutine(int time)
        {
            Color speed = new Color(0, 0, 0, 1f / time);
            flashImage.color = new Color(1, 1, 1, 1);
            while (time != 0)
            {
                time -= 1;
                yield return null;
                flashImage.color -= speed;
            }
            flashImage.color = new Color(1, 1, 1, 0);
        }




        [ContextMenu("Tint")]
        public void Tint(Color color, int time)
        {
            StartCoroutine(TintCoroutine(color, time));
        }

        private IEnumerator TintCoroutine(Color color, int time)
        {
            Color speed = new Color((color.r - tintImage.color.r) / time, (color.g - tintImage.color.g) / time, 
                                    (color.b - tintImage.color.b) / time, (color.a - tintImage.color.a) / time);
            while (time != 0)
            {
                time -= 1;
                tintImage.color += speed;
                yield return null;
            }
            tintImage.color = color;
        }





        [ContextMenu("Fade")]
        public void Fade(bool b, int time)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = FadeCoroutine(b, time);
            StartCoroutine(fadeCoroutine);
        }

        private IEnumerator FadeCoroutine(bool b, int time)
        {
            Color speed = new Color(0, 0, 0, 1f / time);
            if(b == false)
                speed = new Color(0, 0, 0, -1f / time);
            while (time != 0)
            {
                time -= 1;
                fadeImage.color += speed;

                yield return null;
            }
            if (b == false)
                fadeImage.color = new Color(0, 0, 0, 0);
            else
                fadeImage.color = new Color(0, 0, 0, 1);
        }

        #endregion

    } // StoryEventEffectManager class
	
}// #PROJECTNAME# namespace
