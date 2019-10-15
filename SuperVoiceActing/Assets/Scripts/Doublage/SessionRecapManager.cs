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
    public class SessionRecapManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        ImageDictionnary emotionDictionnary;
        [SerializeField]
        Animator animatorRecap;
        [SerializeField]
        InputController inputController;

        [Space]
        [SerializeField]
        TextMeshProUGUI textContractName;
        [SerializeField]
        TextMeshProUGUI textContractWeek;
        [SerializeField]
        TextMeshProUGUI textContractHype;
        [SerializeField]
        TextMeshProUGUI textContractMoney;
        [SerializeField]
        TextMeshProUGUI textContractDescription;

        [Space]
        [SerializeField]
        GameObject recapPanel;
        [SerializeField]
        GameObject recapCharactersPanel;
        [SerializeField]
        PanelContractCharacter[] panelContractCharacters;
        [SerializeField]
        Image[] voiceActorsFace;

        [Space]
        [SerializeField]
        SessionRecapLineData sessionPrefab;
        [SerializeField]
        RectTransform sessionScrollList;

        List<SessionRecapLineData> listSessionLines = new List<SessionRecapLineData>();

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

        public void MenuDisappear()
        {
            animatorRecap.SetTrigger("Disappear");
            inputController.gameObject.SetActive(false);
        }

        public void DrawContract(Contract contract, int indexPhrase)
        {
            inputController.gameObject.SetActive(true);
            animatorRecap.gameObject.SetActive(true);
            animatorRecap.SetTrigger("Appear");
            textContractName.text = contract.Name;
            textContractWeek.text = contract.WeekRemaining.ToString();
            textContractHype.text = "0";// contract.fan.ToString();
            textContractMoney.text = contract.Money.ToString();
            textContractDescription.text = contract.Description;
            for(int i = 0; i < contract.Characters.Count; i++)
            {
                panelContractCharacters[i].gameObject.SetActive(true);
                panelContractCharacters[i].DrawPanel(contract.Characters[i]);
                voiceActorsFace[i].gameObject.SetActive(true);
                voiceActorsFace[i].sprite = contract.VoiceActors[i].SpriteSheets.SpriteIcon;
            }
            DrawSessionLines(contract, indexPhrase);
            MoveScrollList(9999);

        }

        public void ChangeCategory()
        {
            recapPanel.gameObject.SetActive(!recapPanel.gameObject.activeInHierarchy);
            recapCharactersPanel.gameObject.SetActive(!recapCharactersPanel.gameObject.activeInHierarchy);
            MoveScrollList(9999);
        }

        public void DrawSessionLines(Contract contract, int indexPhrase)
        {
            for (int i = listSessionLines.Count; i < indexPhrase; i++)
            {
                listSessionLines.Add(Instantiate(sessionPrefab, sessionScrollList));
                listSessionLines[i].DrawRecapLine(contract.Characters[contract.TextData[i].Interlocuteur].Name,
                                                  contract.Characters[contract.TextData[i].Interlocuteur].RoleSprite,
                                                  contract.TextData[i].Text, 
                                                  GetEmotionSprite(contract.EmotionsUsed[i].emotions));
            }
        }


        public void MoveScrollList(int amount)
        {
            sessionScrollList.anchoredPosition += new Vector2(0,amount);
        }

        private Sprite[] GetEmotionSprite(Emotion[] emotions)
        {
            Sprite[] res = new Sprite[emotions.Length];
            for(int i = 0; i < res.Length; i++)
            {
                res[i] = emotionDictionnary.GetSprite((int)emotions[i]);
            }
            return res;
        }
        
        #endregion
		
	} // SessionRecapManager class
	
}// #PROJECTNAME# namespace
