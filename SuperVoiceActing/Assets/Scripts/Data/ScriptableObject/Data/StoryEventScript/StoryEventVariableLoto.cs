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
    [CreateAssetMenu(fileName = "VariableRandomLoto", menuName = "StoryEvent/StoryEventVariable/Loto", order = 1)]
    public class StoryEventVariableLoto : StoryEventVariableScript
    {
        [SerializeField]
        private int lotoRate = 100000;
        [SerializeField]
        private int lotoWinRate = 1;


        public override void CreateStoryVariable(StoryVariable storyVariable, string variableName, PlayerData playerData)
        {
            playerData.NextRandomEvent.Add(10);
            playerData.Money -= 10 * lotoWinRate;
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