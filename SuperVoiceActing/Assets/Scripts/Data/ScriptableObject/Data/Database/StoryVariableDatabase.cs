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


using System.Linq;

namespace VoiceActing
{
    [System.Serializable]
    public class StoryVariable
    {
        [HideLabel]
        [SerializeField]
        private string variableName;
        public string VariableName
        {
            get { return variableName; }
        }

        [HideInInspector]
        public int value;
        [HideInInspector]
        public string valueText;

        public StoryVariable(int intValue, string stringValue)
        {
            variableName = "";
            value = intValue;
            valueText = stringValue;
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

    [CreateAssetMenu(fileName = "StoryVariableDatabase", menuName = "Database/StoryVariableDatabase", order = 1)]
    public class StoryVariableDatabase : ScriptableObject
    {
        [SerializeField]
        List<StoryVariable> storyVariablesDatas = new List<StoryVariable>();

        public IEnumerable GetAllVariablesNames()
        {
            return storyVariablesDatas.Select(x => new ValueDropdownItem(x.VariableName, x.VariableName));
        }

        public IEnumerable GetAllVariablesIntValue()
        {
            return storyVariablesDatas.Select(x => new ValueDropdownItem(x.VariableName, x.value));
        }

        public IEnumerable GetAllVariablesStringValue()
        {
            return storyVariablesDatas.Select(x => new ValueDropdownItem(x.VariableName, x.valueText));
        }

    } 

} // #PROJECTNAME# namespace