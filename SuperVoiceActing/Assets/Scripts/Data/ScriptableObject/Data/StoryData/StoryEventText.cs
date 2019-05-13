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
        [HorizontalGroup("Interlocuteur")]
        [SerializeField]
        [HideLabel]
        StoryCharacterData interlocuteur;

        [HorizontalGroup("Interlocuteur")]
        [SerializeField]
        [HideLabel]
        EmotionNPC emotionNPC;

        [TabGroup("ParentGroup", "Texte")]
        [SerializeField]
        [TextArea(2,2)]
        [HideLabel]
        private string text = null;
        public string Text
        {
            get { return text;  }
        }

        [TabGroup("ParentGroup", "TexteEng")]
        [SerializeField]
        [TextArea(2, 2)]
        [HideLabel]
        string textEng = null;


        [HorizontalGroup("Option Avancées", LabelWidth = 50)]
        [SerializeField]
        float mouthSpeed = 1;

        [HorizontalGroup("Option Avancées", LabelWidth = 50)]
        [SerializeField]
        bool forceSkip = false;

        [HorizontalGroup("Option Avancées", LabelWidth = 120)]
        [SerializeField]
        bool ignorePlayerInput = false;






        protected TextMeshProUGUI textMeshPro;
        protected CharacterDialogueController characterDialogue;
        protected GameObject nextButton;

        int characterCount = 0;
        int actualTime = 0;

        List<int> pauseList = new List<int>();

        int language = 0;






        public void SetLanguage(int newLanguage)
        {
            language = newLanguage;
        }

        public StoryCharacterData GetInterlocuteur()
        {
            return interlocuteur;
        }

        public void SetNode(TextMeshProUGUI textMesh, List<CharacterDialogueController> charactersEvent, GameObject next)
        {
            textMeshPro = textMesh;
            nextButton = next;
            for (int i = 0; i < charactersEvent.Count; i++)
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
            string actualText = text; // French
            if (language == 1) // English
                actualText = textEng;


            textMeshPro.text = actualText;
            textMeshPro.maxVisibleCharacters = 0;

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
                    case "Shake":
                        //textMeshPro.maxVisibleCharacters = textMeshPro.textInfo.linkInfo[i].linkTextLength;
                        break;
                    case "Flash":
                        //textMeshPro.maxVisibleCharacters = textMeshPro.textInfo.linkInfo[i].linkTextLength;
                        break;
                    default:
                        break;
                }
            }

            if(actualText[0] == '(')
            {
                textMeshPro.color = new Color(0.8f, 0.8f, 1);
            }
            else
            {
                textMeshPro.color = new Color(1, 1, 1);
            }

            while (true)
            {
                // print
                if (actualTime == mouthSpeed && textMeshPro.maxVisibleCharacters < actualText.Length)
                {
                    if (CheckPause() == false)
                    {
                        textMeshPro.maxVisibleCharacters += 1;
                        actualTime = 0;
                    }
                }
                else if (textMeshPro.maxVisibleCharacters < actualText.Length)
                {
                    actualTime += 1;
                }

                // Check Input
                if (ignorePlayerInput == false)
                {
                    if ((Input.GetButtonDown("ControllerA") || Input.GetMouseButtonDown(0)) && textMeshPro.maxVisibleCharacters == actualText.Length)
                    {
                        actualTime = 0;
                        textMeshPro.maxVisibleCharacters = 0;
                        textMeshPro.text = "";
                        nextButton.SetActive(false);
                        break;
                    }
                    else if (Input.GetButtonDown("ControllerA") || Input.GetMouseButtonDown(0))
                    {
                        actualTime = 0;
                        if (CheckSkipPause() == false)
                        {
                            textMeshPro.maxVisibleCharacters = actualText.Length;
                            if (characterDialogue != null)
                                characterDialogue.StopMouth();
                            nextButton.SetActive(true);
                        }
                    }
                }

                //Check End print
                if (textMeshPro.maxVisibleCharacters == textMeshPro.textInfo.characterCount)
                {
                    nextButton.SetActive(true);
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
                    nextButton.SetActive(true);
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