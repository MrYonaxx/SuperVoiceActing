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
        TextMeshProUGUI textSoundEngi;



        [Title("Sound Engineer Audition")]
        [SerializeField]
        Animator animatorAudition;

        [SerializeField]
        TextMeshProUGUI textAuditionContractTitle;
        [SerializeField]
        TextMeshProUGUI textAuditionContractMixingMax;
        [SerializeField]
        TextMeshProUGUI textAuditionContractMixingCurrent;

        [SerializeField]
        RectTransform transformContractMixingProgress;
        [SerializeField]
        RectTransform transformContractMixingPreview;

        [Title("Sound Engineer Button")]
        [SerializeField]
        Animator buttonAuditionSelection;



        List<SoundEngineer> soundEngineersList;

        bool auditionMode = false;

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

        private void QuitMenu()
        {
            animatorMenu.gameObject.SetActive(false);
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
        }


        public void AuditionMode(bool b)
        {
            auditionMode = b;
            if(b == false)
                animatorAudition.gameObject.SetActive(false);
        }


        public void Validate()
        {
            if(auditionMode == false)
            {
                return;
            }
            animatorSoundEngiFace.SetTrigger("Feedback");
            StartCoroutine(WaitFeedback());
        }

        private IEnumerator WaitFeedback()
        {
            //inputController.gameObject.SetActive(false);
            int time = 20;
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            //cameraManager.MoveToCamera(2);
            //menuContractPreparation.SetActor(actorsList[indexActorSelected]);
        }

        protected override void AfterSelection()
        {
            DrawSoundEngi(soundEngineersList[indexSelected]);
        }

        #endregion

    } // MenuStudioSoundEngiManager class
	
}// #PROJECTNAME# namespace
