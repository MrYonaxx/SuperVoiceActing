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
	public class DoublageEventSetCharacter : DoublageEvent
	{
        [SerializeField]
        private StoryCharacterData character;
        public StoryCharacterData Character
        {
            get { return character; }
        }

        [SerializeField]
        private bool customPos = true;
        public bool CustomPos
        {
            get { return customPos; }
        }

        [ShowIf("customPos", true)]
        [SerializeField]
        private Vector3 position;
        public Vector3 Position
        {
            get { return position; }
        }

        [ShowIf("customPos", true)]
        [SerializeField]
        private Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
        }

        [HideIf("customPos", true)]
        [SerializeField]
        private int defaultPosID = 0;
        public int DefaultPosID
        {
            get { return defaultPosID; }
        }

        [SerializeField]
        private bool flipX;
        public bool FlipX
        {
            get { return flipX; }
        }

        [SerializeField]
        private int viewportID;
        public int ViewportID
        {
            get { return viewportID; }
        }


    } // DoublageEventSetCharacter class
	
}// #PROJECTNAME# namespace
