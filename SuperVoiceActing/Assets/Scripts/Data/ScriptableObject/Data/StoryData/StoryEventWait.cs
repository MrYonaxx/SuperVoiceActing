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