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
    /// <summary>
    /// Definition of the StoryEventLoad class
    /// </summary>
    [System.Serializable]
    public class StoryEventLoad : StoryEvent
    {

        [SerializeField]
        StoryEventData DataToLoad;

        public StoryEventData GetDataToLoad()
        {
            return DataToLoad;
        }


    } // StoryEventLoad class

} // #PROJECTNAME# namespace