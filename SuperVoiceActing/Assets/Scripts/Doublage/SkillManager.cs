/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the SkillManager class
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Text")]
        [SerializeField]
        TextMeshProUGUI textMeshActorName;
        [SerializeField]
        TextMeshProUGUI textMeshSkillName;
        [SerializeField]
        TextMeshProUGUI textMeshSkillDesc;

        [Header("Feedback")]
        [SerializeField]
        TextAppearManager textToStop;
        [SerializeField]
        Transform panelDoubleurDefault;
        [SerializeField]
        Transform panelDoubleurPotential;
        [SerializeField]
        CharacterDialogueController doubleur;

        [SerializeField]
        GameObject animationPotentiel;
        [SerializeField]
        GameObject animationSkillName;
        [SerializeField]
        ParticleSystem particleSpeedLines;

        [Header("Managers")]
        [SerializeField]
        CameraController cameraController;
        [SerializeField]
        EmotionAttackManager emotionAttackManager;



        private IEnumerator coroutineSkill = null;
        Vector3 initialScaleText;

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

        public bool CheckPassiveSkills()
        {
            return false;
        }

        public void CheckSkillCondition(VoiceActor voiceActor, string phase, Emotion[] emotions) 
        {
            if (emotions == null)
                return;
            bool next = false;
            for(int i = 0; i < voiceActor.Potentials.Length; i++)
            {
                SkillData skill = voiceActor.Potentials[i];
                Debug.Log(skill.SkillName);
                switch(phase)
                {
                    case "Attack":
                        if(skill.AfterAttack == true)
                        {
                            next = true;
                        }
                        break;

                    case "Critical":
                        if (skill.AfterCritical == true)
                        {
                            next = true;
                        }
                        break;

                    case "Kill":
                        if (skill.AfterKill == true)
                        {
                            next = true;
                        }
                        break;
                }

                if(next == true)
                {
                    Debug.Log("FirstCheck");
                    if (CheckHP((voiceActor.Hp / voiceActor.HpMax), skill.HpInterval))
                    {
                        Debug.Log("SecondCheck");
                        if (CheckPercentage(skill.PercentageActivation))
                        {
                            Debug.Log("FinalCheck");
                            if (CheckAttackType(emotions, skill.PhraseType))
                            {
                                textMeshActorName.text = voiceActor.Name;
                                textMeshSkillName.text = skill.SkillName;
                                textMeshSkillDesc.text = skill.DescriptionBattle;
                                ActorSkillFeedback();
                                return; // On verra pour l'activation de compétence multiple plus tard
                            }
                        }
                    }
                }
            }
        }

        private bool CheckAttackType(Emotion[] emotions, EmotionStat emotionCheck)
        {
            EmotionStat attackCheck = new EmotionStat(emotionCheck.Joy, emotionCheck.Sadness, emotionCheck.Disgust, emotionCheck.Anger,
                                                      emotionCheck.Surprise, emotionCheck.Sweetness, emotionCheck.Fear, emotionCheck.Trust
                                                      );
            for(int i = 0; i < emotions.Length; i++)
            {
                Debug.Log(emotions[i]);
                switch(emotions[i])
                {
                    case Emotion.Neutre:
                        attackCheck.Neutral -= 1;
                        break;
                    case Emotion.Joie:
                        attackCheck.Joy -= 1;
                        break;
                    case Emotion.Tristesse:
                        attackCheck.Sadness -= 1;
                        break;
                    case Emotion.Dégoût:
                        attackCheck.Disgust -= 1;
                        break;
                    case Emotion.Colère:
                        attackCheck.Anger -= 1;
                        break;
                    case Emotion.Surprise:
                        attackCheck.Surprise -= 1;
                        break;
                    case Emotion.Douceur:
                        attackCheck.Sweetness -= 1;
                        break;
                    case Emotion.Peur:
                        attackCheck.Fear -= 1;
                        break;
                    case Emotion.Confiance:
                        attackCheck.Trust -= 1;
                        break;
                }
            }
            if (attackCheck.Neutral > 0) return false;
            if (attackCheck.Joy > 0) return false;
            if (attackCheck.Sadness > 0) return false;
            if (attackCheck.Disgust > 0) return false;
            if (attackCheck.Anger > 0) return false;
            if (attackCheck.Surprise > 0) return false;
            if (attackCheck.Sweetness > 0) return false;
            if (attackCheck.Fear > 0) return false;
            if (attackCheck.Trust > 0) return false;
            return true;
        }

        private bool CheckHP(float hp, Vector2Int hpCheck)
        {
            return (hpCheck.x <= hp && hp <= hpCheck.y);
        }

        private bool CheckPercentage(float percentage)
        {
            return (Random.Range(0, 100) < percentage);
        }















        [ContextMenu("Hey")]
        public void ActorSkillFeedback()
        {
            // Stop text & Camera
            textToStop.SetPauseText(true);
            cameraController.SetCameraPause(true);

            // Activation Animation
            animationSkillName.SetActive(false);
            animationPotentiel.SetActive(true);
            textMeshSkillDesc.gameObject.SetActive(false);
        }

        // Appelé par animationPotentiel
        public void ActorSkillFeedback2()
        {
            emotionAttackManager.SwitchCardTransformToRessource();

            // Doubleur replacé
            doubleur.transform.SetParent(panelDoubleurPotential);
            doubleur.ChangeOrderInLayer(60);
            cameraController.ChangeOrthographicSize(-30, 30);

            // Anim
            particleSpeedLines.Play();
            textMeshSkillDesc.gameObject.SetActive(true);
            animationSkillName.SetActive(true);

            // Text
            textToStop.SetPauseText(true);
            initialScaleText = textToStop.transform.localScale;
            textToStop.transform.localScale = new Vector3(0, 0, 0);

            // Coroutine
            if (coroutineSkill != null)
                StopCoroutine(coroutineSkill);
            coroutineSkill = MoveActorCoroutine(1.08f);
            StartCoroutine(coroutineSkill);
        }

        private IEnumerator MoveActorCoroutine(float speedCoroutine)
        {
            while(doubleur.transform.localPosition != Vector3.zero || doubleur.transform.localEulerAngles != Vector3.zero)
            {

                float angleX = doubleur.transform.localEulerAngles.x;
                if (angleX > 1 && angleX < 359)
                {
                    float ratioX = 0;
                    if (angleX > 180)
                    {
                        angleX = 360 - angleX;
                        ratioX = angleX * speedCoroutine;
                        angleX = ratioX - angleX;
                    }
                    else
                    {
                        ratioX = angleX - (angleX / speedCoroutine);
                        angleX = -ratioX;
                    }
                }
                else
                {
                    angleX = 0;
                }

                float angleY = doubleur.transform.localEulerAngles.y;
                if (angleY > 1 && angleY < 359)
                {
                    float ratioY = 0;
                    if (angleY > 180)
                    {
                        angleY = 360 - angleY;
                        ratioY = angleY * speedCoroutine;
                        angleY = ratioY - angleY;
                    }
                    else
                    {
                        ratioY = angleY - (angleY / speedCoroutine);
                        angleY = -ratioY;
                    }
                }
                else
                {
                    angleY = 0;
                }


                float angleZ = doubleur.transform.localEulerAngles.z;
                if (angleZ > 1 && angleZ < 359)
                {
                    float ratioZ = 0;
                    if (angleZ > 180)
                    {
                        angleZ = 360 - angleZ;
                        ratioZ = angleZ * speedCoroutine;
                        angleZ = ratioZ - angleZ;
                    }
                    else
                    {
                        ratioZ = angleZ - (angleZ / speedCoroutine);
                        angleZ = -ratioZ;
                    }
                }
                else
                {
                    angleZ = 0;
                }
                doubleur.transform.localEulerAngles += new Vector3(angleX, angleY, angleZ);

                doubleur.transform.localPosition /= speedCoroutine;

                if (doubleur.transform.localPosition.magnitude < 0.001f)
                    doubleur.transform.localPosition = Vector3.zero;
                if (doubleur.transform.localEulerAngles.magnitude < 0.001f)
                    doubleur.transform.localEulerAngles = Vector3.zero;

                yield return null;
            }
        }

        public void ActorSkillStopFeedback()
        {
            emotionAttackManager.SwitchCardTransformToBattle();

            // Doubleur replacé
            doubleur.transform.SetParent(panelDoubleurDefault);
            doubleur.ChangeOrderInLayer(-2);

            // Anim stop
            animationPotentiel.SetActive(false);
            particleSpeedLines.Stop();

            // Text
            cameraController.SetCameraPause(false);
            textToStop.SetPauseText(false);
            textToStop.transform.localScale = initialScaleText;

            cameraController.ChangeOrthographicSize(0, 30);
            
            if (coroutineSkill != null)
                StopCoroutine(coroutineSkill);
            coroutineSkill = MoveActorCoroutine(1.02f);
            StartCoroutine(coroutineSkill);
        }

        #endregion

    } // SkillManager class

} // #PROJECTNAME# namespace