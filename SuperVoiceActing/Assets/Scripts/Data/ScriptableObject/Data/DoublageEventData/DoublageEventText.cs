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
    /// Definition of the DoublageEventText class
    /// </summary>

    
    // à fusionner avec stoy Event ? On va dire non pour le moment mais je continue de douter


    [System.Serializable]
    public class DoublageEventText : DoublageEvent
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
        private int cameraEffectID;
        public int CameraEffectID
        {
            get { return cameraEffectID; }
        }

    } // DoublageEventText class

} // #PROJECTNAME# namespace