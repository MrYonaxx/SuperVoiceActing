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

        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            if (loadScene != null)
                eventManager.ResultScreen.LoadNewScene(loadScene);
            /*else
            {
                eventManager.ResultScreen.ShowResultScreen(eventManager.)
                while(true)
                {
                    yield return null;
                }
            }*/
            yield break;
        }
		
	} // DoublageEventResultScreen class
	
}// #PROJECTNAME# namespace
