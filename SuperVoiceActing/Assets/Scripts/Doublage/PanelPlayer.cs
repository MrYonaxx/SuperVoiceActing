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
    /// Definition of the PanelPlayer class
    /// </summary>
    public class PanelPlayer : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        CameraController cameraController;
        
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
        [ContextMenu("cameraScroll")]
        public void PanelScroll()
        {
            //cameraController.ActivateOffset();
            StartCoroutine(PanelScrollCoroutine(20));
        }

        private IEnumerator PanelScrollCoroutine(float time)
        {
            float speedZ = 700 / time;
            while (time != 0)
            {
                this.transform.position += new Vector3(speedZ, 0, 0);
                time -= 1;
                yield return null;
            }
        }

        [ContextMenu("cameraHide")]
        public void PanelHide()
        {
            //cameraController.StopOffset();
            StartCoroutine(PanelHideCoroutine(20));
        }

        private IEnumerator PanelHideCoroutine(float time)
        {
            float speedZ = 700 / time;
            while (time != 0)
            {
                this.transform.position -= new Vector3(speedZ, 0, 0);
                time -= 1;
                yield return null;
            }
        }


        #endregion

    } // PanelPlayer class

} // #PROJECTNAME# namespace