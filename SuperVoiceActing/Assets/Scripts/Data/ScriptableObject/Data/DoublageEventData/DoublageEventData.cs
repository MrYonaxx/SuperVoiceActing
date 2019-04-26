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


    public enum DoublageEventNode
    {
        Text,
        TextPopup,
        Viewport,
        Wait,
        Deck,
        Tuto,
        Sound,
        Load,
        Skill,
        Effect,
        SetCharacter
    }

    [System.Serializable]
    public class DoublageEventDataBox
    {
        //[Header("====================================================================================================================================")]
        [HorizontalGroup("Hey", Width = 0.2f)]
        [HideLabel]
        public DoublageEventNode eventNode;


        //[HorizontalGroup("Hey")]
        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Text)]
        [SerializeField]
        [HideLabel]
        public DoublageEventText doublageEventText = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.TextPopup)]
        [SerializeField]
        [HideLabel]
        public DoublageEventTextPopup doublageEventTextPopup = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Viewport)]
        [SerializeField]
        [HideLabel]
        public DoublageEventViewport doublageEventViewport = null;


        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Wait)]
        [SerializeField]
        [HideLabel]
        public DoublageEventWait doublageEventWait = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Deck)]
        [SerializeField]
        [HideLabel]
        public DoublageEventDeck doublageEventDeck = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Tuto)]
        [SerializeField]
        [HideLabel]
        public DoublageEventTutoPopup doublageTutoPopup = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Sound)]
        [SerializeField]
        [HideLabel]
        public DoublageEventSound doublageEventSound = null;

        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Load)]
        [SerializeField]
        [HideLabel]
        public DoublageEventLoad doublageEventLoad = null;


        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Effect)]
        [SerializeField]
        [HideLabel]
        public DoublageEventEffect doublageEventEffect = null;


        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.Skill)]
        [SerializeField]
        [HideLabel]
        public DoublageEventSkill doublageEventSkill = null;


        [VerticalGroup("Hey/Right")]
        [ShowIf("eventNode", DoublageEventNode.SetCharacter)]
        [SerializeField]
        [HideLabel]
        public DoublageEventSetCharacter doublageEventSetCharacter = null;


        public string GetBoxTitle()
        {
            switch (eventNode)
            {
                case DoublageEventNode.Text:
                    return eventNode + " : " + doublageEventText.Text;
                case DoublageEventNode.TextPopup:
                    return eventNode + " : " + doublageEventTextPopup.Text;
                case DoublageEventNode.Viewport:
                    return eventNode + " : " + doublageEventViewport.ViewportID.ToString();
                case DoublageEventNode.Wait:
                    return eventNode + " : " + doublageEventWait.Wait.ToString();
                case DoublageEventNode.Sound:
                    return eventNode.ToString(); //+ " : " + doublageEventSound..ToString();
                case DoublageEventNode.SetCharacter:
                    return eventNode + " : " + doublageEventSetCharacter.Character.name;
            }
            return null;
        }
    }

    [System.Serializable]
    public class DoublageEventDataNode
    {
        [FoldoutGroup("$name")]
        [SerializeField]
        [HideLabel]
        public DoublageEventDataBox dataBox;

        private string name = " ";

        public void SetTitle()
        {
            name = dataBox.GetBoxTitle();
        }

    }

    /// <summary>
    /// Definition of the DoublageEventData class
    /// </summary>
    [CreateAssetMenu(fileName = "DoublageEventData", menuName = "DoublageEventData", order = 1)]
    public class DoublageEventData : ScriptableObject
    {
        [Header("Condition d'Acivation")]

        [SerializeField]
        private int phraseNumber;
        public int PhraseNumber
        {
            get { return phraseNumber; }
        }

        [SerializeField]
        private bool stopSession = true;
        public bool StopSession
        {
            get { return stopSession; }
        }

        [SerializeField]
        private bool startPhrase = true;
        public bool StartPhrase
        {
            get { return startPhrase; }
        }

        [HideIf("startPhrase", true)]
        [SerializeField]
        private bool equal = false;
        public bool Equal
        {
            get { return equal; }
        }

        [HideIf("startPhrase", true)]
        [SerializeField]
        private float hpPercentage = 100;
        public float HpPercentage
        {
            get { return hpPercentage; }
        }



        [Header("Doublage Event")]
        [SerializeField]
        DoublageEventDataNode[] doublageEvent;


        [Button]
        private void SetTitles()
        {
            for (int i = 0; i < doublageEvent.Length; i++)
            {
                doublageEvent[i].SetTitle();
            }

        }

        public DoublageEvent GetEventNode(int index)
        {
            if (index == doublageEvent.Length)
                return null;

            switch (doublageEvent[index].dataBox.eventNode)
            {
                case DoublageEventNode.Text:
                    return doublageEvent[index].dataBox.doublageEventText;
                case DoublageEventNode.TextPopup:
                    return doublageEvent[index].dataBox.doublageEventTextPopup;
                case DoublageEventNode.Viewport:
                    return doublageEvent[index].dataBox.doublageEventViewport;
                case DoublageEventNode.Wait:
                    return doublageEvent[index].dataBox.doublageEventWait;
                case DoublageEventNode.Deck:
                    return doublageEvent[index].dataBox.doublageEventDeck;
                case DoublageEventNode.Tuto:
                    return doublageEvent[index].dataBox.doublageTutoPopup;
                case DoublageEventNode.Sound:
                    return doublageEvent[index].dataBox.doublageEventSound;
                case DoublageEventNode.Load:
                    return doublageEvent[index].dataBox.doublageEventLoad;
                case DoublageEventNode.Effect:
                    return doublageEvent[index].dataBox.doublageEventEffect;
                case DoublageEventNode.Skill:
                    return doublageEvent[index].dataBox.doublageEventSkill;
                case DoublageEventNode.SetCharacter:
                    return doublageEvent[index].dataBox.doublageEventSetCharacter;
            }
            return null;
        }




    } // DoublageEventData class

} // #PROJECTNAME# namespace