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

    public enum MathOperation
    {
        Equal,
        Add,
        Substract
    }


    [System.Serializable]
    public class StoryEventVariable : StoryEvent
    {
        [HideLabel]
        [HorizontalGroup("Variable")]
        [ValueDropdown("SelectVariable")]
        [SerializeField]
        string variableName;
        public string VariableName
        {
            get { return variableName; }
        }


        [HorizontalGroup("Variable")]
        [HideLabel]
        [SerializeField]
        MathOperation mathOperation;
        [HorizontalGroup("Variable")]
        [HideLabel]
        [SerializeField]
        int newValue;
        [HorizontalGroup("Variable")]
        [HideLabel]
        [SerializeField]
        int newValueRandom;



        private static IEnumerable SelectVariable()
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<StoryVariableDatabase>(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("StoryVariableDatabase")[0]))
                .GetAllVariablesNames();
        }



        public override bool InstantNodeCoroutine()
        {
            return true;
        }

        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            int finalValue = newValue;
            if (newValueRandom >= newValue)
                finalValue = Random.Range(newValue, newValueRandom + 1);
            storyManager.PlayerData.GetStoryVariable(variableName).ApplyMathOperation(mathOperation, finalValue);
            yield break;
        }


    } // StoryEventVariable class
	
}// #PROJECTNAME# namespace
