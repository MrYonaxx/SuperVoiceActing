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
        [HorizontalGroup("Interlocuteur", Width = 0.5f)]
        [SerializeField]
        [HideLabel]
        private VoiceActorData characterMoving;
        public VoiceActorData CharacterMoving { get { return characterMoving; } }


        [HorizontalGroup("Interlocuteur", LabelWidth = 100)]
        [SerializeField]
        float time = 60;

        [SerializeField]
        private Vector3 newPosition = new Vector3(0, 0, 0);
        public Vector3 NewPosition { get { return newPosition; } }


        [SerializeField]
        Vector3 newRotation = new Vector3(0, 0, 0);
        public Vector3 NewRotation { get { return newRotation; } }

        [SerializeField]
        Vector3 newScale = new Vector3(0.5f,0.5f,0.5f);
        public Vector3 NewScale { get { return newScale; } }

        [HorizontalGroup("Advanced", LabelWidth = 70)]
        [SerializeField]
        bool appear = true;
        public bool Appear { get { return appear; } }

        [HorizontalGroup("Advanced", LabelWidth = 70)]
        [SerializeField]
        bool waitEnd = false;

        CharacterDialogueController character;



        public override bool InstantNodeCoroutine()
        {
            if (waitEnd == false)
            {
                return true;
            }
            return false;
        }

        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            yield return MoveCoroutine(storyManager);
        }

        private IEnumerator MoveCoroutine(StoryEventManager storyManager)
        {
            for (int i = 0; i < storyManager.Characters.Count; i++)
            {
                if (storyManager.Characters[i].GetVoiceActorData().NameID == characterMoving.NameID)
                {
                    character = storyManager.Characters[i];
                    break;
                }
            }

            character.FadeCharacter(appear, time);

            float t = 0;
            float rate = (60f / time);
            if (Input.GetButton("ControllerB"))
            {
                t = 1f;
            }

            Vector3 initialPosition = character.GetPosition();
            Vector3 initialRotation = character.transform.eulerAngles;
            Vector3 initialScale = character.transform.localScale;
            while (t < 1f)
            {
                t += Time.deltaTime * rate;
                character.SetPosition(Vector3.Lerp(initialPosition, newPosition, t));
                character.transform.eulerAngles = Vector3.Lerp(initialRotation, newRotation, t);
                character.transform.localScale = Vector3.Lerp(initialScale, newScale, t);
                yield return null;
            }
        }


    } // StoryEventMoveCharacter class

} // #PROJECTNAME# namespace