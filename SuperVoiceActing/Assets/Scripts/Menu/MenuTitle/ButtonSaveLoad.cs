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
using UnityEngine.EventSystems;

namespace VoiceActing
{
	public class ButtonSaveLoad : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Animator animatorButton;
        [SerializeField]
        RectTransform rectTransform;

        [Space]
        [SerializeField]
        GameObject saveFile;
        [SerializeField]
        TextMeshProUGUI textNewFile;

        [Space]
        [SerializeField]
        Image imageSeason;
        [SerializeField]
        Image imageBestActor;
        [SerializeField]
        TextMeshProUGUI textFileMonth;
        [SerializeField]
        TextMeshProUGUI textFileWeek;
        [SerializeField]
        TextMeshProUGUI textTimer;
        [SerializeField]
        TextMeshProUGUI textMoney;
        [SerializeField]
        TextMeshProUGUI textChapter;
        [SerializeField]
        TextMeshProUGUI textPlayerName;

        [SerializeField]
        TextMeshProUGUI textSlotNumber;
        [SerializeField]
        UnityEventInt eventOnClick;

        int buttonIndex;

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

        public void DrawButton(SaveData saveData, ImageDictionnary seasonData, int index)
        {
            this.gameObject.SetActive(true);
            buttonIndex = index;
            if (index < 10)
            {
                textSlotNumber.text = "0" + index;
            }
            else
            {
                textSlotNumber.text = index.ToString();
            }

            if (saveData.SaveTime != "") // en gros null 
            {
                textNewFile.gameObject.SetActive(false);
                saveFile.gameObject.SetActive(true);
                imageBestActor.gameObject.SetActive(true);

                imageBestActor.sprite = saveData.SaveBestVA;
                imageSeason.sprite = seasonData.GetSprite(saveData.SaveSeason);
                textPlayerName.text = saveData.SavePlayerName + " - " + saveData.SaveStudioName;
                textFileMonth.text = saveData.SaveMonth;
                textFileWeek.text = "Semaine " + saveData.SaveWeek;
                textTimer.text = saveData.SaveTime;
                textMoney.text = saveData.SaveMoney;
                textChapter.text = saveData.SaveChapter;
            }
            else
            {
                textNewFile.gameObject.SetActive(true);
                saveFile.gameObject.SetActive(false);
                imageBestActor.gameObject.SetActive(false);
            }

        }

        public void UnselectButton()
        {
            animatorButton.SetTrigger("Unselected");
        }

        public void SelectButton()
        {
            animatorButton.SetTrigger("Selected");
        }

        public void FeedbackSave()
        {
            animatorButton.SetTrigger("FeedbackSave");
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            SelectButton();
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            UnselectButton();
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }

        #endregion

    } // ButtonSaveLoad class
	
}// #PROJECTNAME# namespace
