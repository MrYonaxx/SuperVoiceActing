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
    public abstract class StoryEvent
    {
        public virtual IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            yield break;
        }

        public virtual bool InstantNodeCoroutine()
        {
            return false;
        }

        protected virtual IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }

    } // StoryEvent class

} // #PROJECTNAME# namespace