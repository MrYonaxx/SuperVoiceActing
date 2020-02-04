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
	public class TextSweetnessAppear : TextPerformanceAppear
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        private float angle;
        private float positionY;
        [SerializeField]
        protected float gravity = 0.01f;

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

        protected override void InitializeVertex()
        {
            for (int i = 0; i < 1024; i++)
            {
                vertexAnim[i].damage = false;
                vertexAnim[i].selected = false;
                vertexAnim[i].offset = 0;
                vertexAnim[i].alpha = 0;
                vertexAnim[i].vertexPosition = Vector3.zero;
                vertexAnim[i].angle = 0;
            }
        }
        protected override Vector3 ModifyPosition(int vertexIndex)
        {
            Vector3 position = new Vector3(0, 0, 0);

            if (vertexAnim[vertexIndex].offset > letterOffset)
            {
                position = vertexAnim[vertexIndex].vertexPosition + new Vector3(0, vertexAnim[vertexIndex].offset - (letterOffset * 1.5f), 0);
                vertexAnim[vertexIndex].offset -= gravity;
                vertexAnim[vertexIndex].vertexPosition = position;
            }
            else if (vertexAnim[vertexIndex].offset > 0)
            {
                position = vertexAnim[vertexIndex].vertexPosition - new Vector3(0, vertexAnim[vertexIndex].offset - (letterOffset * 0.5f), 0);
                vertexAnim[vertexIndex].offset -= gravity;
                vertexAnim[vertexIndex].vertexPosition = position;
            }
            else
            {
                vertexAnim[vertexIndex].offset = letterOffset * 2;
                vertexAnim[vertexIndex].vertexPosition = position;
            }

            return position;
        }

        protected override Vector3 ModifyRotation(int vertexIndex)
        {
            if (vertexIndex == 1)
            {
                angle = Mathf.SmoothStep(-6, 6, Mathf.PingPong(loopCount / 100f * 1 , 1f));
            }
            Vector3 rotation = new Vector3(0, 0, angle);
            return rotation;
        }

        #endregion

    } // TextSweetnessAppear class
	
}// #PROJECTNAME# namespace
