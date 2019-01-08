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

        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

    } // StoryEventSceneLoader class

} // #PROJECTNAME# namespace