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
    public class StoryEventCustomData 
    {
        public virtual void ApplyCustomEvent(StoryEventManager storyManager)
        {

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
        [ValueDropdown("SelectStoryEventCustomData")]
        [SerializeField]
        StoryEventCustomData[] eventCustom;

        public override bool InstantNodeCoroutine()
        {
            return true;
        }
        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            //eventCustom.ApplyCustomEvent(storyManager);
            yield break;
        }



        private IEnumerable SelectStoryEventCustomData = new ValueDropdownList<StoryEventCustomData>()
        {
            { "Random Actor", new StoryEventVariableRandomActor() },
            { "Loto", new StoryEventVariableLoto() },
        };


    } // StoryEventScript class

}// #PROJECTNAME# namespace
