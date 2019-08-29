/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VoiceActing
{

    public class SoundEngineerManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Animator animatorMixingTable;

        [SerializeField]
        InputController inputMixingTable;

        [SerializeField]
        Image vignettageNormal;
        /*[SerializeField]
        GameObject vignettageIngeSon;*/

        [SerializeField]
        TextPerformanceAppear textSkillDescriptionAnimation;
        [SerializeField]
        CharacterDialogueController characterSoundEngineer;
        [SerializeField]
        TextMeshPro textSkillDescription;
        [SerializeField]
        GameObject[] characterShadows;




        [Header("Debug")] // Parce qu'on a rien fixé sur le gamejouer
        [SerializeField]
        int columnSize = 3;
        [SerializeField]
        TextMeshPro[] textButtons;
        [SerializeField]
        TextMeshPro[] textCosts;
        [SerializeField]
        SkillDataSoundEngi[] skills;
        [SerializeField]
        Transform selection;
        [SerializeField]
        GameObject[] lightsTrickery;
        [SerializeField]
        Color colorNormal;
        [SerializeField]
        Color colorUnavailable;

        [Header("DebugEmotion")]
        [SerializeField]
        InputController inputEmotion;
        [SerializeField]
        Transform textDescription;

        int indexList = 0;

        [SerializeField]
        EmotionAttackManager emotionAttackManager;
        
        SkillManager skillManager;
        int trickery = 16;

        int currentComboMax = 0;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void AddTrickery(int amount)
        {
            trickery += amount;
            if (trickery > 16)
                trickery = 16;
            for (int i = 0; i < lightsTrickery.Length; i++)
            {
                if (i < trickery)
                    lightsTrickery[i].SetActive(false);
                else
                    lightsTrickery[i].SetActive(true);
            }
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void SetManagers(SkillManager sM, SoundEngineer soundEngi)
        {
            skillManager = sM;
            characterSoundEngineer.SetStoryCharacterData(soundEngi.SpritesSheets);
            skills = soundEngi.Skills;              
            InitializeSoundEngineer();
        }

        public void InitializeSoundEngineer()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i] != null)
                {
                    textButtons[i].text = skills[i].SkillName;
                    textCosts[i].text = skills[i].ProducerCost.ToString();
                }
            }
        }

        public void SwitchToMixingTable()
        {
            animatorMixingTable.SetBool("Active", true);
            inputMixingTable.gameObject.SetActive(true);
            characterSoundEngineer.gameObject.SetActive(true);
            for (int i = 0; i < characterShadows.Length; i++)
            {
                characterShadows[i].gameObject.SetActive(false);
            }
            CheckCostPreventive();
            DrawDescription(skills[indexList]);
        }

        public void SwitchToBattle()
        {
            animatorMixingTable.SetBool("Active", false);
            inputMixingTable.gameObject.SetActive(false);
        }

        public void ShowCharacterShadows()
        {
            characterSoundEngineer.gameObject.SetActive(false);
            for (int i = 0; i < characterShadows.Length; i++)
            {
                characterShadows[i].gameObject.SetActive(true);
            }
        }



        // On grise le nom des skills dont on a pas le coût
        private void CheckCostPreventive()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i] != null)
                {
                    if (trickery < skills[i].ProducerCost)
                        textButtons[i].color = colorUnavailable;
                    else
                        textButtons[i].color = colorNormal;
                }
            }
        }



        public void MoveInList(int direction) // direction du clavier
        {
            switch (direction)
            {
                case 8: //up
                    indexList -= 1;
                    if (indexList < 0)
                        indexList = skills.Length - 1;
                    break;
                case 2: //down
                    indexList += 1;
                    if (indexList >= skills.Length)
                        indexList = 0;
                    break;

                case 4: //left
                    indexList -= columnSize;
                    if (indexList < 0)
                        indexList = skills.Length - 1;
                    break;
                case 6: //right
                    indexList += columnSize;
                    if (indexList >= skills.Length)
                        indexList = 0;
                    break;
            }
            DrawDescription(skills[indexList]);
        }


        public void DrawDescription(SkillData skill)
        {
            if (skill == null)
            {
                textSkillDescription.text = " ";
                textSkillDescriptionAnimation.NewMouthAnim(characterSoundEngineer);
                textSkillDescriptionAnimation.NewPhrase(" ");
                selection.SetParent(textButtons[indexList].transform);
                selection.transform.localPosition = Vector3.zero;
                return;
            }
            textSkillDescription.text = skill.Description;
            textSkillDescriptionAnimation.NewMouthAnim(characterSoundEngineer);
            textSkillDescriptionAnimation.NewPhrase(skill.Description);
            //characterSoundEngineer.SetPhraseEventTextacting(skill.Description, EmotionNPC.Normal);
            selection.SetParent(textButtons[indexList].transform);
            selection.transform.localPosition = Vector3.zero;
        }

        public void ValidateSkill()
        {
            if (skills[indexList] == null)
                return;

            if (CheckTrickeryCost(skills[indexList].ProducerCost) == true)
            {
                if (skills[indexList].CardSelection == true)
                {
                    StartCoroutine(CoroutineWaitEndOfFrame());
                    return;
                }
                AddTrickery(-skills[indexList].ProducerCost);
                skillManager.ApplySkill(skills[indexList]);
                //unityEvent.Invoke();
            }
            //SwitchToEmotion();
        }


        private bool CheckTrickeryCost(int cost)
        {
            if (cost <= trickery)
            {             
                return true;
            }
            return false;
        }









        private IEnumerator CoroutineWaitEndOfFrame()
        {
            inputMixingTable.enabled = false;
            yield return null;
            yield return null;
            SwitchToSelectEmotion(true);
        }






        public void SwitchToSelectEmotion(bool b)
        {
            inputMixingTable.enabled = !b;
            inputEmotion.enabled = b;
            textDescription.gameObject.SetActive(!b);
            if (b == true)
                SetCardSlot();
            else
                ResetCardSlot();
        }



        public void SetCardSlot()
        {
            int addComboMax = 0;
            currentComboMax = emotionAttackManager.GetComboMax();
            if(skills[indexList].CardComboSize.x <= currentComboMax && currentComboMax <= skills[indexList].CardComboSize.y)
            {
                addComboMax = 0;
            }
            else
            {
                addComboMax = skills[indexList].CardComboSize.y - currentComboMax;
                emotionAttackManager.AddComboMax(addComboMax);
            }
            emotionAttackManager.ShowComboSlot(true);
        }

        public void ResetCardSlot()
        {
            int addComboMax = 0;
            if (skills[indexList].PackSelection)
                emotionAttackManager.ResetPack();
            else
                emotionAttackManager.ResetCard();
            if (skills[indexList].CardComboSize.x <= currentComboMax && currentComboMax <= skills[indexList].CardComboSize.y)
            {
                addComboMax = 0;
            }
            else
            {
                addComboMax = skills[indexList].CardComboSize.y - currentComboMax;
                emotionAttackManager.AddComboMax(-addComboMax);
            }
            emotionAttackManager.ShowComboSlot(false);
        }

        public void RemoveCard()
        {
            if (emotionAttackManager.GetComboCount() == -1)
            {
                SwitchToSelectEmotion(false);
            }
            else
            {
                if (skills[indexList].PackSelection)
                    emotionAttackManager.RemovePack();
                else
                    emotionAttackManager.RemoveCard();
            }
        }

        public void SelectEmotion(string emotion)
        {
            if (skills[indexList].PackSelection)
                emotionAttackManager.SelectPack(emotion);
            else
                emotionAttackManager.SelectCard(emotion);
        }



        public void ValidateEmotion()
        {
            if (emotionAttackManager.GetComboCount() >= skills[indexList].CardComboSize.x-1)
            {

                AddTrickery(-skills[indexList].ProducerCost);
                // On target puis on retire les cartes avant d'appliquer le buff
                skills[indexList].ManualTarget(emotionAttackManager.GetComboEmotion(), skills[indexList].PackSelection);
                SwitchToSelectEmotion(false);
                skillManager.ApplySkill(skills[indexList]);

            }
        }






        #endregion

    } // SoundEngineerManager class
	
}// #PROJECTNAME# namespace
