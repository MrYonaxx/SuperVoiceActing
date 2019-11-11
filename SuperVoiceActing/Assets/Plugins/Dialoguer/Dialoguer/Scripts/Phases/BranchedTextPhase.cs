using UnityEngine;
using System.Collections.Generic;

namespace DialoguerCore
{
    /// <summary>
    /// Phase to branch the flow according to a set of choices
    /// </summary>
    /// <seealso cref="DialoguerCore.TextPhase" />
    public class BranchedTextPhase : TextPhase
    {
        /// <summary>
        /// The choices
        /// </summary>
        public readonly List<string> choices;

        /// <summary>
        /// Initializes a new instance of the <see cref="BranchedTextPhase"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="choices">The choices.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="newWindow">if set to <c>true</c> [new window].</param>
        /// <param name="name">The name.</param>
        /// <param name="portrait">The portrait.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="audio">The audio.</param>
        /// <param name="audioDelay">The audio delay.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="outs">The indices of the out phases.</param>
        /// <param name="dialogueID">The dialogue identifier.</param>
        /// <param name="nodeID">The node identifier.</param>
        public BranchedTextPhase( string text, List<string> choices, string themeName, bool newWindow, string name, string portrait, string metadata, string audio, float audioDelay, Rect rect, List<int> outs, int dialogueID, int nodeID ) : base( text, themeName, newWindow, name, portrait, metadata, audio, audioDelay, rect, outs, choices, dialogueID, nodeID )
        {
            this.choices = choices;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            System.Text.StringBuilder choicesString = new System.Text.StringBuilder( string.Format( "Branched Text Phase {0}\n", data.ToString() ), choices.Count * 20 );
            for ( int choiceIndex = 0 ; choiceIndex < choices.Count ; ++choiceIndex )
            {
                choicesString.AppendFormat( "{0}: {1} : Out {2}\n", choiceIndex, choices[choiceIndex], outs[choiceIndex] );
            }
            return choicesString.ToString();
        }
    }
}
