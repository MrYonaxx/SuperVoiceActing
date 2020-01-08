/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class MenuResearchSelectDungeon : MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("SelectDungeon")]
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        MenuResearchMovementManager researchMovementManager;
        [SerializeField]
        private ButtonResearchDungeon prefabResearchDungeon;

        [Title("Menu")]
        [SerializeField]
        private Animator animatorMenu;
        [SerializeField]
        private InputController inputController;

        int researchListSize = 0;
        List<ButtonResearchDungeon> buttonResearchDungeons = new List<ButtonResearchDungeon>();

        #endregion


        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        protected override void Start()
        {
            base.Start();
            DrawResearchDungeon();
        }

        public void ShowMenu(bool b)
        {
            animatorMenu.SetBool("Appear", b);
            inputController.gameObject.SetActive(b);
        }

        private void DrawResearchDungeon()
        {
            for(int i = 0; i < playerData.ResearchExplorationDatas.Length; i++)
            {
                if(playerData.ResearchExplorationDatas[i].DungeonUnlocked == true)
                {
                    buttonResearchDungeons.Add(Instantiate(prefabResearchDungeon, buttonScrollListTransform));
                    buttonsList.Add(buttonResearchDungeons[researchListSize].GetRectTransform());
                    int percentage = (int)(((float)playerData.ResearchExplorationDatas[i].ResearchExplorationCount / playerData.ResearchExplorationDatas[i].ResearchExplorationTotal) * 100);
                    buttonResearchDungeons[researchListSize].DrawDungeonName(researchMovementManager.GetDungeonName(i), percentage);
                    buttonResearchDungeons[researchListSize].gameObject.SetActive(true);
                    researchListSize += 1;
                }
            }
        }

        public void Validate()
        {
            researchMovementManager.SetDungeon(indexSelected);
            ShowMenu(false);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace