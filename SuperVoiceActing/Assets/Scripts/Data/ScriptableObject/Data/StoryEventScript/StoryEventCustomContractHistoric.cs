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
    public class StoryEventCustomContractHistoric: StoryEventCustomData
    {
        [SerializeField]
        ContractType contractType;
        [SerializeField]
        VoiceActorDatabase voiceActorDatabase;

        public override void ApplyCustomEvent(StoryEventManager storyManager)
        {
            List<Contract> contractsToSelect = new List<Contract>();
            for(int i = 0; i < storyManager.PlayerData.ContractHistoric.Count; i++)
            {
                if(storyManager.PlayerData.ContractHistoric[i].ContractType == contractType)
                {
                    contractsToSelect.Add(storyManager.PlayerData.ContractHistoric[i]);
                }
            }

            int rand = Random.Range(0, contractsToSelect.Count);
            int randLine = Random.Range(0, contractsToSelect[rand].TextData.Count);
            VoiceActorData voiceActorData = voiceActorDatabase.GetVoiceActorData(contractsToSelect[rand].VoiceActorsID[contractsToSelect[rand].TextData[randLine].Interlocuteur]);
        }

    } 

} // #PROJECTNAME# namespace