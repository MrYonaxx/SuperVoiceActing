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
                        animatorsStat[i].enabled = true;
                        textStats[i].color = colorBest;
                    }
                    else
                    {
                        animatorsStat[i].enabled = false;
                        textStats[i].color = colorNormal;
                    }
                }
                else
                {
                    animatorsStat[i].enabled = false;
                    textStats[i].color = colorNormal;
                }
            }

            textStats[0].text = role.CharacterStat.Joy.ToString();
            textStats[1].text = role.CharacterStat.Sadness.ToString();
            textStats[2].text = role.CharacterStat.Disgust.ToString();
            textStats[3].text = role.CharacterStat.Anger.ToString();
            textStats[4].text = role.CharacterStat.Surprise.ToString();
            textStats[5].text = role.CharacterStat.Sweetness.ToString();
            textStats[6].text = role.CharacterStat.Fear.ToString();
            textStats[7].text = role.CharacterStat.Trust.ToString();
        }
        
        #endregion
		
	} // PanelContractCharacter class
	
}// #PROJECTNAME# namespace
