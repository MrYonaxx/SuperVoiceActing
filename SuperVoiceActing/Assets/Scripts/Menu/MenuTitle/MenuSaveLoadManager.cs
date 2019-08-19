/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace VoiceActing
{
	public class MenuSaveLoadManager : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        ButtonSaveLoad[] buttonSaveLoads;
        
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

        public void SavePlayerProfile(PlayerData playerData)
        {
            string json = JsonUtility.ToJson(playerData);
            string filePath = string.Format("{0}/saves/player.json", Application.persistentDataPath);
            Debug.Log(filePath);
            FileInfo fileInfo = new FileInfo(filePath);

            if(!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            File.WriteAllText(filePath, json);
        }

        public void LoadPlayerProfile(PlayerData playerData)
        {
            string filePath = string.Format("{0}/saves/player.json", Application.persistentDataPath);
            Debug.Log(filePath);
            if(File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(dataAsJson, playerData);
            }
        }
        
        #endregion
		
	} // MenuSaveLoadManager class
	
}// #PROJECTNAME# namespace
