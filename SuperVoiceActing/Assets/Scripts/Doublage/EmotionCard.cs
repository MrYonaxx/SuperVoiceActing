/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the EmotionCard class
    /// </summary>
    public class EmotionCard : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        private IEnumerator coroutine = null;
        private RectTransform rectTransform;

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

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void MoveCard(RectTransform newTransform, float speed)
        {
            this.transform.SetParent(newTransform);

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = MoveToOrigin(speed);
            StartCoroutine(coroutine);
        }

        private IEnumerator MoveToOrigin(float speed)
        {
            while (this.rectTransform.anchoredPosition != Vector2.zero)
            {
                this.rectTransform.anchoredPosition /= speed;
                yield return null;
            }
            coroutine = null;
        }
        
        
        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace