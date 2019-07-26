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



    public class StoryVariable
    {
        public string variableName;
        public int value;
        public string valueText;

        public StoryVariable()
        {
            variableName = "";
            value = 0;
            valueText = "0";
        }

        public StoryVariable(string vName, int vValue)
        {
            variableName = vName;
            value = vValue;
            valueText = value.ToString();
        }

        public void ApplyMathOperation(MathOperation mathOperation, int newValue)
        {
            switch (mathOperation)
            {
                case MathOperation.Equal:
                    value = newValue;
                    break;
                case MathOperation.Add:
                    value += newValue;
                    break;
                case MathOperation.Substract:
                    value -= newValue;
                    break;
            }
            valueText = value.ToString();
        }

    }



    public class StoryVariableActor : StoryVariable
    {

        public VoiceActor voiceActor;

        public StoryVariableActor(string vName, VoiceActor VA)
        {
            variableName = vName;
            voiceActor = VA;
            value = 0;
            if(VA == null)
                valueText = "";
            else
                valueText = VA.Name;
        }

    }



    [System.Serializable]
    public class StoryEventVariable : StoryEvent
    {


        [SerializeField]
        bool globalVariable;

        [SerializeField]
        bool custom = false;

        [HorizontalGroup("Variable")]
        [SerializeField]
        string variableName;
        public string VariableName
        {
            get { return variableName; }
        }


        [HideIf("custom")]
        [HorizontalGroup("Variable")]
        [HideLabel]
        [SerializeField]
        MathOperation mathOperation;
        [HideIf("custom")]
        [HorizontalGroup("Variable")]
        [HideLabel]
        [SerializeField]
        int newValue;
        [HideIf("custom")]
        [HorizontalGroup("Variable")]
        [HideLabel]
        [SerializeField]
        int newValueRandom;

        [ShowIf("custom")]
        [SerializeField]
        StoryEventVariableScript customScript;



        public void SetNode(List<StoryVariable> localVariable, PlayerData playerData, Dictionary<string, string> dictionary)
        {
            StoryVariable variable = new StoryVariable(variableName, 0);
            if (custom == false)
            {
                int finalValue = newValue;
                if (globalVariable == true)
                {
                //
                }
                else
                {
                    for (int i = 0; i < localVariable.Count; i++)
                    {
                        if (localVariable[i].variableName == variableName)
                        {
                            variable = localVariable[i];
                        }
                    }
                }
                if (newValueRandom >= newValue)
                    finalValue = Random.Range(newValue, newValueRandom + 1);
                variable.ApplyMathOperation(mathOperation, finalValue);             
            }
            else
            {
                variable = customScript.CreateStoryVariable(variableName, playerData);
            }
            localVariable.Add(variable);
            AddToDictionnary(dictionary, variable.variableName, variable.valueText);
        }

        private void AddToDictionnary(Dictionary<string, string> dictionary, string variableName, string value)
        {
            foreach (string k in dictionary.Keys)
            {
                if (k == variableName)
                {
                    dictionary[k] = value;
                    return;
                }
            }
            dictionary.Add("["+variableName+"]", value);
        }

        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }


    } // StoryEventVariable class
	
}// #PROJECTNAME# namespace
