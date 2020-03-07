/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace VoiceActing
{

    public enum StoryEventNode
    {
        Text,
        Wait,
        Effect,
        MoveCharacter,
        Music,
        SceneLoader,
        LoadEvent,
        PlayerData,
        Choices,
        Variable,
        Condition,
        ConditionActor
    }

    [System.Serializable]
    public class StoryEventDataNode
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
        public StoryEventText storyEventText = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Wait)]
        [SerializeField]
        [HideLabel]
        public StoryEventWait storyEventWait = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Effect)]
        [SerializeField]
        [HideLabel]
        public StoryEventEffect storyEventEffect = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.MoveCharacter)]
        [SerializeField]
        [HideLabel]
        public StoryEventMoveCharacter storyEventMoveCharacter = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.SceneLoader)]
        [SerializeField]
        [HideLabel]
        public StoryEventSceneLoader storyEventSceneLoader = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.LoadEvent)]
        [SerializeField]
        [HideLabel]
        public StoryEventLoad storyEventLoadEvent = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Music)]
        [SerializeField]
        [HideLabel]
        public StoryEventMusic storyEventMusic = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.PlayerData)]
        [SerializeField]
        [HideLabel]
        public StoryEventPlayerData storyEventPlayerData = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Choices)]
        [SerializeField]
        [HideLabel]
        public StoryEventChoices storyEventChoices = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Variable)]
        [SerializeField]
        [HideLabel]
        public StoryEventVariable storyEventVariable = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.Condition)]
        [SerializeField]
        [HideLabel]
        public StoryEventConditions storyEventCondition = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", StoryEventNode.ConditionActor)]
        [SerializeField]
        [HideLabel]
        public StoryEventConditionActor storyEventConditionActor = null;




        public string GetBoxTitle()
        {
            switch (eventNode)
            {
                case StoryEventNode.Text:
                    return eventNode + " : " + storyEventText.Text;
                case StoryEventNode.Wait:
                    return eventNode + " : " + storyEventWait.Time.ToString();
                case StoryEventNode.Effect:
                    return eventNode + " : " + storyEventEffect.EventEffect.ToString();
                case StoryEventNode.MoveCharacter:
                    return eventNode + " : " + storyEventMoveCharacter.CharacterToMove.name;
                case StoryEventNode.PlayerData:
                    return eventNode.ToString();
                case StoryEventNode.Choices:
                    return eventNode.ToString();
                case StoryEventNode.Variable:
                    return eventNode.ToString() + " : [" + storyEventVariable.VariableName + "]";
                case StoryEventNode.Condition:
                    return eventNode.ToString() + " ------------------------------------------------------------------------------------------------------------------------";
                default:
                    return eventNode.ToString();
            }
        }
    }

    [System.Serializable]
    public class StoryEventDataBox
    {
        [FoldoutGroup("$name")]
        [SerializeField]
        [HideLabel]
        public StoryEventDataNode dataBox;

        private string name = " ";

        public void SetTitle()
        {
            name = dataBox.GetBoxTitle();
            if (name == null)
                name = " ";
        }

    }

    public interface IStoryEvent<T>
    {

        //void ExecuteEvent(StoryEventManager manager);
    }

    public class StoryEvent6 : IStoryEvent<StoryEvent6>
    {
        [HideInInspector]
        public StoryEventNode Type;

        public bool Equals(StoryEvent6 se6)
        {
            return true;
        }
    }


    [Serializable]
    public class StoryEventText6 : StoryEvent6
    {
        [BoxGroup("Yo !")]
        public string text;
        [BoxGroup("Yo !")]
        public int speed;

        public StoryEventText6()
        {
            Type = StoryEventNode.MoveCharacter;
            speed = 0;
            text = "";
        }
        public void ExecuteEvent(StoryEventManager manager)
        {

        }
    }

    /*[Serializable]
    public struct StoryEventText5 : IStoryEvent<StoryEventText6>
    {
        [HideInInspector]
        public StoryEventNode Type;
        public string text;

        public StoryEventText5(string a)
        {
            Type = StoryEventNode.Text;
            text = a;
        }
        public void ExecuteEvent(StoryEventManager manager)
        {

        }
    }*/

    [Serializable]
    public struct StoryValue : IEquatable<StoryValue>
    {
        //[HideInInspector]
        public StoryEventNode Type;

        [Range(-100, 100)]
        [LabelWidth(70)]
        [LabelText("$Type")]
        public float Value;

        public StoryValue(StoryEventNode type, float value)
        {
            this.Type = type;
            this.Value = value;
        }

        public StoryValue(StoryEventNode type)
        {
            this.Type = type;
            this.Value = 0;
        }

        public bool Equals(StoryValue other)
        {
            return this.Type == other.Type && this.Value == other.Value;
        }
    }

    /// <summary>
    /// Definition of the StoryEventData class
    /// </summary>
    [CreateAssetMenu(fileName = "StoryEventData", menuName = "StoryEvent/StoryEventData", order = 1)]
    public class StoryEventData : SerializedScriptableObject
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
        StoryEventDataBox[] eventNodes;

        [Space]
        [Title("Test2")]
        //[ValueDropdown("CustomAddStatsButton", IsUniqueList = true, DrawDropdownForListElements = false, DropdownTitle = "Story Event Node")]
        [ValueDropdown("FriendlyTextureSizes1", DrawDropdownForListElements = false)]
        [ListDrawerSettings(DraggableItems = true, Expanded = true)]
        [SerializeField]
        List<IEquatable<StoryValue>> eventNodes1;

        private IEnumerable FriendlyTextureSizes1 = new ValueDropdownList<StoryValue>()
        {
            {"Allo", new StoryValue() }
        };

        [Space]
        [Title("Test")]
        //[ValueDropdown("CustomAddStatsButton", IsUniqueList = true, DrawDropdownForListElements = false, DropdownTitle = "Story Event Node")]
        [ValueDropdown("FriendlyTextureSizes", DrawDropdownForListElements = false)]
        [ListDrawerSettings(DraggableItems = true, Expanded = true)]
        [SerializeField]
        List<StoryEvent6> eventNodes2;

        private IEnumerable FriendlyTextureSizes = new ValueDropdownList<StoryEvent6>()
        {
            {"Allo", new StoryEventText6() }
        };
        /*private static IEnumerable FriendlyTextureSizes = new ValueDropdownList<IStoryEvent>()
        {
            {"Text5", new StoryEventText5(StoryEventNode.Text) },
            {"Text6", new StoryEventText6(StoryEventNode.MoveCharacter) }
        };*/

        /*private IEnumerable CustomAddStatsButton()
        {
            return Enum.GetValues(typeof(StoryEventNode)).Cast<StoryEventNode>()
                //.Except(this.stats.Select(x => x.Type))
                .Select(x => new StatValue(x))
                .AppendWith(this.stats)
                .Select(x => new ValueDropdownItem(x.Type.ToString(), x));
        }*/

        public int GetEventSize()
        {
            return eventNodes.Length;
        }

        public StoryEvent GetEventNode(int index)
        {
            if (index == eventNodes.Length)
                return null;

            switch (eventNodes[index].dataBox.eventNode)
            {
                case StoryEventNode.Text:
                    return eventNodes[index].dataBox.storyEventText;
                case StoryEventNode.Wait:
                    return eventNodes[index].dataBox.storyEventWait;
                case StoryEventNode.Effect:
                    return eventNodes[index].dataBox.storyEventEffect;
                case StoryEventNode.MoveCharacter:
                    return eventNodes[index].dataBox.storyEventMoveCharacter;
                case StoryEventNode.SceneLoader:
                    return eventNodes[index].dataBox.storyEventSceneLoader;
                case StoryEventNode.LoadEvent:
                    return eventNodes[index].dataBox.storyEventLoadEvent;
                case StoryEventNode.Music:
                    return eventNodes[index].dataBox.storyEventMusic;
                case StoryEventNode.PlayerData:
                    return eventNodes[index].dataBox.storyEventPlayerData;
                case StoryEventNode.Choices:
                    return eventNodes[index].dataBox.storyEventChoices;
                case StoryEventNode.Variable:
                    return eventNodes[index].dataBox.storyEventVariable;
                case StoryEventNode.Condition:
                    return eventNodes[index].dataBox.storyEventCondition;
                case StoryEventNode.ConditionActor:
                    return eventNodes[index].dataBox.storyEventConditionActor;
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