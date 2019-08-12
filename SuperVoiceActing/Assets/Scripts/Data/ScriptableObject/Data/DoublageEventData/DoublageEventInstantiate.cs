/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [System.Serializable]
	public class DoublageEventInstantiate : DoublageEvent
	{
        [SerializeField]
        private GameObject objectToInstantiate;
        public GameObject ObjectToInstantiate
        {
            get { return objectToInstantiate; }
        }

        [SerializeField]
        private PostProcessingProfile newPostProcess;
        public PostProcessingProfile NewPostProcess
        {
            get { return newPostProcess; }
        }

        [Space]
        [SerializeField]
        private bool transitionForNewBackground = false;
        public bool TransitionForNewBackground
        {
            get { return transitionForNewBackground; }
        }

        [ShowIf("transitionForNewBackground")]
        [SerializeField]
        private int timeTransitionForNewBackground = 60;
        public int TimeTransitionForNewBackground
        {
            get { return timeTransitionForNewBackground; }
        }

        [Space]
        [SerializeField]
        private bool changeSceneRenderSetting = false;
        public bool ChangeSceneRenderSetting
        {
            get { return changeSceneRenderSetting; }
        }


        [ShowIf("changeSceneRenderSetting")]
        [SerializeField]
        private Color newAmbientColor;
        public Color NewAmbientColor
        {
            get { return newAmbientColor; }
        }

        [ShowIf("changeSceneRenderSetting")]
        [SerializeField]
        private Color newFogColor;
        public Color NewFogColor
        {
            get { return newFogColor; }
        }

        [ShowIf("changeSceneRenderSetting")]
        [SerializeField]
        private float newFogDensity;
        public float NewFogDensity
        {
            get { return newFogDensity; }
        }

        [ShowIf("changeSceneRenderSetting")]
        [SerializeField]
        private int timeTransitionRender;
        public int TimeTransitionRender
        {
            get { return timeTransitionRender; }
        }

    } // DoublageEventInstantiate class
	
}// #PROJECTNAME# namespace
