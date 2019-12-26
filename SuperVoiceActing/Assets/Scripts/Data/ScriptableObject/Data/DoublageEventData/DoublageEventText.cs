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

        [SerializeField]
        [TextArea(1,1)]
        [HideLabel]
        private string text;
        public string Text
        {
            get { return text; }
        }


        [HorizontalGroup("Hey8", LabelWidth = 100, Width = 10)]
        [SerializeField]
        private int cameraID = -1;
        public int CameraID
        {
            get { return cameraID; }
        }
        //[HorizontalGroup("Hey8", LabelWidth = 100)]
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


        [HideIf("cameraID", -1)]
        [Title("Position")]
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 camStart;
        public Vector3 CamStart
        {
            get { return camStart; }
        }

        [HideIf("cameraID", -1)]
        [Title("Rotation")]
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 camStartRotation;
        public Vector3 CamStartRotation
        {
            get { return camStartRotation; }
        }

        [HideIf("cameraID", -1)]
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

        [HideIf("cameraID", -1)]
        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd;
        public Vector3 CameraEnd
        {
            get { return cameraEnd; }
        }

        [HideIf("cameraID", -1)]
        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEndRotation;
        public Vector3 CameraEndRotation
        {
            get { return cameraEndRotation; }
        }

        [HideIf("cameraID", -1)]
        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private int timeEnd;
        public int TimeEnd
        {
            get { return timeEnd; }
        }

        [HideIf("cameraID", -1)]
        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd2;
        public Vector3 CameraEnd2
        {
            get { return cameraEnd2; }
        }
        [HideIf("cameraID", -1)]
        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd2Rotation;
        public Vector3 CameraEnd2Rotation
        {
            get { return cameraEnd2Rotation; }
        }
        [HideIf("cameraID", -1)]
        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private int timeEnd2;
        public int TimeEnd2
        {
            get { return timeEnd2; }
        }

        [SerializeField]
        [HideLabel]
        private CameraMovementData cameraMovementData;
        public CameraMovementData CameraMovementData
        {
            get { return cameraMovementData; }
        }
        [SerializeField]
        [HideIf("cameraMovementData")]
        private CamDataNode[] camDataNodes;
        public CamDataNode[] CamDataNodes
        {
            get { return camDataNodes; }
        }


        private CharacterDialogueController FindInterlocutor(DoublageEventManager doublageEventManager)
        {
            for (int i = 0; i < doublageEventManager.Characters.Count; i++)
            {
                if (doublageEventManager.Characters[i].gameObject.activeInHierarchy == false)
                    continue;
                if (doublageEventManager.Characters[i].GetStoryCharacterData() == interlocuteur)
                {
                    /*if (i < charactersActorLenght) // 3 car il y a 3 voice actors
                    {
                        mainText.SetPauseText(true);
                        mainText.HideText();
                    }*/
                    return doublageEventManager.Characters[i];
                }
            }
            return null;
        }

        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            eventManager.InputEvent.gameObject.SetActive(true);
            CharacterDialogueController interloc = FindInterlocutor(eventManager);
            if (cameraID >= 0)
            {
                eventManager.Viewports[cameraID].TextCameraEffect(this);
            }
            if (interloc != null)
            {
                interloc.ChangeEmotion(emotionNPC);
                eventManager.TextEvent[cameraID].NewMouthAnim(interloc);
                eventManager.TextEvent[cameraID].NewPhrase(text);
            }
            else
            {
                eventManager.PrintAllText(true);
            }
            yield return new WaitForSeconds(2);
        }

    } // DoublageEventText class

} // #PROJECTNAME# namespace