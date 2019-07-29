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
    /// <summary>
    /// Definition of the StoryEventLoad class
    /// </summary>
    [System.Serializable]
    public class StoryEventLoad : StoryEvent
    {
        [HideLabel]
        [HorizontalGroup]
        [SerializeField]
        StoryEventData DataToLoad;
        public StoryEventData GetDataToLoad()
        {
            return DataToLoad;
        }

        [HorizontalGroup(LabelWidth = 100)]
        [SerializeField]
        bool saveSceneConfiguration;
        public bool SaveSceneConfiguration()
        {
            return saveSceneConfiguration;
        }

        [HideLabel]
        [SerializeField]
        DoublageEventData DoublageEventToLoad;
        public DoublageEventData GetDoublageEventToLoad()
        {
            return DoublageEventToLoad;
        }


    } // StoryEventLoad class

} // #PROJECTNAME# namespace