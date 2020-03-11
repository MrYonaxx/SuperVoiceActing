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


        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            stop = false;
            storyManager.StoryEventChoiceManager.DrawChoices(this);
            while (storyManager.StoryEventChoiceManager.ChoiceSelected == false)
                yield return null;
            yield return new WaitForSeconds(1f);
            yield return storyManager.ExecuteEvent(storyChoices[storyManager.StoryEventChoiceManager.IndexSelected]);
        }

        public void StopCoroutine()
        {
            stop = true;
        }

    } // StoryEventChoices class
	
}// #PROJECTNAME# namespace
