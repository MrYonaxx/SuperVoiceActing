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
using TMPro;

namespace VoiceActing
{
	public class StoryEventChoiceManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        StoryEventManager storyEventManager;

        [SerializeField]
        Animator[] animatorsZoom;

        [SerializeField]
        Animator[] animatorsChoices;
        [SerializeField]
        TextMeshProUGUI[] textChoices;

        [SerializeField]
        GameObject choicePanel;
        [SerializeField]
        InputController inputController;

        [SerializeField]
        TextMeshProUGUI textBox;


        StoryEventChoices choicesData;
        int indexSelected = -1;

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

        public void DrawChoices(StoryEventChoices data)
        {
            indexSelected = -1;
            choicePanel.SetActive(true);
            ChoiceDezoomOn();
            choicesData = data;
            textBox.text = data.Text;
            StartCoroutine(ChoicesCoroutineAppear());

        }

        private IEnumerator ChoicesCoroutineAppear()
        {
            for (int i = 0; i < choicesData.Answers.Length; i++)
            {
                //animatorsChoices[i].ResetTrigger("Unselected");
                animatorsChoices[i].gameObject.SetActive(true);
                textChoices[i].text = choicesData.Answers[i];
                yield return new WaitForSeconds(0.1f);
            }
            inputController.gameObject.SetActive(true);
        }


        public void SelectUp()
        {
            if (indexSelected != -1)
            {
                animatorsChoices[indexSelected].SetTrigger("Unselected");
                indexSelected -= 1;
                if (indexSelected < 0)
                    indexSelected = choicesData.Answers.Length - 1;
            }
            else
            {
                indexSelected = 0;
            }

            animatorsChoices[indexSelected].SetTrigger("Selected");
        }

        public void SelectDown()
        {
            if (indexSelected != -1)
            {
                animatorsChoices[indexSelected].SetTrigger("Unselected");
                indexSelected += 1;
                if (indexSelected == choicesData.Answers.Length)
                    indexSelected = 0;
            }
            else
            {
                indexSelected = 0;
            }

            animatorsChoices[indexSelected].SetTrigger("Selected");
        }


        public void Validate(int index)
        {
            indexSelected = index;
            Validate();
        }


        public void Validate()
        {
            if (indexSelected == -1)
                return;
            ChoiceDezoomOff();
            inputController.gameObject.SetActive(false);
            storyEventManager.LoadNewStoryEvent(choicesData.StoryChoices[indexSelected]);
            for (int i = 0; i < animatorsChoices.Length; i++)
            {
                if (i == indexSelected)
                    animatorsChoices[i].SetTrigger("Validate");
                else
                    animatorsChoices[i].SetTrigger("Disappear");
            }
            StartCoroutine(WaitEnd());
        }

        private IEnumerator WaitEnd()
        {
            int time = 60;
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            choicesData.StopCoroutine();
        }


        [ContextMenu("Dezoom")]
        public void ChoiceDezoomOn()
        {
            for(int i = 0; i < animatorsZoom.Length; i++)
            {
                animatorsZoom[i].SetTrigger("Dezoom");
            }
        }

        [ContextMenu("DezoomOff")]
        public void ChoiceDezoomOff()
        {
            for (int i = 0; i < animatorsZoom.Length; i++)
            {
                animatorsZoom[i].SetTrigger("Zoom");
            }
        }
        
        #endregion
		
	} // StoryEventChoiceManager class
	
}// #PROJECTNAME# namespace
