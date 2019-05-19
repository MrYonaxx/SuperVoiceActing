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
        private Vector3 newPosition = new Vector3(0, -5.2f, 0);
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
        Vector3 newScale = new Vector3(0.45f,0.45f,0.45f);
        public Vector3 NewScale
        {
            get { return newScale; }
        }

        [HorizontalGroup("Advanced", LabelWidth = 70)]
        [SerializeField]
        bool fadeIn = false;
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
            // Initialization
            float actualTime = time;
            float speedX = (newPosition.x - character.transform.position.x) / actualTime;
            float speedY = (newPosition.y - character.transform.position.y) / actualTime;
            float speedRotateY = (newRotation.y - character.transform.eulerAngles.y) / actualTime;
            if (fadeIn == true)
            {
                character.FadeIn(actualTime);
            }
            else if (fadeOut == true)
            {
                character.FadeOut(actualTime);
            }


            while (actualTime != 0)
            {
                character.transform.position += new Vector3(speedX, speedY, 0);
                character.transform.eulerAngles += new Vector3(0, speedRotateY, 0);
                actualTime -= 1;
                yield return null;
            }
        }


    } // StoryEventMoveCharacter class

} // #PROJECTNAME# namespace