/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{

    [System.Serializable]
    public class StoryEventText : StoryEvent
    {
        [SerializeField]
        StoryCharacterData interlocuteur;

        [SerializeField]
        EmotionNPC emotionNPC;

        [HorizontalGroup("Texte")]
        [SerializeField]
        [TextArea]
        string text = null;


        [SerializeField]
        float mouthSpeed = 2;

        [HorizontalGroup("Option Avancées")]
        [SerializeField]
        bool forceSkip = false;

        [HorizontalGroup("Option Avancées")]
        [SerializeField]
        bool ignorePlayerInput = false;



        protected TextMeshPro textMeshPro;
        protected CharacterDialogueController characterDialogue;

        int characterCount = 0;
        int actualTime = 0;

        List<int> pauseList = new List<int>();


        public void SetNode(TextMeshPro textMesh, CharacterDialogueController[] charactersEvent)
        {
            textMeshPro = textMesh;
            for (int i = 0; i < charactersEvent.Length; i++)
            {
                if (charactersEvent[i].GetStoryCharacterData() == interlocuteur)
                {
                    characterDialogue = charactersEvent[i];
                    break;
                }
            }
        }



        protected override IEnumerator StoryEventCoroutine()
        {
            // Initialization
            textMeshPro.textInfo.linkCount = 0;
            pauseList.Clear();
            actualTime = 0;
            textMeshPro.text = text;

            yield return null;

            textMeshPro.maxVisibleCharacters = 0;

            if (characterDialogue != null)
                characterDialogue.ActivateMouth(mouthSpeed + 7);

            for (int i = 0; i < textMeshPro.textInfo.linkCount; i++)
            {
                switch (textMeshPro.textInfo.linkInfo[i].GetLinkID())
                {
                    case "Pause":
                        pauseList.Add(textMeshPro.textInfo.linkInfo[i].linkTextfirstCharacterIndex);
                        break;
                    case "Print":
                        textMeshPro.maxVisibleCharacters = textMeshPro.textInfo.linkInfo[i].linkTextLength;
                        break;
                    default:
                        break;
                }
            }


            while (true)
            {
                // print
                if (actualTime == mouthSpeed && textMeshPro.maxVisibleCharacters < text.Length)
                {
                    if (CheckPause() == false)
                    {
                        textMeshPro.maxVisibleCharacters += 1;
                        actualTime = 0;
                    }
                }
                else if (textMeshPro.maxVisibleCharacters < text.Length)
                {
                    actualTime += 1;
                }

                // Check Input
                if (ignorePlayerInput == false)
                {
                    if (Input.GetButtonDown("ControllerA") && textMeshPro.maxVisibleCharacters == text.Length)
                    {
                        actualTime = 0;
                        textMeshPro.maxVisibleCharacters = 0;
                        textMeshPro.text = "";
                        break;
                    }
                    else if (Input.GetButtonDown("ControllerA"))
                    {
                        actualTime = 0;
                        if (CheckSkipPause() == false)
                        {
                            textMeshPro.maxVisibleCharacters = text.Length;
                        }
                    }
                }

                //Check End print
                if (textMeshPro.maxVisibleCharacters == text.Length) {
                    if (characterDialogue != null)
                        characterDialogue.StopMouth();
                    if(forceSkip == true)
                    {
                        break;
                    }
                }
                yield return null;
            }
        }





        private bool CheckPause()
        {
            if (pauseList.Count > 0)
            {
                if(textMeshPro.maxVisibleCharacters == pauseList[0])
                {
                    if (characterDialogue != null)
                        characterDialogue.StopMouth();
                    return true;
                }
            }
            return false;
        }

        private bool CheckSkipPause()
        {
            if (pauseList.Count > 0)
            {
                if (textMeshPro.maxVisibleCharacters == pauseList[0])
                {
                    if (characterDialogue != null)
                        characterDialogue.ActivateMouth(mouthSpeed + 7);
                    pauseList.RemoveAt(0);
                }
                else
                {
                    textMeshPro.maxVisibleCharacters = pauseList[0];
                }
                return true;
            }
            return false;
        }


    } // StoryEventText class

} // #PROJECTNAME# namespace