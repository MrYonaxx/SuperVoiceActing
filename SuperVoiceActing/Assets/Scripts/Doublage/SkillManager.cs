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
    [System.Serializable]
    public class Buff
    {
        private int turn;
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        private int stack;
        public int Stack
        {
            get { return stack; }
            set { stack = value; }
        }

        private SkillData skillData;
        public SkillData SkillData
        {
            get { return skillData; }
            set { skillData = value; }
        }

        private EmotionStat buffTarget;
        public EmotionStat BuffTarget
        {
            get { return buffTarget; }
            set { buffTarget = value; }
        }

        public Buff(SkillData skillEffect)
        {
            skillData = skillEffect;
            turn = skillData.BuffData.TurnActive;
        }
    }

    public class MovelistSkill
    {
        public bool active;
        public SkillActorData skillActor;

        public MovelistSkill(SkillActorData skill)
        {
            skillActor = skill;
            active = false;
        }

        public void SetActive(bool b)
        {
            active = b;
        }
    }

    public struct PassiveSkill
    {
        public int user;
        public SkillActorData skillActor;

        public PassiveSkill(int i, SkillActorData skill)
        {
            user = i;
            skillActor = skill;
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

        [SerializeField]
        RectTransform skillDescriptionPanel;
        [SerializeField]
        TextMeshProUGUI textSkillDescription;

        private int movelistDescriptionIndex = -1;


        List<MovelistSkill> movelistActor = new List<MovelistSkill>();

        List<PassiveSkill> skillsPassiveActor = new List<PassiveSkill>();
        List<PassiveSkill> skillsToActivate = new List<PassiveSkill>();

        List<MinorSkillWindow> listSkillWindow = new List<MinorSkillWindow>();
        List<SkillMovelistWindow> listSkillMovelistWindows = new List<SkillMovelistWindow>();

        CameraController cameraController;
        DoublageBattleParameter battleParameter;
        CharacterDialogueController doubleur;

        List<PassiveSkill> bannedSkills = new List<PassiveSkill>();

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
                int relation = voiceActors[k].Relation;
                if (voiceActors[k].Potentials != null)
                {
                    for (int i = 0; i < voiceActors[k].Potentials.Length; i++)
                    {
                        relation -= voiceActors[k].FriendshipLevel[i];
                        if (relation >= 0)
                        {
                            SkillActorData skill = (SkillActorData)skillDatabase.GetSkillData(voiceActors[k].Potentials[i]);
                            if (skill.IsPassive == true)
                            {
                                skillsPassiveActor.Add(new PassiveSkill(k, skill));
                                Debug.Log("Passive : " + skill.SkillName);
                            }
                        }
                    }
                }
            }
        }

        public void SetMovelist(VoiceActor voiceActor, CharacterDialogueController character)
        {
            currentVoiceActor = voiceActor;
            doubleur = character;
            movelistActor.Clear();

            int relation = voiceActor.Relation;
            if (voiceActor.Potentials != null)
            {
                for (int i = 0; i < voiceActor.Potentials.Length; i++)
                {
                    relation -= voiceActor.FriendshipLevel[i];
                    if (relation >= 0)
                    {
                        SkillActorData skill = (SkillActorData)skillDatabase.GetSkillData(voiceActor.Potentials[i]);
                        if (skill.IsPassive == false)
                        {
                            movelistActor.Add(new MovelistSkill(skill));
                            Debug.Log("Movelist : " + skill.SkillName);
                        }
                    }

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
                listSkillMovelistWindows[i].DrawSkillMove(movelistActor[i].skillActor, i);
                listSkillMovelistWindows[i].gameObject.SetActive(true);
            }
            for (int i = movelistActor.Count; i < listSkillMovelistWindows.Count; i++)
                listSkillMovelistWindows[i].gameObject.SetActive(false);
        }


        public void UpdateMovelist()
        {
            Emotion[] emotions = battleParameter.CurrentAttackEmotion;
            bool b = false;
            string combos = "";
            for (int i = 0; i < movelistActor.Count; i++)
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
                    if(movelistActor[i].active == false)
                    {
                        movelistActor[i].active = true;
                        movelistActor[i].skillActor.PreviewSkill(battleParameter);
                    }
                }
                else
                {
                    if (movelistActor[i].active == true)
                    {
                        movelistActor[i].active = false;
                        movelistActor[i].skillActor.StopPreview(battleParameter);
                    }
                }
            }
            doubleur.ActivateAura(b);
            textCurrentCombos.text = combos;
        }


        public void ActivateMovelist()
        {
            textCurrentCombos.text = "";
            bool moveActivated = false;
            for (int i = 0; i < movelistActor.Count; i++)
            {
                listSkillMovelistWindows[i].ResetSkill();
                if (movelistActor[i].active)
                {
                    movelistActor[i].active = false;
                    movelistActor[i].skillActor.StopPreview(battleParameter);
                    ApplySkill(movelistActor[i].skillActor);
                    moveActivated = true;
                }
            }
            if(moveActivated == true)
                doubleur.FeedbackAura();
        }


        public void DrawMovelistDescription()
        {
            movelistDescriptionIndex += 1;
            if(movelistDescriptionIndex == movelistActor.Count)
            {
                HideMovelistDescription();
                return;
            }
            DrawMovelistDescription(movelistDescriptionIndex);
        }

        public void DrawMovelistDescription(int id)
        {
            skillDescriptionPanel.gameObject.SetActive(true);
            //skillDescriptionPanel.anchoredPosition = new Vector2(skillDescriptionPanel.anchoredPosition.x, listSkillMovelistWindows[id].transform.position.y);
            textSkillDescription.text = movelistActor[id].skillActor.Description;
        }

        public void HideMovelistDescription()
        {
            movelistDescriptionIndex = -1;
            skillDescriptionPanel.gameObject.SetActive(false);
        }

        ///   M O V E L I S T
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///   C O N D I T I O N
        ///   
        public void CheckPassiveSkillCondition(int currentActor) 
        {
            for(int i = 0; i < skillsPassiveActor.Count; i++)
            {
                if (skillsPassiveActor[i].skillActor.OnlyWhenMain == true && skillsPassiveActor[i].user == currentActor ||
                   skillsPassiveActor[i].skillActor.OnlyWhenSupport == true && skillsPassiveActor[i].user != currentActor)
                {
                    if (CheckConditions(skillsPassiveActor[i].skillActor) == true)
                    {
                        skillsToActivate.Add(skillsPassiveActor[i]);
                        if (skillsPassiveActor[i].skillActor.OnlyOnce == true)
                        {
                            bannedSkills.Add(skillsPassiveActor[i]);
                            skillsPassiveActor.RemoveAt(i);
                            i -= 1;
                        }
                    }
                }
            }
        }

        public bool CheckConditions(SkillActorData skill)
        {
            for (int i = 0; i < skill.SkillConditions.Length; i++)
            {
                if (skill.SkillConditions[i].GetSkillConditionNode().CheckCondition(battleParameter) == false)
                    return false;
            }
            return true;
        }

        ///   C O N D I T I O N
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void UnbanSkills(bool line)
        {
            for(int i = 0; i < bannedSkills.Count; i++)
            {
                if (bannedSkills[i].skillActor.OnlyOncePerLine == true && line == true)
                {
                    skillsPassiveActor.Add(bannedSkills[i]);
                    bannedSkills.RemoveAt(i);
                    i -= 1;
                }
                if (bannedSkills[i].skillActor.OnlyOncePerSwitch == true && line == false)
                {
                    skillsPassiveActor.Add(bannedSkills[i]);
                    bannedSkills.RemoveAt(i);
                    i -= 1;
                }
            }
        }






        public void SetSkillText(VoiceActor va, SkillActorData skill)
        {
            textMeshActorName.text = va.VoiceActorName;
            textMeshSkillName.text = skill.SkillName;
            textMeshSkillDesc.text = skill.DescriptionBattle;
            animationPotentiel.SetBool("isMalus", skill.IsMalus);
        }


        public IEnumerator ActivatePassiveSkills(List<VoiceActor> voiceActors)
        {
            for (int i = 0; i < skillsToActivate.Count; i++)
            {
                if (skillsToActivate[i].skillActor.BigAnimation == true)
                {
                    SetSkillText(voiceActors[skillsToActivate[i].user], skillsToActivate[i].skillActor);
                    ActorSkillFeedback();
                    while (inSkillAnimation == true)
                        yield return null;
                    skillsToActivate[i].skillActor.ApplySkill(battleParameter);
                }
                else
                {
                    DrawMinorSkills(voiceActors[skillsToActivate[i].user], skillsToActivate[i].skillActor);
                    skillsToActivate[i].skillActor.ApplySkill(battleParameter);
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
            /*for (int i = 0; i < skillWindowIndex; i++)
            {
                listSkillWindow[i].HideFeedback();
            }*/
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
        /// B U F F



        public void CheckBuffs(List<VoiceActor> voiceActors, bool timingLine = false)
        {
            for(int i = 0; i < voiceActors.Count; i++)
            {
                for (int j = 0; j < voiceActors[i].Buffs.Count; j++)
                {
                    if (voiceActors[i].Buffs[j].SkillData.BuffData.Infinite)
                        continue;
                    if (voiceActors[i].Buffs[j].SkillData.BuffData.LineCount == timingLine)
                    {
                        voiceActors[i].Buffs[j].Turn -= 1;
                        if (voiceActors[i].Buffs[j].SkillData.BuffData.TickEffect == true)
                        {
                            voiceActors[i].Buffs[j].SkillData.ApplySkillEffects(battleParameter);
                        }
                        if (voiceActors[i].Buffs[j].Turn <= 0)
                        {
                            if (voiceActors[i].Buffs[j].SkillData.BuffData.EndEffect == true)
                            {
                                voiceActors[i].Buffs[j].SkillData.ApplySkillEffects(battleParameter);
                            }
                            else
                            {
                                voiceActors[i].Buffs[j].SkillData.RemoveSkill(battleParameter);
                            }
                            voiceActors[i].Buffs.RemoveAt(j);
                            j -= 1;
                        }
                    }
                }
            }
        }

        public void CheckBuffsConditions(List<VoiceActor> voiceActors, bool timingLine = false)
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                for (int j = 0; j < voiceActors[i].Buffs.Count; j++)
                {
                    if (voiceActors[i].Buffs[j].SkillData.BuffData.ConditionRemove)
                    {
                        if(CheckConditions((SkillActorData)voiceActors[i].Buffs[j].SkillData) == false) 
                        {
                            // remove buff && remove skill effect si c'est pas un tick effect
                        }
                    }
                }
            }
        }

        /// B U F F
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////















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