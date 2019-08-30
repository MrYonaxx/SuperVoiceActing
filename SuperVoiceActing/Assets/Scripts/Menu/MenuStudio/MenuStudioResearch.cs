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
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuStudioResearch : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Menu Research")]
        [SerializeField]
        ResearchDatabase researchDatabase;
        [SerializeField]
        ButtonResearch buttonResearchPrefab;
        [SerializeField]
        Animator animatorMenu;


        [Title("Menu Categories")]
        [SerializeField]
        Animator animatorCategory;
        [SerializeField]
        TextMeshProUGUI[] textCategories;

        [Title("Menu Description")]

        [SerializeField]
        TextMeshProUGUI textResearchName;
        [SerializeField]
        TextMeshProUGUI textResearchLevels;
        [SerializeField]
        TextMeshProUGUI textResearchPrice;

        [SerializeField]
        TextMeshProUGUI textResearchCurrentLevel;
        [SerializeField]
        TextMeshProUGUI textResearchCurrentDescription;
        [SerializeField]

        TextMeshProUGUI textResearchNextLevel;
        [SerializeField]
        TextMeshProUGUI textResearchNextDescription;

        [SerializeField]
        TextMeshProUGUI textResearch;





        int indexCategory = 0;

        List<ButtonResearch> buttonResearchList = new List<ButtonResearch>();
        int indexCreateList = 0;

        PlayerData playerData;
        ResearchPlayerLevel[] researchPlayerLevels;

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


        private void OnEnable()
        {
            animatorMenu.gameObject.SetActive(true);
            CreateResearchesButtons();
            DrawCategories();
            DrawResearch();
            AfterSelection();
        }

        public void QuitMenu()
        {
            animatorMenu.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        public void SetResearchPlayerLevels(PlayerData data)
        {
            playerData = data;
            researchPlayerLevels = data.ResearchesLevels;
        }

        public void CreateResearchesButtons()
        {
            for(int i = 0; i < researchDatabase.ResearchesData[indexCategory].Researches.Length; i++)
            {
                CreateButtonResearch(researchDatabase.ResearchesData[indexCategory].Researches[i]);
            }
            CleanButtons();
        }

        public void CreateButtonResearch(ResearchData researchData)
        {
            if (buttonResearchList.Count <= indexCreateList) // On doit créer un nouveau bouton
            {
                buttonResearchList.Add(Instantiate(buttonResearchPrefab, buttonScrollListTransform));
                buttonResearchList[indexCreateList].DrawResearch(researchData, researchPlayerLevels[indexCategory].ResearchLevel[indexCreateList]);
                buttonsList.Add(buttonResearchList[indexCreateList].GetRectTransform());
            }
            else // On peut réécrire un bouton existant
            {
                buttonResearchList[indexCreateList].DrawResearch(researchData, researchPlayerLevels[indexCategory].ResearchLevel[indexCreateList]);
                if (buttonsList.Count <= indexCreateList)
                    buttonsList.Add(buttonResearchList[indexCreateList].GetRectTransform());
                else
                    buttonsList[indexCreateList] = buttonResearchList[indexCreateList].GetRectTransform();
            }
            buttonResearchList[indexCreateList].gameObject.SetActive(true);
            indexCreateList += 1;
        }

        public void CleanButtons()
        {
            for (int i = indexCreateList; i < buttonResearchList.Count; i++)
            {
                buttonResearchList[i].gameObject.SetActive(false);
                if (buttonsList.Count > indexCreateList)
                    buttonsList.RemoveAt(buttonsList.Count - 1);
            }
            indexCreateList = 0;
        }





        public void DrawResearchDescription(ResearchData data, int playerLevel)
        {
            textResearchName.text = data.ResearchName;



            if (playerLevel != 0)
                textResearchCurrentDescription.text = string.Format(data.ResearchDescription, GetLevelTotalValue(data.ResearchValue, playerLevel));
            else
                textResearchCurrentDescription.text = " - ";

            if(playerLevel == data.ResearchPrice.Length)
            {
                textResearchCurrentLevel.text = "MAX";

                textResearchLevels.text = "MAX";
                textResearchPrice.text = " - ";

                textResearchNextLevel.text = "MAX";
                textResearchNextDescription.text = "";
            }
            else
            {
                textResearchCurrentLevel.text = (playerLevel + 1).ToString();

                textResearchLevels.text = (playerLevel + 1) + " / " + (data.ResearchPrice.Length + 1);
                textResearchPrice.text = data.ResearchPrice[playerLevel].ToString();

                textResearchNextLevel.text = (playerLevel + 2).ToString();
                textResearchNextDescription.text = string.Format(data.ResearchDescription, GetLevelTotalValue(data.ResearchValue, playerLevel + 1));
            }

        }

        private int GetLevelTotalValue(int[] data, int playerLevel)
        {
            int best = 0;
            for(int i = 0; i < playerLevel; i++)
            {
                best += data[i];
            }
            return best;
        }






        protected override void AfterSelection()
        {
            base.AfterSelection();
            DrawResearchDescription(researchDatabase.ResearchesData[indexCategory].Researches[indexSelected], researchPlayerLevels[indexCategory].ResearchLevel[indexSelected]);
        }



        public void Validate()
        {
            int level = researchPlayerLevels[indexCategory].ResearchLevel[indexSelected];
            if (level == researchDatabase.ResearchesData[indexCategory].Researches[indexSelected].ResearchPrice.Length)
                return;
            if (playerData.ResearchPoint >= researchDatabase.ResearchesData[indexCategory].Researches[indexSelected].ResearchPrice[level])
            {
                // On applique l'effet
                researchDatabase.ResearchesData[indexCategory].Researches[indexSelected].ApplyResearchEffect(playerData, researchPlayerLevels[indexCategory].ResearchLevel[indexSelected]);

                // On modifie playerData
                researchPlayerLevels[indexCategory].ResearchLevel[indexSelected] += 1;
                playerData.ResearchPoint -= researchDatabase.ResearchesData[indexCategory].Researches[indexSelected].ResearchPrice[level];

                // On redraw
                buttonResearchList[indexSelected].DrawResearch(researchDatabase.ResearchesData[indexCategory].Researches[indexSelected], researchPlayerLevels[indexCategory].ResearchLevel[indexSelected]);
                DrawResearchDescription(researchDatabase.ResearchesData[indexCategory].Researches[indexSelected], researchPlayerLevels[indexCategory].ResearchLevel[indexSelected]);

                DrawResearch();

            }


        }






        public void CategoryLeft()
        {
            indexCategory += 1;
            if (indexCategory == researchDatabase.ResearchesData.Length)
            {
                indexCategory = 0;
            }
            animatorCategory.transform.position = textCategories[indexCategory].transform.position;
            indexSelected = 0;
            CreateResearchesButtons();
        }

        public void CategoryRight()
        {
            indexCategory -= 1;
            if (indexCategory == -1)
            {
                indexCategory = researchDatabase.ResearchesData.Length-1;
            }
            animatorCategory.transform.position = textCategories[indexCategory].transform.position;
            indexSelected = 0;
            CreateResearchesButtons();
        }

        public void DrawCategories()
        {
            for (int i = 0; i < textCategories.Length; i++)
            {
                textCategories[i].text = researchDatabase.ResearchesData[i].CategoryName;
            }
        }



        public void DrawResearch()
        {
            textResearch.text = playerData.ResearchPoint.ToString();
        }



        #endregion

    } // MenuStudioResearch class
	
}// #PROJECTNAME# namespace
