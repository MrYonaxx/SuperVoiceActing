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
        MoveCharacter,
        SceneLoader,
        LoadEvent,
        Music
    }

    [System.Serializable]
    public class StoryEventDataNode
    {
        //[Header("====================================================================================================================================")]
        [FoldoutGroup("$eventNode")]
        [HorizontalGroup("Hey")]
        [HideLabel]
        public StoryEventNode eventNode;

        //[FoldoutGroup("$eventNode")]
        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Text)]
        [SerializeField]
        [HideLabel]
        public StoryEventText storyEventText = null;

        [FoldoutGroup("$eventNode")]
        [ShowIf("eventNode", StoryEventNode.Wait)]
        [SerializeField]
        public StoryEventWait storyEventWait = null;

        [FoldoutGroup("$eventNode")]
        [ShowIf("eventNode", StoryEventNode.MoveCharacter)]
        [SerializeField]
        public StoryEventMoveCharacter storyEventMoveCharacter = null;

        [FoldoutGroup("$eventNode")]
        [ShowIf("eventNode", StoryEventNode.SceneLoader)]
        [SerializeField]
        public StoryEventSceneLoader storyEventSceneLoader = null;

        [FoldoutGroup("$eventNode")]
        [ShowIf("eventNode", StoryEventNode.LoadEvent)]
        [SerializeField]
        public StoryEventLoad storyEventLoadEvent = null;

        [FoldoutGroup("$eventNode")]
        [ShowIf("eventNode", StoryEventNode.Music)]
        [SerializeField]
        public StoryEventMusic storyEventMusic = null;

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
                case StoryEventNode.SceneLoader:
                    return eventNodes[index].storyEventSceneLoader;
                case StoryEventNode.LoadEvent:
                    return eventNodes[index].storyEventLoadEvent;
                case StoryEventNode.Music:
                    return eventNodes[index].storyEventMusic;
            }
            return null;
        }


    } // StoryEventData class

} // #PROJECTNAME# namespace