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
    [System.Serializable]
    public class OptionData 
    {
        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        public TextMeshProUGUI textOptionData;
        [HorizontalGroup(LabelWidth = 150)]
        [SerializeField]
        public bool isSlider;
        [HorizontalGroup]
        [SerializeField]
        public string[] options = { "On", "Off" };

        [ShowIf("isSlider")]
        [HideLabel]
        [SerializeField]
        public Slider sliderOption;
    }

	public class MenuOptions : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        Resolution[] resolutions;


        [Title("Menu Options")]
        [SerializeField]
        GameObject menuOption;
        [SerializeField]
        int indexFullscreen = 0;
        [SerializeField]
        int indexResolution = 1;
        [SerializeField]
        int indexQuality = 2;
        [SerializeField]
        int indexAmbientOcclusion = 3;
        [SerializeField]
        int indexBloom = 4;

        [SerializeField]
        int indexMusic = 5;
        [SerializeField]
        int indexSound = 6;
        [SerializeField]
        int indexVoice = 7;

        [SerializeField]
        int indexShowCommand = 8;
        [SerializeField]
        int indexMouthSpeed = 9;
        [SerializeField]
        int indexLanguage = 10;


        [Title("")]
        [SerializeField]
        OptionData[] optionDatas;

        [SerializeField]
        AudioSource voiceAudioSource;


        [Title("InputController")]
        [SerializeField]
        InputController mainInputController;
        [SerializeField]
        InputController dropdownInputController;
        [SerializeField]
        InputController sliderInputController;

        private int[] indexOptions = { 0,0,0,0,0,0,0,0,0,0,0 };
        private int indexCurrentOption;

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
            menuOption.gameObject.SetActive(true);
            LoadPlayerOptionData();
            DrawOptionsValue();
        }

        public void OnDisable()
        {
            menuOption.gameObject.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();
            resolutions = Screen.resolutions;
            System.Array.Reverse(resolutions);
            optionDatas[indexResolution].options = new string[resolutions.Length];
            int tmpIndex = 0;
            for (int i = 0; i <= resolutions.Length; i++)
            {
                optionDatas[indexResolution].options[tmpIndex] = resolutions[i].width + "x" + resolutions[i].height;
                tmpIndex += 1;
            }
            LoadPlayerOptionData();
            DrawOptionsValue();
        }

        private void LoadPlayerOptionData()
        {
            indexOptions[indexFullscreen] = PlayerPrefs.GetInt("Fullscreen");
            indexOptions[indexResolution] = PlayerPrefs.GetInt("Resolution");
            indexOptions[indexQuality] = PlayerPrefs.GetInt("Quality");
            indexOptions[indexAmbientOcclusion] = PlayerPrefs.GetInt("AmbientOcclusion");
            indexOptions[indexBloom] = PlayerPrefs.GetInt("Bloom");
            indexOptions[indexMusic] = PlayerPrefs.GetInt("MusicVolume");
            indexOptions[indexSound] = PlayerPrefs.GetInt("SoundVolume");
            indexOptions[indexVoice] = PlayerPrefs.GetInt("VoiceVolume");
            indexOptions[indexShowCommand] = PlayerPrefs.GetInt("ShowCommand");
            indexOptions[indexMouthSpeed] = PlayerPrefs.GetInt("MouthSpeed");
            indexOptions[indexLanguage] = PlayerPrefs.GetInt("Language");
        }
        private void DrawOptionsValue()
        {
            for(int i = 0; i < indexOptions.Length; i++)
            {
                if (optionDatas[i].isSlider == true)
                {
                    optionDatas[i].textOptionData.text = indexOptions[i].ToString();
                    optionDatas[i].sliderOption.value = indexOptions[i];
                }
                else
                {
                    optionDatas[i].textOptionData.text = optionDatas[i].options[indexOptions[i]];
                }
            }
        }

        protected override void AfterSelection()
        {
            if (optionDatas[indexSelected].isSlider == true)
            {
                indexCurrentOption = (int)optionDatas[indexSelected].sliderOption.value;
                sliderInputController.gameObject.SetActive(true);
                dropdownInputController.gameObject.SetActive(false);
            }
            else
            {
                indexCurrentOption = indexOptions[indexSelected];
                dropdownInputController.gameObject.SetActive(true);
                sliderInputController.gameObject.SetActive(false);
            }

            voiceAudioSource.gameObject.SetActive(false);
        }

        public void RewritePlayerPrefs()
        {
            PlayerPrefs.SetInt("Fullscreen", indexOptions[indexFullscreen]);
            PlayerPrefs.SetInt("Resolution", indexOptions[indexResolution]);
            PlayerPrefs.SetInt("Quality", indexOptions[indexQuality]);
            PlayerPrefs.SetInt("AmbientOcclusion", indexOptions[indexAmbientOcclusion]);
            PlayerPrefs.SetInt("Bloom", indexOptions[indexBloom]);
            PlayerPrefs.SetInt("MusicVolume", indexOptions[indexMusic]);
            PlayerPrefs.SetInt("SoundVolume", indexOptions[indexSound]);
            PlayerPrefs.SetInt("VoiceVolume", indexOptions[indexVoice]);
            PlayerPrefs.SetInt("ShowCommand", indexOptions[indexShowCommand]);
            PlayerPrefs.SetInt("MouthSpeed", indexOptions[indexMouthSpeed]);
            PlayerPrefs.SetInt("Language", indexOptions[indexLanguage]);
        }

        public void ApplyChange()
        {
            LoadPlayerOptionData();
            ActivateFullscreen();
            ChangeResolution();
            ChangeMusicVolume();
            ChangeSoundVolume();
            ChangeVoiceVolume();

            this.gameObject.SetActive(false);
            menuOption.gameObject.SetActive(false);
        }









        public void MoveValueDropdown(bool left)
        {
            if (left == true)
            {
                indexCurrentOption -= 1;
                if (indexCurrentOption == -1)
                    indexCurrentOption = optionDatas[indexSelected].options.Length - 1;
            }
            else
            {
                indexCurrentOption += 1;
                if (indexCurrentOption == optionDatas[indexSelected].options.Length)
                    indexCurrentOption = 0;
            }
            indexOptions[indexSelected] = indexCurrentOption;
            optionDatas[indexSelected].textOptionData.text = optionDatas[indexSelected].options[indexCurrentOption];
        }

        public void MoveValueSlider(bool left)
        {
            if(left == true)
                optionDatas[indexSelected].sliderOption.value -= 1;
            else
                optionDatas[indexSelected].sliderOption.value += 1;
            indexOptions[indexSelected] = (int)optionDatas[indexSelected].sliderOption.value;
            optionDatas[indexSelected].textOptionData.text = optionDatas[indexSelected].sliderOption.value.ToString();

            ChangeMusicVolume();
            ChangeSoundVolume();
            ChangeVoiceVolume();

            if (indexSelected == indexVoice)
            {
                voiceAudioSource.gameObject.SetActive(true);
                voiceAudioSource.volume = optionDatas[indexSelected].sliderOption.value / 100f;
            }
        }








        public void ActivateFullscreen()
        {
            if(indexOptions[indexFullscreen] == 0)
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            else
                Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        public void ChangeResolution()
        {
            Screen.SetResolution(resolutions[indexOptions[indexResolution]].width, resolutions[indexOptions[indexResolution]].height, Screen.fullScreen);
        }

        public void ChangeMusicVolume()
        {
            AudioManager.Instance.SetMusicVolume(indexOptions[indexMusic]);
        }
        public void ChangeSoundVolume()
        {
            AudioManager.Instance.SetSoundVolume(indexOptions[indexSound]);
        }
        public void ChangeVoiceVolume()
        {
            AudioManager.Instance.SetVoiceVolume(indexOptions[indexVoice]);
        }

        #endregion

    } // MenuOptions class
	
}// #PROJECTNAME# namespace
