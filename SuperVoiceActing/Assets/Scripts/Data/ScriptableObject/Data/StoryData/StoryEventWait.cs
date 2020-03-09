/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{

    [System.Serializable]
    public class StoryEventWait : StoryEvent
    {
        [SerializeField]
        [HideLabel]
        float time = 60;
        public float Time
        {
            get { return time; }
        }


        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {

            yield return new WaitForSeconds(time / 60f);
        }

    } // StoryEventText class


} // #PROJECTNAME# namespace