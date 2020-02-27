using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using TMPro;

namespace VoiceActing
{
    // À supprimer
    public class Trailer : MonoBehaviour
    {
        [SerializeField]
        DoublageManager doublageManager;
        [SerializeField]
        EnemyManager enemyManager;
        [SerializeField]
        ResultScreen resultScreen;
        [SerializeField]
        CameraController cameraController;
        [SerializeField]
        TextAppearManager textAppearManager;
        [SerializeField]
        TextAppearManager mainTextAppearManager;
        [SerializeField]
        CharacterDialogueController characterDialogueController;
        [SerializeField]
        CharacterDialogueController characterDialogueControllerDA;
        [SerializeField]
        StoryCharacterData doremiSprite;
        [SerializeField]
        StoryCharacterData daSprite;
        [SerializeField]
        GameObject speedline;
        [SerializeField]
        Animator finalTransition;
        [SerializeField]
        TextMeshProUGUI textTimer;

        [Title("IntroSequence")]
        [SerializeField]
        TextPerformanceAppear introText;
        [SerializeField]
        TextPerformanceAppear daText;
        [SerializeField]
        GameObject textIntro;
        [SerializeField]
        Image introBlackScreen;

        [Title("Management Sequence")]
        [SerializeField]
        List<ContractData> contractList;
        [SerializeField]
        MenuContractAvailable menuContractAvailable;
        [SerializeField]
        List<VoiceActorData> actorsList;
        [SerializeField]
        MenuActorsManager menuActorsManager;
        [SerializeField]
        Transform menuActorSprite;
        [SerializeField]
        Transform menuActorSpriteShadow;

        [SerializeField]
        protected TextMeshProUGUI textMeshPro;
        int t = 0;

        [Title("AudioSource")]
        [SerializeField]
        protected SimpleSpectrum spectrum;
        [SerializeField]
        protected AudioClip audioClipBattleTheme;
        [SerializeField]
        protected AudioClip audioClipBattleThemeClimax;
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
        CameraMovementData camMov4;
        [SerializeField]
        CameraMovementData camMov5;
        [SerializeField]
        CameraMovementData camMov6;
        [SerializeField]
        CameraMovementData camMov7;
        [SerializeField]
        CameraMovementData camMov8;
        [SerializeField]
        CameraMovementData camMov10;
        [SerializeField]
        CameraMovementData camMov11;
        [SerializeField]
        CameraMovementData camMov12;

        [SerializeField]
        CameraMovementData camMov13;
        [SerializeField]
        CameraMovementData camMov14;
        [SerializeField]
        CameraMovementData camMov15;
        [SerializeField]
        CameraMovementData camMov16;
        [SerializeField]
        CameraMovementData camMov17;

        [SerializeField]
        UnityEvent eventStart;
        [SerializeField]
        UnityEvent eventStart2;
        [SerializeField]
        UnityEvent eventStart3;
        [SerializeField]
        UnityEvent eventStart4;
        [SerializeField]
        UnityEvent eventStart7;
        [SerializeField]
        UnityEvent eventStart8;
        [SerializeField]
        UnityEvent eventStart9;
        [SerializeField]
        UnityEvent eventStart10;
        [SerializeField]
        UnityEvent eventStart11;
        [SerializeField]
        UnityEvent eventStart12;
        [SerializeField]
        UnityEvent eventStart13;
        [SerializeField]
        UnityEvent eventStart15;
        [SerializeField]
        UnityEvent eventStart16;
        [SerializeField]
        UnityEvent eventStart17;

        private int validateTime = 0;
        int minute = 0;
        int second = 0;
        int frame = 0;
        private IEnumerator timerCoroutine;

        public void Start()
        {
            StartCoroutine(TrailerCoroutine());
        }

        private IEnumerator TrailerCoroutine()
        {
            introBlackScreen.gameObject.SetActive(true);
            textAppearManager.SetMouth(characterDialogueController);
            characterDialogueController.SetStoryCharacterData(doremiSprite);
            yield return new WaitForSeconds(5f);
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
            timerCoroutine = TimerCoroutine();
            StartCoroutine(timerCoroutine);
            eventStart.Invoke();
            yield return new WaitForSeconds(1f);
            AudioManager.Instance.SetMusicVolume(70);
            AudioManager.Instance.PlayMusic(audioClipBattleTheme);
            cameraController.MoveToInitialPosition(300);
            yield return new WaitForSeconds(2f);
            textAppearManager.NewPhrase("Bonjour, je m'appelle Dorémi et je suis là pour vous parler de SUPER VOICE ACTING !", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);

            cameraController.SetNoCameraEffect(true);
            cameraController.CameraDataMovement(camMov1);
            textAppearManager.NewPhrase("SUPER VOICE ACTING est un jeu de gestion / RPG.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2f);
            textAppearManager.NewPhrase("Ayant bien sûr pour thème le doublage.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);

            eventStart2.Invoke();
            cameraController.CameraDataMovement(camMov2);
            textAppearManager.NewPhrase("Inspiré par des jeux tels que Phoenix Wright ou Trauma Center.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2.2f);
            eventStart3.Invoke();
            cameraController.CameraDataMovement(camMov3);
            textAppearManager.NewPhrase("Le jeu présentera l'industrie du doublage sous un angle des plus extravagants !", Emotion.Surprise);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);

            eventStart4.Invoke();
            cameraController.CameraDataMovement(camMov4);
            textAppearManager.NewPhrase("Pour ce faire, le joueur incarne un directeur artistique de doublage.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2.5f);

            cameraController.CameraDataMovement(camMov5);
            textAppearManager.NewPhrase("Gérez votre studio d'enregistrement.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2.5f);

            cameraController.CameraDataMovement(camMov6);
            textAppearManager.NewPhrase("Entretenez votre matériel audio et votre équipe technique.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);



            eventStart7.Invoke();
            cameraController.CameraDataMovement(camMov7);
            List<Contract> cList = new List<Contract>();
            for(int i = 0; i < contractList.Count; i++)
            {
                cList.Add(new Contract(contractList[i]));
            }
            menuContractAvailable.SetContractAvailable(cList);
            menuContractAvailable.SelectContract(0);
            textAppearManager.NewPhrase("Acceptez des contrats tels que des films, des publicités ou des jeux vidéo.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(1f);
            menuContractAvailable.SelectContractDown();
            yield return new WaitForSeconds(1f);
            menuContractAvailable.SelectContractDown();
            yield return new WaitForSeconds(2f);


            cameraController.CameraDataMovement(camMov8);
            List<VoiceActor> vaList = new List<VoiceActor>();
            for (int i = 0; i < actorsList.Count; i++)
            {
                vaList.Add(new VoiceActor(actorsList[i]));
            }
            vaList[0].Availability = false;
            vaList[0].ActorMentalState = VoiceActorState.Dead;
            vaList[1].Availability = false;
            vaList[1].ActorMentalState = VoiceActorState.Absent;
            menuActorsManager.SetListActors(vaList);
            eventStart8.Invoke();
            textAppearManager.NewPhrase("Auditionnez les bons comédiens pour interpréter les rôles.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            menuActorsManager.SelectActorDown();
            yield return new WaitForSeconds(1f);
            menuActorSprite.localScale = new Vector3(1, 1, 1);
            menuActorSpriteShadow.localScale = new Vector3(1, 1, 1);
            menuActorsManager.SelectActorDown();
            yield return new WaitForSeconds(3f);

            eventStart9.Invoke();
            cameraController.CameraDataMovement(camMov10);
            textAppearManager.NewPhrase("Et enfin commencez la super session d'enrezistreume...", Emotion.Confiance);
            textAppearManager.ApplyDamage(85);
            yield return null;
            yield return new WaitForSeconds(1.5f);
            cameraController.SetNoCameraEffect(true);
            StopCoroutine(timerCoroutine);
            textAppearManager.SetLetterSpeed(20);
            eventStart10.Invoke();
            AudioManager.Instance.StopMusicWithScratch(120);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(1.5f);

            eventStart11.Invoke();
            characterDialogueControllerDA.SetStoryCharacterData(daSprite);
            yield return new WaitForSeconds(0.8f);
            daText.NewMouthAnim(characterDialogueControllerDA);
            daText.NewPhrase("On va la refaire.");
            yield return new WaitForSeconds(2f);
            eventStart12.Invoke();
            yield return new WaitForSeconds(1f);
            doublageManager.gameObject.SetActive(true);
            doublageManager.StartSession();
            yield return null;
            mainTextAppearManager.ApplyDamage(85);

        }

        public void ValidateAttack()
        {
            textAppearManager.SetPauseText(true);
            validateTime += 1;
            if(validateTime == 1)
            {
                StartCoroutine(TrailerCoroutine2());
            }
            else if (validateTime == 2)
            {
                StartCoroutine(TrailerCoroutine3());
            }
            else if (validateTime == 3)
            {
                StartCoroutine(TrailerCoroutine4());
            }
            else if (validateTime == 4)
            {
                StartCoroutine(TrailerCoroutine5());
            }
            else if (validateTime == 5)
            {
                StartCoroutine(TrailerCoroutine6());
            }
        }

        private IEnumerator TrailerCoroutine2()
        {
            StartCoroutine(timerCoroutine);
            AudioManager.Instance.PlayMusic(audioClipBattleThemeClimax);
            doublageManager.AttackFeedbackPhase();
            cameraController.SetNoCameraEffect(false);
            eventStart13.Invoke();
            while (mainTextAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(1f);
            doublageManager.ForceNextLine();
            doublageManager.RemoveCard();
            doublageManager.SetPhrase();
        }

        private IEnumerator TrailerCoroutine3()
        {
            doublageManager.AttackFeedbackPhase();
            while (mainTextAppearManager.GetEndLine() == false)
                yield return null;
            doublageManager.NewTurn();
        }

        private IEnumerator TrailerCoroutine4()
        {
            doublageManager.AttackFeedbackPhase();
            while (mainTextAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(4.5f);
            doublageManager.ForceNextLine();
            doublageManager.RemoveCard();
            doublageManager.SetPhrase();
        }

        private IEnumerator TrailerCoroutine5()
        {
            doublageManager.AttackFeedbackPhase();
            while (mainTextAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(4.5f);
            doublageManager.ForceNextLine();
            doublageManager.RemoveCard();
            doublageManager.SetPhrase();
        }

        private IEnumerator TrailerCoroutine6()
        {
            characterDialogueController.ActivateAura(false);
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);
            textAppearManager.HideText();
            cameraController.SetNoCameraEffect(true);
            cameraController.CameraDataMovement(camMov11);
            eventStart15.Invoke();
            Emotion[] array = { Emotion.Joie };
            enemyManager.DamagePhrase(100, array, 0);
            mainTextAppearManager.TextPop();
            mainTextAppearManager.SetPauseText(true);
            doublageManager.ShowUI(false);

            doublageManager.ShowResultScreen();
            textAppearManager.SetPauseText(false);
            textAppearManager.SetLetterSpeed(2);
            yield return new WaitForSeconds(3f);
            enemyManager.ResetHalo();
            mainTextAppearManager.HideText();
            textAppearManager.ShowText();
            textAppearManager.NewPhrase("Une fois la session terminée, je gagne en expérience.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2f);

            cameraController.CameraDataMovement(camMov12);
            resultScreen.ValidateNext();
            resultScreen.ValidateNext();
            eventStart16.Invoke();
            textAppearManager.NewPhrase("Dans le but de devenir une meilleure comédienne !", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3.5f);


            cameraController.CameraDataMovement(camMov13);
            textAppearManager.NewPhrase("Enfin le jeu sera accompagné d'un scénario...", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2.5f);

            textAppearManager.NewPhrase("Avec des enjeux dramatiques !", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);

            cameraController.CameraDataMovement(camMov14);
            textAppearManager.NewPhrase("À présent si le projet vous intéresse...", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3f);

            speedline.gameObject.SetActive(true);
            cameraController.CameraDataMovement(camMov15);
            characterDialogueController.Shake();
            textAppearManager.NewPhrase("Soutenez-nous !", Emotion.Surprise);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(3.5f);

            speedline.gameObject.SetActive(false);
            cameraController.CameraDataMovement(camMov16);
            textAppearManager.NewPhrase("Et sur ces paroles...", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2.5f);

            textAppearManager.NewPhrase("Merci de m'avoir écouté.", Emotion.Confiance);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            yield return new WaitForSeconds(2f);

            speedline.gameObject.SetActive(true);
            cameraController.CameraDataMovement(camMov17);
            eventStart17.Invoke();
            textAppearManager.NewPhrase("Et à bientôt j'espère !", Emotion.Joie);
            textAppearManager.ApplyDamage(100);
            while (textAppearManager.GetEndLine() == false)
                yield return null;
            AudioManager.Instance.StopMusic(300);
            yield return new WaitForSeconds(3f);
            finalTransition.gameObject.SetActive(true);
            finalTransition.SetTrigger("End");
        }





        public void SoundCritical()
        {
            AudioManager.Instance.PlaySound(daSprite.CharacterVoice);
        }


        public void DrawTextCard()
        {
            t += 1;
            if(t == 1)
            {
                textMeshPro.text = "Joie";
            }
            else if (t == 2)
            {
                textMeshPro.text = "Tristesse";
            }
            else if (t == 3)
            {
                textMeshPro.text = "Joie";
            }
            else if (t == 4)
            {
                textMeshPro.text = "Joie";
            }
            else if (t == 5)
            {
                textMeshPro.text = "Confiance";
            }
            else if (t == 6)
            {
                textMeshPro.text = "Excité";
                characterDialogueController.ActivateAura(true);
            }
        }

        public void TimerStart()
        {
            StartCoroutine(TimerCoroutine());
        }
        public IEnumerator TimerCoroutine()
        {
            while(true)
            {
                frame += 1;
                if (frame == 60)
                {
                    second += 1;
                    frame = 0;
                }
                if (second == 60)
                {
                    minute += 1;
                    second = 0;
                }
                textTimer.text = "00:0" + minute + ":";
                if (second < 10)
                {
                    textTimer.text += "0" + second + ":";
                }
                else
                {
                    textTimer.text += second + ":";
                }
                if (frame < 10)
                {
                    textTimer.text += "0" + frame;
                }
                else
                {
                    textTimer.text += frame;
                }
                yield return null;
            }

        }


    }
}
