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
	public class StoryEventConditionActor : StoryEvent
    {
        [SerializeField]
        string variableName;

        [HorizontalGroup]
        [SerializeField]
        VoiceActorData[] voiceActorDatas;

        [HorizontalGroup]
        [SerializeField]
        StoryEventData[] storyEvents;

        [SerializeField]
        StoryEventData defaultEvent;


        public StoryEventData SetNode(List<StoryVariable> localVariable, PlayerData playerData)
        {
            StoryVariableActor storyVariable;
            for(int i = 0; i < localVariable.Count; i++)
            {
                if(localVariable[i] is StoryVariableActor)
                {
                    storyVariable = (StoryVariableActor) localVariable[i];
                    if (storyVariable.variableName == variableName)
                    {
                        return CheckActor(storyVariable.GetActorName());
                    }
                }
            }
            return defaultEvent;
        }

        private StoryEventData CheckActor(string actorName)
        {
            for (int i = 0; i < voiceActorDatas.Length; i++)
            {
                if (voiceActorDatas[i].Name == actorName)
                    return storyEvents[i];
            }
            return defaultEvent;
        }



    } // StoryEventConditionActor class
	
}// #PROJECTNAME# namespace
