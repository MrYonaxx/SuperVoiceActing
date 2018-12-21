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

        [Header("EventText")]
        [SerializeField]
        TextMeshPro textMeshPro;

        [Header("Characters")]
        [SerializeField]
        CharacterDialogueController[] characters;




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
                if (currentNode is StoryEventText)
                {
                    StoryEventText node = (StoryEventText) currentNode;
                    node.SetNode(textMeshPro, characters);
                }
                if (currentNode is StoryEventMoveCharacter)
                {
                    StoryEventMoveCharacter node = (StoryEventMoveCharacter)currentNode;
                    node.SetNode(characters);
                }
                yield return StartCoroutine(currentNode.GetStoryEvent());
                i += 1;
                StartCoroutine(NextNodeCoroutine());
            }
        }
        
        #endregion

    } // StoryEventManager class

} // #PROJECTNAME# namespace