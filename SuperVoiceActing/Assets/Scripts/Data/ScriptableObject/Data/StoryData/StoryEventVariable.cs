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

        public StoryVariable(string vName, string vValue)
        {
            variableName = vName;
            value = 0;
            valueText = vValue;
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

        [ShowIf("custom")]
        [SerializeField]
        string otherVariable;



        public void SetNode(List<StoryVariable> localVariable, PlayerData playerData, Dictionary<string, string> dictionary)
        {
            StoryVariable variable = new StoryVariable(variableName, 0);

            bool b = false;
            if (globalVariable == true) // Check si la valeur existe 
            {
                for (int i = 0; i < playerData.GlobalVariables.Count; i++)
                {
                    if (playerData.GlobalVariables[i].variableName == variableName)
                    {
                        variable = playerData.GlobalVariables[i];
                        b = true;
                    }
                }
                if (b == false)
                {
                    playerData.GlobalVariables.Add(variable);
                    variable = playerData.GlobalVariables[playerData.GlobalVariables.Count - 1];
                }
            }
            else
            {
                for (int i = 0; i < localVariable.Count; i++)
                {
                    if (localVariable[i].variableName == variableName)
                    {
                        variable = localVariable[i];
                        b = true;
                    }
                }
                if (b == false)
                {
                    localVariable.Add(variable);
                    variable = localVariable[localVariable.Count - 1];
                }
            }

            if (custom == false) // Creation de la valeur de la variable
            {
                int finalValue = newValue;
                if (newValueRandom >= newValue)
                    finalValue = Random.Range(newValue, newValueRandom + 1);
                variable.ApplyMathOperation(mathOperation, finalValue);
            }
            else
            {
                customScript.CreateStoryVariable(variable, variableName, playerData);
            }
            AddToDictionnary(dictionary, variable.variableName, variable.valueText);
        }





        private void AddToDictionnary(Dictionary<string, string> dictionary, string variableName, string value)
        {
            string realVariableName = "[" + variableName + "]";
            foreach (string k in dictionary.Keys)
            {
                if (k == realVariableName)
                {
                    dictionary[k] = value;
                    return;
                }
            }
            dictionary.Add(realVariableName, value);
        }

        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }


    } // StoryEventVariable class
	
}// #PROJECTNAME# namespace
