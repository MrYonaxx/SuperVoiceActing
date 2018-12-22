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
        Camera
    }

    [System.Serializable]
    public class DoublageEventDataNode
    {
        [Header("====================================================================================================================================")]
        public DoublageEventNode eventNode;

        [ShowIf("eventNode", DoublageEventNode.Text)]
        [SerializeField]
        public DoublageEventText doublageEventText = null;

        [ShowIf("eventNode", DoublageEventNode.Camera)]
        [SerializeField]
        public DoublageEventCamera doublageEventCamera = null;

        /*[ShowIf("eventNode", DoublageEventNode.MoveCharacter)]
        [SerializeField]
        public StoryEventMoveCharacter storyEventMoveCharacter = null;*/

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
                case DoublageEventNode.Camera:
                    return doublageEvent[index].doublageEventCamera;
            }
            return null;
        }




    } // DoublageEventData class

} // #PROJECTNAME# namespace