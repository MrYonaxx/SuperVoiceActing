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
using UnityEngine.SceneManagement;

namespace VoiceActing
{
	public class MenuNextChapter : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        TextMeshProUGUI textChapterNumber;
        [SerializeField]
        TextMeshProUGUI textChapterTitleNumber;
        [SerializeField]
        TextMeshProUGUI textChapterTitle;
        [SerializeField]
        string[] chapterTitle;

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

        public void DrawChapter()
        {
            textChapterNumber.text = playerData.CurrentChapter.ToString();
            textChapterTitle.text = chapterTitle[playerData.CurrentChapter];
            textChapterTitleNumber.text = textChapterTitleNumber.text + " " + playerData.CurrentChapter;
        }

        public void LoadScene()
        {
            SceneManager.LoadScene("Bureau");
        }

        
        #endregion
		
	} // MenuNextChapter class
	
}// #PROJECTNAME# namespace
