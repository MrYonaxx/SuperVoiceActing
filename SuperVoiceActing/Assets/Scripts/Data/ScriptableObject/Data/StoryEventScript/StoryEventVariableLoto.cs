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
    public class StoryEventVariableLoto : StoryEventCustomData
    {
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableName;
        [SerializeField]
        private int lotoRate = 100000;
        [SerializeField]
        private int lotoWinRate = 1;

        public StoryEventVariableLoto()
        {
            variableName = "";
            lotoRate = 100000;
            lotoWinRate = 1;
        }

        public override void ApplyCustomEvent(StoryEventManager storyManager)
        {
            storyManager.PlayerData.NextRandomEvent.Add(10);
            storyManager.PlayerData.Money -= 10 * lotoWinRate;
            StoryVariable storyVariable = storyManager.PlayerData.GetStoryVariable(variableName);
            int r = Random.Range(0, lotoRate);
            if(r <= lotoWinRate)
            {
                storyVariable.value = 1;
                return;
            }
            storyVariable.value = 0;
        }

    }

} // #PROJECTNAME# namespace