/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{

    public enum ScriptCustom
    {
        SwitchActor,
    }

	public class StoryEventScript : StoryEvent
	{

        [SerializeField]
        [HideLabel]
        ScriptCustom scriptCustom;

        [ShowIf("scriptCustom", ScriptCustom.SwitchActor)]
        [SerializeField]
        [HideLabel]
        public StoryEventVariable storyEventVariable = null;


    } // StoryEventScript class
	
}// #PROJECTNAME# namespace
