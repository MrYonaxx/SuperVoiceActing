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


        int choiceSize = 0;
        int indexSelected = -1;
        public int IndexSelected
        {
            get { return indexSelected; }
        }

        bool choiceSelected = false;
        public bool ChoiceSelected
        {
            get { return choiceSelected; }
        }

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
            Debug.Log(data.StoryChoices);
            choiceSize = data.Answers.Length;
            indexSelected = -1;
            choiceSelected = false;
            choicePanel.SetActive(true);
            ChoiceDezoomOn();
            StartCoroutine(ChoicesCoroutineAppear(data));

        }

        private IEnumerator ChoicesCoroutineAppear(StoryEventChoices choicesData)
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





        public void Select(int index)
        {
            if (choiceSelected == true)
                return;
            if (indexSelected != -1)
            {
                animatorsChoices[indexSelected].SetTrigger("Unselected");
            }
            indexSelected = index;
            animatorsChoices[indexSelected].SetTrigger("Selected");
        }

        public void SelectUp()
        {
            if (indexSelected != -1)
            {
                animatorsChoices[indexSelected].SetTrigger("Unselected");
                indexSelected -= 1;
                if (indexSelected < 0)
                    indexSelected = choiceSize - 1;
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
                if (indexSelected == choiceSize)
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
            if (choiceSelected == true)
                return;
            indexSelected = index;
            Validate();
        }


        public void Validate()
        {
            if (choiceSelected == true)
                return;
            if (indexSelected == -1)
                return;
            ChoiceDezoomOff();
            inputController.gameObject.SetActive(false);
            for (int i = 0; i < animatorsChoices.Length; i++)
            {
                if (i == indexSelected)
                    animatorsChoices[i].SetTrigger("Validate");
                else
                    animatorsChoices[i].SetTrigger("Disappear");
            }
            choiceSelected = true;
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
