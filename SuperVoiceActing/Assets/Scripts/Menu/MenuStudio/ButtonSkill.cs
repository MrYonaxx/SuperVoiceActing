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

namespace VoiceActing
{
	public class ButtonSkill : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        RectTransform skillProgress;
        [SerializeField]
        TextMeshProUGUI skillName;
        [SerializeField]
        TextMeshProUGUI skillCost;
        [SerializeField]
        GameObject formationIcon;

        [Space]
        [SerializeField]
        Color colorCost;
        [SerializeField]
        Color colorFormation;

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

        public void DrawSkill(SkillData skill)
        {
            if (skill == null)
            {
                skillProgress.transform.localScale = Vector3.zero;
                skillName.text = "";
                skillCost.text = "";
            }
            else
            {
                skillProgress.transform.localScale = Vector3.one;
                skillName.text = skill.SkillName;
                skillName.color = Color.white;
                skillCost.text = skill.ProducerCost.ToString();
                skillCost.color = colorCost;
            }
            formationIcon.gameObject.SetActive(false);
        }

        public void DrawFormation(string name, int formationTime, int formationTotalTime)
        {
            skillProgress.transform.localScale = new Vector2((float)formationTime / formationTotalTime, 1);
            skillName.text = name;
            skillName.color = colorFormation;
            skillCost.text = (formationTotalTime-formationTime).ToString();
            skillCost.color = colorFormation;
            formationIcon.gameObject.SetActive(true);
        }

        #endregion

    } // ButtonSkill class
	
}// #PROJECTNAME# namespace
