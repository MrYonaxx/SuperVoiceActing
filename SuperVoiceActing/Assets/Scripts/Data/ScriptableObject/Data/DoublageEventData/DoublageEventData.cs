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
        Camera,
        Wait,
        Deck,
        Tuto
    }

    [System.Serializable]
    public class DoublageEventDataNode
    {
        [Header("====================================================================================================================================")]
        public DoublageEventNode eventNode;

        [ShowIf("eventNode", DoublageEventNode.Text)]
        [SerializeField]
        public DoublageEventText doublageEventText = null;

        [ShowIf("eventNode", DoublageEventNode.TextPopup)]
        [SerializeField]
        public DoublageEventTextPopup doublageEventTextPopup = null;

        [ShowIf("eventNode", DoublageEventNode.Camera)]
        [SerializeField]
        public DoublageEventCamera doublageEventCamera = null;

        [ShowIf("eventNode", DoublageEventNode.Wait)]
        [SerializeField]
        public DoublageEventWait doublageEventWait = null;

        [ShowIf("eventNode", DoublageEventNode.Deck)]
        [SerializeField]
        public DoublageEventDeck doublageEventDeck = null;

        [ShowIf("eventNode", DoublageEventNode.Tuto)]
        [SerializeField]
        public DoublageEventTutoPopup doublageTutoPopup = null;

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



        public DoublageEvent GetEventNode(int index)
        {
            if (index == doublageEvent.Length)
                return null;

            switch (doublageEvent[index].eventNode)
            {
                case DoublageEventNode.Text:
                    return doublageEvent[index].doublageEventText;
                case DoublageEventNode.TextPopup:
                    return doublageEvent[index].doublageEventTextPopup;
                case DoublageEventNode.Camera:
                    return doublageEvent[index].doublageEventCamera;
                case DoublageEventNode.Wait:
                    return doublageEvent[index].doublageEventWait;
                case DoublageEventNode.Deck:
                    return doublageEvent[index].doublageEventDeck;
                case DoublageEventNode.Tuto:
                    return doublageEvent[index].doublageTutoPopup;
            }
            return null;
        }




    } // DoublageEventData class

} // #PROJECTNAME# namespace