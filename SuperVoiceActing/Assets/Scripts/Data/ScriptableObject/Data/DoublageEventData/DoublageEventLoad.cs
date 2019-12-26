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







        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            if (doublageEventData != null)
            {
                eventManager.LoadEvent(doublageEventData);
                //yield return eventManager.ExecuteEvent(doublageEventData);
                /*currentEvent = node.DoublageEventData;
                indexEvent = -1;
                eventManager.ExecuteEvent();*/
            }
            else if (storyEventData != null)
            {
                //panelFlashback.SetActive(true);
                //StartCoroutine(TransitionFlashback(true));
                //storyEventManager.StartStoryEventDataWithScene(node.StoryEventData);
            }
            yield break;
        }

        /*public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            eventManager.StoryEventManager.StartStoryEventDataWithScene(storyEventData);
            yield return null;
            /*int time = 30;
            float speed = 1f / time;
            while (time != 0)
            {
                time -= 1;
                flashbackTransition.color += new Color(0, 0, 0, speed);
                yield return null;
            }
            viewportFlashback.SetActive(appear);

            speed = -speed;
            time = 45;
            while (time != 0)
            {
                time -= 1;
                flashbackTransition.color += new Color(0, 0, 0, speed);
                yield return null;
            }
            panelFlashback.SetActive(appear);
        }*/

    } // DoublageEventLoad class
	
}// #PROJECTNAME# namespace
