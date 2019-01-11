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
    /// Definition of the StoryEventMoveCharacter class
    /// </summary>
    [System.Serializable]
    public class StoryEventMoveCharacter : StoryEvent
    {

        [SerializeField]
        StoryCharacterData characterToMove;

        [SerializeField]
        float time = 60;

        [SerializeField]
        Vector3 newPosition;
        [SerializeField]
        Vector3 newRotation;
        [SerializeField]
        Vector3 newScale;

        [SerializeField]
        bool fadeIn = false;
        [SerializeField]
        bool fadeOut = false;
        [SerializeField]
        bool waitEnd = false;

        CharacterDialogueController character;

        public void SetNode(CharacterDialogueController[] charactersEvent)
        {
            for (int i = 0; i < charactersEvent.Length; i++)
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
                actualTime -= 1;
                yield return null;
            }
        }


    } // StoryEventMoveCharacter class

} // #PROJECTNAME# namespace