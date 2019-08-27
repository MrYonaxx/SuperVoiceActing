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
        SkillData[] skills;
        [SerializeField]
        Transform selection;
        [SerializeField]
        GameObject[] lightsTrickery;
        [SerializeField]
        Color colorNormal;
        [SerializeField]
        Color colorUnavailable;
        [SerializeField]
        UnityEvent unityEvent;

        [Header("DebugEmotion")]
        [SerializeField]
        InputController inputEmotion;
        [SerializeField]
        Transform[] emotionButtons;
        [SerializeField]
        Transform selectionEmotion;
        [SerializeField]
        Transform textDescription;

        int indexList = 0;
        int indexEmotion = 0;

        SkillManager skillManager;
        int trickery = 16;

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

        public void SetManagers(SkillManager sM)
        {
            skillManager = sM;
            InitializeSoundEngineer();
        }

        public void InitializeSoundEngineer()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                textButtons[i].text = skills[i].SkillName;
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

        public void SwitchToEmotion()
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
                if (trickery < skills[i].ProducerCost)
                    textButtons[i].color = colorUnavailable;
                else
                    textButtons[i].color = colorNormal;
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
            textSkillDescription.text = skill.Description;
            characterSoundEngineer.SetPhraseEventTextacting(skill.Description, EmotionNPC.Normal);
            selection.SetParent(textButtons[indexList].transform);
            selection.transform.localPosition = Vector3.zero;
        }

        public void ValidateSkill()
        {
            if (CheckTrickeryCost(skills[indexList].ProducerCost) == true)
            {
                /*if (skills[indexList].SkillTarget == SkillTarget.ManualPackSelection)
                {
                    StartCoroutine(CoroutineWaitEndOfFrame());
                    //SwitchToSelectEmotion(true);
                    return;
                }*/
                AddTrickery(-skills[indexList].ProducerCost);
                skillManager.ApplySkill(skills[indexList]);
                unityEvent.Invoke();
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
            selectionEmotion.gameObject.SetActive(b);
            textDescription.gameObject.SetActive(!b);
        }

        public void SelectEmotion(int direction)
        {
            switch (direction)
            {
                case 4: //left
                    indexEmotion -= 1;
                    if (indexEmotion < 0)
                        indexEmotion = emotionButtons.Length - 1;
                    break;
                case 6: //right
                    indexEmotion += 1;
                    if (indexEmotion >= emotionButtons.Length)
                        indexEmotion = 0;
                    break;
            }
            selectionEmotion.SetParent(emotionButtons[indexEmotion].transform);
            selectionEmotion.transform.localPosition = Vector3.zero;
        }

        public void ValidateEmotion()
        {
            SwitchToSelectEmotion(false);
            skills[indexList].ManualTarget((Emotion)indexEmotion + 1);
            AddTrickery(-skills[indexList].ProducerCost);
            skillManager.ApplySkill(skills[indexList]);
        }






        #endregion

    } // SoundEngineerManager class
	
}// #PROJECTNAME# namespace
