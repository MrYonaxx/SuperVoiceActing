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
    public class StoryEventCustomStrike: StoryEventCustomData
    {


        public override StoryEventData ApplyCustomEvent(StoryEventManager storyManager)
        {
            int rand = Random.Range((int) (storyManager.PlayerData.VoiceActors.Count * 0.25f), (int)(storyManager.PlayerData.VoiceActors.Count * 0.75f));
            for(int i = 0; i < rand; i++)
            {
                int r = Random.Range(0, storyManager.PlayerData.VoiceActors.Count);
                storyManager.PlayerData.VoiceActors[r].ActorMentalState = VoiceActorState.Dead;
            }
            return null;
        }
    } 

} // #PROJECTNAME# namespace