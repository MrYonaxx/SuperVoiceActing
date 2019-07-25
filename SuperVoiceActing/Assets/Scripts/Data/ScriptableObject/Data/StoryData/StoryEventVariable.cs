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

    public struct StoryVariable
    {
        public string variableName;
        public int value;

        public StoryVariable(string vName, int vValue)
        {
            variableName = vName;
            value = vValue;
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
        }

    }

    [System.Serializable]
    public class StoryEventVariable : StoryEvent
    {


        [SerializeField]
        bool globalVariable;

        [HorizontalGroup("Variable")]
        [SerializeField]
        string variableName;
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








        public void SetNode(List<StoryVariable> localVariable, PlayerData playerData)
        {
            StoryVariable variable = new StoryVariable(variableName, 0);
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
                finalValue = Random.Range(newValue, newValueRandom+1);
            variable.ApplyMathOperation(mathOperation, finalValue);
            localVariable.Add(variable);
        }


        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }


    } // StoryEventVariable class
	
}// #PROJECTNAME# namespace
