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
    public class DoublageEventLoad : DoublageEvent
    {
        [SerializeField]
        private DoublageEventData doublageEventData;
        public DoublageEventData DoublageEventData
        {
            get { return doublageEventData; }
        }

        [SerializeField]
        private StoryEventData storyEventData;
        public StoryEventData StoryEventData
        {
            get { return storyEventData; }
        }

    } // DoublageEventLoad class
	
}// #PROJECTNAME# namespace
