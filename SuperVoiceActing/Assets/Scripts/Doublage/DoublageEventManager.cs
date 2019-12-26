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
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class DoublageEventManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Events")]
        [SerializeField]
        protected PanelPlayer panelPlayer;
        public PanelPlayer PanelPlayer
        {
            get { return panelPlayer; }
        }

        [SerializeField]
        protected InputController inputEvent;
        public InputController InputEvent
        {
            get { return inputEvent; }
        }

        [SerializeField]
        protected TextAppearManager mainText;
        public TextAppearManager MainText
        {
            get { return mainText; }
        }

        [SerializeField]
        protected ViewportManager[] viewports;
        public ViewportManager[] Viewports
        {
            get { return viewports; }
        }

        [SerializeField]
        protected TextPerformanceAppear[] textEvent;
        public TextPerformanceAppear[] TextEvent
        {
            get { return textEvent; }
        }




        [Title("Characters")]
        [SerializeField]
        protected CharacterDialogueController characterPrefab;
        public CharacterDialogueController CharacterPrefab
        {
            get { return characterPrefab; }
        }

        [SerializeField]
        protected Transform charactersParent;
        public Transform CharactersParent
        {
            get { return charactersParent; }
        }

        [SerializeField]
        protected int charactersActorLenght = 3;
        public int CharactersActorLenght
        {
            get { return charactersActorLenght; }
        }

        [SerializeField]
        protected List<CharacterDialogueController> characters = new List<CharacterDialogueController>();
        public List<CharacterDialogueController> Characters
        {
            get { return characters; }
        }

        [SerializeField]
        protected Transform[] defaultCharacterTransform;

        [Title("Tuto & Fin")]
        [SerializeField]
        protected GameObject popup;
        [SerializeField]
        protected TextMeshProUGUI textTitle;
        [SerializeField]
        protected TextMeshProUGUI textBody;

        [Title("Managers")]
        /*[SerializeField]
        protected DoublageManager doublageManager;*/

        [SerializeField]
        protected StoryEventManager storyEventManager;
        public StoryEventManager StoryEventManager
        {
            get { return storyEventManager; }
        }

        [SerializeField]
        protected EventBackgroundManager eventBackgroundManager;
        public EventBackgroundManager EventBackgroundManager
        {
            get { return eventBackgroundManager; }
        }

        [Title("Feedbacks")]
        [SerializeField]
        protected Animator animatorBlackBand;
        public Animator AnimatorBlackBand
        {
            get { return animatorBlackBand; }
        }

        [SerializeField]
        protected Image flashbackTransition;
        [SerializeField]
        protected GameObject viewportFlashback;
        [SerializeField]
        protected GameObject panelFlashback;






        //protected int indexEvent = -1;
        protected List<DoublageEventData> globalEvents = new List<DoublageEventData>();
        protected List<DoublageEventData> currentEvents = new List<DoublageEventData>();

        /*bool clearText = false;
        bool clearAllText = false;*/
        //SkillManager skillManager;

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

        public void SetManagers(SkillManager sM, List<DoublageEventData> contractEvents)
        {
            //skillManager = sM;
            globalEvents = contractEvents;
        }

        /*public bool CheckStopSession()
        {
            return currentEvent.StopSession;
        }*/




        public virtual void PrintAllText(bool forceSkip = false)
        {
            /*if (forceSkip == false)
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
            //ExecuteEvent();*/
        }


        // =========================================================================================

        public IEnumerator StartEvent()
        {
            for(int i = 0; i < currentEvents.Count; i++)
            {
                if (currentEvents[i].StopSession == true)
                {
                    yield return ExecuteEvent(currentEvents[i]);
                }
                else
                {
                    StartCoroutine(ExecuteEvent(currentEvents[i]));
                }
            }
            currentEvents.Clear();
            animatorBlackBand.SetBool("Appear", false);
        }


        public IEnumerator ExecuteEvent(DoublageEventData currentEvent)
        {
            DoublageEvent currentNode = null;
            for(int i = 0; i < currentEvent.GetEventSize(); i++)
            {
                currentNode = currentEvent.GetEventNode(i);

                yield return currentNode.ExecuteNodeCoroutine(this);
            }

            /*for (int i = 1; i < viewports.Length; i++)
                viewports[i].SetCameraEffect(false);
            for (int i = 0; i < textEvent.Length; i++)
            {
                textEvent[i].NewPhrase("");
                textEvent[i].gameObject.SetActive(false);
            }
            if (storyEventData.StopSession == true)
            {
                doublageManager.SwitchToDoublage();
                mainText.SetPauseText(false);
                mainText.ShowText();
                if (eventStartLine == true)
                {
                    doublageManager.SetPhrase();
                }
                else
                {
                    doublageManager.NewTurn(true);
                }

                eventStartLine = false;
            }*/

            /*if (currentNode != null)
            {
                if(currentNode.ExecuteNode(this) == false)
                    StartCoroutine(currentNode.ExecuteNodeCoroutine(this));

                yield return currentNode.ExecuteNodeCoroutine(this);
                StartCoroutine(ExecuteEvent());

                if (currentNode is DoublageEventDeck)
                {
                    DoublageEventDeck node = (DoublageEventDeck)currentNode;
                    doublageManager.ModifyPlayerDeck(node.NewDeck, node.AddComboMax);
                    //StartCoroutine(WaitCoroutine(1));
                }



                else if(currentNode is DoublageEventTutoPopup)
                {
                    DoublageEventTutoPopup node = (DoublageEventTutoPopup)currentNode;
                    popup.gameObject.SetActive(true);
                    textTitle.text = node.TitlePopup;
                    textBody.text = node.TextPopup;
                    inputEvent.gameObject.SetActive(true);
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
                    //FindInterlocutor(node.Character).ModifyCharacter(node);
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


            }*/
            /*else // Fin d'event
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
            }*/
        }

        public void LoadEvent(DoublageEventData doublageEventData)
        {
            currentEvents.Add(doublageEventData);
        }

        // =========================================================================================



        // Check s'il y a des events à jouer et si oui on les met dans la liste
        public bool CheckEvent(int indexPhrase, bool startLine, float hp)
        {
            bool result = false;
            for (int i = 0; i < globalEvents.Count; i++)
            {
                if (CheckEventCondition(globalEvents[i], indexPhrase, startLine, hp) == true)
                {
                    currentEvents.Add(globalEvents[i]);
                    globalEvents.RemoveAt(i);
                    i -= 1;
                    result = true;
                }
            }
            return result;
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









        /*public void StopFlashback()
        {
            //indexEvent = -1;
            ExecuteEvent();
            StartCoroutine(TransitionFlashback(false));
        }*/



        public void CreateCharacters(DoublageEventSetCharacter node)
        {
            characters.Add(Instantiate(characterPrefab, charactersParent));
            characters[characters.Count - 1].SetStoryCharacterData(node.Character);
            characters[characters.Count - 1].SetFlip(node.FlipX);
            if (node.CustomPos == false)
            {
                characters[characters.Count - 1].transform.localPosition = defaultCharacterTransform[node.DefaultPosID].localPosition;
                characters[characters.Count - 1].transform.localRotation = defaultCharacterTransform[node.DefaultPosID].localRotation;
            }
            else
            {
                characters[characters.Count - 1].transform.localPosition = node.Position;
                characters[characters.Count - 1].transform.localRotation = Quaternion.Euler(node.Rotation);
            }
            characters[characters.Count - 1].ChangeOrderInLayer(node.OrderLayer);
            characters[characters.Count - 1].ChangeTint(node.Color);
        }






        #endregion

    } // DoublageEventManager class

}// #PROJECTNAME# namespace
