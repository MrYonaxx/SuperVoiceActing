/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuStudioSoundEngiFormation : MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Menu SoundEngi Formation")]
        [SerializeField]
        ButtonEquipement buttonPrefab;

        [Title("Menu Formation Description")]
        [SerializeField]
        TextMeshProUGUI textFormationName;
        [SerializeField]
        TextMeshProUGUI textFormationTime;
        [SerializeField]
        TextMeshProUGUI textFormationSkillName;
        [SerializeField]
        TextMeshProUGUI textFormationSkillDescription;

        [SerializeField]
        List<ButtonEquipement> buttonFormationList = new List<ButtonEquipement>();

        List<FormationData> playerFormationDatas = new List<FormationData>();

        [SerializeField]
        SoundEngineerData[] soundEngiTable;
        [SerializeField]
        MenuStudioSoundEngiSkillSelection menuStudioSoundEngiSkillSelection;

        int soundEngiID = 0;
        SoundEngineer soundEngineer;

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
            animatorSelection.gameObject.SetActive(true);
        }
        private void OnDisable()
        {
            animatorSelection.gameObject.SetActive(false);
        }

        public void SetSoundEngiID(SoundEngineer soundEngi)
        {
            soundEngineer = soundEngi;
            for (int i = 0; i < soundEngiTable.Length; i++)
            {
                if (soundEngi.EngineerName == soundEngiTable[i].EngineerName)
                {
                    soundEngiID = i;
                    RedrawList();
                    return;
                }
            }

        }

        public void RedrawList()
        {
            for (int i = 0; i < playerFormationDatas.Count; i++)
            {
                buttonFormationList[i].DrawFormation(playerFormationDatas[i], soundEngiID);
            }
            indexSelected = 0;
            //MoveScrollRect();
        }

        public void CreateButtonFormation(List<FormationData> formDatas)
        {
            playerFormationDatas = formDatas;
            buttonFormationList.Clear();
            buttonsList.Clear();

            for (int i = 0; i < playerFormationDatas.Count; i++)
            {
                buttonFormationList.Add(Instantiate(buttonPrefab, buttonScrollListTransform));
                buttonFormationList[i].DrawFormation(playerFormationDatas[i], 0);
                buttonFormationList[i].gameObject.SetActive(true);
                buttonsList.Add(buttonFormationList[i].GetRectTransform());
            }
        }

        private void DrawFormationDescription(FormationData formData)
        {
            textFormationName.text = formData.FormationName;
            textFormationTime.text = formData.FormationTime.ToString();

            if (formData.FormationSkills[soundEngiID] != null)
            {
                textFormationSkillName.text = formData.FormationSkills[soundEngiID].SkillName;
                textFormationSkillDescription.text = formData.FormationSkills[soundEngiID].Description;
            }
            else
            {
                textFormationSkillName.text = "(Ce personnage ne peut rien apprendre.)";
                textFormationSkillDescription.text = "";
            }
        }

        protected override void AfterSelection()
        {
            base.AfterSelection();
            DrawFormationDescription(playerFormationDatas[indexSelected]);
        }

        public void Validate()
        {
            if (playerFormationDatas[indexSelected].FormationSkills[soundEngiID] == null)
            {
                return;
            }
            this.gameObject.SetActive(false);
            menuStudioSoundEngiSkillSelection.gameObject.SetActive(true);
        }

        public void SetSkillFormation()
        {
            soundEngineer.SetSkillFormation(menuStudioSoundEngiSkillSelection.GetIndexSelected(),
                                            playerFormationDatas[indexSelected].FormationSkills[soundEngiID],
                                            playerFormationDatas[indexSelected].FormationTime);
        }

        #endregion

    } // MenuStudioSoundEngiFormation class
	
}// #PROJECTNAME# namespace
