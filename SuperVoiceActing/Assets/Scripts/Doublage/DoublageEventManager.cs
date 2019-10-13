/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace VoiceActing
{
	public class DoublageEventManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Events")]
        [SerializeField]
        protected PanelPlayer panelPlayer;
        [SerializeField]
        protected InputController inputEvent;
        [SerializeField]
        protected TextAppearManager mainText;
        [SerializeField]
        protected ViewportManager[] viewports;
        [SerializeField]
        protected TextPerformanceAppear[] textEvent;

        [Header("Characters")]
        [SerializeField]
        protected CharacterDialogueController characterPrefab;
        [SerializeField]
        protected Transform charactersParent;
        [SerializeField]
        protected int charactersActorLenght = 3;
        [SerializeField]
        protected List<CharacterDialogueController> characters = new List<CharacterDialogueController>();
        [SerializeField]
        protected Transform[] defaultCharacterTransform;

        [Header("Tuto & Fin")]
        [SerializeField]
        protected GameObject popup;
        [SerializeField]
        protected TextMeshProUGUI textTitle;
        [SerializeField]
        protected TextMeshProUGUI textBody;
        [SerializeField]
        protected string endScene;

        [Header("Managers")]
        [SerializeField]
        protected DoublageManager doublageManager;
        [SerializeField]
        protected StoryEventManager storyEventManager;
        [SerializeField]
        protected EventBackgroundManager eventBackgroundManager;

        [Header("Feedbacks")]
        [SerializeField]
        protected Animator animatorBlackBand;
        [SerializeField]
        protected Image flashbackTransition;
        [SerializeField]
        protected GameObject viewportFlashback;
        [SerializeField]
        protected GameObject panelFlashback;







        protected bool eventStartLine = false;
        protected int indexEvent = -1;
        protected DoublageEventData currentEvent;

        bool clearText = false;
        bool clearAllText = false;
        CharacterDialogueController interloc;
        SkillManager skillManager;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetManagers(SkillManager sM)
        {
            skillManager = sM;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */


        public void SetCharactersSprites(List<VoiceActor> actorsContract)
        {
            for (int i = 0; i < actorsContract.Count; i++)
            {
                if (actorsContract[i] != null)
                {
                    characters[i].SetStoryCharacterData(actorsContract[i].SpriteSheets);
                    characters[i].gameObject.SetActive(true);
                }
            }
        }

        /*public Transform GetCharacterSprites(int actorIndex)
        {
            return characters[actorIndex].GetSpriteRenderer().transform;
        }*/


        public bool CheckStopSession()
        {
            return currentEvent.StopSession;
        }




        public virtual void PrintAllText(bool forceSkip = false)
        {
            if (forceSkip == false)
            {
                bool checkText = false;
                for (int i = 0; i < textEvent.Length; i++)
                {
                    if (textEvent[i].PrintAllText() == false)
                        checkText = true;
                }
                if (checkText == true)
                    return;
            }

            if(clearText == true)
            {
                clearText = false;
                //interloc.GetTextActing().NewPhrase(" ");
            }

            if(clearAllText == true)
            {
                clearAllText = false;
                for (int i = 0; i < textEvent.Length; i++)
                {
                    textEvent[i].NewPhrase(" ");
                }
                mainText.SetPauseText(true);
                mainText.HideText();
            }
            popup.gameObject.SetActive(false);
            inputEvent.gameObject.SetActive(false);
            ExecuteEvent();
        }


        // =========================================================================================
        private void ExecuteEvent()
        {
            indexEvent += 1;
            //inputController.gameObject.SetActive(false);
            DoublageEvent currentNode = currentEvent.GetEventNode(indexEvent);
            if (currentNode != null)
            {
                if (currentNode is DoublageEventText)
                {
                    DoublageEventText node = (DoublageEventText)currentNode;
                    inputEvent.gameObject.SetActive(true);
                    interloc = FindInterlocutor(node.Interlocuteur);
                    if (node.CameraID >= 0)
                    {
                        viewports[node.CameraID].TextCameraEffect(node);
                    }
                    /*if (node.ChangeViewportText == true)
                    {
                        interloc.SetTextActing(textEvent[node.CameraID]);
                        textEvent[node.CameraID].NewMouthAnim
                    }*/
                    clearAllText = node.ClearAllText;
                    clearText = node.ClearText;
                    if (interloc != null)
                    {
                        interloc.ChangeEmotion(node.EmotionNPC);
                        textEvent[node.CameraID].NewMouthAnim(interloc);
                        textEvent[node.CameraID].NewPhrase(node.Text);
                        //interloc.SetPhraseEventTextacting(node.Text, node.EmotionNPC);
                    }
                    else
                    {
                        PrintAllText(true);
                    }
                }



                else if (currentNode is DoublageEventViewport)
                {
                    animatorBlackBand.SetBool("Appear", true);
                    DoublageEventViewport node = (DoublageEventViewport)currentNode;
                    viewports[node.ViewportID].SetViewportSetting(node);
                    ExecuteEvent();
                }



                else if(currentNode is DoublageEventTextPopup)
                {
                    DoublageEventTextPopup node = (DoublageEventTextPopup)currentNode;
                    panelPlayer.StartPopup("Ingé son", node.Text);
                    ExecuteEvent();
                }



                else if(currentNode is DoublageEventWait)
                {
                    DoublageEventWait node = (DoublageEventWait)currentNode;
                    StartCoroutine(WaitCoroutine(node.Wait));
                }



                else if(currentNode is DoublageEventDeck)
                {
                    DoublageEventDeck node = (DoublageEventDeck)currentNode;
                    doublageManager.ModifyPlayerDeck(node.NewDeck, node.AddComboMax);
                    StartCoroutine(WaitCoroutine(1));
                }



                else if(currentNode is DoublageEventTutoPopup)
                {
                    DoublageEventTutoPopup node = (DoublageEventTutoPopup)currentNode;
                    popup.gameObject.SetActive(true);
                    textTitle.text = node.TitlePopup;
                    textBody.text = node.TextPopup;
                    inputEvent.gameObject.SetActive(true);
                }



                else if(currentNode is DoublageEventSound)
                {
                    DoublageEventSound node = (DoublageEventSound)currentNode;
                    if (node.Music == true)
                    {
                        if (node.StopMusic == true)
                        {
                            AudioManager.Instance.StopMusic(node.TimeTransition);
                        }
                        else
                        {
                            AudioManager.Instance.PlayMusic(node.MusicClip, node.TimeTransition);
                        }
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound(node.SoundClip);
                    }
                    ExecuteEvent();
                }



                else if(currentNode is DoublageEventLoad)
                {
                    DoublageEventLoad node = (DoublageEventLoad) currentNode;
                    if (node.DoublageEventData != null)
                    {
                        currentEvent = node.DoublageEventData;
                        indexEvent = -1;
                        ExecuteEvent();
                    }
                    else if (node.StoryEventData != null)
                    {
                        panelFlashback.SetActive(true);
                        StartCoroutine(TransitionFlashback(true));
                        storyEventManager.StartStoryEventDataWithScene(node.StoryEventData);
                    }
                }



                else if(currentNode is DoublageEventSetCharacter)
                {
                    DoublageEventSetCharacter node = (DoublageEventSetCharacter)currentNode;
                    characters.Add(Instantiate(characterPrefab, charactersParent));
                    //charactersMaxLength = 1;
                    characters[characters.Count-1].SetStoryCharacterData(node.Character);
                    //characters[characters.Count-1].SetTextActing(textEvent[node.ViewportID]);
                    characters[characters.Count-1].SetFlip(node.FlipX);
                    if(node.CustomPos == false)
                    {
                        characters[characters.Count-1].transform.localPosition = defaultCharacterTransform[node.DefaultPosID].localPosition;
                        characters[characters.Count-1].transform.localRotation = defaultCharacterTransform[node.DefaultPosID].localRotation;
                    }
                    else
                    {
                        characters[characters.Count-1].transform.localPosition = node.Position;
                        characters[characters.Count-1].transform.localRotation = Quaternion.Euler(node.Rotation);
                    }
                    ExecuteEvent();

                }



                else if (currentNode is DoublageEventEffect)
                {
                    DoublageEventEffect node = (DoublageEventEffect)currentNode;
                    viewports[node.ViewportID].StartEffect(node);
                    ExecuteEvent();

                }



                else if (currentNode is DoublageEventSkill)
                {
                    DoublageEventSkill node = (DoublageEventSkill) currentNode;
                    if (node.Skill is SkillActorData)
                        skillManager.ForceSkill((SkillActorData)node.Skill);
                    else
                        skillManager.ApplySkill(node.Skill);
                    StartCoroutine(WaitForceSkill());

                }


                else if (currentNode is DoublageEventMoveCharacter)
                {
                    DoublageEventMoveCharacter node = (DoublageEventMoveCharacter)currentNode;
                    FindInterlocutor(node.Character).ModifyCharacter(node);
                    ExecuteEvent();

                }




                else if (currentNode is DoublageEventResultScreen)
                {
                    DoublageEventResultScreen node = (DoublageEventResultScreen)currentNode;
                    if (node.LoadScene != null)
                        SceneManager.LoadScene(node.LoadScene, LoadSceneMode.Single);
                    else
                        doublageManager.ShowResultScreen();

                }

                else if (currentNode is DoublageEventInstantiate)
                {
                    eventBackgroundManager.InstantiateNewObject((DoublageEventInstantiate)currentNode);
                    ExecuteEvent();
                }

                else if (currentNode is DoublageEventCameraData)
                {
                    viewports[0].SetCameraData((DoublageEventCameraData)currentNode);
                    ExecuteEvent();
                }


            }
            else // Fin d'event
            {
                for (int i = 1; i < viewports.Length; i++)
                    viewports[i].SetCameraEffect(false);
                animatorBlackBand.SetBool("Appear", false);
                for (int i = 0; i < textEvent.Length; i++)
                {
                    textEvent[i].NewPhrase("");
                    textEvent[i].gameObject.SetActive(false);
                }
                if (currentEvent.StopSession == true)
                {
                    doublageManager.SwitchToDoublage();
                    mainText.SetPauseText(false);
                    mainText.ShowText();
                    if(eventStartLine == true)
                    {
                        doublageManager.SetPhrase();
                    }
                    else
                    {
                        doublageManager.NewTurn(true);
                    }

                    eventStartLine = false;
                }
            }
        }

        // =========================================================================================



        private IEnumerator WaitCoroutine(float time)
        {
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            ExecuteEvent();
        }

        private IEnumerator WaitForceSkill()
        {
            yield return doublageManager.WaitSkillManager();
            ExecuteEvent();
        }

        private CharacterDialogueController FindInterlocutor(StoryCharacterData characterToFind)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].gameObject.activeInHierarchy == false)
                    continue;
                if (characters[i].GetStoryCharacterData() == characterToFind)
                {
                    if (i < charactersActorLenght) // 3 car il y a 3 voice actors
                    {
                        mainText.SetPauseText(true);
                        mainText.HideText();
                    }
                    return characters[i];
                }
            }
            return null;
        }

        public void StartEvent(DoublageEventData storyEventData)
        {
            indexEvent = -1;
            currentEvent = storyEventData;
            for (int j = 0; j < textEvent.Length; j++)
            {
                textEvent[j].gameObject.SetActive(true);
            }
            eventStartLine = false;
            ExecuteEvent();
        }

        public bool CheckEvent(Contract contrat, int indexPhrase, bool startLine, float hp)
        {
            for (int i = 0; i < contrat.EventData.Count; i++)
            {
                if (CheckEventCondition(contrat.EventData[i], indexPhrase, startLine, hp) == true)
                {
                    indexEvent = -1;
                    currentEvent = contrat.EventData[i];
                    for (int j = 0; j < textEvent.Length; j++)
                    {
                        textEvent[j].gameObject.SetActive(true);                       
                    }
                    contrat.EventData.RemoveAt(i);
                    eventStartLine = startLine;
                    ExecuteEvent();
                    if (currentEvent.StopSession == true)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        private bool CheckEventCondition(DoublageEventData doublageEvent, int indexPhrase, bool startLine, float hp)
        {
            if (doublageEvent.PhraseNumber == indexPhrase)
            {
                if (doublageEvent.StartPhrase == true && startLine == true)
                {
                    return true;
                }
                else if (doublageEvent.StartPhrase == false)
                {
                    if (doublageEvent.Equal == true && doublageEvent.HpPercentage == hp)
                    {
                        return true;
                    }
                    else if (doublageEvent.Equal == false && doublageEvent.HpPercentage > hp)
                    {
                        return true;
                    }
                }
            }
            return false;
        }









        public void StopFlashback()
        {
            //indexEvent = -1;
            ExecuteEvent();
            StartCoroutine(TransitionFlashback(false));
        }






        protected IEnumerator TransitionFlashback(bool appear)
        {
            int time = 30;
            float speed = 1f / time;
            while(time != 0)
            {
                time -= 1;
                flashbackTransition.color += new Color(0, 0, 0, speed);
                yield return null;
            }
            viewportFlashback.SetActive(appear);

            speed = -speed;
            time = 45;
            while (time != 0)
            {
                time -= 1;
                flashbackTransition.color += new Color(0, 0, 0, speed);
                yield return null;
            }
            panelFlashback.SetActive(appear);
        }


























        #endregion

    } // DoublageEventManager class

}// #PROJECTNAME# namespace
