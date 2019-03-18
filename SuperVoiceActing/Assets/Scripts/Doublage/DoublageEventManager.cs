﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        protected CameraController[] cameraControllerEvent;
        [SerializeField]
        protected CharacterDialogueController[] characters;
        [SerializeField]
        protected TextPerformanceAppear[] textEvent;
        [SerializeField]
        protected GameObject[] popups;
        [SerializeField]
        protected string endScene;


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





        public virtual void PrintAllText()
        {
            /*if (textAppearManager.PrintAllText() == false)
            {
                return;
            }*/
            for (int i = 0; i < textEvent.Length; i++)
            {
                if (textEvent[i].PrintAllText() == false)
                    return;
            }
            for (int i = 0; i < popups.Length; i++)
            {
                popups[i].SetActive(false);
            }
            inputEvent.gameObject.SetActive(false);
            ExecuteEvent();
        }


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
                    FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.Text, node.CameraEffectID);
                    /*if (playerData.Language == 1)
                        FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.TextEng, node.CameraEffectID);
                    else
                        FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.Text, node.CameraEffectID);*/
                }
                if (currentNode is DoublageEventCamera)
                {
                    DoublageEventCamera node = (DoublageEventCamera)currentNode;
                    cameraControllerEvent[node.CameraID].ChangeCameraViewport(node.ViewportX, node.ViewportY, node.ViewportWidth, node.ViewportHeight, node.Time);
                    ExecuteEvent();
                }
                if (currentNode is DoublageEventTextPopup)
                {
                    DoublageEventTextPopup node = (DoublageEventTextPopup)currentNode;
                    panelPlayer.StartPopup("Ingé son", node.Text);
                    ExecuteEvent();
                }
                if (currentNode is DoublageEventWait)
                {
                    DoublageEventWait node = (DoublageEventWait)currentNode;
                    StartCoroutine(WaitCoroutine(node.Wait));
                }
                if (currentNode is DoublageEventDeck)
                {
                    DoublageEventDeck node = (DoublageEventDeck)currentNode;
                    //emotionAttackManager.ModifiyDeck(node.NewDeck);
                    StartCoroutine(WaitCoroutine(1));
                }
                if (currentNode is DoublageEventTutoPopup)
                {
                    DoublageEventTutoPopup node = (DoublageEventTutoPopup)currentNode;
                    popups[node.PopupID].SetActive(true);
                    inputEvent.gameObject.SetActive(true);
                }
                if (currentNode is DoublageEventSound)
                {
                    DoublageEventSound node = (DoublageEventSound)currentNode;
                    //audioSourceKillPhrase.PlayOneShot(node.Audio);
                    ExecuteEvent();
                }
            }
            else // Fin d'event
            {
                /*emotionAttackManager.SwitchCardTransformToBattle();
                SetPhrase();*/
            }
        }

        private IEnumerator WaitCoroutine(float time)
        {
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            ExecuteEvent();
        }

        private CharacterDialogueController FindInterlocutor(StoryCharacterData characterToFind)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].GetStoryCharacterData() == characterToFind)
                {
                    return characters[i];
                }
            }
            return null;
        }



        public bool CheckEvent(Contract contrat, int indexPhrase, bool startLine, float hp)
        {

            for (int i = 0; i < contrat.EventData.Length; i++)
            {
                if (CheckEventCondition(contrat.EventData[i], indexPhrase, startLine, hp) == true)
                {
                    indexEvent = -1;
                    currentEvent = contrat.EventData[i];
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

        #endregion

    } // DoublageEventManager class
	
}// #PROJECTNAME# namespace
