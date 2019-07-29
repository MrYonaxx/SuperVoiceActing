/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the StoryEventManager class
    /// </summary>
    public class StoryEventManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("Event")]
        [SerializeField]
        StoryEventData storyEventData;

        [Title("Player")]
        [SerializeField]
        PlayerData playerData;

        [Title("EventText")]
        [SerializeField]
        Image imageBackground;
        [SerializeField]
        TextMeshProUGUI textMeshPro;
        [SerializeField]
        GameObject next;
        [SerializeField]
        Animator animName;
        [SerializeField]
        TextMeshProUGUI textName;
        [SerializeField]
        TextMeshProUGUI textDate;
        [SerializeField]
        TextMeshProUGUI textLocation;
        /*[SerializeField]
        AudioSource audioSourceText;
        */
        [Title("Characters")]
        [SerializeField]
        CharacterDialogueController characterPrefab;
        [SerializeField]
        Transform transformCharacter;

        [SerializeField]
        List<CharacterDialogueController> characters = new List<CharacterDialogueController>();

        [Title("Story Effect")]
        [SerializeField]
        StoryEventChoiceManager storyEventChoiceManager;
        [SerializeField]
        StoryEventEffectManager storyEventEffectManager;

        [FoldoutGroup("Optionnel")]
        [Title("DoublageEventManager (Acting only)")]
        [SerializeField]
        DoublageEventManager doublageEventManager;

        [FoldoutGroup("Optionnel")]
        [Title("PhoneCall")]
        [SerializeField]
        UnityEvent unityEvent;


        List<StoryVariable> localVariables = new List<StoryVariable>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();


        private IEnumerator coroutineStory = null;
        private IEnumerator coroutineAnimName = null;

        StoryEvent currentNode;
        int i = 0;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */
        public bool DebugData()
        {
            if (storyEventData != null)
                return true;
            return false;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Awake()
        {
            CreateDictionnary();
            if (storyEventData != null)
            {
                CreateScene();
                StartCoroutine(NextNodeCoroutine());
            }
            
        }
        

        public void CreateScene()
        {
            if(playerData.Date.month != 0)
                textDate.text = playerData.MonthName[playerData.Date.month-1] + " - Semaine " + playerData.Date.week;
            for (int i = 0; i < characters.Count; i++)
            {
                Destroy(characters[i].gameObject);
            }
            characters.Clear();
            imageBackground.sprite = storyEventData.Background;
            for(int i = 0; i < storyEventData.Characters.Length; i++)
            {
                characters.Add(Instantiate(characterPrefab, transformCharacter));
                characters[characters.Count - 1].SetStoryCharacterData(storyEventData.Characters[i].CharacterToMove);
                characters[characters.Count - 1].transform.localPosition = storyEventData.Characters[i].NewPosition;
                characters[characters.Count - 1].transform.localScale = storyEventData.Characters[i].NewScale;
                characters[characters.Count - 1].transform.eulerAngles = storyEventData.Characters[i].NewRotation;
                characters[characters.Count - 1].FadeCharacter(storyEventData.Characters[i].FadeIn, 1);
            }
        }


        public void CreateScene(StoryEventData newStoryEvent)
        {
            textDate.text = playerData.MonthName[playerData.Date.month-1] + " - Semaine " + playerData.Date.week;
            for (int i = 0; i < characters.Count; i++)
            {
                Destroy(characters[i].gameObject);
            }
            characters.Clear();
            imageBackground.sprite = newStoryEvent.Background;
            for (int i = 0; i < newStoryEvent.Characters.Length; i++)
            {
                characters.Add(Instantiate(characterPrefab, transformCharacter));
                characters[characters.Count - 1].SetStoryCharacterData(newStoryEvent.Characters[i].CharacterToMove);
                characters[characters.Count - 1].transform.localPosition = newStoryEvent.Characters[i].NewPosition;
                characters[characters.Count - 1].transform.localScale = newStoryEvent.Characters[i].NewScale;
                characters[characters.Count - 1].transform.eulerAngles = newStoryEvent.Characters[i].NewRotation;
                characters[characters.Count - 1].FadeCharacter(newStoryEvent.Characters[i].FadeIn, 1);
            }
        }


        public void StartStoryEventData(StoryEventData newStoryEvent)
        {
            i = 0;
            storyEventData = newStoryEvent;
            StartCoroutine(NextNodeCoroutine());
        }

        public void StartStoryEventDataWithScene(StoryEventData newStoryEvent)
        {
            i = 0;
            storyEventData = newStoryEvent;
            CreateScene();
            storyEventEffectManager.Fade(false, 60);
            storyEventEffectManager.Tint(Color.clear, 60);
            StartCoroutine(NextNodeCoroutine());
        }

        private IEnumerator NextNodeCoroutine()
        {
            currentNode = storyEventData.GetEventNode(i);
            if (currentNode != null)
            {
                HideName();

                // ==============================================================================================================================
                if (currentNode is StoryEventText)
                {
                    StoryEventText node = (StoryEventText) currentNode;
                    node.SetLanguage(playerData.Language);
                    node.SetNode(textMeshPro, characters, next, dictionary);
                    DrawName(node.GetInterlocuteur());
                }

                else if (currentNode is StoryEventMoveCharacter)
                {
                    StoryEventMoveCharacter node = (StoryEventMoveCharacter)currentNode;
                    node.SetNode(characters);
                    if(node.GetWaitEnd() == false)
                    {
                        StartCoroutine(node.MoveCoroutine());
                    }
                }

                else if (currentNode is StoryEventLoad)
                {
                    StoryEventLoad node = (StoryEventLoad) currentNode;
                    if (node.GetDataToLoad() != null)
                    {
                        storyEventData = node.GetDataToLoad();
                        if(node.SaveSceneConfiguration() == false)
                            CreateScene();
                        i = -1;
                    }
                    else if (node.GetDoublageEventToLoad() != null)
                    {
                        doublageEventManager.StopFlashback(node.GetDoublageEventToLoad());
                    }
                }

                else if (currentNode is StoryEventEffect)
                {
                    StoryEventEffect node = (StoryEventEffect)currentNode;
                    node.SetNode(storyEventEffectManager);
                }

                else if (currentNode is StoryEventPlayerData)
                {
                    StoryEventPlayerData node = (StoryEventPlayerData)currentNode;
                    node.SetNode(playerData);
                }
                else if (currentNode is StoryEventChoices)
                {
                    storyEventChoiceManager.DrawChoices((StoryEventChoices)currentNode);
                }

                else if (currentNode is StoryEventVariable)
                {
                    StoryEventVariable node = (StoryEventVariable)currentNode;
                    node.SetNode(localVariables, playerData, dictionary);
                }
                else if (currentNode is StoryEventConditions)
                {
                    StoryEventConditions node = (StoryEventConditions)currentNode;
                    if(node.CheckCondition(localVariables, playerData) == false)
                    {
                        while(!(currentNode is StoryEventEndConditions))
                        {
                            i += 1;
                            currentNode = storyEventData.GetEventNode(i);
                        }
                    }
                }
                else if (currentNode is StoryEventConditionActor)
                {
                    StoryEventConditionActor node = (StoryEventConditionActor)currentNode;
                    storyEventData = node.SetNode(localVariables, playerData);
                    CreateScene();
                    i = -1;
                }
                // ==============================================================================================================================


                yield return StartCoroutine(currentNode.GetStoryEvent());
                if (currentNode is StoryEventText)
                {
                    //audioSourceText.Play();
                }
                i += 1;
                StartCoroutine(NextNodeCoroutine());
            }
            else
            {
                unityEvent.Invoke();
            }
        }


        public void LoadNewStoryEvent(StoryEventData data)
        {
            storyEventData = data;
            i = -1;
        }









        private void CreateDictionnary()
        {
            dictionary.Add("[PlayerName]", playerData.PlayerName);
            dictionary.Add("[StudioName]", playerData.StudioName);
        }





        private void DrawName(StoryCharacterData interlocuteur)
        {
            if (interlocuteur == null)
            {
                HideName();
                return;
            }
            textName.text = interlocuteur.GetName();
            if (coroutineAnimName != null)
                StopCoroutine(coroutineAnimName);
            coroutineAnimName = ScaleAnimName(0.7f, false);
            StartCoroutine(coroutineAnimName);
            animName.Play("ANIM_BandeRouge");
        }


        private void HideName()
        {
            if (coroutineAnimName != null)
                StopCoroutine(coroutineAnimName);
            coroutineAnimName = ScaleAnimName(0, true);
            StartCoroutine(coroutineAnimName);
        }

        private IEnumerator ScaleAnimName(float target, bool changeRotation)
        {
            while(animName.transform.localScale.y < target - 0.05f || animName.transform.localScale.y > target + 0.05f)
            {
                if (animName.transform.localScale.y < target)
                    animName.transform.localScale += new Vector3(0, 0.05f, 0);
                else if (animName.transform.localScale.y > target)
                    animName.transform.localScale -= new Vector3(0, 0.05f, 0);
                yield return null;
            }
            animName.transform.localScale = new Vector3(animName.transform.localScale.x, target, animName.transform.localScale.z);
            if (changeRotation == true)
                animName.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5, 0));
        }

        #endregion

    } // StoryEventManager class

} // #PROJECTNAME# namespace