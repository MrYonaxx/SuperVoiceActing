/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VoiceActing
{
	public class MenuTransitionDoublage : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private CameraBureau camBureau;

        [SerializeField]
        private Animator animatorCamera;

        [SerializeField]
        private GameObject fond;

        [SerializeField]
        GameObject particles;

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

        [ContextMenu("Tranition")]
        public void StartTransition()
        {
            particles.SetActive(false);
            camBureau.TransitionDoublage();
            fond.SetActive(true);
            animatorCamera.enabled = true;
            AudioManager.Instance.StopMusic(120);
        }



        
        #endregion
		
	} // MenuTransitionDoublage class
	
}// #PROJECTNAME# namespace
