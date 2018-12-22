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
    public class DoublageEventCamera : DoublageEvent
    {

        [SerializeField]
        private int cameraID;
        public int CameraID
        {
            get { return cameraID; }
        }

        [SerializeField]
        private int time;
        public int Time
        {
            get { return time; }
        }

        [HorizontalGroup("Viewport")]
        [SerializeField]
        private float viewportX;
        public float ViewportX
        {
            get { return viewportX; }
        }

        [HorizontalGroup("Viewport")]
        [SerializeField]
        private float viewportY;
        public float ViewportY
        {
            get { return viewportY; }
        }

        [HorizontalGroup("ViewportRect")]
        [SerializeField]
        private float viewportWidth;
        public float ViewportWidth
        {
            get { return viewportWidth; }
        }

        [HorizontalGroup("ViewportRect")]
        [SerializeField]
        private float viewportHeight;
        public float ViewportHeight
        {
            get { return viewportHeight; }
        }



    } // DoublageEventCamera class

} // #PROJECTNAME# namespace