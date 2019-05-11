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
        /*[SerializeField]
        AudioSource audioSourceText;
        */
        [Title("Characters")]
        [SerializeField]
        CharacterDialogueController characterPrefab;
        [SerializeField]
        Transform transformCharacter;

        List<CharacterDialogueController> characters = new List<CharacterDialogueController>();

        [Title("DoublageEventManager (Acting only)")]
        [SerializeField]
        DoublageEventManager doublageEventManager;


        private IEnumerator coroutineAnimName = null;

        StoryEvent currentNode;
        int i = 0;


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

        ////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected void Awake()
        {
            
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            if (storyEventData != null)
            {
                CreateScene();
                StartCoroutine(NextNodeCoroutine());
            }
            
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        /*protected void Update()
        {
            
        }*/

        public void CreateScene()
        {
            imageBackground.sprite = storyEventData.Background;
            for(int i = 0; i < storyEventData.Characters.Length; i++)
            {
                characters.Add(Instantiate(characterPrefab, transformCharacter));
                characters[characters.Count - 1].SetStoryCharacterData(storyEventData.Characters[i].CharacterToMove);
                characters[characters.Count - 1].transform.position = storyEventData.Characters[i].NewPosition;
                characters[characters.Count - 1].transform.localScale = storyEventData.Characters[i].NewScale;
                characters[characters.Count - 1].transform.eulerAngles = storyEventData.Characters[i].NewRotation;
            }
        }


        public void StartStoryEventData(StoryEventData newStoryEvent)
        {
            storyEventData = newStoryEvent;
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
                    node.SetNode(textMeshPro, characters, next);
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
                        i = -1;
                    }
                    else if (node.GetDoublageEventToLoad() != null)
                    {
                        doublageEventManager.StopFlashback(node.GetDoublageEventToLoad());
                    }
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