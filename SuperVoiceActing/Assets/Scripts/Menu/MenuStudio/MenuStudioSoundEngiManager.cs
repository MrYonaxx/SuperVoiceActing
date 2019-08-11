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
	public class MenuStudioSoundEngiManager : MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Animator")]
        [SerializeField]
        Animator animatorMenu;



        [Title("Sound Engineer Information")]
        [SerializeField]
        Image imageSoundEngiFace;
        [SerializeField]
        Animator animatorSoundEngiFace;

        [SerializeField]
        TextMeshProUGUI textSoundEngiName;
        [SerializeField]
        TextMeshProUGUI textSoundEngiLevel;
        [SerializeField]
        TextMeshProUGUI textSoundEngiSalary;
        [SerializeField]
        TextMeshProUGUI textSoundEngiMixage;
        [SerializeField]
        TextMeshProUGUI textSoundEngiTrickery;



        [Title("Sound Engineer Audition")]
        [SerializeField]
        Animator animatorAudition;

        [SerializeField]
        TextMeshProUGUI textAuditionContractTitle;
        [SerializeField]
        TextMeshProUGUI textAuditionContractMixingAdd;

        [SerializeField]
        RectTransform transformContractMixingProgress;
        [SerializeField]
        RectTransform transformContractMixingPreview;

        [SerializeField]
        MenuContractPreparation menuContractPreparation;

        [Title("Sound Engineer Button")]
        [SerializeField]
        InputController inputController;
        [SerializeField]
        Animator buttonAuditionSelection;



        List<SoundEngineer> soundEngineersList;

        bool auditionMode = false;
        int auditionMaxMixing;


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
            if (auditionMode == true)
                animatorAudition.gameObject.SetActive(true);
        }

        public void QuitMenu()
        {
            animatorMenu.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        public void SetSoundEngiList(List<SoundEngineer> soundEngineers)
        {
            buttonsList = new List<RectTransform>(soundEngineers.Count);
            for (int i = 0; i < soundEngineers.Count; i++)
            {
                buttonsList.Add(null);
            }
            soundEngineersList = soundEngineers;
        }




        public void DrawSoundEngi(SoundEngineer soundEngineer)
        {
            imageSoundEngiFace.sprite = soundEngineer.SpritesSheets.SpriteNormal[0];
            textSoundEngiName.text = soundEngineer.EngineerName;
            textSoundEngiLevel.text = soundEngineer.Level.ToString();
            textSoundEngiSalary.text = soundEngineer.Salary.ToString();
            textSoundEngiMixage.text = soundEngineer.MixingPower.ToString();
            textSoundEngiTrickery.text = soundEngineer.ArtificeGauge.ToString();

            if (auditionMode == true)
                DrawSoundEngiPreview();
        }






        public void AuditionMode(bool b)
        {
            auditionMode = b;
            if(b == false)
                animatorAudition.gameObject.SetActive(false);
        }

        public void DrawAudition(string contractTitle, Sprite contractType, int contractMixingCurrent, int contractMixingMax)
        {
            textAuditionContractTitle.text = contractTitle;
            transformContractMixingProgress.localScale = new Vector2((float)contractMixingCurrent / contractMixingMax, transformContractMixingProgress.localScale.y);
            auditionMaxMixing = contractMixingMax;
            DrawSoundEngiPreview();
        }

        private void DrawSoundEngiPreview()
        {
            float size = (float)soundEngineersList[indexSelected].MixingPower / auditionMaxMixing;
            if (size >= 1)
                size = 1;
            transformContractMixingPreview.localScale = new Vector2(size, transformContractMixingPreview.localScale.y);
            textAuditionContractMixingAdd.text = "+" + soundEngineersList[indexSelected].MixingPower;
        }






        public void Validate()
        {
            if(auditionMode == false)
            {
                return;
            }
            animatorSoundEngiFace.SetTrigger("Feedback");
            inputController.gameObject.SetActive(false);      
        }

        public void SetSoundEngiToContract()
        {
            menuContractPreparation.SetSoundEngi(soundEngineersList[indexSelected]);
            inputController.gameObject.SetActive(true);
        }

        protected override void AfterSelection()
        {
            DrawSoundEngi(soundEngineersList[indexSelected]);
        }

        #endregion

    } // MenuStudioSoundEngiManager class
	
}// #PROJECTNAME# namespace
