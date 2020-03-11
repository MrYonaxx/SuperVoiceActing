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
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string storyVariableName;

        public StoryEventVariableRandomActor()
        {
            storyVariableName = "";
        }


        public override void ApplyCustomEvent(StoryEventManager storyManager)
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
            if(rand < actorsIDAvailable.Count)
            {
                storyManager.PlayerData.GetStoryVariable(storyVariableName).valueText = actorsIDAvailable[rand];
            }

        }




    } // StoryEventVariableRandomActor class
	
}// #PROJECTNAME# namespace
