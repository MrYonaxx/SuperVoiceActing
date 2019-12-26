/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageEventTextPopup class
    /// </summary>
    /// 
    [System.Serializable]
    public class DoublageEventTextPopup : DoublageEvent
    {

        [SerializeField]
        private StoryCharacterData interlocuteur;
        public StoryCharacterData Interlocuteur
        {
            get { return interlocuteur; }
        }

        [SerializeField]
        private EmotionNPC emotionNPC;
        public EmotionNPC EmotionNPC
        {
            get { return emotionNPC; }
        }

        [SerializeField]
        [TextArea]
        private string text;
        public string Text
        {
            get { return text; }
        }

        [SerializeField]
        private float time;
        public float Time
        {
            get { return time; }
        }





        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            eventManager.PanelPlayer.StartPopup(interlocuteur.GetName(), text);
            yield break;
        }

    } // DoublageEventTextPopup class

} // #PROJECTNAME# namespace