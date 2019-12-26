/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageEventCamera class
    /// </summary>

    [System.Serializable]
    public class DoublageEventViewport : DoublageEvent
    {

        [SerializeField]
        private int viewportID;
        public int ViewportID
        {
            get { return viewportID; }
        }

        [Header("Viewport")]
        [SerializeField]
        private bool changeViewport = true;
        public bool ChangeViewport
        {
            get { return changeViewport; }
        }

        [ShowIf("changeViewport")]
        [SerializeField]
        [MinValue(1)]
        private int time = 600;
        public int Time
        {
            get { return time; }
        }
        [ShowIf("changeViewport")]
        [SerializeField]
        private Vector2 anchorMin = new Vector2(0,0);
        public Vector2 AnchorMin
        {
            get { return anchorMin; }
        }
        [ShowIf("changeViewport")]
        [SerializeField]
        private Vector2 anchorMax = new Vector2(1, 1);
        public Vector2 AnchorMax
        {
            get { return anchorMax; }
        }
        [ShowIf("changeViewport")]
        [SerializeField]
        private float rotationZ;
        public float RotationZ
        {
            get { return rotationZ; }
        }

        [Header("Camera Viewport")]
        [SerializeField]
        private bool camViewport;
        public bool CamViewport
        {
            get { return camViewport; }
        }

        [ShowIf("camViewport")]
        [SerializeField]
        [MinValue(1)]
        private int timeCamera = 1;
        public int TimeCamera
        {
            get { return timeCamera; }
        }
        [ShowIf("camViewport")]
        [SerializeField]
        private Vector3 camPosition;
        public Vector3 CamPosition
        {
            get { return camPosition; }
        }
        [ShowIf("camViewport")]
        [SerializeField]
        private Vector3 camRotation;
        public Vector3 CamRotation
        {
            get { return camRotation; }
        }

        [HideInInspector]
        [Header("TextPosition")]
        [SerializeField]
        private bool changeTextPosition;
        public bool ChangeTextPosition
        {
            get { return changeTextPosition; }
        }

        [ShowIf("changeTextPosition")]
        [SerializeField]
        private Vector3 textPosition;
        public Vector3 TextPosition
        {
            get { return textPosition; }
        }


        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            eventManager.AnimatorBlackBand.SetBool("Appear", true);
            eventManager.Viewports[viewportID].SetViewportSetting(this);

            yield break;
        }


    } // DoublageEventCamera class

} // #PROJECTNAME# namespace