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

        [SerializeField]
        private int time;
        public int Time
        {
            get { return time; }
        }

        [SerializeField]
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        [SerializeField]
        private Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
        }

        //[HorizontalGroup("Viewport")]
        [SerializeField]
        private Vector2 viewportSize;
        public Vector2 ViewportSize
        {
            get { return viewportSize; }
        }

        /*[HorizontalGroup("Viewport")]
        [SerializeField]
        private float viewportY;
        public float ViewportY
        {
            get { return viewportY; }
        }*/

        /*[HorizontalGroup("ViewportRect")]
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
        }*/



    } // DoublageEventCamera class

} // #PROJECTNAME# namespace