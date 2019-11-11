using UnityEngine;
using DialoguerCore;
using DialoguerEditor;

/// <summary>
/// Component used by the <see cref="WaitPhase"/> class to wait before starting the next phase
/// </summary>
public class WaitPhaseComponent : MonoBehaviour
{
    /// <summary>
    /// The type
    /// </summary>
    public DialogueEditorWaitTypes type;

    /// <summary>
    /// The phase that created this instance
    /// </summary>
    public WaitPhase phase;

    /// <summary>
    /// Indicates whether the wait must run
    /// </summary>
    public bool go = false;

    /// <summary>
    /// The wait duration
    /// </summary>
    public float duration = 0;

    /// <summary>
    /// The elapsed time
    /// </summary>
    public float elapsed = 0;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    /// <param name="phase">The phase.</param>
    /// <param name="type">The type.</param>
    /// <param name="duration">The duration.</param>
    public void Init( WaitPhase phase, DialogueEditorWaitTypes type, float duration )
    {
        this.phase = phase;
        this.type = type;
        this.duration = duration;
        elapsed = 0;
        go = true;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if ( !go ) return;

        float deltaTime = Time.deltaTime;

        switch ( type )
        {
            case DialogueEditorWaitTypes.Seconds:
                elapsed += deltaTime;
                break;

            case DialogueEditorWaitTypes.Frames:
                elapsed += 1;
                break;
        }
        if ( elapsed >= duration ) WaitComplete();
    }

    /// <summary>
    /// Called when the wait has completed
    /// </summary>
    private void WaitComplete()
    {
        go = false;
        phase.WaitComplete();
        phase = null;
        Destroy( gameObject );
    }
}