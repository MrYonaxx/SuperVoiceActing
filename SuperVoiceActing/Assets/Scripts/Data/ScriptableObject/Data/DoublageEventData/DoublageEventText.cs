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
    /// <summary>
    /// Definition of the DoublageEventText class
    /// </summary>

    
    // à fusionner avec stoy Event ? On va dire non pour le moment mais je continue de douter
    

    [System.Serializable]
    public class DoublageEventText : DoublageEvent
    {
        [SerializeField]
        [HideLabel]
        [HorizontalGroup("Interlocuteur")]
        private StoryCharacterData interlocuteur;
        public StoryCharacterData Interlocuteur
        {
            get { return interlocuteur; }
        }

        [SerializeField]
        [HideLabel]
        [HorizontalGroup("Interlocuteur", Width = 0.2f)]
        private EmotionNPC emotionNPC;
        public EmotionNPC EmotionNPC
        {
            get { return emotionNPC; }
        }

        [TabGroup("ParentGroup", "Texte")]
        [SerializeField]
        [TextArea(1,1)]
        [HideLabel]
        private string text;
        public string Text
        {
            get { return text; }
        }

        [TabGroup("ParentGroup", "TexteEng")]
        [SerializeField]
        [TextArea(1, 1)]
        [HideLabel]
        private string textEng;
        public string TextEng
        {
            get { return textEng; }
        }


        [HorizontalGroup("Hey8", LabelWidth = 100)]
        [SerializeField]
        private int cameraID;
        public int CameraID
        {
            get { return cameraID; }
        }
        [HorizontalGroup("Hey8", LabelWidth = 100)]
        [SerializeField]
        private bool clearText;
        public bool ClearText
        {
            get { return clearText; }
        }
        [HorizontalGroup("Hey8", LabelWidth = 100)]
        [SerializeField]
        private bool clearAllText;
        public bool ClearAllText
        {
            get { return clearAllText; }
        }
        [HorizontalGroup("Hey8", LabelWidth = 100)]
        [SerializeField]
        private bool changeViewportText;
        public bool ChangeViewportText
        {
            get { return changeViewportText; }
        }

        [Title("Position")]
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 camStart;
        public Vector3 CamStart
        {
            get { return camStart; }
        }
        [Title("Rotation")]
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 camStartRotation;
        public Vector3 CamStartRotation
        {
            get { return camStartRotation; }
        }
        [Title("Time")]
        // -1 pour ne pas set de position initiale
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private int timeStart;
        public int TimeStart
        {
            get { return timeStart; }
        }


        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd;
        public Vector3 CameraEnd
        {
            get { return cameraEnd; }
        }

        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEndRotation;
        public Vector3 CameraEndRotation
        {
            get { return cameraEndRotation; }
        }
        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private int timeEnd;
        public int TimeEnd
        {
            get { return timeEnd; }
        }

        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd2;
        public Vector3 CameraEnd2
        {
            get { return cameraEnd2; }
        }
        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd2Rotation;
        public Vector3 CameraEnd2Rotation
        {
            get { return cameraEnd2Rotation; }
        }
        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private int timeEnd2;
        public int TimeEnd2
        {
            get { return timeEnd2; }
        }




    } // DoublageEventText class

} // #PROJECTNAME# namespace