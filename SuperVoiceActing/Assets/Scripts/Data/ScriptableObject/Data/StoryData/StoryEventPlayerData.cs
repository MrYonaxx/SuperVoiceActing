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
        ContractData contractToAdd;
        [SerializeField]
        StoryEventData eventToAdd;
        [SerializeField]
        StoryEventData eventEndToAdd;
        [SerializeField]
        VoiceActorData voiceActorToAdd;
        [SerializeField]
        bool voiceActorAdd;
        [SerializeField]
        bool voiceActorRemove;
        [SerializeField]
        bool voiceActorUnavailable;
        [SerializeField]
        int newChapter;
        [SerializeField]
        string newObjective;
        [SerializeField]
        bool skipNextWeek;
        [SerializeField]
        bool unlockStudio;

        [SerializeField]
        StoryEventCustomScript storyEventCustomScript;

        public void SetNode(PlayerData playerData)
        {


            if(currentContract != null)
            {
                playerData.CurrentContract = new Contract(currentContract);
                playerData.CurrentContract.CheckCharacterLock(playerData.VoiceActors);
            }



            if (contractToAdd != null)
            {
                playerData.ContractAccepted.Add(new Contract(contractToAdd));
                playerData.ContractAccepted[playerData.ContractAccepted.Count-1].CheckCharacterLock(playerData.VoiceActors);
                /*for (int i = 0; i < playerData.ContractAccepted.Count; i++)
                {
                    playerData.ContractAccepted.Add(new Contract(contractToAdd));
                    if (playerData.ContractAccepted[i] == null)
                    {
                        playerData.ContractAccepted[i] = new Contract(contractToAdd);
                        CheckCharacterLock(playerData, playerData.ContractAccepted[i]);
                        break;
                    }
                }*/
            }



            if (eventToAdd != null)
            {
                playerData.NextStoryEventsStartWeek.Add(eventToAdd);
            }


            if (eventEndToAdd != null)
            {
                playerData.NextStoryEvents.Add(eventEndToAdd);
            }


            if (voiceActorToAdd != null)
            {
                if (voiceActorRemove == true)
                {
                    for (int i = 0; i < playerData.VoiceActors.Count; i++)
                    {
                        if (playerData.VoiceActors[i].VoiceActorName == voiceActorToAdd.Name)
                            playerData.VoiceActors.RemoveAt(i);
                    }
                }
                else if (voiceActorAdd == true)
                {
                    playerData.VoiceActors.Add(new VoiceActor(voiceActorToAdd));
                }

                if (voiceActorUnavailable == true)
                {
                    for (int i = 0; i < playerData.VoiceActors.Count; i++)
                    {
                        if (playerData.VoiceActors[i].VoiceActorName == voiceActorToAdd.Name)
                        {
                            playerData.VoiceActors[i].Availability = false;
                            playerData.VoiceActors[i].ActorMentalState = VoiceActorState.Absent;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < playerData.VoiceActors.Count; i++)
                    {
                        if (playerData.VoiceActors[i].VoiceActorName == voiceActorToAdd.Name)
                        {
                            playerData.VoiceActors[i].Availability = true;
                            playerData.VoiceActors[i].ActorMentalState = VoiceActorState.Fine;
                        }
                    }
                }
            }

            if (newChapter != 0)
            {
                playerData.SetNewChapter(newChapter, newObjective);
            }

            if(skipNextWeek == true)
            {
                playerData.IsLoading = true;
            }
            if (unlockStudio == true)
                playerData.MenuStudioUnlocked = true;

            if(storyEventCustomScript != null)
            {
                storyEventCustomScript.ApplyCustomScript(playerData);
            }

        }




        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;

        }

    } // StoryEventPlayerData class
	
}// #PROJECTNAME# namespace
