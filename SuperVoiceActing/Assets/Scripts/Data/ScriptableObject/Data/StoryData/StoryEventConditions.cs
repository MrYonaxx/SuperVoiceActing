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
	public class StoryEventConditions : StoryEvent
	{
        [HorizontalGroup("Conditions", LabelWidth = 100)]
        [SerializeField]
        string variableName;
        [HorizontalGroup("Conditions")]
        [HideLabel]
        [SerializeField]
        MathOperator mathOperator;
        [HorizontalGroup("Conditions")]
        [HideLabel]
        [SerializeField]
        int value;

        [HideLabel]
        [SerializeField]
        StoryEventData storyEvent;
        public StoryEventData StoryEvent
        {
            get { return storyEvent; }
        }


        public bool CheckCondition(List<StoryVariable> localVariable, PlayerData playerData)
        {
            int variableValue = 0;
            for (int i = 0; i < localVariable.Count; i++)
            {
                if (localVariable[i].variableName == variableName)
                {
                    variableValue = localVariable[i].value;
                }
            }
            return CheckConditionOperator(variableValue);
        }

        private bool CheckConditionOperator(int variableValue)
        {
            switch(mathOperator)
            {
                case MathOperator.Equal:
                    return (variableValue == value);
                case MathOperator.Less:
                    return (variableValue == value);
                case MathOperator.LessEqual:
                    return (variableValue == value);
                case MathOperator.More:
                    return (variableValue == value);
                case MathOperator.MoreEqual:
                    return (variableValue == value);
            }
            return false;
        }


        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }

    } // StoryEvent1 class
	
}// #PROJECTNAME# namespace
