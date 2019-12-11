/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SkillActorButton: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        ImageDictionnary emotionDictionary;

        [SerializeField]
        TextMeshProUGUI textSkill;
        [SerializeField]
        TextMeshProUGUI textSkillRelationCost;
        [SerializeField]
        RectTransform skillProgress;

        [SerializeField]
        Image[] imageMovelist;

        [SerializeField]
        Color colorAvailable;
        [SerializeField]
        Color colorUnavailable;

        [SerializeField]
        int skillNumber;
        [SerializeField]
        UnityEventInt eventEnter;
        [SerializeField]
        UnityEventInt eventExit;

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

        public void DrawSkillActor(SkillActorData skill, int relation, int skillCost)
        {
            textSkill.text = skill.SkillName;

            int size = 0;
            for (int i = 0; i < skill.PhraseType.GetLength(); i++)
            {
                int emotionNumber = skill.PhraseType.GetEmotion(i);
                for (int j = 0; j < emotionNumber; j++)
                {
                    imageMovelist[size].sprite = emotionDictionary.GetSprite(i);
                    imageMovelist[size].gameObject.SetActive(true);
                    size += 1;
                }
            }

            for(int i = size; i < imageMovelist.Length; i++)
                imageMovelist[size].gameObject.SetActive(false);

            if (relation < skillCost)
            {
                textSkillRelationCost.text = relation + " / " + skillCost;
                textSkillRelationCost.gameObject.SetActive(true);

                skillProgress.localScale = new Vector3(1 - (relation / (float)skillCost), 1, 1);
                skillProgress.gameObject.SetActive(true);

                for (int i = 0; i < imageMovelist.Length; i++)
                {
                    imageMovelist[i].color = colorUnavailable;
                }
                textSkill.color = colorUnavailable;
            }
            else
            {
                textSkillRelationCost.gameObject.SetActive(false);
                skillProgress.gameObject.SetActive(false);

                for (int i = 0; i < imageMovelist.Length; i++)
                {
                    imageMovelist[i].color = colorAvailable;
                }
                textSkill.color = colorAvailable;
            }

        }


        public void OnPointerEnter(PointerEventData data)
        {
            eventEnter.Invoke(skillNumber);
        }
        public void OnPointerExit(PointerEventData data)
        {
            eventExit.Invoke(skillNumber);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace