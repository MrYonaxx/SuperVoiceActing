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
        public StoryEventData StoryEventData
        {
            get { return storyEventData; }
        }

        [Title("Player")]
        [SerializeField]
        PlayerData playerData;
        public PlayerData PlayerData
        {
            get { return playerData; }
        }


        [Title("Managers")]
        [SerializeField]
        CalendarData calendarData;

        [Title("EventText")]
        [SerializeField]
        Image imageBackground;

        [SerializeField]
        TextMeshProUGUI textMeshPro;
        public TextMeshProUGUI TextMeshPro { get { return textMeshPro; } }

        [SerializeField]
        GameObject next;
        public GameObject Next { get { return next; } }

        [SerializeField]
        Animator animName;
        [SerializeField]
        Animator animatorTextbox;
        public Animator AnimatorTextbox { get { return animatorTextbox; } }

        [SerializeField]
        TextMeshProUGUI textName;
        [SerializeField]
        TextMeshProUGUI textDate;
        [SerializeField]
        TextMeshProUGUI textLocation;

        [Title("Characters")]
        [SerializeField]
        CharacterDialogueController characterPrefab;
        [SerializeField]
        Transform transformCharacter;
        [SerializeField]
        Transform cameraCenter;
        public Transform CameraCenter { get { return cameraCenter; } }

        [SerializeField]
        List<CharacterDialogueController> characters = new List<CharacterDialogueController>();
        public List<CharacterDialogueController> Characters { get { return characters; } }

        [Title("Story Effect")]
        [SerializeField]
        StoryEventChoiceManager storyEventChoiceManager;
        public StoryEventChoiceManager StoryEventChoiceManager { get { return storyEventChoiceManager; } }

        [SerializeField]
        StoryEventEffectManager storyEventEffectManager;
        public StoryEventEffectManager StoryEventEffectManager { get { return storyEventEffectManager; } }


        [SerializeField]
        UnityEvent unityEvent;


        List<StoryVariable> localVariables = new List<StoryVariable>();

        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public Dictionary<string, string> Dictionary { get { return dictionary; } }


        //private IEnumerator coroutineStory = null;
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
                StartCoroutine(StartEvent(storyEventData));
            }
            
        }

        private string StringReplace(string stringB, Dictionary<string, string> dict)
        {
            foreach (string k in dict.Keys)
            {
                stringB = stringB.Replace(k, dict[k]);
            }
            return stringB;
        }


        public void CreateScene(StoryEventData newStoryEvent)
        {
            if (newStoryEvent.CreateNewScene == false)
                return;
            textDate.text = calendarData.GetMonthName(playerData.Date.month-1) + " - Semaine " + playerData.Date.week;
            for (int i = 0; i < characters.Count; i++)
            {
                Destroy(characters[i].gameObject);
            }
            characters.Clear();

            if (newStoryEvent.Background != null)
            {
                imageBackground.sprite = newStoryEvent.Background.SpriteBackground;
                textLocation.text = newStoryEvent.Background.NameBackground;
                textLocation.text = StringReplace(textLocation.text, dictionary);
            }
            for (int i = 0; i < newStoryEvent.Characters.Length; i++)
            {
                characters.Add(Instantiate(characterPrefab, transformCharacter));
                characters[characters.Count - 1].SetVoiceActorData(newStoryEvent.Characters[i].CharacterMoving);
                characters[characters.Count - 1].SetPosition(newStoryEvent.Characters[i].NewPosition);
                //characters[characters.Count - 1].transform.localPosition = newStoryEvent.Characters[i].NewPosition;
                characters[characters.Count - 1].transform.localScale = newStoryEvent.Characters[i].NewScale;
                characters[characters.Count - 1].transform.eulerAngles = newStoryEvent.Characters[i].NewRotation;
                characters[characters.Count - 1].FadeCharacter(newStoryEvent.Characters[i].Appear, 1);
            }
        }


        /// <summary>
        /// Lance un event à la fin et créer une scène.
        /// </summary>
        /// <param name="storyEvent"></param>
        /// <returns></returns>
        public IEnumerator StartEvent(StoryEventData newStoryEvent)
        {
            CreateScene(newStoryEvent);
            storyEventEffectManager.Fade(false, 60);
            storyEventEffectManager.Tint(Color.clear, 60);
            yield return null;
            yield return ExecuteEvent(newStoryEvent);
            unityEvent.Invoke();
        }

        /// <summary>
        /// N'active pas d'event en fin de coroutine
        /// </summary>
        /// <param name="storyEvent"></param>
        /// <returns></returns>
        public IEnumerator ExecuteEvent(StoryEventData storyEvent)
        {
            for(int i = 0; i < storyEvent.GetEventSize(); i++)
            {
                currentNode = storyEvent.GetEventNode(i);
                HideName();
                if (currentNode.InstantNodeCoroutine() == true)
                    StartCoroutine(currentNode.ExecuteNodeCoroutine(this));
                else
                    yield return currentNode.ExecuteNodeCoroutine(this);
            }
            // ==============================================================================================================================
            /*else if (currentNode is StoryEventChoices)
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
                    storyEventData = node.StoryEvent;
                    i = -1;
                }
            }
            else if (currentNode is StoryEventConditionActor)
            {
                StoryEventConditionActor node = (StoryEventConditionActor)currentNode;
                storyEventData = node.SetNode(localVariables, playerData);
                CreateScene();
                i = -1;
            }*/
            // ==============================================================================================================================
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





        public void DrawName(string characterName)
        {
            if (characterName.Equals("[PlayerName]"))
                textName.text = playerData.PlayerName;
            else
                textName.text = characterName;
            if (coroutineAnimName != null)
                StopCoroutine(coroutineAnimName);
            coroutineAnimName = ScaleAnimName(0.7f, false);
            StartCoroutine(coroutineAnimName);
            animName.Play("ANIM_BandeRouge");
        }


        public void HideName()
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