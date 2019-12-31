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
using Sirenix.OdinInspector;

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

        private BuffData buffData;
        public BuffData BuffData
        {
            get { return buffData; }
            set { buffData = value; }
        }

        private SkillEffectData[] skillEffectbuff;
        public SkillEffectData[] SkillEffectbuff
        {
            get { return skillEffectbuff; }
            set { skillEffectbuff = value; }
        }

        private EmotionStat buffTarget;
        public EmotionStat BuffTarget
        {
            get { return buffTarget; }
            set { buffTarget = value; }
        }

        public Buff(SkillEffectData[] skillEffect, BuffData buff)
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

        [Title("Managers")]
        [SerializeField]
        CharacterSpriteDatabase characterSpriteDatabase;
        [SerializeField]
        SkillDatabase skillDatabase;

        [Title("Text")]
        [SerializeField]
        TextMeshProUGUI textMeshActorName;
        [SerializeField]
        TextMeshProUGUI textMeshSkillName;
        [SerializeField]
        TextMeshProUGUI textMeshSkillDesc;

        [SerializeField]
        TextMeshProUGUI textCurrentCombos;

        [Title("Feedback")]
        [SerializeField]
        TextAppearManager textToStop;
        [SerializeField]
        Transform panelDoubleurDefault;
        [SerializeField]
        Transform panelDoubleurPotential;



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


        [Header("Movelist")]
        [SerializeField]
        SkillMovelistWindow skillMovelistWindow;
        [SerializeField]
        RectTransform skillMovelistPanel;




        List<SkillActorData> movelistActor = new List<SkillActorData>();
        List<SkillActorData> skillsPassiveActor = new List<SkillActorData>();
        List<SkillActorData> skillsToActivate = new List<SkillActorData>();

        List<MinorSkillWindow> listSkillWindow = new List<MinorSkillWindow>();
        List<SkillMovelistWindow> listSkillMovelistWindows = new List<SkillMovelistWindow>();

        CameraController cameraController;
        DoublageBattleParameter battleParameter;
        CharacterDialogueController doubleur;

        List<string> bannedSkills = new List<string>();

        private int skillWindowIndex = 0;
        private int minorSkillIndex = -1;

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

        public SkillData GetSkill(string skillID)
        {
            if (skillID == "")
                return null;
            return skillDatabase.GetSkillData(skillID);
        }

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

        public void SetManagers(DoublageBattleParameter battleP, CameraController cC)
        {
            battleParameter = battleP;
            cameraController = cC;
            initialSkillPosition = panelDoubleurPotential.localPosition;
            SetPassiveSkills(battleP.VoiceActors);
        }


        public void SetPassiveSkills(List<VoiceActor> voiceActors)
        {
            for (int k = 0; k < voiceActors.Count; k++)
            {
                if (voiceActors[k].Potentials != null)
                {
                    for (int i = 0; i < voiceActors[k].Potentials.Length; i++)
                    {
                        SkillActorData skill = (SkillActorData)skillDatabase.GetSkillData(voiceActors[k].Potentials[i]);
                        if (skill.IsPassive == true)
                            skillsPassiveActor.Add(skill);
                    }
                }
            }
        }

        public void SetMovelist(VoiceActor voiceActor, CharacterDialogueController character)
        {
            currentVoiceActor = voiceActor;
            doubleur = character;
            movelistActor.Clear();
            if (voiceActor.Potentials != null)
            {
                for (int i = 0; i < voiceActor.Potentials.Length; i++)
                {
                    SkillActorData skill = (SkillActorData)skillDatabase.GetSkillData(voiceActor.Potentials[i]);
                    if (skill.IsPassive == false)
                        movelistActor.Add(skill);
                }
            }
            DrawMovelist(voiceActor);
        }












        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   M O V E L I S T
        ///   
        private void DrawMovelist(VoiceActor voiceActor)
        {
            for (int i = 0; i < movelistActor.Count; i++)
            {
                if (listSkillMovelistWindows.Count <= i)
                    listSkillMovelistWindows.Add(Instantiate(skillMovelistWindow, skillMovelistPanel));
                listSkillMovelistWindows[i].DrawSkillMove(movelistActor[i]);
                listSkillMovelistWindows[i].gameObject.SetActive(true);
            }
            for (int i = voiceActor.Potentials.Length; i < listSkillMovelistWindows.Count; i++)
                listSkillMovelistWindows[i].gameObject.SetActive(false);
        }


        public void UpdateMovelist()
        {
            Emotion[] emotions = battleParameter.LastAttackEmotion;
            bool b = false;
            string combos = "";
            for (int i = 0; i < listSkillMovelistWindows.Count; i++)
            {
                if (listSkillMovelistWindows[i].CheckMove(emotions) == true)
                {
                    if (b == false)
                    {
                        b = true;
                        combos = listSkillMovelistWindows[i].GetSkillName();
                    }
                    else
                    {
                        combos += " - " + listSkillMovelistWindows[i].GetSkillName();
                    }
                    skillsToActivate.Add(movelistActor[i]);
                }
                else
                {
                    skillsToActivate.Remove(movelistActor[i]);
                }
            }
            doubleur.ActivateAura(b);
            textCurrentCombos.text = combos;
        }


        public void ActivateMovelist()
        {
            if (skillsToActivate.Count != 0)
            {
                for (int i = 0; i < skillsToActivate.Count; i++)
                {
                    ApplySkill(skillsToActivate[i]);
                }
                skillsToActivate.Clear();
            }
        }

        ///   M O V E L I S T
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   C O N D I T I O N
        ///   
        public void CheckPassiveSkillCondition() 
        {
            for(int i = 0; i < skillsPassiveActor.Count; i++)
            {
                if (skillsPassiveActor[i].CheckConditions(battleParameter) == true)
                {
                    if (CheckBannedSkills(skillsPassiveActor[i].SkillName))
                    {
                        if (skillsPassiveActor[i].OnlyOnce == true)
                            bannedSkills.Add(skillsPassiveActor[i].SkillName);
                        skillsToActivate.Add(skillsPassiveActor[i]);
                    }
                }
            }
        }

        private bool CheckBannedSkills(string skillName)
        {
            for(int i = 0; i < bannedSkills.Count; i++)
            {
                if(bannedSkills[i] == skillName)
                    return false;
            }
            return true;
        }

        ///   C O N D I T I O N
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////









        public void SetSkillText(VoiceActor va, SkillActorData skill)
        {
            textMeshActorName.text = va.VoiceActorName;
            textMeshSkillName.text = skill.SkillName;
            textMeshSkillDesc.text = skill.DescriptionBattle;
            animationPotentiel.SetBool("isMalus", skill.IsMalus);
        }


        public IEnumerator ActivatePassiveSkills()
        {
            for (int i = 0; i < skillsToActivate.Count; i++)
            {
                if (skillsToActivate[i].BigAnimation == true)
                {
                    SetSkillText(currentVoiceActor, skillsToActivate[i]);
                    ActorSkillFeedback();
                    while (inSkillAnimation == true)
                        yield return null;
                    skillsToActivate[i].ApplySkill(battleParameter);
                }
                else
                {
                    DrawMinorSkills(currentVoiceActor, skillsToActivate[i]);
                }

            }
            ActivateNextMinorSkill();
            skillsToActivate.Clear();
        }





        public void DrawMinorSkills(VoiceActor actor, SkillActorData skill)
        {
            if (listSkillWindow.Count <= skillWindowIndex)
            {
                listSkillWindow.Add(Instantiate(minorSkillPrefab, minorSkillPanel));
                listSkillWindow[listSkillWindow.Count - 1].SetTransform(listSkillWindow.Count - 1);
                listSkillWindow[listSkillWindow.Count - 1].DrawSkill(characterSpriteDatabase.GetCharacterData(actor.VoiceActorID).SpriteIcon, skill.SkillName, skill.DescriptionBattle);
            }
            else
            {
                listSkillWindow[skillWindowIndex].DrawSkill(characterSpriteDatabase.GetCharacterData(actor.VoiceActorID).SpriteIcon, skill.SkillName, skill.DescriptionBattle);
            }
            skillWindowIndex += 1;
        }


        // Appelé par Window Minor Skill Prefab suite à ActivateMinorSkills()
        public void ActivateNextMinorSkill()
        {
            minorSkillIndex += 1;
            if (minorSkillIndex >= skillWindowIndex)
            {
                minorSkillIndex = -1;
                return;
            }
            listSkillWindow[minorSkillIndex].ActivateFeedback();
        }

        public void HideSkillWindow()
        {
            for (int i = 0; i < skillWindowIndex; i++)
            {
                listSkillWindow[i].HideFeedback();
            }
            skillWindowIndex = 0;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   A P P L Y    S K I L L

        public void ApplySkill(SkillData skill)
        {
            skill.ApplySkill(battleParameter);
        }

        public void RemoveSkill(SkillData skill)
        {
            //skill.RemoveSkill(doublageBattleParameter);
        }

        public void RemoveSkillEffect(SkillEffectData skillEffect)
        {
            //skillEffect.RemoveSkillEffect(doublageManager);
        }

        public void ForceSkill(SkillActorData skill)
        {
            SetSkillText(currentVoiceActor, skill);
            ActorSkillFeedback();
            ApplySkill(skill);
        }


        public void PreviewSkill(SkillData skill)
        {
            //skill.PreviewTarget(doublageManager);
        }

        public void StopPreview(SkillData skill)
        {
            //skill.StopPreview(doublageManager);
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