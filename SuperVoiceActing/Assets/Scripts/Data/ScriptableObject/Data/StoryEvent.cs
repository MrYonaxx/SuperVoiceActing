/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the StoryEvent class
    /// </summary>
    /// 
    [System.Serializable]
    public class StoryEvent
    {

        protected TextMeshPro textMeshPro;
        protected CharacterDialogueController characterDialogue;





        public IEnumerator GetStoryEvent(TextMeshPro eventText, CharacterDialogueController[] charactersEvent)
        {
            textMeshPro = eventText;
            characterDialogue = SetCharacterDialogue(charactersEvent);
            return StoryEventCoroutine();
        }

        protected virtual IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }

        protected virtual CharacterDialogueController SetCharacterDialogue(CharacterDialogueController[] charactersEvent)
        {
            return null;
        }

    } // StoryEvent class

} // #PROJECTNAME# namespace