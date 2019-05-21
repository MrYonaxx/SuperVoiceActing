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
	public class StoryEventChoices : StoryEvent
	{
        [HorizontalGroup("Choix")]
        [SerializeField]
        string[] answers;

        [HorizontalGroup("Choix")]
        [SerializeField]
        StoryEventData[] storyChoices;

    } // StoryEventChoices class
	
}// #PROJECTNAME# namespace
