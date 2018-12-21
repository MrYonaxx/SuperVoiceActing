/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageTutorialManager class
    /// </summary>
    public class DoublageTutorialManager : DoublageManager
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Tutorial")]

        [SerializeField]
        InputController inputText;
        [SerializeField]
        string[] placeHolderText;

        int index = 0;
        int indexText = 0;

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
        protected override void Start()
        {
            inputText.enabled = true;
            //StartCoroutine(TutorialSequence());
        }
        
        public void PrintAllText()
        {
            if (textPerformanceAppear.PrintAllText() == true)
            {
                PhaseTuto();
                /*index += 1;
                textPerformanceAppear.NewPhrase(placeHolderText[index]);*/
            }
        }


        public void PhaseTuto()
        {
            switch(index)
            {
                case 0:
                    indexText += 1;
                    textPerformanceAppear.NewPhrase(placeHolderText[indexText]);
                    break;
                case 1:
                    cameraController.IngeSon3();
                    break;
            }
            index += 1;
        }

        /*private IEnumerator TutorialSequence()
        {
            //yield return new WaitForSeconds()
        }*/

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {

        }
        
        #endregion

    } // DoublageTutorialManager class

} // #PROJECTNAME# namespace