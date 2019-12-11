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
    [CreateAssetMenu(fileName = "VariableRandomActor", menuName = "StoryEvent/StoryEventVariable/RandomActor", order = 1)]
    public class StoryEventVariableRandomActor : StoryEventVariableScript
	{



        public override void CreateStoryVariable(StoryVariable storyVariable, string variableName, PlayerData playerData)
        {
            List<string> actorsIDAvailable = new List<string>();
            for(int i = 0; i < playerData.VoiceActors.Count; i++)
            {
                if(playerData.VoiceActors[i].Availability == true)
                {
                    actorsIDAvailable.Add(playerData.VoiceActors[i].VoiceActorID);
                }
            }
            if (playerData.CurrentContract != null)
            {
                for (int i = 0; i < playerData.CurrentContract.VoiceActorsID.Count; i++)
                {
                    actorsIDAvailable.Remove(playerData.CurrentContract.VoiceActorsID[i]);
                }
            }



            int rand = Random.Range(0, actorsIDAvailable.Count);
            if(rand < actorsIDAvailable.Count)
            {
                storyVariable.valueText = actorsIDAvailable[rand];
                //return new StoryVariable(variableName, actorsIDAvailable[rand]);
            }
            /*else
            {
                return new StoryVariable(variableName, null);
            }*/

        }




    } // StoryEventVariableRandomActor class
	
}// #PROJECTNAME# namespace
