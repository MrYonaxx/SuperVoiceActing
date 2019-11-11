using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace DialoguerCore
{
    /// <summary>
    /// Represent a save to the dialoguer state
    /// </summary>
    [System.Serializable]
    public struct DialoguerStateSave
    {
        /// <summary>
        /// Gets the dialogue identifier.
        /// </summary>
        /// <value>
        /// The dialogue identifier.
        /// </value>
        [SerializeField]
        private int dialogueID;

        public int DialogueID
        {
            get { return dialogueID; }
            private set { dialogueID = value; }
        }

        /// <summary>
        /// Gets the phase identifier.
        /// </summary>
        /// <value>
        /// The phase identifier.
        /// </value>
        [SerializeField]
        private int phaseID;

        public int PhaseID
        {
            get { return phaseID; }
            set { phaseID = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialoguerStateSave"/> struct.
        /// </summary>
        /// <param name="dialogueID">The dialogue identifier.</param>
        /// <param name="phaseID">The phase identifier.</param>
        public DialoguerStateSave( DialoguerDialogues dialogueID, int phaseID = -1 ) : this( (int) dialogueID, phaseID ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialoguerStateSave"/> struct.
        /// </summary>
        /// <param name="dialogueID">The dialogue identifier.</param>
        /// <param name="phaseID">The phase identifier.</param>
        public DialoguerStateSave( int dialogueID, int phaseID = -1 )
        {
            this.dialogueID = dialogueID;
            this.phaseID = phaseID;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format( string.Format( "DialogueID : {0} ; PhaseID : {1}", DialogueID, PhaseID ) );
        }
    }

    /// <summary>
    /// Manager of the dialogues
    /// </summary>
    public class DialoguerDialogueManager
    {
        /// <summary>
        /// Gets the current phase.
        /// </summary>
        /// <value>
        /// The current phase.
        /// </value>
        public static AbstractDialoguePhase Phase { get; private set; }

        /// <summary>
        /// Gets the current dialogue.
        /// </summary>
        /// <value>
        /// The current dialogue.
        /// </value>
        public static DialoguerDialogue Dialogue { get; private set; }

        /// <summary>
        /// Each phases the manager went through.
        /// </summary>
        private static Stack<DialoguerStateSave> history;

        /// <summary>
        /// The phases used to start a new dialogue
        /// </summary>
        private static List<DialoguerStateSave> startDialoguePhases;

        /// <summary>
        /// The index of the dialogue to resume when a sub-dialogue ends
        /// </summary>
        private static int lastDialogueIndex;

        /// <summary>
        /// The name of the event used to start a dialogue
        /// </summary>
        private const string startDialogueEventName = "startdialogue";

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize ()
        {
            history = new System.Collections.Generic.Stack<DialoguerStateSave>();
            startDialoguePhases = new System.Collections.Generic.List<DialoguerStateSave>();
            lastDialogueIndex = -1;
        }
        // Nam Nam Nam
        public static void ClearHistory()
        {
            history.Clear();
        }
        // Nam Nam 


        public static DialoguerStateSave[] History
        {
            get { return history.ToArray(); }
            set { history = new Stack<DialoguerStateSave>(value.Reverse());}
        }

        public static AbstractDialoguePhase LastPhase
        {
            get
            {
                if (history.Count >= 2)
                {
                    DialoguerStateSave currentStateSave = history.Pop();
                    DialoguerStateSave lastStateSave = history.Peek();
                    DialoguerDialogue dialogue = DialoguerDataManager.GetDialogueById(lastStateSave.DialogueID);
                    AbstractDialoguePhase lastPhase = dialogue.phases[lastStateSave.PhaseID];
                    history.Push(currentStateSave);
                    return lastPhase;
                }
                return null;
            }
        }

        /// <summary>
        /// Starts the dialogue.
        /// </summary>
        /// <param name="save">The save of the dialogue ton start.</param>
        public static void StartDialogue( DialoguerStateSave save )
        {
            StartDialogue( save.DialogueID, save.PhaseID );
        }

        /// <summary>
        /// Starts the dialogue.
        /// </summary>
        /// <param name="dialogueId">The dialogue identifier.</param>
        public static void StartDialogue( int dialogueId )
        {
            StartDialogue( dialogueId, -1 );
        }

        /// <summary>
        /// Starts the dialogue.
        /// </summary>
        /// <param name="dialogueId">The dialogue identifier.</param>
        /// <param name="phaseId">The phase identifier.</param>
        public static void StartDialogue( int dialogueId, int phaseId )
        {
            //Debug.LogError( "Starting dialogue " + dialogueId + " / " + phaseId );
            if ( Dialogue != null )
            {
                if ( Dialogue.ID != dialogueId )
                    startDialoguePhases.Add( new DialoguerStateSave( Dialogue.ID, Phase != null ? Phase.ID : Dialogue.startPhaseId ) );
                DialoguerEventManager.dispatchOnSuddenlyEnded( Dialogue.ID );
            }

            // Set References
            Dialogue = DialoguerDataManager.GetDialogueById( dialogueId );
            Dialogue.Reset();

            // Dispatch onStart event
            DialoguerEventManager.dispatchOnStarted( Dialogue.ID );

            if ( phaseId < 0 )
            {
                if ( Dialogue.startPhaseId < 0 )
                    throw new System.Exception( string.Format( "The start phase of the dialogue {0} ({1}) is not linked to any other phase.", Dialogue.name, Dialogue.ID ) );
                phaseId = Dialogue.startPhaseId;
            }
            
            SetupPhase( phaseId );
        }

        /// <summary>
        /// Resumes the dialogue.
        /// </summary>
        /// <param name="save">The save of the dialogue ton start.</param>
        private static void ResumeDialogue( DialoguerStateSave save )
        {
            ResumeDialogue( save.DialogueID, save.PhaseID );
        }

        /// <summary>
        /// Resumes the dialogue.
        /// </summary>
        /// <param name="dialogueId">The dialogue identifier.</param>
        /// <param name="phaseId">The phase identifier.</param>
        private static void ResumeDialogue( int dialogueId, int phaseId )
        {
            // Set References
            Dialogue = DialoguerDataManager.GetDialogueById( dialogueId );

            //Debug.Log( "Resuming dialogue " + Dialogue.name + " " + phaseId );

            if ( phaseId < 0 )
                phaseId = Dialogue.startPhaseId;

            // If we are resuming a "StartDialogue" phase, jump to the next phase instead
            if ( IsStartDialoguePhase( Dialogue.phases[phaseId] ) )
            {
                phaseId = Dialogue.phases[phaseId].GetNextPhaseID();
            }

            SetupPhase( phaseId );
        }

        /// <summary>
        /// Adds the current state to the history.
        /// </summary>
        private static void AddToHistory()
        {
            if ( Dialogue != null )
                AddToHistory( Dialogue.ID, Phase == null ? Dialogue.startPhaseId : Phase.ID );
        }

        /// <summary>
        /// Adds the given information into to the history.
        /// </summary>
        private static void AddToHistory( int dialogueID, int phaseID )
        {
            history.Push( new DialoguerStateSave( dialogueID, phaseID ) );

            //Debug.Log( "History : [" + string.Join( " ; ", history.Select( s => (s.DialogueID + "," + s.PhaseID) ).ToArray() ) + "]" );
        }

        /// <summary>
        /// Moves back to the direct previous phase.
        /// </summary>
        /// <param name="phasesCount">The number of phases to .</param>
        public static void MoveBack()
        {
            MoveBack( 1 );
        }

        /// <summary>
        /// Moves back to a previous phase.
        /// </summary>
        /// <param name="phasesCount">The number of phases to .</param>
        public static void MoveBack( int phasesCount )
        {
            DialoguerStateSave save;
            DialoguerDialogue dialogue = Dialogue;
            AbstractDialoguePhase phase = Phase;

            if ( history == null || history.Count <= 1 || phasesCount <= 0 || Phase == null )
                return;
            
            // Popping save objects the desired number of times or until the stack only contains 1 element
            do
            {
                save = history.Pop();

                // Checking if popping another dialogue
                if ( dialogue.ID != save.DialogueID )
                {
                    dialogue = DialoguerDataManager.GetDialogueById( save.DialogueID );
                    phase = dialogue.phases[save.PhaseID];

                    //Debug.Log( "Popping phase #" + save.PhaseID + " : " + phase.GetType().ToString() );

                    if ( phase is EndPhase )
                        lastDialogueIndex--;

                    // Make sure to select a Text or BranchedText phase
                    if ( !(phase is TextPhase) && !(phase is BranchedTextPhase)  )
                    {
                        phasesCount++;
                    }

                }
                phasesCount--;
            }
            while ( phasesCount >= 0 && history.Count > 0 );
            
            Dialogue = DialoguerDataManager.GetDialogueById( save.DialogueID );
            SetupPhase( save.PhaseID );
        }

        /// <summary>
        /// Indicates whether the given phase starts a new dialogue
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        private static bool IsStartDialoguePhase( AbstractDialoguePhase phase )
        {
            SendMessagePhase sendMessagePhase = phase as SendMessagePhase;

            return ( sendMessagePhase != null && sendMessagePhase.message.Equals( startDialogueEventName, System.StringComparison.OrdinalIgnoreCase ) );
        }

        /// <summary>
        /// Continues to the default next phase.
        /// </summary>
        public static void Continue()
        {
            Continue( 0 );
        }

        /// <summary>
        /// Continues to the next phase with the given index.
        /// </summary>
        /// <param name="outIndex">The out identifier.</param>
        public static void Continue( int outIndex )
        {
            // Continue Dialogues
            if ( Phase != null )
                Phase.SelectOutPhase( outIndex );
        }

        /// <summary>
        /// Jumps to the specified save point.
        /// </summary>
        /// <param name="save">The save point ot jump to.</param>
        public static void JumpTo( DialoguerStateSave save )
        {
            JumpTo( save.DialogueID, save.PhaseID );
        }

        /// <summary>
        /// Jumps to the first phase of the given dialogue.
        /// </summary>
        /// <param name="dialogueID">The dialogue identifier.</param>
        public static void JumpTo( int dialogueID )
        {
            JumpTo( dialogueID, 0 );
        }

        /// <summary>
        /// Jumps to the given dialogue, to the given phase ID.
        /// </summary>
        /// <param name="dialogueID">The dialogue identifier.</param>
        /// <param name="phaseID">The phase identifier.</param>
        public static void JumpTo( int dialogueID, int phaseID )
        {
            Phase.OnPhaseComplete -= OnPhaseComplete;
            Phase.Terminate();

            if ( dialogueID != Dialogue.ID )
                StartDialogue( dialogueID, phaseID );
            else
                SetupPhase( phaseID );
        }

        /// <summary>
        /// Ends the dialogue.
        /// </summary>
        public static void EndDialogue()
        {
            // Dispatch onEnd event
            DialoguerEventManager.dispatchOnWindowClose();

            // Dispatch onEnd event
            DialoguerEventManager.dispatchOnEnded( Dialogue.ID );

            DialoguerStateSave[] historyArray = history.ToArray();

            //Debug.Log(string.Join("-", historyArray.Select(save => save.DialogueID + "." + save.PhaseID).ToArray()));
            DialoguerStateSave parentDialogue = GetParentDialogue(historyArray);
            DialoguerDialogue savedDialogue  = null;
            AbstractDialoguePhase savedPhase = null;
            AbstractDialoguePhase nextPhase  = null;
            bool initialEndPhaseReached      = true;
            bool remainingDialogueAvailable  = parentDialogue.DialogueID >= 0;
            
            if ( remainingDialogueAvailable )
            {
                savedDialogue = DialoguerDataManager.GetDialogueById( parentDialogue.DialogueID );
                savedPhase = savedDialogue.phases[parentDialogue.PhaseID];
                nextPhase = savedDialogue.phases[savedPhase.GetNextPhaseID()];
                initialEndPhaseReached = history.Count > 0
                    && savedDialogue.ID == historyArray[historyArray.Length - 1].DialogueID
                    && nextPhase is EndPhase;
            }

            if ( initialEndPhaseReached || !remainingDialogueAvailable )
            {
                if ( initialEndPhaseReached )
                    DialoguerEventManager.dispatchOnEnded( savedDialogue != null ? savedDialogue.ID : Dialogue.ID );

                // Reset current dialogue
                Dialogue.Reset();

                // Clean up
                Reset();
            }
            else if ( remainingDialogueAvailable )
            {
                ResumeDialogue( parentDialogue.DialogueID, savedPhase.ID );
            }
        }

        /// <summary>
        /// Gets the parent dialogue of the current dialogue.
        /// </summary>
        /// <param name="startDialoguePhasesArray">The array containing the phases that ahs started new dialogues.</param>
        /// <returns></returns>
        private static DialoguerStateSave GetParentDialogue( DialoguerStateSave[] startDialoguePhasesArray )
        {
            DialoguerStateSave parentDialogue = new DialoguerStateSave(-1);

            for ( int i = 1 ; i < startDialoguePhasesArray.Length ; ++i )
            {
                DialoguerDialogue dialogue = DialoguerDataManager.GetDialogueById( startDialoguePhasesArray[i].DialogueID );
                AbstractDialoguePhase phase = dialogue.phases[startDialoguePhasesArray[i].PhaseID];
                if ( phase is SendMessagePhase && ( phase as SendMessagePhase ).message.Equals( startDialogueEventName, System.StringComparison.OrdinalIgnoreCase ) && GetNextDialogueID( phase as SendMessagePhase ) == Dialogue.ID )
                    return startDialoguePhasesArray[i];
            }

            return parentDialogue;
        }

        /// <summary>
        /// Sets up the phase.
        /// </summary>
        /// <param name="nextPhaseId">The next phase identifier.</param>
        /// <exception cref="System.ArgumentException"><c>nextPhaseId</c> is less than 0.</exception>
        private static void SetupPhase( int nextPhaseId )
        {
            if ( nextPhaseId < 0 )
                throw new System.ArgumentException( string.Format( "The given phase identifier is not valid: {0}", nextPhaseId ) );

            if ( Dialogue == null ) return;

            AbstractDialoguePhase phase = Dialogue.phases[nextPhaseId];
            phase.ID = nextPhaseId;

            //Debug.LogError( "Setting up phase " + phase.ID + " (" + phase.GetType().ToString() + ") of dialogue " + Dialogue.name + "\n" + currentDialogueIndex );

            if ( Phase != null ) Phase.resetEvents();

            AddToHistory( Dialogue.ID, nextPhaseId );

            Phase = phase;

            // Directly end the dialogue and eventually, retrieve an ongoing parent dialogue
            if ( phase is EndPhase )
            {
                EndDialogue();
                return;
            }
            // Start a new dialogue if the phase is a SendMessagePhase with the `StartDialogue` instruction
            else if ( IsStartDialoguePhase( phase ) )
            {
                try
                {
                    //Debug.LogFormat( "Start Dialogue #{0}, phase {1} ", GetNextDialogueID( phase as SendMessagePhase ), phase.ID );
                    StartDialogue( GetNextDialogueID( phase as SendMessagePhase ) );
                }
                catch( System.ArgumentException e )
                {
                    Debug.LogErrorFormat( "Either the dialogue {0} does not exist in the list of dialogues, or the Start phase is not linked to any other phase.\nMake sure you have generated the Dialogues enums from the `dialoguer_data_object`", ( phase as SendMessagePhase ).metadata );
                }
                return;
            }

            phase.OnPhaseComplete += OnPhaseComplete;

            if ( phase is TextPhase )
                DialoguerEventManager.dispatchOnTextPhase( ( phase as TextPhase ).data );
            else if ( phase is BranchedTextPhase )
                DialoguerEventManager.dispatchOnTextPhase( ( phase as BranchedTextPhase ).data );

            phase.Start( Dialogue.localVariables );
        }

        private static int GetNextDialogueID( SendMessagePhase sendMessagePhase )
        {
            int nextDialogueID = (int) (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), sendMessagePhase.metadata );
            return nextDialogueID;
        }

        /// <summary>
        /// Called when the current phase has been completed.
        /// </summary>
        /// <param name="nextPhaseId">The next phase identifier.</param>
        private static void OnPhaseComplete( int nextPhaseId )
        {
            SetupPhase( nextPhaseId );
        }

        /// <summary>
        /// Determines whether the specified phase is windowed.
        /// </summary>
        /// 
        /// <param name="phase">The phase.</param>
        /// <returns>
        ///   <c>true</c> if the specified phase is windowed; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsWindowed( AbstractDialoguePhase phase )
        {
            if ( phase is TextPhase || phase is BranchedTextPhase )
            {
                Debug.Log( "Phase is: " + phase.GetType().ToString() );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        private static void Reset()
        {
            Phase = null;
            Dialogue = null;
        }
    }
}
