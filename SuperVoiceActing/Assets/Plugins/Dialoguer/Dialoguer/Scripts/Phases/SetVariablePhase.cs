using UnityEngine;
using System.Collections.Generic;
using DialoguerEditor;

namespace DialoguerCore
{
    /// <summary>
    /// Phase to set the value of a single variable
    /// </summary>
    /// <seealso cref="DialoguerCore.AbstractDialoguePhase" />
    public class SetVariablePhase : AbstractDialoguePhase
    {
        /// <summary>
        /// The scope of the variable to set
        /// </summary>
        public readonly VariableEditorScopes scope;

        /// <summary>
        /// The type of the variable to set
        /// </summary>
        public readonly VariableEditorTypes type;

        /// <summary>
        /// The identifier of the variable to set
        /// </summary>
        public readonly int variableId;

        /// <summary>
        /// The operation to apply to the variable
        /// </summary>
        public readonly VariableEditorSetEquation equation;

        /// <summary>
        /// The value set by the user
        /// </summary>
        public readonly string inputValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetVariablePhase"/> class.
        /// </summary>
        /// <param name="scope">The scope of the variable to set.</param>
        /// <param name="type">The type of the variable to set.</param>
        /// <param name="variableId">The identifier of the variable to set.</param>
        /// <param name="equation">The operation to apply to the variable.</param>
        /// <param name="inputValue">The value set by the user.</param>
        /// <param name="outs">The indices of the out phases.</param>
        public SetVariablePhase( VariableEditorScopes scope, VariableEditorTypes type, int variableId, VariableEditorSetEquation equation, string inputValue, List<int> outs ) : base( outs )
        {
            this.scope = scope;
            this.type = type;
            this.variableId = variableId;
            this.equation = equation;
            this.inputValue = inputValue;
        }

        /// <summary>
        /// Called when the phase is started.
        /// </summary>
        protected override void onStart()
        {
            bool success = false;

            switch ( type )
            {
                case VariableEditorTypes.Boolean:
                    success = SetBool();
                    break;

                case VariableEditorTypes.Float:
                    success = SetFloat();
                    break;

                case VariableEditorTypes.String:
                    success = SetString();
                    break;
            }

            if ( !success ) Debug.LogWarning( "[SetVariablePhase] Could not parse setValue" );

            SelectOutPhase( 0 );
            Terminate();
        }

        /// <summary>
        /// Sets the bool.
        /// </summary>
        /// <returns><c>true</c> if the operation succeeded, <c>false</c> otherwise</returns>
        private bool SetBool()
        {
            bool inputBool;
            bool success = bool.TryParse( inputValue, out inputBool );
            switch ( equation )
            {
                case VariableEditorSetEquation.Equals:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.booleans[variableId] = inputBool;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalBoolean( variableId, inputBool );
                    }
                    break;

                case VariableEditorSetEquation.Toggle:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.booleans[variableId] = !_localVariables.booleans[variableId];
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalBoolean( variableId, !DialoguerDataManager.GetGlobalBoolean( variableId ) );
                    }
                    success = true;
                    break;
            }

            return success;
        }

        /// <summary>
        /// Sets the float.
        /// </summary>
        /// <returns><c>true</c> if the operation succeeded, <c>false</c> otherwise</returns>
        private bool SetFloat()
        {
            float inputFloat;
            bool success = float.TryParse( inputValue, out inputFloat );
            switch ( equation )
            {
                case VariableEditorSetEquation.Equals:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.floats[variableId] = inputFloat;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalFloat( variableId, inputFloat );
                    }
                    break;

                case VariableEditorSetEquation.Add:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.floats[variableId] += inputFloat;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalFloat( variableId, DialoguerDataManager.GetGlobalFloat( variableId ) + inputFloat );
                    }
                    break;

                case VariableEditorSetEquation.Subtract:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.floats[variableId] -= inputFloat;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalFloat( variableId, DialoguerDataManager.GetGlobalFloat( variableId ) - inputFloat );
                    }
                    break;

                case VariableEditorSetEquation.Multiply:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.floats[variableId] *= inputFloat;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalFloat( variableId, DialoguerDataManager.GetGlobalFloat( variableId ) * inputFloat );
                    }
                    break;

                case VariableEditorSetEquation.Divide:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.floats[variableId] /= inputFloat;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalFloat( variableId, DialoguerDataManager.GetGlobalFloat( variableId ) / inputFloat );
                    }
                    break;

            }

            return success;
        }

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <returns><c>true</c> if the operation succeeded, <c>false</c> otherwise</returns>
        private bool SetString()
        {
            bool success = true;
            string inputString = inputValue;
            switch ( equation )
            {
                case VariableEditorSetEquation.Equals:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.strings[variableId] = inputString;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalString( variableId, inputString );
                    }
                    break;

                case VariableEditorSetEquation.Add:
                    if ( scope == VariableEditorScopes.Local )
                    {
                        _localVariables.strings[variableId] += inputString;
                    }
                    else
                    {
                        DialoguerDataManager.SetGlobalString( variableId, DialoguerDataManager.GetGlobalString( variableId ) + inputString );
                    }
                    break;
            }

            return success;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return string.Format( "Set Variable Phase\nScope: {0}\nType: {1}\nVariable ID: {2}\nEquation: {3}\nSet Value: {4}\nOut: {5}\n", this.scope.ToString() , this.type.ToString(), this.variableId, this.equation.ToString(), this.inputValue.ToString(), this.outs[0] );
        }
    }
}
