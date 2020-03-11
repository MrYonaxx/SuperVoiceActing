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
    public interface IStoryEventCustomData
    {

    }

    public class StoryEventCustomData : ScriptableObject
    {
        public virtual StoryEventData ApplyCustomEvent(StoryEventManager storyManager)
        {
            return null;
        }
        protected static IEnumerable SelectVariable()
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<StoryVariableDatabase>(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("StoryVariableDatabase")[0]))
                .GetAllVariablesNames();
        }
    }


    [System.Serializable]
	public class StoryEventCustom : StoryEvent
    {
        [SerializeField]
        StoryEventCustomData eventCustom;

        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            StoryEventData storyEvent = eventCustom.ApplyCustomEvent(storyManager);
            if (storyEvent != null)
                yield return storyManager.ExecuteEvent(storyEvent);
            yield return null;
        }

    } // StoryEventScript class

}// #PROJECTNAME# namespace
