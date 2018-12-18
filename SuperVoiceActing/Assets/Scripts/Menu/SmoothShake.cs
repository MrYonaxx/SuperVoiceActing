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

        bool moving = false;
        private IEnumerator coroutine = null;

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

        ////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected void Awake()
        {
            
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            if (moving == false)
            {
                coroutine = MoveCoroutine(Random.Range(-shakeRange, shakeRange), Random.Range(-shakeRange, shakeRange), 0, shakeTime);
                StartCoroutine(coroutine);
            }
        }

        private IEnumerator MoveCoroutine(float x, float y, float z, float time)
        {
            float speedX = (x - this.transform.position.x) / time;
            float speedY = (y - this.transform.position.y) / time;
            float speedZ = (z - this.transform.position.z) / time;
            moving = true;
            while (time != 0)
            {
                this.transform.position += new Vector3(speedX, speedY, speedZ);
                speedX /= 1.01f;
                speedY /= 1.01f;
                time -= 1;
                yield return null;
            }
            moving = false;
            //this.transform.position = new Vector3(x, y, z);
        }

        #endregion

    } // SmoothShake class

} // #PROJECTNAME# namespace