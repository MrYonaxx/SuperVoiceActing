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
	public class TextTrustAppear : TextPerformanceAppear
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        /*[SerializeField]
        float scaleLimit = 1.5f;*/
        [SerializeField]
        Vector3 scaleSpeed = new Vector3(0.25f,0.25f,0);

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
            for (int i = 0; i < 255; i++)
            {
                vertexAnim[i].damage = false;
                vertexAnim[i].selected = false;
                vertexAnim[i].offset = 0;
                vertexAnim[i].alpha = 0;
                vertexAnim[i].vertexPosition = Vector3.zero;
                vertexAnim[i].vertexScale = Vector3.zero;
            }
        }

        protected override Vector3 ModifyScale(int vertexIndex)
        {
            if(vertexAnim[vertexIndex].alpha < 255)
                vertexAnim[vertexIndex].vertexScale += scaleSpeed;
            else if (vertexAnim[vertexIndex].vertexScale.x > 1)
                vertexAnim[vertexIndex].vertexScale -= (scaleSpeed / 2);

            return vertexAnim[vertexIndex].vertexScale;
        }


        #endregion

    } // TextTrustAppear class
	
}// #PROJECTNAME# namespace
