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
	public class MenuContractInfo : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI[] textMeshProUGUI;

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

        public void DrawText(string[] infos)
        {
            for(int i = 0; i < textMeshProUGUI.Length; i++)
            {
                if (i < infos.Length)
                {
                    textMeshProUGUI[i].gameObject.SetActive(true);
                    textMeshProUGUI[i].text = infos[i];
                }
                else
                {
                    textMeshProUGUI[i].gameObject.SetActive(false);
                }
            }
        }



        #endregion

    } // MenuContractInfo class
	
}// #PROJECTNAME# namespace
