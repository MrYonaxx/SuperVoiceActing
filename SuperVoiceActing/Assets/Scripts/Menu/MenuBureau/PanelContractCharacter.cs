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
        RectTransform transformTimbre;

        [Header("")]

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

            transformTimbre.anchorMin = new Vector2((role.Timbre.x + 10) / 20f, 0);
            transformTimbre.anchorMax = new Vector2((role.Timbre.y + 10) / 20f, 1);
            transformTimbre.anchoredPosition = Vector3.zero;

            int[] bestValues = new int[8];

            bestValues[0] = role.CharacterStat.Joy;
            bestValues[1] = role.CharacterStat.Sadness;
            bestValues[2] = role.CharacterStat.Disgust;
            bestValues[3] = role.CharacterStat.Anger;
            bestValues[4] = role.CharacterStat.Surprise;
            bestValues[5] = role.CharacterStat.Sweetness;
            bestValues[6] = role.CharacterStat.Fear;
            bestValues[7] = role.CharacterStat.Trust;

            /*int best = 0;
            int secondBest = 0;

            for (int i = 0; i < bestValues.Length; i++)
            {
                if(bestValues[i] > best)
                {
                    secondBest = best;
                    best = bestValues[i];
                }
                else if (bestValues[i] > secondBest)
                {
                    secondBest = bestValues[i];
                }
            }*/

            for (int i = 0; i < bestValues.Length; i++)
            {
                if (bestValues[i] != 0)
                {
                    if (bestValues[i] == role.BestStat || bestValues[i] == role.SecondBestStat)
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
                else
                {
                    //animatorsStat[i].enabled = false;
                    textStats[i].color = colorNormal;
                }
            }

            textStats[0].text = role.CharacterStat.Joy.ToString();
            imageSlider[0].transform.localScale = new Vector3(role.CharacterStat.Joy / 100f, 1, 1);

            textStats[1].text = role.CharacterStat.Sadness.ToString();
            imageSlider[1].transform.localScale = new Vector3(role.CharacterStat.Sadness / 100f, 1, 1);

            textStats[2].text = role.CharacterStat.Disgust.ToString();
            imageSlider[2].transform.localScale = new Vector3(role.CharacterStat.Disgust / 100f, 1, 1);

            textStats[3].text = role.CharacterStat.Anger.ToString();
            imageSlider[3].transform.localScale = new Vector3(role.CharacterStat.Anger / 100f, 1, 1);

            textStats[4].text = role.CharacterStat.Surprise.ToString();
            imageSlider[4].transform.localScale = new Vector3(role.CharacterStat.Surprise / 100f, 1, 1);

            textStats[5].text = role.CharacterStat.Sweetness.ToString();
            imageSlider[5].transform.localScale = new Vector3(role.CharacterStat.Sweetness / 100f, 1, 1);

            textStats[6].text = role.CharacterStat.Fear.ToString();
            imageSlider[6].transform.localScale = new Vector3(role.CharacterStat.Fear / 100f, 1, 1);

            textStats[7].text = role.CharacterStat.Trust.ToString();
            imageSlider[7].transform.localScale = new Vector3(role.CharacterStat.Trust / 100f, 1, 1);
        }
        
        #endregion
		
	} // PanelContractCharacter class
	
}// #PROJECTNAME# namespace
