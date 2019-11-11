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
    [CreateAssetMenu(fileName = "Condition", menuName = "StoryEvent/Condition/Condition", order = 1)]
    public class RandomEventCondition : ScriptableObject
	{

        public virtual bool CheckCondition(PlayerData playerData)
        {
            return false;
        }
		
	} // RandomEventCondition class
	
}// #PROJECTNAME# namespace
