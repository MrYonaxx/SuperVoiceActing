using UnityEngine;
using System.Collections;
using DialoguerEditor;
using DialoguerCore;

public delegate void DialoguerCallback();

public class Dialoguer
{
    /// <summary>
    /// Gets a value indicating whether this <see cref="Dialoguer"/> is ready.
    /// </summary>
    /// <value>
    ///   <c>true</c> if ready; otherwise, <c>false</c>.
    /// </value>
    public static bool Ready { get; private set; }

    /// <summary>
    /// Call this in order to initialize the Dialoguer system.
    /// </summary>
    public static void Initialize()
    {
        // Only initialize once
        if ( Ready )
            return;

        DialoguerDialogueManager.Initialize();

        // Initialize DialoguerDataManager
        DialoguerDataManager.Initialize();

        Ready = true;
    }
}