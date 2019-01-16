/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;

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
        [Header("Event")]
        [SerializeField]
        StoryEventData storyEventData;

        [Header("Player")]
        [SerializeField]
        PlayerData playerData;

        [Header("EventText")]
        [SerializeField]
        TextMeshPro textMeshPro;
        [SerializeField]
        GameObject next;
        [SerializeField]
        Animator animName;
        [SerializeField]
        TextMeshPro textName;
        [SerializeField]
        AudioSource audioSourceText;

        [Header("Characters")]
        [SerializeField]
        CharacterDialogueController[] characters;


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
            StartCoroutine(NextNodeCoroutine());
            
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        /*protected void Update()
        {
            
        }*/


        private IEnumerator NextNodeCoroutine()
        {
            currentNode = storyEventData.GetEventNode(i);
            if (currentNode != null)
            {
                HideName();
                if (currentNode is StoryEventText)
                {
                    StoryEventText node = (StoryEventText) currentNode;
                    node.SetLanguage(playerData.Language);
                    node.SetNode(textMeshPro, characters, next);
                    DrawName(node.GetInterlocuteur());
                }
                if (currentNode is StoryEventMoveCharacter)
                {
                    StoryEventMoveCharacter node = (StoryEventMoveCharacter)currentNode;
                    node.SetNode(characters);
                    if(node.GetWaitEnd() == false)
                    {
                        StartCoroutine(node.MoveCoroutine());
                    }
                }
                if (currentNode is StoryEventLoad)
                {
                    StoryEventLoad node = (StoryEventLoad) currentNode;
                    storyEventData = node.GetDataToLoad();
                    i = -1;
                }
                yield return StartCoroutine(currentNode.GetStoryEvent());
                if (currentNode is StoryEventText)
                {
                    audioSourceText.Play();
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