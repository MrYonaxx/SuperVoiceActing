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
using TMPro;

namespace VoiceActing
{
	public class PanelContractCharacter : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI textRoleName;
        [SerializeField]
        TextMeshProUGUI textRoleFan;
        [SerializeField]
        TextMeshProUGUI textRoleLine;
        [SerializeField]
        TextMeshProUGUI textRoleCadence;
        [SerializeField]
        TextMeshProUGUI textRoleInfluence;
        [SerializeField]
        TextMeshProUGUI textCharacterType;
        [SerializeField]
        RectTransform transformTimbre;

        [SerializeField]
        Image roleFace;

        [Header("")]
        [SerializeField]
        float maxStatValue = 100f;
        [SerializeField]
        Color colorBest;
        [SerializeField]
        Color colorNormal;

        [SerializeField]
        TextMeshProUGUI[] textStats;

        [Header("")]

        [SerializeField]
        Animator[] animatorsStat;

        [SerializeField]
        RectTransform[] imageSlider;



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

        public void DrawPanel(Role role)
        {
            textRoleName.text = role.Name;
            textRoleFan.text = role.Fan.ToString();
            textRoleLine.text = role.Line.ToString();
            textRoleCadence.text = role.Attack.ToString();
            textRoleInfluence.text = role.Defense.ToString();
            if (textCharacterType != null)
            {
                textCharacterType.text = role.RoleType.ToString();
            }
            /*if (textCharacterType != null)
            {
                textCharacterType.text = role.RoleType + " - " + role.RolePersonality;
            }*/
            if (role.RoleSprite == null)
            {
                roleFace.enabled = false;
            }
            else
            {
                roleFace.enabled = true;
                roleFace.sprite = role.RoleSprite;
            }

            transformTimbre.anchorMin = new Vector2((role.Timbre.x + 10) / 20f, 0);
            transformTimbre.anchorMax = new Vector2((role.Timbre.y + 10) / 20f, 1);
            transformTimbre.anchoredPosition = Vector3.zero;


            for (int i = 0; i < textStats.Length; i++)
            {

                int currentStat = role.CharacterStat.GetEmotion(i+1);
                textStats[i].text = currentStat.ToString();
                imageSlider[i].transform.localScale = new Vector3(currentStat / maxStatValue, 1, 1);
                if (i + 1 == role.BestStatEmotion || i + 1 == role.SecondBestStatEmotion)
                {
                    //animatorsStat[i].enabled = true;
                    textStats[i].color = colorBest;
                }
                else
                {
                    //animatorsStat[i].enabled = false;
                    textStats[i].color = colorNormal;
                }
            }

        }
        
        #endregion
		
	} // PanelContractCharacter class
	
}// #PROJECTNAME# namespace
