/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
	public class LigneMenuFeedback : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        //[SerializeField]
        //RectTransform[] linesParent;
        [SerializeField]
        RectTransform[] lines;
        [SerializeField]
        float[] angleValues;

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
        private void Start()
        {
            angleValues = new float[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                angleValues[i] = lines[i].transform.eulerAngles.z;
            }
        }

        public void FeedbackLinesMenu()
        {
            //SetNewLineAngle();
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = RotateCameraCoroutine(10);
            StartCoroutine(coroutine);
        }



        private IEnumerator RotateCameraCoroutine(float time)
        {
            float[] speedZ = new float[lines.Length];
            float angleZ = 0;
            float zTarget = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                angleZ = lines[i].transform.eulerAngles.z;
                if (angleZ > 180)
                    angleZ = -(360 - angleZ);
                zTarget = Random.Range(angleValues[i] - 8, angleValues[i] + 8);
                if (zTarget > 180)
                    zTarget = -(360 - zTarget);
                speedZ[i] = (zTarget - angleZ) / time;
                //speedZ[i] *= 2;
            }

            while (time != 0)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].transform.eulerAngles += new Vector3(0, 0, speedZ[i]);
                    //speedZ[i] *= 0.9f;
                }
                time -= 1;
                yield return null;
            }
            //this.transform.eulerAngles = new Vector3(0, 0, z);
            coroutine = RotateCameraCoroutine(500);
            StartCoroutine(coroutine);
        }



        #endregion

    } // LigneMenuFeedback class
	
}// #PROJECTNAME# namespace
