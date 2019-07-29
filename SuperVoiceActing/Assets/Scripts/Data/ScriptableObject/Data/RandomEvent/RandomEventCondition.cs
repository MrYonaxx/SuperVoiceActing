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
	public class RandomEventCondition : ScriptableObject
	{

        public virtual bool CheckCondition(PlayerData playerData)
        {
            return false;
        }
		
	} // RandomEventCondition class
	
}// #PROJECTNAME# namespace
