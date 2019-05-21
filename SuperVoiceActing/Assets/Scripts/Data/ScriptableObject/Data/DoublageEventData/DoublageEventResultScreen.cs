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
	public class DoublageEventResultScreen : DoublageEvent
    {
        [SerializeField]
        private string loadScene;
        public string LoadScene
        {
            get { return loadScene; }
        }
		
	} // DoublageEventResultScreen class
	
}// #PROJECTNAME# namespace
