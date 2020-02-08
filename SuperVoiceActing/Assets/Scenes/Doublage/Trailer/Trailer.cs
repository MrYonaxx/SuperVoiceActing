using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    // À supprimer
    public class Trailer : MonoBehaviour
    {
        [SerializeField]
        DoublageManager doublageManager;
        [SerializeField]
        CameraController cameraController;
        [SerializeField]
        TextAppearManager textAppearManager;
        [SerializeField]
        CharacterDialogueController characterDialogueController;
        [SerializeField]
        StoryCharacterData doremiSprite;

        [Title("IntroSequence")]
        [SerializeField]
        TextPerformanceAppear introText;
        [SerializeField]
        TextPerformanceAppear daText;
        [SerializeField]
        GameObject textIntro;
        [SerializeField]
        Image introBlackScreen;

        [Title("AudioSource")]
        [SerializeField]
        protected SimpleSpectrum spectrum;
        [SerializeField]
        protected AudioClip audioClipBattleTheme;
        [SerializeField]
        protected AudioClip audioClipYokaiDisco;
        [SerializeField]
        protected AudioClip audioClipKillPhrase;
        [SerializeField]
        protected AudioClip audioClipKillPhrase2;
        [SerializeField]
        protected AudioClip audioClipAttack;
        [SerializeField]
        protected AudioClip audioClipAttack2;
        [SerializeField]
        protected AudioClip audioClipSpotlight;

        [Title("Camera")]
        [SerializeField]
        CameraMovementData camMov1;
        [SerializeField]
        CameraMovementData camMov2;
        [SerializeField]
        CameraMovementData camMov3;
        [SerializeField]
        CameraMovementData camMov10;

        [SerializeField]
        UnityEvent eventStart;
        [SerializeField]
        UnityEvent eventStart2;
        [SerializeField]
        UnityEvent eventStart3;
        [SerializeField]
        UnityEvent eventStart10;
        [SerializeField]
        UnityEvent eventStart11;
        [SerializeField]
        UnityEvent eventStart12;

        public void Start()
        {
            StartCoroutine(TrailerCoroutine());
        }

        private IEnumerator TrailerCoroutine()
        {
            introBlackScreen.gameObject.SetActive(true);
            textAppearManager.SetMouth(characterDialogueController);
            characterDialogueController.SetStoryCharacterData(doremiSprite);
            yield return new WaitForSeconds(1);
            introText.NewPhrase("Super Voice Acting");
            yield return new WaitForSeconds(3f);
            introText.NewPhrase("In session !");
            yield return new WaitForSeconds(3);
            textIntro.SetActive(false);
            yield return new WaitForSeconds(1f);
            introBlackScreen.gameObject.SetActive(false);
            introBlackScreen.enabled = false;
            AudioManager.Instance.PlaySound(audioClipSpotlight);
            spectrum.audioSource = AudioManager.Instance.GetAudioSourceMusic();
            spectrum.enabled = true;
            //cameraController.MoveToInitialPosition(300);
            eventStart.Invoke();
            yield return new WaitForSeconds(1f);
            AudioManager.Instance.PlayMusic(audioClipBattleTheme);
            cameraController.MoveToInitialPosition(300);
            yield return new WaitForSeconds(2f);
            textAppearManager.NewPhrase("Bonjour, je m'appelle Dorémi et je suis là pour vous parler de Super voice acting !", Emotion.Confiance);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2f);

            cameraController.CameraDataMovement(camMov1);
            textAppearManager.NewPhrase("Super voice acting est un jeu de gestion rpg.", Emotion.Confiance);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(1.5f);
            textAppearManager.NewPhrase("Ayant bien sur pour thème le doublage.", Emotion.Confiance);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);

            eventStart2.Invoke();
            cameraController.CameraDataMovement(camMov2);
            textAppearManager.NewPhrase("Inspiré par des jeux tels que Phoenix wright ou Trauma center.", Emotion.Confiance);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2f);
            eventStart3.Invoke();
            cameraController.CameraDataMovement(camMov3);
            textAppearManager.NewPhrase("Le jeu présentera l'industrie du doublage sous un angle des plus extravagant !", Emotion.Joie);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2f);

            cameraController.CameraDataMovement(camMov10);
            textAppearManager.NewPhrase("Et enfin commencer la super session d'enrezistremme...", Emotion.Confiance);
            textAppearManager.ApplyDamage(85);
            yield return null;
            yield return new WaitForSeconds(1.5f);
            cameraController.SetNoCameraEffect(true);
            textAppearManager.SetLetterSpeed(20);
            eventStart10.Invoke();
            AudioManager.Instance.StopMusicWithScratch(120);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(1.5f);

            eventStart11.Invoke();
            yield return new WaitForSeconds(0.8f);
            daText.NewPhrase("On va la refaire.");
            yield return new WaitForSeconds(2f);
            eventStart12.Invoke();
            yield return new WaitForSeconds(1f);
            doublageManager.gameObject.SetActive(true);
            doublageManager.StartSession();

        }
    }
}
