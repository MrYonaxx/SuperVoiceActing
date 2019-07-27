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
	public class RandomEventDatabase : ScriptableObject
	{

        [SerializeField]
        StoryEventData[] randomStoryEvents;
        [SerializeField]
        RandomEventCondition[] randomStoryEventsCondition;

        [SerializeField]
        RandomEvent[] randomEvents;
        [SerializeField]
        RandomEventCondition[] randomEventsCondition;


        public StoryEventData GetStoryRandomEvent()
        {
            return null;
        }

        public RandomEvent GetRandomEvent()
        {
            return null;
        }

    } // RandomEventDatabase class
	
}// #PROJECTNAME# namespace
