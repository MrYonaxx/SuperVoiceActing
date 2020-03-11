/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [System.Serializable]
    public class VariableCondition
    {
        [HorizontalGroup("Conditions", LabelWidth = 100)]
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableName;
        [HorizontalGroup("Conditions", Width = 100)]
        [HideLabel]
        [SerializeField]
        MathOperator mathOperator;
        [HorizontalGroup("Conditions", Width = 100)]
        [HideLabel]
        [SerializeField]
        int value;

        public bool CheckCondition(PlayerData playerData)
        {
            return CheckConditionOperator(playerData.GetStoryVariable(variableName).value);
        }

        private bool CheckConditionOperator(int variableValue)
        {
            switch (mathOperator)
            {
                case MathOperator.Equal:
                    return (variableValue == value);
                case MathOperator.Less:
                    return (variableValue < value);
                case MathOperator.LessEqual:
                    return (variableValue <= value);
                case MathOperator.More:
                    return (variableValue > value);
                case MathOperator.MoreEqual:
                    return (variableValue >= value);
            }
            return false;
        }

        private static IEnumerable SelectVariable()
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<StoryVariableDatabase>(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("StoryVariableDatabase")[0]))
                .GetAllVariablesNames();
        }

    }

    /// <summary>
    /// Definition of the StoryEventLoad class
    /// </summary>
    [System.Serializable]
    public class StoryEventLoad : StoryEvent
    {
        [HideLabel]
        [HorizontalGroup("StoryEventLoad")]
        [SerializeField]
        StoryEventData dataToLoad;

        [HideLabel]
        [SerializeField]
        [HorizontalGroup("StoryEventLoad")]
        DoublageEventData DoublageEventToLoad;

        [HorizontalGroup(PaddingLeft = 5, PaddingRight = 20)]
        [SerializeField]
        VariableCondition[] conditions;


        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            for(int i = 0; i < conditions.Length; i++)
            {
                if (conditions[i].CheckCondition(storyManager.PlayerData) == false)
                    yield break;
            }
            if (dataToLoad.CreateNewScene == true)
                storyManager.CreateScene(dataToLoad);
            yield return storyManager.ExecuteEvent(dataToLoad);
            yield return null;
        }

        public string GetCondition()
        {
            if (conditions != null)
            {
                if (conditions.Length != 0)
                    return " --- Conditions ---";
            }            
            return "";
        }

    } // StoryEventLoad class

} // #PROJECTNAME# namespace