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

        #endregion

    } // StoryEventEffectManager class
	
}// #PROJECTNAME# namespace
