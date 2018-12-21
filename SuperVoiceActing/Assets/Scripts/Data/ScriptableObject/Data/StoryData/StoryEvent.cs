/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the StoryEvent class
    /// </summary>
    /// 
    [System.Serializable]
    public class StoryEvent
    {

        public IEnumerator GetStoryEvent()
        {
            return StoryEventCoroutine();
        }

        protected virtual IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }

    } // StoryEvent class

} // #PROJECTNAME# namespace