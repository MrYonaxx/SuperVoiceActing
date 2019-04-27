/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
    [System.Serializable]
	public class DoublageEventMoveCharacter : DoublageEvent
    {
        [SerializeField]
        private StoryCharacterData character;
        public StoryCharacterData Character
        {
            get { return character; }
        }

        [SerializeField]
        private int orderLayer;
        public int OrderLayer
        {
            get { return orderLayer; }
        }

        [SerializeField]
        private Color color;
        public Color Color
        {
            get { return color; }
        }

    } // DoublageEventMoveCharacter class
	
}// #PROJECTNAME# namespace
