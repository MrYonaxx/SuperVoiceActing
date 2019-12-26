/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [System.Serializable]
	public class DoublageEventCameraData : DoublageEvent
	{
        [SerializeField]
        private CameraMovementData currentCameraData;
        public CameraMovementData CurrentCameraData
        {
            get { return currentCameraData; }
        }

        [SerializeField]
        private CameraMovementData[] cameraSet1;
        public CameraMovementData[] CameraSet1
        {
            get { return cameraSet1; }
        }

        [SerializeField]
        private CameraMovementData[] cameraSet2;
        public CameraMovementData[] CameraSet2
        {
            get { return cameraSet2; }
        }

        [SerializeField]
        private int animatorCustomCameraID;
        public int AnimatorCustomCameraID
        {
            get { return animatorCustomCameraID; }
        }



        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            eventManager.Viewports[0].SetCameraData(this);
            yield break;
        }


    } // DoublageEventCameraData class
	
}// #PROJECTNAME# namespace
