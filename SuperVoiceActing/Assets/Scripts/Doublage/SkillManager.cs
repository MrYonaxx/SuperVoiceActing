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
using System.Collections.Generic;

namespace VoiceActing
{
    [System.Serializable] // à réactiver si un jour j'ai besoin de sauvegardé la liste des buffs
    public class Buff
    {
        private int turn;
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        private SkillEffectData skillEffectbuff;
        public SkillEffectData SkillEffectbuff
        {
            get { return skillEffectbuff; }
            set { skillEffectbuff = value; }
        }

        private BuffData buffData;
        public BuffData BuffData
        {
            get { return buffData; }
            set { buffData = value; }
        }

        public Buff(SkillEffectData skillEffect, BuffData buff)
        {
            skillEffectbuff = skillEffect;
            turn = buff.TurnActive;
            buffData = buff;
        }
    }



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
        Animator animationPotentiel;
        [SerializeField]
        GameObject animationSkillName;
        [SerializeField]
        ParticleSystem particleSpeedLines;

        CameraController cameraController;
        DoublageManager doublageManager;



        [Header("Buff")]
        List<string> bannedSkills = new List<string>();




        private IEnumerator coroutineSkill = null;
        private bool reprintTextEnemy = false;
        Vector3 initialScaleText;
        private bool inSkillAnimation = false;
        Vector3 initialSkillPosition;

        VoiceActor currentVoiceActor;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */
        public bool InSkillAnimation()
        {
            return inSkillAnimation;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public bool CheckReprintTextEnemy()
        {
            if(reprintTextEnemy == true)
            {
                reprintTextEnemy = false;
                return true;
            }
            return false;
        }

        public void SetManagers(DoublageManager dm, CameraController cC)
        {
            doublageManager = dm;
            cameraController = cC;
            initialSkillPosition = panelDoubleurPotential.localPosition;
        }


        public void SetCurrentVoiceActor(VoiceActor voiceActor)
        {
            currentVoiceActor = voiceActor;
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   C O N D I T I O N


        public bool CheckSkillCondition(VoiceActor voiceActor, string phase, EmotionCard[] emotions) 
        {
            /*if (emotions == null)
                return false;*/
            bool next = false;
            for(int i = 0; i < voiceActor.Potentials.Length; i++)
            {
                SkillActorData skill = voiceActor.Potentials[i];
                Debug.Log(skill.SkillName);
                switch(phase)
                {
                    case "Start":
                        if (skill.AfterStart == true)
                            next = true;
                        break;

                    case "Selection":
                        if (skill.AfterAttack == true)
                            next = true;
                        break;

                    case "Attack":
                        if(skill.AfterAttack == true)
                            next = true;
                        break;

                    case "Critical":
                        if (skill.AfterCritical == true)
                            next = true;
                        break;

                    case "Counter":
                        if (skill.AfterAttack == true)
                            next = true;
                        break;

                    case "Kill":
                        if (skill.AfterKill == true)
                            next = true;
                        break;
                }

                if(next == true)
                {
                    if (CheckHP((voiceActor.Hp / (float)voiceActor.HpMax), skill.HpInterval))
                    {
                        if (CheckPercentage(skill.PercentageActivation))
                        {
                            if (CheckAttackType(emotions, skill.PhraseType))
                            {
                                if (CheckBannedSkills(skill.SkillName))
                                {
                                    if (skill.OnlyOnce == true)
                                        bannedSkills.Add(skill.SkillName);
                                    SetSkillText(voiceActor, skill);
                                    ActorSkillFeedback();
                                    skill.ApplySkill(doublageManager);
                                    //ApplySkill(skill);
                                    return true; // On verra pour l'activation de compétence multiple plus tard
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void SetSkillText(VoiceActor va, SkillActorData skill)
        {
            textMeshActorName.text = va.Name;
            textMeshSkillName.text = skill.SkillName;
            textMeshSkillDesc.text = skill.DescriptionBattle;
            animationPotentiel.SetBool("isMalus", skill.IsMalus);
        }

        private bool CheckAttackType(EmotionCard[] emotions, EmotionStat emotionCheck)
        {
            EmotionStat attackCheck = new EmotionStat(emotionCheck.Joy, emotionCheck.Sadness, emotionCheck.Disgust, emotionCheck.Anger,
                                                      emotionCheck.Surprise, emotionCheck.Sweetness, emotionCheck.Fear, emotionCheck.Trust
                                                      );
            if (emotions != null)
            {
                for (int i = 0; i < emotions.Length; i++)
                {
                    if (emotions[i] == null)
                    {
                        continue;
                    }
                    attackCheck.Add((int)emotions[i].GetEmotion(), -1);
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
            hp *= 100;
            return (hpCheck.x <= hp && hp <= hpCheck.y);
        }

        private bool CheckPercentage(float percentage)
        {
            return (Random.Range(0, 100) < percentage);
        }

        private bool CheckBannedSkills(string skillName)
        {
            for(int i = 0; i < bannedSkills.Count; i++)
            {
                if(bannedSkills[i] == skillName)
                {
                    return false;
                }
            }
            return true;
        }







        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   A P P L Y    S K I L L

        public void ApplySkill(SkillData skill)
        {
            skill.ApplySkill(doublageManager);
        }

        public void ForceSkill(SkillActorData skill)
        {
            SetSkillText(currentVoiceActor, skill);
            ActorSkillFeedback();
            ApplySkill(skill);
        }





        /*public void CheckBuffs(List<Buff> buffs, SkillTarget skillTarget)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                buffs[i].Turn -= 1;
                if (buffs[i].Turn == 0)
                {
                    //buffs[i].SkillEffectbuff.RemoveSkillEffect(skillTarget, actorsManager, enemyManager, doublageManager);
                    buffs.RemoveAt(i);
                }
            }
        }*/







        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// FEEDBACK
        [ContextMenu("Hey")]
        public void ActorSkillFeedback()
        {
            // Stop text & Camera
            textToStop.SetPauseText(true);
            cameraController.SetCameraPause(true);

            // Activation Animation
            animationSkillName.SetActive(false);
            animationPotentiel.gameObject.SetActive(true);
            textMeshSkillDesc.gameObject.SetActive(false);

            inSkillAnimation = true;
        }

        // Appelé par animationPotentiel
        public void ActorSkillFeedback2()
        {
            //emotionAttackManager.SwitchCardTransformToRessource();

            // Doubleur replacé
            panelDoubleurPotential.transform.localPosition = initialSkillPosition + currentVoiceActor.SkillOffset;  //actorsManager.GetSkillPositionOffset();
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

            // Doubleur replacé
            doubleur.transform.SetParent(panelDoubleurDefault);
            doubleur.ChangeOrderInLayer(-2);

            // Anim stop
            animationPotentiel.gameObject.SetActive(false);
            particleSpeedLines.Stop();

            // Text
            cameraController.SetCameraPause(false);
            textToStop.SetPauseText(false);
            textToStop.transform.localScale = initialScaleText;

            cameraController.ChangeOrthographicSize(0, 30);

            inSkillAnimation = false;
            if (coroutineSkill != null)
                StopCoroutine(coroutineSkill);
            coroutineSkill = MoveActorCoroutine(1.02f);
            StartCoroutine(coroutineSkill);
        }

        #endregion

    } // SkillManager class

} // #PROJECTNAME# namespace