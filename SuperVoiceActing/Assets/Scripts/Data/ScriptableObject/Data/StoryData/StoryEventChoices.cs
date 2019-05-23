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
        [SerializeField]
        private string text;
        public string Text
        {
            get { return text; }
        }

        [HorizontalGroup("Choix")]
        [SerializeField]
        private string[] answers;
        public string[] Answers
        {
            get { return answers; }
        }

        [HorizontalGroup("Choix")]
        [SerializeField]
        private StoryEventData[] storyChoices;
        public StoryEventData[] StoryChoices
        {
            get { return storyChoices; }
        }

        private bool stop = false;

        public void StopCoroutine()
        {
            stop = true;
        }

        protected override IEnumerator StoryEventCoroutine()
        {
            while (stop == false)
                yield return null;
            stop = false;

        }

    } // StoryEventChoices class
	
}// #PROJECTNAME# namespace
