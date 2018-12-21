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

    public enum StoryEventNode
    {
        Text,
        Wait,
        MoveCharacter
    }

    [System.Serializable]
    public class StoryEventDataNode
    {
        [Header("====================================================================================================================================")]
        public StoryEventNode eventNode;

        [ShowIf("eventNode", StoryEventNode.Text)]
        [SerializeField]
        public StoryEventText storyEventText = null;

        [ShowIf("eventNode", StoryEventNode.Wait)]
        [SerializeField]
        public StoryEventWait storyEventWait = null;

        [ShowIf("eventNode", StoryEventNode.MoveCharacter)]
        [SerializeField]
        public StoryEventMoveCharacter storyEventMoveCharacter = null;

    }


    /// <summary>
    /// Definition of the StoryEventData class
    /// </summary>
    [CreateAssetMenu(fileName = "StoryEventData", menuName = "StoryEvent", order = 1)]
    public class StoryEventData : ScriptableObject
    {
        [SerializeField]
        StoryEventDataNode[] eventNodes;

        public StoryEvent GetEventNode(int index)
        {
            if (index == eventNodes.Length)
                return null;

            switch (eventNodes[index].eventNode)
            {
                case StoryEventNode.Text:
                    return eventNodes[index].storyEventText;
                case StoryEventNode.Wait:
                    return eventNodes[index].storyEventWait;
                case StoryEventNode.MoveCharacter:
                    return eventNodes[index].storyEventMoveCharacter;
            }
            return null;
        }


    } // StoryEventData class

} // #PROJECTNAME# namespace