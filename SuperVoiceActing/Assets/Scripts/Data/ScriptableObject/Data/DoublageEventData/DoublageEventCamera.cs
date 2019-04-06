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
        private int time;
        public int Time
        {
            get { return time; }
        }
        [ShowIf("changeViewport")]
        [SerializeField]
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        [ShowIf("changeViewport")]
        [SerializeField]
        private Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
        }
        [ShowIf("changeViewport")]
        [SerializeField]
        private Vector2 viewportSize;
        public Vector2 ViewportSize
        {
            get { return viewportSize; }
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
        [ShowIf("camViewport")]
        [SerializeField]
        private int timeCamera;
        public int TimeCamera
        {
            get { return timeCamera; }
        }

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


    } // DoublageEventCamera class

} // #PROJECTNAME# namespace