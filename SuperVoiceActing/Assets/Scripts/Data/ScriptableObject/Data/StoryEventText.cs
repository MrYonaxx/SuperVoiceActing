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

    [System.Serializable]
    public class StoryEventText : StoryEvent
    {
        [SerializeField]
        StoryCharacterData interlocuteur;

        [SerializeField]
        EmotionNPC emotionNPC;

        [SerializeField]
        [TextArea]
        string text = null;

        [SerializeField]
        float mouthSpeed = 2;


        int characterCount = 0;
        int actualTime = 0;


        protected override IEnumerator StoryEventCoroutine()
        {

            actualTime = 0;
            textMeshPro.maxVisibleCharacters = 0;
            textMeshPro.text = text;
            if(characterDialogue != null)
                characterDialogue.ActivateMouth();
            while (true)
            {
                if (actualTime == mouthSpeed && textMeshPro.maxVisibleCharacters < text.Length)
                {
                    textMeshPro.maxVisibleCharacters += 1;
                    actualTime = 0;
                }
                else if (textMeshPro.maxVisibleCharacters < text.Length)
                {
                    actualTime += 1;
                }

                if (Input.GetButtonDown("ControllerA") && textMeshPro.maxVisibleCharacters == text.Length)
                {
                    actualTime = 0;
                    textMeshPro.maxVisibleCharacters = 0;
                    textMeshPro.text = null;
                    break;
                }
                else if (Input.GetButtonDown("ControllerA") )
                {
                    actualTime = 0;
                    textMeshPro.maxVisibleCharacters = text.Length;
                }
                if (textMeshPro.maxVisibleCharacters == text.Length) {
                    if (characterDialogue != null)
                        characterDialogue.StopMouth();
                }
                yield return null;
            }
            yield return null;
        }


        protected override CharacterDialogueController SetCharacterDialogue(CharacterDialogueController[] charactersEvent)
        {
            for(int i = 0; i < charactersEvent.Length; i++)
            {
                if(charactersEvent[i].GetStoryCharacterData() == interlocuteur)
                {
                    return charactersEvent[i];
                }
            }
            return null;
        }


    } // StoryEventText class

} // #PROJECTNAME# namespace