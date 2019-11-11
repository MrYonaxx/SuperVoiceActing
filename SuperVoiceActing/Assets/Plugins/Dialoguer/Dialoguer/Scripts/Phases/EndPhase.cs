
namespace DialoguerCore
{
    /// <summary>
    /// The end phases represents the end of the dialogue
    /// </summary>
    /// <seealso cref="DialoguerCore.AbstractDialoguePhase" />
    public class EndPhase : AbstractDialoguePhase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPhase"/> class.
        /// </summary>
        public EndPhase() : base( null ){}

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return "End Phase";
        }
    }
}
