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

        [Header("Managers")]
        [SerializeField]
        SkillDatabase skillDatabase;

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

        CharacterDialogueController doubleur;


        [SerializeField]
        Animator animationPotentiel;
        [SerializeField]
        GameObject animationSkillName;
        [SerializeField]
        ParticleSystem particleSpeedLines;


        [Header("Minor Skills")]
        [SerializeField]
        MinorSkillWindow minorSkillPrefab;
        [SerializeField]
        RectTransform minorSkillPanel;

        List<SkillActorData> skillsActors = new List<SkillActorData>();
        List<SkillActorData> skillsToActivate = new List<SkillActorData>();

        List<MinorSkillWindow> listSkillWindow = new List<MinorSkillWindow>();

        CameraController cameraController;
        DoublageManager doublageManager;



        [Header("Buff")]
        List<string> bannedSkills = new List<string>();

        private int skillWindowIndex = 0;
        private int minorSkillIndex = 0;

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

        public void SetManagers(DoublageManager dm, CameraController cC, List<VoiceActor> voiceActors)
        {
            doublageManager = dm;
            cameraController = cC;
            initialSkillPosition = panelDoubleurPotential.localPosition;
            SetSkills(voiceActors);
        }


        public void SetCurrentVoiceActor(VoiceActor voiceActor, CharacterDialogueController character)
        {
            currentVoiceActor = voiceActor;
            doubleur = character;
        }

        private void SetSkills(List<VoiceActor> voiceActors)
        {
            for (int k = 0; k < voiceActors.Count; k++)
            {
                for (int i = 0; i < voiceActors[k].Potentials.Length; i++)
                {
                    skillsActors.Add((SkillActorData)skillDatabase.GetSkillData(voiceActors[k].Potentials[i]));
                }
            }
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   C O N D I T I O N

        // Aucune gestion de priorité, càd que on check tout les skills en même temps, pas les un après les autres. peut enlever certains combos.

        public void CheckSkillCondition(List<VoiceActor> voiceActors, List<SkillActiveTiming> phases, Emotion[] emotions, bool minorSkill) 
        {
            bool next = false;
            bool currentMainActor = false;
            skillWindowIndex = 0;
            int n = 0;
            for (int k = 0; k < voiceActors.Count; k++)
            {
                if (voiceActors[k] == currentVoiceActor)
                    currentMainActor = true;
                else
                    currentMainActor = false;

                for (int i = 0; i < voiceActors[k].Potentials.Length; i++)
                {
                    next = false;
                    // Cette partie est dangereuse et peut potentiellement provoquer des bugs à surveiller
                    SkillActorData skill = skillsActors[n];
                    n+=1;
                    //
                    if (currentMainActor == false && skill.OnlyWhenMain == true)
                        continue;
                    for (int j = 0; j < phases.Count; j++)
                    {
                        if (skill.ActivationTiming == phases[j])
                        {
                            next = true;
                            break;
                        }
                    }

                    if (next == true)
                    {
                        if (CheckHP((voiceActors[k].Hp / (float)voiceActors[k].HpMax), skill.HpInterval))
                        {
                            if (CheckPercentage(skill.PercentageActivation))
                            {
                                if (CheckAttackType(emotions, skill.PhraseType))
                                {
                                    if (CheckBannedSkills(skill.SkillName))
                                    {
                                        if (skill.OnlyOnce == true)
                                            bannedSkills.Add(skill.SkillName);
                                        skillsToActivate.Add(skill);
                                        DrawMinorSkills(voiceActors[k], skill);
                                    }
                                }
                            }
                        }
                    }
                    //
                }
            }
        }

        public void SetSkillText(VoiceActor va, SkillActorData skill)
        {
            textMeshActorName.text = va.Name;
            textMeshSkillName.text = skill.SkillName;
            textMeshSkillDesc.text = skill.DescriptionBattle;
            animationPotentiel.SetBool("isMalus", skill.IsMalus);
        }

        private bool CheckAttackType(Emotion[] emotions, EmotionStat emotionCheck)
        {
            EmotionStat attackCheck = new EmotionStat(emotionCheck.Joy, emotionCheck.Sadness, emotionCheck.Disgust, emotionCheck.Anger,
                                                      emotionCheck.Surprise, emotionCheck.Sweetness, emotionCheck.Fear, emotionCheck.Trust
                                                      );
            if (emotions != null)
            {
                for (int i = 0; i < emotions.Length; i++)
                {
                    attackCheck.Add((int)emotions[i], -1);
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







        public IEnumerator ActivateBigSkill()
        {
            for(int i = 0; i < skillsToActivate.Count; i++)
            {
                if(skillsToActivate[i].BigAnimation == true)
                {
                    SetSkillText(currentVoiceActor, skillsToActivate[i]);
                    ActorSkillFeedback();
                    while (inSkillAnimation == true)
                        yield return null;
                    skillsToActivate[i].ApplySkill(doublageManager);
                    skillsToActivate.RemoveAt(i);
                    i-=1;
                }
                
            }
        }



        public void HideSkillWindow()
        {
            for (int i = 0; i < skillWindowIndex; i++)
            {
                listSkillWindow[i].HideFeedback();
            }
            skillWindowIndex = 0;
        }

        public void DrawMinorSkills(VoiceActor actor, SkillActorData skill)
        {
            if (skill.BigAnimation == false)
            {
                if (listSkillWindow.Count <= skillWindowIndex)
                {
                    listSkillWindow.Add(Instantiate(minorSkillPrefab, minorSkillPanel));
                    listSkillWindow[listSkillWindow.Count - 1].SetTransform(listSkillWindow.Count - 1);
                    listSkillWindow[listSkillWindow.Count - 1].DrawSkill(actor.SpriteSheets.SpriteIcon, skill.SkillName, skill.DescriptionBattle);
                }
                else
                {
                    listSkillWindow[skillWindowIndex].DrawSkill(actor.SpriteSheets.SpriteIcon, skill.SkillName, skill.DescriptionBattle);
                }
                skillWindowIndex += 1;
            }
        }

        public void ActivateMinorSkills()
        {
            if (skillsToActivate.Count == 0)
                return;

            for (int i = 0; i < skillsToActivate.Count; i++)
            {
                if (skillsToActivate[i].BigAnimation == false)
                {
                    skillsToActivate[i].ApplySkill(doublageManager);
                    skillsToActivate.RemoveAt(i);
                    i -= 1;
                }
            }
            minorSkillIndex = 0;
            listSkillWindow[minorSkillIndex].ActivateFeedback();
        }


        // Appelé par Window Minor Skill Prefab suite à ActivateMinorSkills()
        public void ActivateNextMinorSkill()
        {
            minorSkillIndex += 1;
            if (minorSkillIndex == skillWindowIndex)
            {
                minorSkillIndex = 0;
                return;
            }
            listSkillWindow[minorSkillIndex].ActivateFeedback();
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   A P P L Y    S K I L L

        public void ApplySkill(SkillData skill)
        {
            skill.ApplySkill(doublageManager);
        }

        public void RemoveSkill(SkillData skill)
        {
            skill.RemoveSkill(doublageManager);
        }

        public void RemoveSkillEffect(SkillEffectData skillEffect)
        {
            skillEffect.RemoveSkillEffect(doublageManager);
        }

        public void ForceSkill(SkillActorData skill)
        {
            SetSkillText(currentVoiceActor, skill);
            ActorSkillFeedback();
            ApplySkill(skill);
        }


        public void PreviewSkill(SkillData skill)
        {
            skill.PreviewTarget(doublageManager);
        }

        public void StopPreview(SkillData skill)
        {
            skill.StopPreview(doublageManager);
        }





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