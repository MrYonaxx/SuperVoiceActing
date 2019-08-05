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
        TextMeshProUGUI textContractCurrentMixing;
        [SerializeField]
        TextMeshProUGUI textContractTotalMixing;
        [SerializeField]
        RectTransform rectTransformMixingProgress;

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
            //imageSoundEngiIcon.sprite = contract.SoundEngineer.
            textContractCurrentMixing.text = contract.CurrentMixing.ToString();
            textContractTotalMixing.text = contract.TotalMixing.ToString();
            rectTransformMixingProgress.localScale = new Vector3((float)contract.CurrentMixing / contract.TotalMixing, rectTransformMixingProgress.localScale.y, rectTransformMixingProgress.localScale.z);
        }


        public override void ValidateButton(Contract contract)
        {
            menuStudioMain.SwitchToMenu(0);
            cameraBureau.MoveToCamera(3);
        }

        #endregion

    } // MenuContractPreparationSoundEngi class
	
}// #PROJECTNAME# namespace
