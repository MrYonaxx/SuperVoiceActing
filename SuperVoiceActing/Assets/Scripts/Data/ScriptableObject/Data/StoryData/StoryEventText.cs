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
using System.Text;

namespace VoiceActing
{

    public enum EmotionBalloon
    {
        None,
        Exclamation,
        Question,
        MusicNote,
        Heart,
        Angry,
        SweatDrop,
        Annoyed,
        Silence,
        Idea,
        Sleep
    }

    [System.Serializable]
    public class StoryEventText : StoryEvent
    {
        [HorizontalGroup("Interlocuteur", Width = 0.5f)]
        [SerializeField]
        [HideLabel]
        StoryCharacterData interlocuteur;

        [HorizontalGroup("Interlocuteur", Width = 0.25f)]
        [SerializeField]
        [HideLabel]
        EmotionNPC emotionNPC;

        [HorizontalGroup("Interlocuteur", Width = 0.25f)]
        [SerializeField]
        [HideLabel]
        EmotionBalloon emotionEmoticonBalloon;

        //[TabGroup("ParentGroup", "Texte")]
        [SerializeField]
        [TextArea(2,2)]
        [HideLabel]
        private string text = null;
        public string Text
        {
            get { return text;  }
        }

        /*[TabGroup("ParentGroup", "TexteEng")]
        [SerializeField]
        [TextArea(2, 2)]
        [HideLabel]
        string textEng = null;*/


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

        //int characterCount = 0;
        int actualTime = 0;

        List<int> pauseList = new List<int>();

        //int language = 0;

        string actualText = null;








        public void SetLanguage(int newLanguage)
        {
            //language = newLanguage;
        }

        public StoryCharacterData GetInterlocuteur()
        {
            return interlocuteur;
        }

        public void SetNode(TextMeshProUGUI textMesh, List<CharacterDialogueController> charactersEvent, GameObject next, Dictionary<string, string> dictionary)
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


            actualText = text;
            this.actualText = StringReplace(this.actualText, dictionary);

        }

        private string StringReplace(string stringB, Dictionary<string, string> dict)
        {
            foreach (string k in dict.Keys)
            {
                stringB = stringB.Replace(k, dict[k]);
            }
            return stringB;
        }

        protected override IEnumerator StoryEventCoroutine()
        {
            // Initialization
            textMeshPro.textInfo.linkCount = 0;
            pauseList.Clear();
            actualTime = 0;

            /*string actualText = text; // French
            if (language == 1) // English
                actualText = textEng;*/


            textMeshPro.text = actualText;
            textMeshPro.maxVisibleCharacters = 0;

            if (characterDialogue != null)
            {
                characterDialogue.PlayAnimBalloon(-1);
            }

            yield return null;

            textMeshPro.maxVisibleCharacters = 0;

            if (characterDialogue != null)
            {
                if (mouthSpeed <= -1)
                {
                    characterDialogue.ActivateMouth(mouthSpeed + 7, true);
                    //mouthSpeed *= -1;
                }
                else
                {
                    characterDialogue.ActivateMouth(mouthSpeed + 7);
                }
                characterDialogue.ChangeEmotion(emotionNPC);
                characterDialogue.PlayAnimBalloon((int)emotionEmoticonBalloon - 1);
            }

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
                if (actualTime == Mathf.Abs(mouthSpeed) && textMeshPro.maxVisibleCharacters < actualText.Length)
                {
                    if (CheckPause() == false)
                    {
                        textMeshPro.maxVisibleCharacters += 1;
                        actualTime = 0;
                        /*if (textMeshPro.maxVisibleCharacters > 0)
                        {
                            if (actualText[textMeshPro.maxVisibleCharacters-1] == ',' || actualText[textMeshPro.maxVisibleCharacters - 1] == '.')
                                actualTime -= 20;
                        }*/
                    }
                }
                else if (textMeshPro.maxVisibleCharacters < actualText.Length)
                {
                    actualTime += 1;
                }

                // Check Input
                if (ignorePlayerInput == false)
                {
                    if (Input.GetButton("ControllerB") && textMeshPro.maxVisibleCharacters == actualText.Length)
                    {
                        actualTime = 0;
                        textMeshPro.maxVisibleCharacters = 0;
                        textMeshPro.text = "";
                        nextButton.SetActive(false);
                        break;
                    }
                    else if (Input.GetButton("ControllerB"))
                    {
                        actualTime = 0;
                        if (CheckSkipPause() == false)
                        {
                            textMeshPro.maxVisibleCharacters = actualText.Length;
                            if (characterDialogue != null)
                                characterDialogue.StopMouth();
                            nextButton.SetActive(true);
                        }
                        yield return null;
                        yield return null;
                    }
                    else if ((Input.GetButtonDown("ControllerA") || Input.GetMouseButtonDown(0)) && textMeshPro.maxVisibleCharacters == actualText.Length)
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