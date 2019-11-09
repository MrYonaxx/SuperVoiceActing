/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{


    [System.Serializable]
    public class StoryEventCustomScript : ScriptableObject
    {

        public virtual void ApplyCustomScript(PlayerData playerData)
        {
        }

    }
    [System.Serializable]
	public class StoryEventVariableScript : ScriptableObject
	{

        public virtual StoryVariable CreateStoryVariable(string variableName, PlayerData playerData)
        {
            return new StoryVariable();
        }

    } // StoryEventVariableScript class
	
}// #PROJECTNAME# namespace
