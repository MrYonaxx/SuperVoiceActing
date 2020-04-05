/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the StoryEventSceneLoader class
    /// </summary>
    /// 
    [System.Serializable]
    public class StoryEventSceneLoader : StoryEvent
    {
        [SerializeField]
        string sceneName = "";

        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

    } // StoryEventSceneLoader class

} // #PROJECTNAME# namespace