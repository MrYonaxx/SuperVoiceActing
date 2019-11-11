using System.Collections.Generic;

namespace DialoguerCore
{
    public class SendMessagePhase : AbstractDialoguePhase
    {
        /// <summary>
        /// The message
        /// </summary>
        public readonly string message;

        /// <summary>
        /// The metadata
        /// </summary>
        public readonly string metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessagePhase"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="metadata">The metadata.</param>
        /// <param name="outs">The indices of the out phases.</param>
        public SendMessagePhase( string message, string metadata, List<int> outs ) : base( outs )
        {
            this.message = message;
            this.metadata = metadata;
        }

        /// <summary>
        /// Called when the phase is started.
        /// </summary>
        protected override void onStart()
        {
            DialoguerEventManager.dispatchOnMessageEvent( message, metadata );
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
            return string.Format( "Send Message Phase\nMessage: {0}\nMetadata: {1}", message, metadata );
        }
    }
}
