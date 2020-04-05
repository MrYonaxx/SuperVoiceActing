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
	public class StoryEventPlayerData : StoryEvent
	{
        [SerializeField]
        ContractData currentContract;

        [SerializeField]
        ContractData[] contractToAdd;
        [SerializeField]
        ContractData[] contractsAvailable;

        [SerializeField]
        StoryEventData[] eventToAdd;
        [SerializeField]
        StoryEventData[] eventEndToAdd;

        [SerializeField]
        VoiceActorData[] voiceActorToAdd;
        [SerializeField]
        VoiceActorData[] voiceActorToRemove;
        [SerializeField]
        VoiceActorData[] voiceActorUnavailable;

        public override bool InstantNodeCoroutine()
        {
            return true;
        }


        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            if (currentContract != null)
            {
                storyManager.PlayerData.CurrentContract = new Contract(currentContract);
                storyManager.PlayerData.CurrentContract.CheckCharacterLock(storyManager.PlayerData.VoiceActors);
            }

            for (int i = 0; i < contractToAdd.Length; i++)
            {
                storyManager.PlayerData.ContractAccepted.Add(new Contract(contractToAdd[i]));
                storyManager.PlayerData.ContractAccepted[storyManager.PlayerData.ContractAccepted.Count - 1].CheckCharacterLock(storyManager.PlayerData.VoiceActors);
            }
            for (int i = 0; i < contractsAvailable.Length; i++)
            {
                storyManager.PlayerData.ContractAvailable.Add(new Contract(contractsAvailable[i]));
                storyManager.PlayerData.ContractAvailable[storyManager.PlayerData.ContractAvailable.Count - 1].CheckCharacterLock(storyManager.PlayerData.VoiceActors);
            }



            for (int i = 0; i < eventToAdd.Length; i++)
            {
                storyManager.PlayerData.NextStoryEventsStartWeek.Add(eventToAdd[i]);
            }
            for (int i = 0; i < eventEndToAdd.Length; i++)
            {
                storyManager.PlayerData.NextStoryEventsStartWeek.Add(eventEndToAdd[i]);
            }



            for (int i = 0; i < voiceActorToAdd.Length; i++)
            {
                storyManager.PlayerData.VoiceActors.Add(new VoiceActor(voiceActorToAdd[i]));
            }
            for (int i = 0; i < voiceActorToRemove.Length; i++)
            {
                for (int j = 0; j < storyManager.PlayerData.VoiceActors.Count; j++)
                {
                    if (storyManager.PlayerData.VoiceActors[i].VoiceActorID == voiceActorToRemove[i].NameID)
                        storyManager.PlayerData.VoiceActors.RemoveAt(i);
                }
            }
            for (int i = 0; i < voiceActorUnavailable.Length; i++)
            {
                for (int j = 0; j < storyManager.PlayerData.VoiceActors.Count; j++)
                {
                    if (storyManager.PlayerData.VoiceActors[i].VoiceActorID == voiceActorUnavailable[i].NameID)
                    {
                        storyManager.PlayerData.VoiceActors[i].Availability = false;
                        storyManager.PlayerData.VoiceActors[i].ActorMentalState = VoiceActorState.Absent;
                    }
                }
            }


            yield return null;
        }


    } // StoryEventPlayerData class
	
}// #PROJECTNAME# namespace
