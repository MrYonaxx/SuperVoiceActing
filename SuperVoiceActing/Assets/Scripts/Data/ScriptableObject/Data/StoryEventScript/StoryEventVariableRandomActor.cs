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
    public class StoryEventVariableRandomActor : StoryEventCustomData
    {
        [SerializeField]
        VoiceActorData[] voiceActorDatas;
        [SerializeField]
        StoryEventData[] storyEventDatas;

        [SerializeField]
        StoryEventData defaultEvent;


        public override StoryEventData ApplyCustomEvent(StoryEventManager storyManager)
        {
            List<string> actorsIDAvailable = new List<string>();
            for(int i = 0; i < storyManager.PlayerData.VoiceActors.Count; i++)
            {
                if(storyManager.PlayerData.VoiceActors[i].Availability == true)
                {
                    actorsIDAvailable.Add(storyManager.PlayerData.VoiceActors[i].VoiceActorID);
                }
            }
            if (storyManager.PlayerData.CurrentContract != null)
            {
                for (int i = 0; i < storyManager.PlayerData.CurrentContract.VoiceActorsID.Count; i++)
                {
                    actorsIDAvailable.Remove(storyManager.PlayerData.CurrentContract.VoiceActorsID[i]);
                }
            }

            int rand = Random.Range(0, actorsIDAvailable.Count);
            for(int i = 0; i < voiceActorDatas.Length; i++)
            {
                if (actorsIDAvailable[i].Equals(voiceActorDatas[i].name))
                    return storyEventDatas[i];
            }
            return defaultEvent;
        }




    } // StoryEventVariableRandomActor class
	
}// #PROJECTNAME# namespace
