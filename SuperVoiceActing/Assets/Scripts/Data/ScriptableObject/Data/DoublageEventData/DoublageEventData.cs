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
        Sound
    }

    [System.Serializable]
    public class DoublageEventDataBox
    {
        //[Header("====================================================================================================================================")]
        [HorizontalGroup("Hey", Width = 0.2f)]
        [HideLabel]
        public DoublageEventNode eventNode;


        [HorizontalGroup("Hey")]
        [ShowIf("eventNode", DoublageEventNode.Text)]
        [SerializeField]
        [HideLabel]
        public DoublageEventText doublageEventText = null;


        [HorizontalGroup("Hey")]
        [ShowIf("eventNode", DoublageEventNode.TextPopup)]
        [SerializeField]
        [HideLabel]
        public DoublageEventTextPopup doublageEventTextPopup = null;


        [HorizontalGroup("Hey")]
        [ShowIf("eventNode", DoublageEventNode.Viewport)]
        [SerializeField]
        [HideLabel]
        public DoublageEventViewport doublageEventViewport = null;


        [HorizontalGroup("Hey")]
        [ShowIf("eventNode", DoublageEventNode.Wait)]
        [SerializeField]
        [HideLabel]
        public DoublageEventWait doublageEventWait = null;


        [ShowIf("eventNode", DoublageEventNode.Deck)]
        [SerializeField]
        [HideLabel]
        public DoublageEventDeck doublageEventDeck = null;


        [ShowIf("eventNode", DoublageEventNode.Tuto)]
        [SerializeField]
        [HideLabel]
        public DoublageEventTutoPopup doublageTutoPopup = null;


        [ShowIf("eventNode", DoublageEventNode.Sound)]
        [SerializeField]
        [HideLabel]
        public DoublageEventSound doublageEventSound = null;


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
            }
            return null;
        }




    } // DoublageEventData class

} // #PROJECTNAME# namespace