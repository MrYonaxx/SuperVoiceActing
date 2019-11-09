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
    [CreateAssetMenu(fileName = "StoryEventCustomStrike", menuName = "StoryEvent/StoryEventCustom/Strike", order = 1)]
    public class StoryEventCustomStrike: StoryEventCustomScript
    {
        public override void ApplyCustomScript(PlayerData playerData)
        {
            int rand = Random.Range((int) (playerData.VoiceActors.Count * 0.25f), (int)(playerData.VoiceActors.Count * 0.75f));
            for(int i = 0; i < rand; i++)
            {
                int r = Random.Range(0, playerData.VoiceActors.Count);
                playerData.VoiceActors[r].ActorMentalState = VoiceActorState.Dead;
            }
        }
    } 

} // #PROJECTNAME# namespace