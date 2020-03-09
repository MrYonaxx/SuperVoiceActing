/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace VoiceActing
{

	public class MenuSaveLoadManager : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        VoiceActorDatabase characterSpriteDatabase;

        [Header("Menu Save")]
        [SerializeField]
        string saveFileName = "save";
        [SerializeField]
        string saveAllDataName = "savesData";
        [SerializeField]
        SaveDatabase saveDatabase;
        [SerializeField]
        ImageDictionnary seasonData;


        [Header("Menu Save")]
        [SerializeField]
        ButtonSaveLoad buttonSavePrefab;
        [SerializeField]
        UnityEvent eventOnQuit;


        private int maxSlot = 20;


        List<ButtonSaveLoad> buttonSaveLoad = new List<ButtonSaveLoad>();

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

        protected override void Start()
        {
            base.Start();
            LoadPlayerSavesData();
            maxSlot = saveDatabase.SavesDatas.Length;
            CreateButton();
        }

        public void CreateButton()
        {
            if(buttonSaveLoad.Count != maxSlot)
            {
                for(int i = 0; i < maxSlot; i++)
                {
                    buttonSaveLoad.Add(Instantiate(buttonSavePrefab, buttonScrollListTransform));
                    buttonsList.Add(buttonSaveLoad[buttonSaveLoad.Count - 1].GetRectTransform());
                    StoryCharacterData loadSprite = characterSpriteDatabase.GetCharacterData(saveDatabase.SavesDatas[i].SaveBestVA);
                    buttonSaveLoad[i].DrawButton(saveDatabase.SavesDatas[i], seasonData, loadSprite, i);
                }
            }
        }



        protected override void BeforeSelection()
        {
            base.BeforeSelection();
            buttonSaveLoad[indexSelected].UnselectButton();
        }
        protected override void AfterSelection()
        {
            base.BeforeSelection();
            buttonSaveLoad[indexSelected].SelectButton();
        }

        public void Validate(int index)
        {
            indexSelected = index;
        }

        public void Validate(PlayerData playerData)
        {
            SavePlayerProfile(playerData, indexSelected);
            StoryCharacterData loadSprite = characterSpriteDatabase.GetCharacterData(saveDatabase.SavesDatas[indexSelected].SaveBestVA);
            buttonSaveLoad[indexSelected].DrawButton(saveDatabase.SavesDatas[indexSelected], seasonData, loadSprite, indexSelected);
            buttonSaveLoad[indexSelected].FeedbackSave();
        }

        public void Load(PlayerData playerData)
        {
            if(LoadPlayerProfile(playerData, indexSelected))
            {
                playerData.SetLoading();
                SceneManager.LoadScene("Bureau");
            }

        }

        public void QuitMenu()
        {
            eventOnQuit.Invoke();
        }

        public void SavePlayerProfile(PlayerData playerData, int index)
        {
            // Save Datas
            saveDatabase.SavesDatas[index].WriteSaveData(playerData);
            SavePlayerSaveDatas();

            // timer hack (j'ajoute le temps pour la save puis je l'enlève une fois enregistré
            playerData.AddTimer();


            string json = JsonUtility.ToJson(playerData);
            string filePath = string.Format("{0}/saves/{1}{2}.json", Application.persistentDataPath, saveFileName, index);
            Debug.Log(filePath);
            FileInfo fileInfo = new FileInfo(filePath);

            if(!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            File.WriteAllText(filePath, json);



            playerData.SubstractTimer();

        }

        public void SavePlayerSaveDatas()
        {
            string json = JsonUtility.ToJson(saveDatabase);
            string filePath = string.Format("{0}/saves/{1}.json", Application.persistentDataPath, saveAllDataName);

            Debug.Log(filePath);
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            File.WriteAllText(filePath, json);
        }







        public bool LoadPlayerProfile(PlayerData playerData, int index)
        {
            string filePath = string.Format("{0}/saves/{1}{2}.json", Application.persistentDataPath, saveFileName, index);
            Debug.Log(filePath);
            if(File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(dataAsJson, playerData);
                return true;
            }
            return false;
        }








        public void LoadPlayerSavesData()
        {
            string filePath = string.Format("{0}/saves/{1}.json", Application.persistentDataPath, saveAllDataName);
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(dataAsJson, saveDatabase);
            }
        }




        #endregion

    } // MenuSaveLoadManager class
	
}// #PROJECTNAME# namespace
