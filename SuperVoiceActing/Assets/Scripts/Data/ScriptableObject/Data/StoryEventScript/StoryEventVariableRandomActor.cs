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



        public override StoryVariable CreateStoryVariable(string variableName, PlayerData playerData)
        {
            List<VoiceActor> actorsAvailable = new List<VoiceActor>();
            for(int i = 0; i < playerData.VoiceActors.Count; i++)
            {
                if(playerData.VoiceActors[i].Availability == true)
                {
                    actorsAvailable.Add(playerData.VoiceActors[i]);
                }
            }
            if (playerData.CurrentContract != null)
            {
                for (int i = 0; i < playerData.CurrentContract.VoiceActors.Count; i++)
                {
                    actorsAvailable.Remove(playerData.CurrentContract.VoiceActors[i]);
                }
            }



            int rand = Random.Range(0, actorsAvailable.Count);
            if(rand < actorsAvailable.Count)
            {
                return new StoryVariableActor(variableName, actorsAvailable[rand]);
            }
            else
            {
                return new StoryVariableActor(variableName, null);
            }

        }




    } // StoryEventVariableRandomActor class
	
}// #PROJECTNAME# namespace
