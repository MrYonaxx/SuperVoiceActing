using UnityEngine;
using System.Collections.Generic;
using DialoguerEditor;

namespace DialoguerCore
{
    /// <summary>
    /// Phase to branch the flow according to a condition
    /// </summary>
    /// <seealso cref="DialoguerCore.AbstractDialoguePhase" />
    public class ConditionalPhase : AbstractDialoguePhase
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
        public readonly VariableEditorGetEquation equation;

        /// <summary>
        /// The value set by the user
        /// </summary>
        public readonly string inputValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPhase"/> class.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="type">The type.</param>
        /// <param name="variableId">The variable identifier.</param>
        /// <param name="equation">The equation.</param>
        /// <param name="inputValue">The input value.</param>
        /// <param name="outs">The indices of the out phases.</param>
        public ConditionalPhase( VariableEditorScopes scope, VariableEditorTypes type, int variableId, VariableEditorGetEquation equation, string inputValue, List<int> outs ) : base( outs )
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
            bool isTrue = false;

            switch ( type )
            {
                case VariableEditorTypes.Boolean:
                    isTrue = CheckBool();
                    break;

                case VariableEditorTypes.Float:
                    isTrue = CheckFloat();
                    break;

                case VariableEditorTypes.String:
                    isTrue = CheckString();
                    break;
            }

            if ( isTrue )
            {
                //Debug.Log ("[ConditionalPhase] Continue 0");
                SelectOutPhase( 0 );
            }
            else
            {
                //Debug.Log ("[ConditionalPhase] Continue 1");
                SelectOutPhase( 1 );
            }

            Terminate();
        }

        /// <summary>
        /// Checks the bool.
        /// </summary>
        /// <returns><c>true</c> if the condition between the input boolean and the chosen variable is satisfied, <c>false</c> otherwise</returns>
        private bool CheckBool()
        {
            bool isTrue = false;
            bool parsedBool;
            bool checkBool = ( scope == VariableEditorScopes.Local ) ?
                _localVariables.booleans[variableId] :
                DialoguerDataManager.GetGlobalBoolean( variableId ) ;

            if ( !bool.TryParse( inputValue, out parsedBool ) )
                Debug.LogError( "[ConditionalPhase] Could Not Parse Bool: " + inputValue );

            switch ( equation )
            {
                case VariableEditorGetEquation.Equals:
                    if ( parsedBool == checkBool ) isTrue = true;
                    break;

                case VariableEditorGetEquation.NotEquals:
                    if ( parsedBool != checkBool ) isTrue = true;
                    break;
            }

            return isTrue;
        }

        /// <summary>
        /// Checks the float.
        /// </summary>
        /// <returns><c>true</c> if the condition between the input float and the chosen variable is satisfied, <c>false</c> otherwise</returns>
        private bool CheckFloat()
        {
            bool isTrue = false;
            float parsedFloat;
            float checkFloat = ( scope == VariableEditorScopes.Local ) ?
                _localVariables.floats[variableId] :
                DialoguerDataManager.GetGlobalFloat( variableId );

            if ( !float.TryParse( inputValue, out parsedFloat ) )
                Debug.LogError( "[ConditionalPhase] Could Not Parse Float: " + inputValue );
            
            switch ( equation )
            {
                case VariableEditorGetEquation.Equals:
                    if ( checkFloat == parsedFloat ) isTrue = true;
                    break;

                case VariableEditorGetEquation.NotEquals:
                    if ( checkFloat != parsedFloat ) isTrue = true;
                    break;

                case VariableEditorGetEquation.EqualOrGreaterThan:
                    if ( checkFloat >= parsedFloat ) isTrue = true;
                    break;

                case VariableEditorGetEquation.EqualOrLessThan:
                    if ( checkFloat <= parsedFloat ) isTrue = true;
                    break;

                case VariableEditorGetEquation.GreaterThan:
                    if ( checkFloat > parsedFloat ) isTrue = true;
                    //Debug.Log ("[ConditionalPhase] " +_checkFloat+" > "+_parsedFloat+" = "+isTrue);
                    break;

                case VariableEditorGetEquation.LessThan:
                    if ( checkFloat < parsedFloat ) isTrue = true;
                    break;
            }

            return isTrue;
        }

        /// <summary>
        /// Checks the string.
        /// </summary>
        /// <returns><c>true</c> if the condition between the input string and the chosen variable is satisfied, <c>false</c> otherwise</returns>
        private bool CheckString()
        {
            bool isTrue = false;
            string parsedString = inputValue;
            string checkString = ( scope == VariableEditorScopes.Local ) ?
                _localVariables.strings[variableId] :
                DialoguerDataManager.GetGlobalString( variableId );

            switch ( equation )
            {
                case VariableEditorGetEquation.Equals:
                    if ( parsedString == checkString ) isTrue = true;
                    break;

                case VariableEditorGetEquation.NotEquals:
                    if ( parsedString != checkString ) isTrue = true;
                    break;
            }

            return isTrue;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return string.Format( "Set Variable Phase\nScope: {0}\nType: {1}\nVariable ID: {2}\nEquation: {3}\nGet Value: {4}\nTrue Out: {5}\nFalse Out: {6}", this.scope.ToString(), type.ToString(), variableId, equation.ToString(), inputValue, outs[0], outs[1] );
        }
    }
}
