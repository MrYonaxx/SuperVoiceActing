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
	public class MenuStudioSoundEngiManager : MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


        [SerializeField]
        Animator animatorMenu;

        [SerializeField]
        Image imageSoundEngiFace;

        [SerializeField]
        TextMeshProUGUI textSoundEngiName;
        [SerializeField]
        TextMeshProUGUI textSoundEngiLevel;
        [SerializeField]
        TextMeshProUGUI textSoundEngi;

        List<SoundEngineer> soundEngineersList;

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
            DrawSoundEngi(soundEngineersList[indexSelected]);
            animatorMenu.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            animatorMenu.gameObject.SetActive(false);
        }


        public void SetSoundEngiList(List<SoundEngineer> soundEngineers)
        {
            soundEngineersList = soundEngineers;
        }

        public void DrawSoundEngi(SoundEngineer soundEngineer)
        {
            imageSoundEngiFace.sprite = soundEngineer.SpritesSheets.SpriteNormal[0];
            textSoundEngiName.text = soundEngineer.EngineerName;
            textSoundEngiLevel.text = soundEngineer.Level.ToString();
        }

        protected override void AfterSelection()
        {
            DrawSoundEngi(soundEngineersList[indexSelected]);
        }

        #endregion

    } // MenuStudioSoundEngiManager class
	
}// #PROJECTNAME# namespace
