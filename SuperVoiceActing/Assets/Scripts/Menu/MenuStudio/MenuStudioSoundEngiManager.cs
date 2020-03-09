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
        [SerializeField]
        VoiceActorDatabase characterSpriteDatabase;

        [Title("Animator")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuStudioSoundEngiFormation menuSoundEngiFormation;


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
        [SerializeField]
        ButtonSkill[] buttonSkills;



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
        [SerializeField]
        GameObject panelResearch;

        [Title("Sound Engineer Button")]
        [SerializeField]
        InputController inputController;
        [SerializeField]
        Animator buttonAuditionSelection;



        List<SoundEngineer> soundEngineersList;

        bool auditionMode = false;
        int auditionMaxMixing;
        int formationComplete = 0;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetFormationComplete()
        {
            return formationComplete;
        }

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
            {
                panelResearch.gameObject.SetActive(false);
                animatorAudition.gameObject.SetActive(true);
            }
        }

        public void QuitMenu()
        {
            animatorMenu.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            panelResearch.gameObject.SetActive(true);
        }

        public void SetSoundEngiList(List<SoundEngineer> soundEngineers)
        {
            buttonsList = new List<RectTransform>(soundEngineers.Count);
            for (int i = 0; i < soundEngineers.Count; i++)
            {
                buttonsList.Add(null);
                if(soundEngineers[i].Formation() == true)
                {
                    formationComplete += 1;
                }
            }
            soundEngineersList = soundEngineers;
        }


        public void RedrawCurrentSoundEngi()
        {
            DrawSoundEngi(soundEngineersList[indexSelected]);
        }

        public void DrawSoundEngi(SoundEngineer soundEngineer)
        {
            imageSoundEngiFace.sprite = characterSpriteDatabase.GetCharacterData(soundEngineer.SoundEngineerID).SpriteNormal[0];
            textSoundEngiName.text = soundEngineer.EngineerName;
            textSoundEngiLevel.text = soundEngineer.Level.ToString();
            textSoundEngiSalary.text = soundEngineer.Salary.ToString();
            textSoundEngiMixage.text = soundEngineer.MixingPower.ToString();
            textSoundEngiTrickery.text = soundEngineer.ArtificeGauge.ToString();

            for(int i = 0; i < buttonSkills.Length; i++)
            {
                if (i < soundEngineer.Skills.Length)
                {
                    buttonSkills[i].DrawSkill(soundEngineer.Skills[i]);
                }
                else
                {
                    buttonSkills[i].DrawSkill(null);
                }
            }
            if (soundEngineer.FormationSkill != null)
            {
                buttonSkills[soundEngineer.FormationSkillID].DrawFormation(soundEngineer.FormationSkill.SkillName, soundEngineer.FormationSkillTime, soundEngineer.FormationSkillTotalTime);
            }

            if (auditionMode == true)
                DrawSoundEngiPreview();
        }






        public void AuditionMode(bool b)
        {
            auditionMode = b;
            if (b == false)
            {
                animatorAudition.gameObject.SetActive(false);
                panelResearch.gameObject.SetActive(true);
            }
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







        public void SwitchToFormationMenu(bool b)
        {
            if (b == true)
            {
                animatorMenu.SetTrigger("FormationAppear");
                inputController.gameObject.SetActive(false);
                menuSoundEngiFormation.gameObject.SetActive(true);
                menuSoundEngiFormation.SetSoundEngiID(soundEngineersList[indexSelected]);
            }
            else
            {
                animatorMenu.SetTrigger("FormationDisappear");
                inputController.gameObject.SetActive(true);
                menuSoundEngiFormation.gameObject.SetActive(false);
            }
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
