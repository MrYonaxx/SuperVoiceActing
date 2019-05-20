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
    [System.Serializable]
	public class StoryEventPlayerData : StoryEvent
	{

        [SerializeField]
        ContractData contractToAdd;
        [SerializeField]
        StoryEventData eventToAdd;
        [SerializeField]
        VoiceActorData voiceActorToAdd;

        public void SetNode(PlayerData playerData)
        {
            for(int i = 0; i < playerData.ContractAccepted.Count; i++)
            {
                if(playerData.ContractAccepted[i] == null)
                {
                    playerData.ContractAccepted[i] = new Contract(contractToAdd);
                    CheckCharacterLock(playerData, playerData.ContractAccepted[i]);
                    break;
                }
            }
        }

        public void CheckCharacterLock(PlayerData playerData, Contract newContract)
        {
            for (int i = 0; i < newContract.Characters.Count; i++)
            {
                if (newContract.Characters[i].CharacterLock != null)
                {
                    // On cherche l'acteur dans le monde
                    for (int j = 0; j < playerData.VoiceActors.Count; j++)
                    {
                        if (newContract.Characters[i].CharacterLock.Name == playerData.VoiceActors[j].Name)
                        {
                            newContract.VoiceActors[i] = playerData.VoiceActors[j];
                            break;
                        }
                    }
                    // Si l'acteur n'est pas dans la liste, on l'invoque
                    if (newContract.VoiceActors[i] == null)
                    {
                        playerData.VoiceActors.Add(new VoiceActor(newContract.Characters[i].CharacterLock));
                        newContract.VoiceActors[i] = playerData.VoiceActors[playerData.VoiceActors.Count-1];
                    }

                }
            }
        }

        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;

        }

    } // StoryEventPlayerData class
	
}// #PROJECTNAME# namespace
