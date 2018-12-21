/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{

    [System.Serializable]
    public class StoryEventWait : StoryEvent
    {
        [SerializeField]
        float time = 60;

        protected override IEnumerator StoryEventCoroutine()
        {
            float actualTime = time;
            while (actualTime != 0)
            {
                yield return null;
                actualTime -= 1;
            }
        }

    } // StoryEventText class


} // #PROJECTNAME# namespace