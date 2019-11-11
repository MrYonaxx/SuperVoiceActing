using UnityEngine;

namespace DialoguerCore
{
    public class EmptyPhase : AbstractDialoguePhase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyPhase"/> class.
        /// </summary>
        public EmptyPhase() : base( null )
        {
            Debug.LogWarning( "Something went wrong, phase is EmptyPhase" );
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return "Empty Phase\nEmpty Phases should not be generated, something went wrong.\n";
        }
    }
}
