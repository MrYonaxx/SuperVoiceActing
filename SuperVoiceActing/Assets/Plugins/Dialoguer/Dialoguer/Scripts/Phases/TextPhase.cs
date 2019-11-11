using UnityEngine;
using System.Collections.Generic;

namespace DialoguerCore
{
    /// <summary>
    /// Phase to handle a simple text
    /// </summary>
    /// <seealso cref="DialoguerCore.AbstractDialoguePhase" />
    public class TextPhase : AbstractDialoguePhase
    {
        /// <summary>
        /// The data
        /// </summary>
        public readonly DialoguerTextData data;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPhase"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="newWindow">if set to <c>true</c> [new window].</param>
        /// <param name="name">The name.</param>
        /// <param name="portrait">The portrait.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="audio">The audio.</param>
        /// <param name="audioDelay">The audio delay.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="outs">The indices of the out phases.</param>
        /// <param name="choices">The choices.</param>
        /// <param name="dialogueID">The dialogue identifier.</param>
        /// <param name="nodeID">The node identifier.</param>
        public TextPhase( string text, string themeName, bool newWindow, string name, string portrait, string metadata, string audio, float audioDelay, Rect rect, List<int> outs, List<string> choices, int dialogueID, int nodeID ) : base( outs )
        {
            data = new DialoguerTextData( text, themeName, newWindow, name, portrait, metadata, audio, audioDelay, rect, choices, dialogueID, nodeID );
        }

        /// <summary>
        /// Called when the phase is started.
        /// </summary>
        protected override void onStart()
        {
            // Override and do nothing, wait for user to "continue" dialogue
        }

        /// <summary>
        /// Selects the out phase.
        /// </summary>
        /// <param name="nextPhaseId">The next phase identifier.</param>
        public override void SelectOutPhase( int nextPhaseId )
        {
            if ( data.newWindow )
            {
                DialoguerEventManager.dispatchOnWindowClose();
            }
            base.SelectOutPhase( nextPhaseId );
            Terminate();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return string.Format( "Text Phase\n{0}\nOut: {1}", data.ToString(), outs[0] );
        }
    }
}
