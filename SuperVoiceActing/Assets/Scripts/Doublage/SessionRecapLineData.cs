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

        public void DrawRecapLine(string roleName, Sprite roleSprite, string line, Emotion[] emotions)
        {
            this.gameObject.SetActive(true);
            imageRole.sprite = roleSprite;
            textRoleName.text = roleName;
            textRoleLine.text = line;
        }

        #endregion

    } // SessionRecapLineData class
	
}// #PROJECTNAME# namespace
