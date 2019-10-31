/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the StoryEventMoveCharacter class
    /// </summary>
    [System.Serializable]
    public class StoryEventMoveCharacter : StoryEvent
    {

        [HorizontalGroup("Interlocuteur")]
        [HideLabel]
        [SerializeField]
        private StoryCharacterData characterToMove;
        public StoryCharacterData CharacterToMove
        {
            get { return characterToMove; }
        }

        [HorizontalGroup("Interlocuteur", LabelWidth = 100)]
        [SerializeField]
        float time = 60;

        [SerializeField]
        private Vector3 newPosition = new Vector3(0, 0, 0);
        public Vector3 NewPosition
        {
            get { return newPosition; }
        }

        [SerializeField]
        Vector3 newRotation;
        public Vector3 NewRotation
        {
            get { return newRotation; }
        }

        [SerializeField]
        Vector3 newScale = new Vector3(0.5f,0.5f,0.5f);
        public Vector3 NewScale
        {
            get { return newScale; }
        }

        [HorizontalGroup("Advanced", LabelWidth = 70)]
        [SerializeField]
        bool fadeIn = true;
        public bool FadeIn
        {
            get { return fadeIn; }
        }

        [HorizontalGroup("Advanced", LabelWidth = 70)]
        [SerializeField]
        bool fadeOut = false;

        [HorizontalGroup("Advanced", LabelWidth = 70)]
        [SerializeField]
        bool waitEnd = false;

        CharacterDialogueController character;






        public void SetNode(List<CharacterDialogueController> charactersEvent)
        {
            for (int i = 0; i < charactersEvent.Count; i++)
            {
                if (charactersEvent[i].GetStoryCharacterData() == characterToMove)
                {
                    character = charactersEvent[i];
                    break;
                }
            }
        }

        public bool GetWaitEnd()
        {
            return waitEnd;
        }


        protected override IEnumerator StoryEventCoroutine()
        {
            if(waitEnd == false)
            {
                yield break;
            }
            else
            {
                yield return MoveCoroutine();
            }

        }

        public IEnumerator MoveCoroutine()
        {
            if (fadeIn == true)
            {
                character.FadeIn(time);
            }
            else if (fadeOut == true)
            {
                character.FadeOut(time);
            }

            float t = 0;
            float rate = (60f / time);
            if (Input.GetButton("ControllerB"))
            {
                t = 1f;
            }

            Vector3 initialPosition = character.GetPosition();
            Vector3 initialRotation = character.transform.eulerAngles;
            while (t < 1f)
            {
                t += Time.deltaTime * rate;
                character.SetPosition(Vector3.Lerp(initialPosition, newPosition, t));
                character.transform.eulerAngles = Vector3.Lerp(initialRotation, newRotation, t);
                yield return null;
            }
        }


    } // StoryEventMoveCharacter class

} // #PROJECTNAME# namespace