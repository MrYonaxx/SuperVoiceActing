﻿/*****************************************************************
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

        /*[SerializeField]
        TextPerformanceAppear textDirector;

        [SerializeField]
        InputController inputText;*/
        [SerializeField]
        string[] placeHolderText;



        /*int index = 0;
        int indexText = 0;*/

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
        /*protected void Awake()
        {
            
        }*/


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        /*protected override void Start()
        {
            inputText.enabled = true;
            //StartCoroutine(TutorialSequence());
        }*/
        
        /*public void PrintAllText()
        {
            if (textPerformanceAppear.PrintAllText() == true && textDirector.PrintAllText() == true)
            {
                PhaseTuto();
            }
        }*/


        /*public void PhaseTuto()
        {
            switch(index)
            {
                case 0:
                    indexText += 1;
                    textPerformanceAppear.NewPhrase(placeHolderText[indexText]);
                    break;
                case 1:
                    cameraController.IngeSon3();
                    indexText += 1;
                    textDirector.NewPhrase(placeHolderText[indexText]);
                    break;
                case 2:
                    //cameraController.IngeSon3();
                    indexText += 1;
                    textDirector.NewPhrase(placeHolderText[indexText]);
                    break;
                case 3:
                    //cameraController.IngeSon3();
                    indexText += 1;
                    textDirector.NewPhrase(placeHolderText[indexText]);
                    break;
                case 4:
                    emotionAttackManager.ModifiyDeck(Emotion.Joie, 1);
                    cameraController.IngeSon3Cancel();
                    indexText += 1;
                    textPerformanceAppear.NewPhrase(placeHolderText[indexText]);
                    break;
                case 5:
                    emotionAttackManager.SwitchCardTransformIntro();
                    inputController.enabled = true;
                    break;
            }
            index += 1;
        }*/

        /*private IEnumerator TutorialSequence()
        {
            //yield return new WaitForSeconds()
        }*/

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        /*protected void Update()
        {

        }
        */
        #endregion

    } // DoublageTutorialManager class

} // #PROJECTNAME# namespace