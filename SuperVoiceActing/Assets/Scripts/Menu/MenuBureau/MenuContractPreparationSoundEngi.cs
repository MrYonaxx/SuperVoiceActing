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
	public class MenuContractPreparationSoundEngi : MenuContractPreparationAnnexe
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        CharacterSpriteDatabase characterSpriteDatabase;

        [Space]
        [SerializeField]
        Image imageSoundEngiIcon;
        [SerializeField]
        Image imageSoundEngiFace;
        [SerializeField]
        Animator animatorSoundEngiFace;
        [SerializeField]
        TextMeshProUGUI textContractCurrentMixing;
        [SerializeField]
        TextMeshProUGUI textContractTotalMixing;
        [SerializeField]
        RectTransform rectTransformMixingProgress;
        [SerializeField]
        RectTransform rectTransformMixingPreview;

        [Title("DrawSoundEngi")]
        [SerializeField]
        GameObject panelSoundEngi;
        [SerializeField]
        GameObject panelNoSoundEngi;
        [SerializeField]
        TextMeshProUGUI textSoundEngiName;
        [SerializeField]
        TextMeshProUGUI textSoundEngiLevel;
        [SerializeField]
        TextMeshProUGUI textSoundEngiTrickery;
        [SerializeField]
        TextMeshProUGUI textSoundEngiMixing;
        [SerializeField]
        TextMeshProUGUI textSoundEngiBonus;
        [SerializeField]
        TextMeshProUGUI textSoundEngiLongevite;
        [SerializeField]
        ButtonSkill[] buttonSkills;

        [SerializeField]
        CameraBureau cameraBureau;
        [SerializeField]
        MenuStudioMain menuStudioMain;
        [SerializeField]
        MenuStudioSoundEngiManager menuStudioSoundEngiManager;

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

        public override bool CanAdd(Contract contract)
        {
            if(contract.TotalMixing <= 0)
            {
                buttonAnimator.gameObject.SetActive(false);
                return false;
            }
            else
            {
                buttonAnimator.gameObject.SetActive(true);
                DrawContract(contract);
                return true;
            }
        }

        public override void DrawContract(Contract contract)
        {
            menuStudioSoundEngiManager.DrawAudition(contract.Name, null, contract.CurrentMixing, contract.TotalMixing);

            textContractCurrentMixing.text = contract.CurrentMixing.ToString();
            textContractTotalMixing.text = contract.TotalMixing.ToString();
            rectTransformMixingProgress.localScale = new Vector2((float)contract.CurrentMixing / contract.TotalMixing, rectTransformMixingProgress.localScale.y);

            SoundEngineer soundEngi = playerData.GetSoundEngiFromName(contract.SoundEngineerID);
            if (soundEngi == null)
            {
                panelSoundEngi.gameObject.SetActive(false);
                panelNoSoundEngi.gameObject.SetActive(true);
                imageSoundEngiIcon.gameObject.SetActive(false);
                imageSoundEngiFace.gameObject.SetActive(false);
                rectTransformMixingPreview.localScale = new Vector2(0, rectTransformMixingPreview.localScale.y);
            }
            else
            {
                panelSoundEngi.gameObject.SetActive(true);
                panelNoSoundEngi.gameObject.SetActive(false);
                float size = (float)soundEngi.MixingPower / contract.TotalMixing;
                if (size >= 1)
                    size = 1;
                rectTransformMixingPreview.localScale = new Vector2(size, rectTransformMixingPreview.localScale.y);
                DrawSoundEngi(soundEngi);
            }
        }


        private void DrawSoundEngi(SoundEngineer soundEngi)
        {
            imageSoundEngiIcon.gameObject.SetActive(true);
            imageSoundEngiIcon.sprite = characterSpriteDatabase.GetCharacterData(soundEngi.SoundEngineerID).SpriteIcon;
            imageSoundEngiFace.gameObject.SetActive(true);
            imageSoundEngiFace.sprite = characterSpriteDatabase.GetCharacterData(soundEngi.SoundEngineerID).SpriteNormal[0];
            animatorSoundEngiFace.SetTrigger("FeedbackSoundEngi");

            textSoundEngiName.text = soundEngi.EngineerName;
            textSoundEngiLevel.text = "Lv." + soundEngi.Level;
            textSoundEngiMixing.text = soundEngi.MixingPower.ToString();
            textSoundEngiTrickery.text = soundEngi.ArtificeGauge.ToString();

            textSoundEngiBonus.text = "0";
            textSoundEngiLongevite.text = "0";

            for(int i = 0; i < soundEngi.Skills.Length; i++)
            {
                buttonSkills[i].DrawSkill(soundEngi.Skills[i]);
            }

        }

        public override void UnselectButton()
        {
            base.UnselectButton();
            menuStudioSoundEngiManager.AuditionMode(false);
        }

        public override void SelectButton()
        {
            base.SelectButton();
            menuStudioSoundEngiManager.AuditionMode(true);
        }

        public override void ValidateButton(Contract contract)
        {
            menuStudioMain.SwitchToMenu(1);
            cameraBureau.MoveToCamera(3);
        }

        #endregion

    } // MenuContractPreparationSoundEngi class
	
}// #PROJECTNAME# namespace
