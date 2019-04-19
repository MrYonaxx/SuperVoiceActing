/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the TextJoyAppear class
    /// </summary>
    public class TextJoyAppear : TextPerformanceAppear
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        protected float gravity = 0.1f;

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
                vertexAnim[i].offset = letterOffset * 3;
                vertexAnim[i].alpha = 0;
                vertexAnim[i].vertexPosition = Vector3.zero;
            }
        }

        protected override Vector3 ModifyPosition(int vertexIndex)
        {
            Vector3 position = new Vector3(0, 0, 0);

            if (vertexAnim[vertexIndex].offset > letterOffset * 2)
            {
                position = vertexAnim[vertexIndex].vertexPosition + new Vector3(0, vertexAnim[vertexIndex].offset - (letterOffset * 2.5f), 0);
                vertexAnim[vertexIndex].offset -= gravity;
                vertexAnim[vertexIndex].vertexPosition = position;
            }
            else if (vertexAnim[vertexIndex].offset >= letterOffset)
            {
                position = vertexAnim[vertexIndex].vertexPosition - new Vector3(0, (vertexAnim[vertexIndex].offset - (letterOffset * 1.5f)) * 0.5f, 0);
                vertexAnim[vertexIndex].offset -= gravity * 2;
                vertexAnim[vertexIndex].vertexPosition = position;
            }
            else if (vertexAnim[vertexIndex].offset >= 0)
            {
                position = vertexAnim[vertexIndex].vertexPosition + new Vector3(0, (vertexAnim[vertexIndex].offset - (letterOffset * 0.5f)) * 0.25f, 0);
                vertexAnim[vertexIndex].offset -= gravity * 4;
                vertexAnim[vertexIndex].vertexPosition = position;
            }
            else if (vertexAnim[vertexIndex].offset >= -letterOffset * 2)
            {
                vertexAnim[vertexIndex].offset -= gravity * 0.25f;
            }
            else
            {
                vertexAnim[vertexIndex].offset = letterOffset * 3;
            }
            return position;
        }

        #endregion

    } // TextJoyAppear class

} // #PROJECTNAME# namespace