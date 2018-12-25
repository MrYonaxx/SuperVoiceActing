/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the MouthAnimationImage class
    /// </summary>
    public class MouthAnimationImage : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Image imageRenderer;

        [SerializeField]
        float speedMouth = 5;

        [SerializeField]
        Sprite[] mouthmovement;

        IEnumerator mouthCoroutine = null;

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


        protected void Start()
        {
            ActivateMouth(speedMouth);
        }

        public void ActivateMouth(float speed = -1)
        {
            if(speed == -1)
                speed = speedMouth;

            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);
            mouthCoroutine = MouthAnim(speed);
            StartCoroutine(mouthCoroutine);
        }

        public void DesactivateMouth()
        {
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);

            changeMouthSprite(0);
        }

        private IEnumerator MouthAnim(float speed)
        {
            int i = 0;
            float time = speed;
            while (time > 0)
            {
                time -= 1;
                yield return null;
                if (time == 0)
                {
                    i += Random.Range(1, 2);
                    if (i >= mouthmovement.Length)
                        i = 0;
                    changeMouthSprite(i);
                    time = speed;
                }
            }
        }

        public void changeMouthSprite(int index)
        {
            imageRenderer.sprite = mouthmovement[index];
        }

        #endregion

    } // MouthAnimationImage class

} // #PROJECTNAME# namespace