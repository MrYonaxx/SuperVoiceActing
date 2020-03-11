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
        private VoiceActorData interlocutor;

        [HorizontalGroup("Interlocuteur", Width = 0.25f)]
        [SerializeField]
        [HideLabel]
        EmotionNPC emotionNPC;

        [HorizontalGroup("Interlocuteur", Width = 0.25f)]
        [SerializeField]
        [HideLabel]
        EmotionBalloon emotionEmoticonBalloon;

        [SerializeField]
        [TextArea(2,2)]
        [HideLabel]
        private string text = null;
        public string Text
        {
            get { return text;  }
        }



        [HorizontalGroup("Option Avancées", LabelWidth = 50)]
        [SerializeField]
        float mouthSpeed = 1;

        [HorizontalGroup("Option Avancées", LabelWidth = 50)]
        [SerializeField]
        bool forceSkip = false;

        [HorizontalGroup("Option Avancées", LabelWidth = 120)]
        [SerializeField]
        bool ignorePlayerInput = false;






        int actualTime = 0;
        int actualLenght = 0;

        List<int> pauseList = new List<int>();

        string actualText = null;
        int voiceLetterInterval = 3;
        int voiceLetterCurrentInterval = 0;


        private string StringReplace(string stringB, Dictionary<string, string> dict)
        {
            foreach (string k in dict.Keys)
            {
                stringB = stringB.Replace(k, dict[k]);
            }
            return stringB;
        }


        public void PlayVoice(char character, CharacterDialogueController characterDialogue)
        {
            voiceLetterCurrentInterval += 1;
            if(voiceLetterCurrentInterval >= voiceLetterInterval)
            {
                if (character != ' ' && character != ',' && character != '.' && character != '?' && character != '!')
                {
                    if (characterDialogue != null)
                        characterDialogue.PlayVoice();
                    voiceLetterCurrentInterval = 0;
                }
            }
        }

        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            // Initialization ==========================================================
            TextMeshProUGUI textMeshPro = storyManager.TextMeshPro;
            GameObject nextButton = storyManager.Next;
            CharacterDialogueController characterDialogue = null;
            if (interlocutor != null)
            {
                for (int i = 0; i < storyManager.Characters.Count; i++)
                {
                    if (storyManager.Characters[i].GetVoiceActorData().NameID == interlocutor.NameID)
                    {
                        characterDialogue = storyManager.Characters[i];
                        characterDialogue.SpeakAnimation();
                        break;
                    }
                }
            }

            actualText = text;
            this.actualText = StringReplace(this.actualText, storyManager.Dictionary);

            textMeshPro.textInfo.linkCount = 0;
            pauseList.Clear();
            actualTime = 0;
            voiceLetterCurrentInterval = voiceLetterInterval;
            textMeshPro.text = actualText;
            textMeshPro.maxVisibleCharacters = 0;

            if (characterDialogue != null)
            {
                characterDialogue.PlayAnimBalloon(-1);
                storyManager.DrawName(characterDialogue.GetVoiceActorData().ActorName);
                storyManager.CameraCenter.transform.position = characterDialogue.transform.position;
            }
            else
            {
                storyManager.HideName();
            }
            storyManager.AnimatorTextbox.SetTrigger("Feedback");

            yield return null;

            // Initialization ==========================================================

            actualLenght = textMeshPro.textInfo.characterCount;

            if (characterDialogue != null)
            {
                if (mouthSpeed <= -1)
                {
                    characterDialogue.ActivateMouth(mouthSpeed + 6, true);
                }
                else
                {
                    characterDialogue.ActivateMouth(mouthSpeed + 6);
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
                    default:
                        break;
                }
            }

            if (actualText[0] == '(')
                textMeshPro.color = new Color(0.8f, 0.8f, 1);
            else
                textMeshPro.color = new Color(1, 1, 1);




            //    B O U C L E    P R I N C I P A L     =====================================================

            while (true)
            {
                // Print letter + wait time
                if (actualTime == Mathf.Abs(mouthSpeed) && textMeshPro.maxVisibleCharacters < actualLenght)
                {
                    if (CheckPause(textMeshPro, characterDialogue) == false)
                    {
                        textMeshPro.maxVisibleCharacters += 1;
                        actualTime = 0;
                        if (textMeshPro.maxVisibleCharacters > 0 && textMeshPro.maxVisibleCharacters < actualLenght)
                        {
                            if (actualText[textMeshPro.maxVisibleCharacters - 1] == ',' && actualText[textMeshPro.maxVisibleCharacters] == ' ' ||
                                actualText[textMeshPro.maxVisibleCharacters - 1] == '.' && actualText[textMeshPro.maxVisibleCharacters] == ' ' ||
                                actualText[textMeshPro.maxVisibleCharacters - 1] == '?' && actualText[textMeshPro.maxVisibleCharacters] == ' ' ||
                                actualText[textMeshPro.maxVisibleCharacters - 1] == '!' && actualText[textMeshPro.maxVisibleCharacters] == ' ')
                                actualTime -= 12;
                        }
                        PlayVoice(actualText[textMeshPro.maxVisibleCharacters - 1], characterDialogue);
                    }
                    else
                        nextButton.SetActive(true);
                }
                else if (textMeshPro.maxVisibleCharacters < actualLenght)
                {
                    actualTime += 1;
                }

                // Check Input
                if (ignorePlayerInput == false)
                {
                    if ((Input.GetButton("ControllerB") || Input.GetMouseButton(1)) && textMeshPro.maxVisibleCharacters == actualLenght)
                    {
                        actualTime = 0;
                        textMeshPro.maxVisibleCharacters = 0;
                        textMeshPro.text = "";
                        nextButton.SetActive(false);
                        break;
                    }
                    else if (Input.GetButton("ControllerB") || Input.GetMouseButton(1))
                    {
                        actualTime = 0;
                        if (CheckSkipPause(textMeshPro, characterDialogue) == false)
                        {
                            textMeshPro.maxVisibleCharacters = actualLenght;
                            if (characterDialogue != null)
                                characterDialogue.StopMouth();
                            nextButton.SetActive(true);
                        }
                        yield return null;
                        yield return null;
                    }
                    else if ((Input.GetButtonDown("ControllerA") || Input.GetMouseButtonDown(0)) && textMeshPro.maxVisibleCharacters == actualLenght)
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
                        if (CheckSkipPause(textMeshPro, characterDialogue) == false)
                        {
                            textMeshPro.maxVisibleCharacters = actualLenght;
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
                    if (forceSkip == true)
                    {
                        break;
                    }
                }
                yield return null;
            }
        }


        protected override IEnumerator StoryEventCoroutine()
        {
            yield return null;
        }





        private bool CheckPause(TextMeshProUGUI textMeshPro, CharacterDialogueController characterDialogue)
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

        private bool CheckSkipPause(TextMeshProUGUI textMeshPro, CharacterDialogueController characterDialogue)
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