/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [CreateAssetMenu(fileName = "StoryEventDoublageDatabase", menuName = "Database/StoryEventDoublageDatabase", order = 1)]
    public class SessionStoryEventDatabase : ScriptableObject
    {

        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/")]
        [SerializeField]
        private List<DoublageEventData> doublageEventDatabase;
        public List<DoublageEventData> DoublageEventDatabase
        {
            get { return doublageEventDatabase; }
        }


        [Button]
        private void DebugFunction()
        {

        }

    }

} // #PROJECTNAME# namespace