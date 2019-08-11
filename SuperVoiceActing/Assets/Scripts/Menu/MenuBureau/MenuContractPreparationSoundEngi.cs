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
	public class MenuContractPreparationSoundEngi : MenuContractPreparationAnnexe
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

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
            menuStudioSoundEngiManager.DrawAudition(contract.Name, contract.IconSprite, contract.CurrentMixing, contract.TotalMixing);

            textContractCurrentMixing.text = contract.CurrentMixing.ToString();
            textContractTotalMixing.text = contract.TotalMixing.ToString();
            rectTransformMixingProgress.localScale = new Vector2((float)contract.CurrentMixing / contract.TotalMixing, rectTransformMixingProgress.localScale.y);

            if (contract.SoundEngineer == null)
            {
                imageSoundEngiIcon.gameObject.SetActive(false);
                imageSoundEngiFace.gameObject.SetActive(false);
                rectTransformMixingPreview.localScale = new Vector2(0, rectTransformMixingPreview.localScale.y);
            }
            else
            {
                imageSoundEngiIcon.gameObject.SetActive(true);
                imageSoundEngiIcon.sprite = contract.SoundEngineer.SpritesSheets.SpriteIcon;
                imageSoundEngiFace.gameObject.SetActive(true);
                imageSoundEngiFace.sprite = contract.SoundEngineer.SpritesSheets.SpriteNormal[0];
                animatorSoundEngiFace.SetTrigger("FeedbackSoundEngi");
                float size = (float)contract.SoundEngineer.MixingPower / contract.TotalMixing;
                if (size >= 1)
                    size = 1;
                rectTransformMixingPreview.localScale = new Vector2(size, rectTransformMixingPreview.localScale.y);
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
            menuStudioMain.SwitchToMenu(0);
            cameraBureau.MoveToCamera(3);
        }

        #endregion

    } // MenuContractPreparationSoundEngi class
	
}// #PROJECTNAME# namespace
