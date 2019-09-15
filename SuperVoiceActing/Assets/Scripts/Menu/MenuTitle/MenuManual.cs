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
	public class MenuManual : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Space]
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        RecapDatabase recapDatabase;

        [Space]
        [SerializeField]
        GameObject menuManual;
        [SerializeField]
        ButtonManual buttonManualPrefab;

        [Space]
        [SerializeField]
        GameObject entryPanel;
        [SerializeField]
        TextMeshProUGUI textEntryTitle;
        [SerializeField]
        TextMeshProUGUI textEntryContent;

        [Space]
        [SerializeField]
        InputController inputController;
        [SerializeField]
        InputController inputEntry;

        List<RecapData> listRecap = new List<RecapData>();
        List<ButtonManual> listButtonManual = new List<ButtonManual>();

        bool isManualMenu = true;

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

        public void OnEnable()
        {
            menuManual.gameObject.SetActive(true);
            if (isManualMenu == true)
                CreateManualRecap();
            isManualMenu = true;
            CreateButtonList();
            indexSelected = 0;
        }

        public void OnDisable()
        {
            menuManual.gameObject.SetActive(false);
        }

        public void CreateManualRecap()
        {
            listRecap.Clear();
            for(int i = 0; i < playerData.GameManualUnlocked.Length; i++)
            {
                if(playerData.GameManualUnlocked[i] == true)
                {
                    listRecap.Add(recapDatabase.RecapDatas[i]);
                }
            }
        }

        public void CreateStoryRecap(RecapDatabase database)
        {
            listRecap.Clear();
            for (int i = 0; i < playerData.StoryResumeUnlocked.Length; i++)
            {
                if (playerData.StoryResumeUnlocked[i] == true)
                {
                    listRecap.Add(database.RecapDatas[i]);
                }
            }
            isManualMenu = false;
        }


        public void CreateButtonList()
        {
            buttonsList.Clear();
            for (int i = 0; i < listRecap.Count; i++)
            {
                if (i >= listButtonManual.Count)
                {
                    listButtonManual.Add(Instantiate(buttonManualPrefab, buttonScrollListTransform));
                    listButtonManual[i].SetID(i);
                }
                listButtonManual[i].gameObject.SetActive(true);
                listButtonManual[i].DrawEntry(listRecap[i].RecapTitle);
                buttonsList.Add(listButtonManual[i].GetRectTransform());
            }
            for (int i = listRecap.Count; i < listButtonManual.Count; i++)
            {
                listButtonManual[i].gameObject.SetActive(false);
            }
        }



        public void Validate()
        {
            if(listRecap.Count != 0)
                DrawEntry(listRecap[indexSelected]);
        }

        public void DrawEntry(RecapData entry)
        {
            inputController.gameObject.SetActive(false);
            inputEntry.gameObject.SetActive(true);

            entryPanel.gameObject.SetActive(true);
            textEntryTitle.text = entry.RecapTitle;
            textEntryContent.text = entry.RecapContent;
        }

        public void HideEntry()
        {
            inputController.gameObject.SetActive(true);
            inputEntry.gameObject.SetActive(false);

            entryPanel.gameObject.SetActive(false);
        }



        #endregion

    } // MenuManual class
	
}// #PROJECTNAME# namespace
