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
    /// Definition of the SmoothShake class
    /// </summary>
    public class SmoothShake : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        float shakeRange = 0.1f;
        [SerializeField]
        float shakeTime = 60;
        [SerializeField]
        bool isRectTransform = false;

        bool moving = false;
        private IEnumerator coroutine = null;

        private RectTransform rectTransform = null;

        private float originX = 0;
        private float originY = 0;

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
            originX = this.transform.localPosition.x;
            originY = this.transform.localPosition.y;
            if (isRectTransform)
            {
                rectTransform = GetComponent<RectTransform>();
                originX = rectTransform.anchoredPosition.x;
                originY = rectTransform.anchoredPosition.y;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            if(isRectTransform)
            {
                if (moving == false)
                {
                    coroutine = MoveCoroutineRectTransform(originX + Random.Range(-shakeRange, shakeRange), originY + Random.Range(-shakeRange, shakeRange), shakeTime);
                    StartCoroutine(coroutine);
                }
            }
            else if (moving == false)
            {
                coroutine = MoveCoroutine(originX + Random.Range(-shakeRange, shakeRange), originY + Random.Range(-shakeRange, shakeRange), 0, shakeTime);
                StartCoroutine(coroutine);
            }
        }

        protected void OnEnable()
        {
            if(coroutine != null)
                StopCoroutine(coroutine);
            moving = false;
        }

        /*private IEnumerator MoveCoroutine(float x, float y, float z, float time)
        {
            Vector3 speed = new Vector3((x - this.transform.position.x) / time, (y - this.transform.position.y) / time, (z -this.transform.position.z) / time);
            moving = true;
            while (time != 0)
            {
                this.transform.position += speed;
                speed /= 1.01f;
                time -= 1;
                yield return null;
            }
            moving = false;
        }*/

        private IEnumerator MoveCoroutine(float x, float y, float z, float time)
        {
            time /= 60;
            //Vector3 speed = new Vector3((x - this.transform.position.x) / time, (y - this.transform.position.y) / time, (z - this.transform.position.z) / time);
            moving = true;
            Vector3 finalPosition = new Vector3(x - this.transform.position.x, y - this.transform.position.y, z - this.transform.position.z);
            //Vector3 currentPosition = this.transform.position;
            float t = 0;
            while (t < 1)
            {
                t += (Time.deltaTime / time);
                //Debug.Log(t);
                this.transform.position = Vector3.Lerp(this.transform.position, finalPosition, 0.025f);
                //this.transform.position += speed;
                //speed /= 1.01f;
                //time -= 1;
                yield return null;
            }
            moving = false;
        }

        private IEnumerator MoveCoroutineRectTransform(float x, float y, float time)
        {
            float speedX = (x - rectTransform.anchoredPosition.x) / time;
            float speedY = (y - rectTransform.anchoredPosition.y) / time;
            moving = true;
            while (time != 0)
            {
                rectTransform.anchoredPosition += new Vector2(speedX, speedY);
                speedX /= 1.01f;
                speedY /= 1.01f;
                time -= 1;
                yield return null;
            }
            moving = false;
        }

        #endregion

    } // SmoothShake class

} // #PROJECTNAME# namespace