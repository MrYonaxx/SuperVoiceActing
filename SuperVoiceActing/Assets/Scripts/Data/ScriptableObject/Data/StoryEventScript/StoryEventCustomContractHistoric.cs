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
    [CreateAssetMenu(fileName = "CustomContractHistoricRandom", menuName = "StoryEvent/StoryEventCustom/ContractHistoricRandom", order = 1)]
    public class StoryEventCustomContractHistoric: StoryEventCustomData
    {
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableContractName;
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableVoiceActorName;
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableTextData;
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableScore;

        [Space]
        [SerializeField]
        ContractType contractType;
        [SerializeField]
        VoiceActorDatabase voiceActorDatabase;

        [SerializeField]
        StoryEventData nextStoryEvent;
        [SerializeField]
        StoryEventData defaultEvent;

        public override StoryEventData ApplyCustomEvent(StoryEventManager storyManager)
        {
            List<Contract> contractsToSelect = new List<Contract>();
            for(int i = 0; i < storyManager.PlayerData.ContractHistoric.Count; i++)
            {
                if(storyManager.PlayerData.ContractHistoric[i].ContractType == contractType)
                {
                    contractsToSelect.Add(storyManager.PlayerData.ContractHistoric[i]);
                }
            }
            if(contractsToSelect.Count == 0)
                return defaultEvent;

            int rand = Random.Range(0, contractsToSelect.Count);
            storyManager.PlayerData.GetStoryVariable(variableContractName).SetValueString(contractsToSelect[rand].Name);

            int randLine = Random.Range(0, contractsToSelect[rand].TextData.Count);
            storyManager.PlayerData.GetStoryVariable(variableTextData).SetValueString(contractsToSelect[rand].TextData[randLine].Text);

            VoiceActorData voiceActorData = voiceActorDatabase.GetVoiceActorData(contractsToSelect[rand].VoiceActorsID[contractsToSelect[rand].TextData[randLine].Interlocuteur]);
            storyManager.PlayerData.GetStoryVariable(variableVoiceActorName).SetValueString(voiceActorData.ActorName);

            storyManager.PlayerData.GetStoryVariable(variableScore).SetValueInt((int)((contractsToSelect[rand].Score / (float)contractsToSelect[rand].HighScore) * 100));

            return nextStoryEvent;
        }

    } 

} // #PROJECTNAME# namespace