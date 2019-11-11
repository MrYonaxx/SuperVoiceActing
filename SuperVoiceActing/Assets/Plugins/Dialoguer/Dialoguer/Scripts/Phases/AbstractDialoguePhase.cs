using System.Collections.Generic;


namespace DialoguerCore
{
    /// <summary>
    /// The abstract class all the phases derive from
    /// </summary>
    public abstract class AbstractDialoguePhase
    {
        /// <summary>
        /// The indices of the out phases
        /// </summary>
        public readonly int[] outs;

        /// <summary>
        /// The index of the next phase
        /// </summary>
        protected int nextPhaseId;

        /// <summary>
        /// The local variables
        /// </summary>
        protected DialoguerVariables _localVariables;

        /// <summary>
        /// Occurs when the phase is completed
        /// </summary>
        public event System.Action<int> OnPhaseComplete;

        /// <summary>
        /// The state
        /// </summary>
        private PhaseState _state;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public PhaseState state
        {
            get
            {
                return _state;
            }
            protected set
            {
                _state = value;
                switch ( _state )
                {
                    case PhaseState.Inactive:
                        // Do Nothing
                        break;

                    case PhaseState.Start:
                        onStart();
                        break;

                    case PhaseState.Action:
                        onAction();
                        break;

                    case PhaseState.Complete:
                        onComplete();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDialoguePhase"/> class.
        /// </summary>
        /// <param name="outs">The indices of the out phases.</param>
        public AbstractDialoguePhase( List<int> outs )
        {
            if ( outs != null )
            {
                int[] outsClone = outs.ToArray();
                this.outs = outsClone.Clone() as int[];
            }
        }

        /// <summary>
        /// Starts and assign the given local variables.
        /// </summary>
        /// <param name="localVars">The local vars.</param>
        public void Start( DialoguerVariables localVars )
        {
            Reset();
            _localVariables = localVars;
            state = PhaseState.Start;
        }

        /// <summary>
        /// Terminates the phase
        /// </summary>
        public void Terminate()
        {
            state = PhaseState.Complete;
        }

        /// <summary>
        /// Continues to the next phase with the given index
        /// </summary>
        /// <param name="outIndex">Index of the out.</param>
        virtual public void SelectOutPhase( int outIndex )
        {
            int nextId = GetNextPhaseID( outIndex );

            if ( nextId >= 0 )
            {
                nextId = outs[outIndex];
            }
            else
            {
                //Debug.LogWarning( "Invalid Out Id" );
            }

            nextPhaseId = nextId;
        }

        /// <summary>
        /// Gets the next phase identifier.
        /// </summary>
        /// <param name="outIndex">Index of the out phase.</param>
        /// <returns></returns>
        public int GetNextPhaseID( int outIndex = 0 )
        {
            int phaseID = 0;

            if ( outs != null && outIndex < outs.Length && outIndex >= 0 )
                phaseID = outs[outIndex];

            return phaseID;
        }

        /// <summary>
        /// Called when the phase is started.
        /// </summary>
        virtual protected void onStart()
        {
            state = PhaseState.Action;
        }

        /// <summary>
        /// Called when the phase enters in action.
        /// </summary>
        virtual protected void onAction()
        {
            state = PhaseState.Complete;
        }

        /// <summary>
        /// Called when the phase completes
        /// </summary>
        virtual protected void onComplete()
        {
            dispatchPhaseComplete( nextPhaseId );
            state = PhaseState.Inactive;
            Reset();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        virtual protected void Reset()
        {
            nextPhaseId = ( outs != null && outs[0] >= 0 ) ? outs[0] : 0;
            _localVariables = null;
        }

        /// <summary>
        /// Dispatches the phase complete event.
        /// </summary>
        /// <param name="nextPhaseId">The identifier of the next phase.</param>
        private void dispatchPhaseComplete( int nextPhaseId )
        {
            if ( OnPhaseComplete != null ) OnPhaseComplete( nextPhaseId );
        }

        /// <summary>
        /// Resets the events.
        /// </summary>
        public void resetEvents()
        {
            OnPhaseComplete = null;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return GetType().ToString();
        }

    }
}