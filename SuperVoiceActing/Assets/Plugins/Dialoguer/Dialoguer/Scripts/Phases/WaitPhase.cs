using UnityEngine;
using System.Collections.Generic;
using DialoguerEditor;

namespace DialoguerCore
{
    /// <summary>
    /// Phase to make the flow wait for a specified amount of time / frames
    /// </summary>
    /// <seealso cref="DialoguerCore.AbstractDialoguePhase" />
    public class WaitPhase : AbstractDialoguePhase
    {
        /// <summary>
        /// The type of the wait (frames, seconds, ...)
        /// </summary>
        public readonly DialogueEditorWaitTypes type;

        /// <summary>
        /// The wait duration
        /// </summary>
        public readonly float duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitPhase"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="outs">The indices of the out phases.</param>
        public WaitPhase( DialogueEditorWaitTypes type, float duration, List<int> outs ) : base( outs )
        {
            this.type = type;
            this.duration = duration;
        }

        /// <summary>
        /// Called when the phase is started.
        /// </summary>
        protected override void onStart()
        {
            DialoguerEventManager.dispatchOnWaitStart();

            if ( type == DialogueEditorWaitTypes.Continue ) return;

            GameObject gameObject = new GameObject("Dialoguer WaitPhaseTimer");
            WaitPhaseComponent waitPhaseComponent = gameObject.AddComponent<WaitPhaseComponent>();

            waitPhaseComponent.Init( this, type, duration );
        }

        /// <summary>
        /// Called when the wait has completed.
        /// </summary>
        public void WaitComplete()
        {
            DialoguerEventManager.dispatchOnWaitComplete();
            state = PhaseState.Complete;
        }

        /// <summary>
        /// Selects the out phase.
        /// </summary>
        /// <param name="outId">The index of the out phase to select.</param>
        public override void SelectOutPhase( int outId )
        {
            if ( type != DialogueEditorWaitTypes.Continue ) return;
            DialoguerEventManager.dispatchOnWaitComplete();
            base.SelectOutPhase( outId );
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
            return string.Format( "Wait Phase\nType: {0}\nDuration: {1}", type.ToString(), this.duration );
        }
    }
}
