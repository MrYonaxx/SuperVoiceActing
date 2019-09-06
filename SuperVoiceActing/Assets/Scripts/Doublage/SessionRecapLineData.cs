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
	public class SessionRecapLineData : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Image imageRole;
        [SerializeField]
        TextMeshProUGUI textRoleName;

        [SerializeField]
        TextMeshProUGUI textRoleLine;
        [SerializeField]
        Image[] imagesEmotions;
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

        public void DrawRecapLine(string roleName, Sprite roleSprite, string line, Sprite[] emotions)
        {
            this.gameObject.SetActive(true);
            if (roleSprite == null)
                imageRole.enabled = false;
            else
            {
                imageRole.enabled = true;
                imageRole.sprite = roleSprite;
            }
            textRoleName.text = roleName;
            textRoleLine.text = line;
            for(int i = 0; i < imagesEmotions.Length; i++)
            {
                if (emotions[i] == null)
                    imagesEmotions[i].enabled = false;
                else
                {
                    imagesEmotions[i].enabled = true;
                    imagesEmotions[i].sprite = emotions[i];
                }
            }
        }

        #endregion

    } // SessionRecapLineData class
	
}// #PROJECTNAME# namespace
