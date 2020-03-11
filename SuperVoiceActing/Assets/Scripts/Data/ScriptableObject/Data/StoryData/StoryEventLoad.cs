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
    /// Definition of the StoryEventLoad class
    /// </summary>
    [System.Serializable]
    public class StoryEventLoad : StoryEvent
    {
        [HideLabel]
        [HorizontalGroup("StoryEventLoad")]
        [SerializeField]
        StoryEventData dataToLoad;

        [HideLabel]
        [SerializeField]
        [HorizontalGroup("StoryEventLoad")]
        DoublageEventData DoublageEventToLoad;


        public DoublageEventData GetDoublageEventToLoad()
        {
            return DoublageEventToLoad;
        }


        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            if (dataToLoad.CreateNewScene == true)
                storyManager.CreateScene(dataToLoad);
            yield return storyManager.ExecuteEvent(dataToLoad);
        }

    } // StoryEventLoad class

} // #PROJECTNAME# namespace