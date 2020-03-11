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
    public class StoryEventDataNode2 : StoryEventData
    {
        //[Header("====================================================================================================================================")]
        [HorizontalGroup("Hey", Width = 0.2f)]
        [HideLabel]
        public StoryEventNode eventNode;

        //[FoldoutGroup("$eventNode")]
        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Text)]
        [SerializeField]
        [HideLabel]
        public StoryEventText[] storyEventText = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Wait)]
        [SerializeField]
        [HideLabel]
        public StoryEventWait[] storyEventWait = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Effect)]
        [SerializeField]
        [HideLabel]
        public StoryEventEffect[] storyEventEffect = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.MoveCharacter)]
        [SerializeField]
        [HideLabel]
        public StoryEventMoveCharacter[] storyEventMoveCharacter = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.SceneLoader)]
        [SerializeField]
        [HideLabel]
        public StoryEventSceneLoader[] storyEventSceneLoader = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.LoadEvent)]
        [SerializeField]
        [HideLabel]
        public StoryEventLoad[] storyEventLoadEvent = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Music)]
        [SerializeField]
        [HideLabel]
        public StoryEventMusic[] storyEventMusic = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.PlayerData)]
        [SerializeField]
        [HideLabel]
        public StoryEventPlayerData[] storyEventPlayerData = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Choices)]
        [SerializeField]
        [HideLabel]
        public StoryEventChoices[] storyEventChoices = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Variable)]
        [SerializeField]
        [HideLabel]
        public StoryEventVariable[] storyEventVariable = null;





        public string GetBoxTitle()
        {
            switch (eventNode)
            {
                case StoryEventNode.Text:
                    return eventNode + " : " + storyEventText[0].Text;
                case StoryEventNode.Wait:
                    return eventNode + " : " + storyEventWait[0].Time.ToString();
                case StoryEventNode.Effect:
                    return eventNode + " : " + storyEventEffect[0].EventEffect.ToString();
                case StoryEventNode.MoveCharacter:
                    return eventNode + " : " + storyEventMoveCharacter[0].CharacterMoving.name;
                case StoryEventNode.PlayerData:
                    return eventNode.ToString();
                case StoryEventNode.Choices:
                    return eventNode.ToString();
                case StoryEventNode.Variable:
                    return eventNode.ToString() + " : [" + storyEventVariable[0].VariableName + "]";
                default:
                    return eventNode.ToString();
            }
        }
    }

    [System.Serializable]
    public class StoryEventDataBox2
    {
        [FoldoutGroup("$name")]
        [SerializeField]
        [HideLabel]
        public StoryEventDataNode2 dataBox;

        private string name = " ";

        public void SetTitle()
        {
            name = dataBox.GetBoxTitle();
            if (name == null)
                name = " ";
        }

    }

    /// <summary>
    /// Definition of the StoryEventData class
    /// </summary>
    [CreateAssetMenu(fileName = "StoryEventData", menuName = "StoryEvent/StoryEventData2", order = 1)]
    public class StoryEventData2 : ScriptableObject
    {
        [Title("Scene Info")]
        [SerializeField]
        StoryBackgroundData background;
        public StoryBackgroundData Background
        {
            get { return background; }
        }


        [SerializeField]
        StoryEventMoveCharacter[] characters;
        public StoryEventMoveCharacter[] Characters
        {
            get { return characters; }
        }


        [Space]
        [Title("Event")]
        [SerializeField]
        StoryEventDataBox2[] eventNodes;



        public StoryEvent GetEventNode(int index)
        {
            if (index == eventNodes.Length)
                return null;

            switch (eventNodes[index].dataBox.eventNode)
            {
                case StoryEventNode.Text:
                    return eventNodes[index].dataBox.storyEventText[0];
                case StoryEventNode.Wait:
                    return eventNodes[index].dataBox.storyEventWait[0];
                case StoryEventNode.Effect:
                    return eventNodes[index].dataBox.storyEventEffect[0];
                case StoryEventNode.MoveCharacter:
                    return eventNodes[index].dataBox.storyEventMoveCharacter[0];
                case StoryEventNode.SceneLoader:
                    return eventNodes[index].dataBox.storyEventSceneLoader[0];
                case StoryEventNode.LoadEvent:
                    return eventNodes[index].dataBox.storyEventLoadEvent[0];
                case StoryEventNode.Music:
                    return eventNodes[index].dataBox.storyEventMusic[0];
                case StoryEventNode.PlayerData:
                    return eventNodes[index].dataBox.storyEventPlayerData[0];
                case StoryEventNode.Choices:
                    return eventNodes[index].dataBox.storyEventChoices[0];
                case StoryEventNode.Variable:
                    return eventNodes[index].dataBox.storyEventVariable[0];
            }
            return null;
        }

        [Button]
        private void SetTitles()
        {
            for (int i = 0; i < eventNodes.Length; i++)
            {
                eventNodes[i].SetTitle();
            }
        }


    } // StoryEventData class

} // #PROJECTNAME# namespace
