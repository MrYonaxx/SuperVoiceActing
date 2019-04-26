/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        protected List<CharacterDialogueController> characters = new List<CharacterDialogueController>();
        [SerializeField]
        protected Transform[] defaultCharacterTransform;



        [Header("Tuto & Fin")]
        /*[SerializeField]
        protected GameObject[] popups;*/
        [SerializeField]
        protected string endScene;

        [Header("Managers")]
        [SerializeField]
        protected DoublageManager doublageManager;
        [SerializeField]
        protected StoryEventManager storyEventManager;

        [Header("Feedbacks")]
        [SerializeField]
        protected Animator animatorBlackBand;
        [SerializeField]
        protected Image flashbackTransition;
        [SerializeField]
        protected GameObject viewportFlashback;

        protected bool eventStartLine = false;
        protected int indexEvent = -1;
        protected DoublageEventData currentEvent;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */



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
                    characters[i].SetStoryCharacterData(actorsContract[i].SpriteSheets);
            }
        }


        public bool CheckStopSession()
        {
            return currentEvent.StopSession;
        }




        public virtual void PrintAllText()
        {
            bool checkText = false;
            for (int i = 0; i < textEvent.Length; i++)
            {
                if (textEvent[i].PrintAllText() == false)
                    checkText = true;
            }
            if (checkText == true)
                return;
            /*for (int i = 0; i < popups.Length; i++)
            {
                popups[i].SetActive(false);
            }*/
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
                    FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.Text);
                    if(node.CameraID != 0)
                        viewports[node.CameraID].TextCameraEffect(node);
                    /*if (playerData.Language == 1)
                        FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.TextEng, node.CameraEffectID);
                    else
                        FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.Text, node.CameraEffectID);*/
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
                    //emotionAttackManager.ModifiyDeck(node.NewDeck);
                    StartCoroutine(WaitCoroutine(1));
                }

                else if(currentNode is DoublageEventTutoPopup)
                {
                    DoublageEventTutoPopup node = (DoublageEventTutoPopup)currentNode;
                    //popups[node.PopupID].SetActive(true);
                    inputEvent.gameObject.SetActive(true);
                }

                else if(currentNode is DoublageEventSound)
                {
                    DoublageEventSound node = (DoublageEventSound)currentNode;
                    //audioSourceKillPhrase.PlayOneShot(node.Audio);
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
                        StartFlashback(node.StoryEventData);
                    }
                }

                else if(currentNode is DoublageEventSetCharacter)
                {
                    DoublageEventSetCharacter node = (DoublageEventSetCharacter)currentNode;
                    characters.Add(Instantiate(characterPrefab, charactersParent));
                    //charactersMaxLength = 1;
                    characters[characters.Count-1].SetStoryCharacterData(node.Character);
                    characters[characters.Count-1].SetTextActing(textEvent[node.ViewportID]);
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
                    if(node.Skill is SkillActorData)
                        doublageManager.ForceSkill((SkillActorData) node.Skill);
                    StartCoroutine(WaitForceSkill());

                }




            }
            else // Fin d'event
            {
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
                if (characters[i].GetStoryCharacterData() == characterToFind)
                {
                    if (i < 3) // 3 car il y a 3 voice actors
                    {
                        mainText.SetPauseText(true);
                        mainText.HideText();
                    }
                    return characters[i];
                }
            }
            return null;
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
                    return true;
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






        public void StartFlashback(StoryEventData storyEventData)
        {
            storyEventManager.StartStoryEventData(storyEventData);
            StartCoroutine(TransitionFlashback(true));
        }




        public void StopFlashback(DoublageEventData doublageEventData)
        {
            currentEvent = doublageEventData;
            indexEvent = -1;
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
        }


























        #endregion

    } // DoublageEventManager class

}// #PROJECTNAME# namespace
