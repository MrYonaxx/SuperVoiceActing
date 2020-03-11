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
        Custom
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
        [ShowIf("eventNode", StoryEventNode.Custom)]
        [SerializeField]
        [HideLabel]
        public StoryEventCustom storyEventCustom = null;



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
                    return eventNode + " : " + storyEventMoveCharacter.CharacterMoving.name;
                case StoryEventNode.PlayerData:
                    return eventNode.ToString();
                case StoryEventNode.Choices:
                    return eventNode.ToString();
                case StoryEventNode.Variable:
                    return eventNode.ToString() + " : " + storyEventVariable.VariableName;
                case StoryEventNode.Condition:
                    return eventNode.ToString() + " ------------------------------------------------------------------------------------------------------------------------";
                default:
                    return eventNode.ToString();
            }
        }
    }

    // Class requise pour le FoldoutGroup
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




    public class StoryEventDataTest
    {
    }

    public class StoryEventDataTest2 : StoryEventDataTest
    {
        public string a;
        public StoryEventDataTest2()
        {
            a = "a";
        }
    }
    public class StoryEventDataTest3 : StoryEventDataTest
    {
        public string b;
        public StoryEventDataTest3()
        {
            b = "a";
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
        bool createNewScene = true;
        public bool CreateNewScene
        {
            get { return createNewScene; }
        }

        [ShowIf("createNewScene")]
        [SerializeField]
        StoryBackgroundData background;
        public StoryBackgroundData Background
        {
            get { return background; }
        }

        [ShowIf("createNewScene")]
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
        [Title("Event")]
        [ValueDropdown("Test2")]
        [SerializeField]
        StoryEventDataTest eventNodes2;

        private IEnumerable Test2 = new ValueDropdownList<StoryEventDataTest>()
        {
            { "Wesh", new StoryEventDataTest2() },
            { "Random Actor", new StoryEventDataTest3() },
        };

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
                case StoryEventNode.Custom:
                    return eventNodes[index].dataBox.storyEventCustom;
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