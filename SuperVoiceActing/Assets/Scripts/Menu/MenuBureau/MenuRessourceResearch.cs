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
	public class MenuRessourceResearch : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Animator animatorResearch;
        [SerializeField]
        TextMeshProUGUI textResearchPoint;
        [SerializeField]
        MenuRessourceResearch[] menuResearch;

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
        public void ShowMenu()
        {
            animatorResearch.SetBool("Appear", true);
        }

        public void HideMenu()
        {
            animatorResearch.SetBool("Appear", false);
        }

        public void DrawResearchPoint(int value)
        {
            textResearchPoint.text = value.ToString();
            for(int i = 0; i < menuResearch.Length; i++)
            {
                menuResearch[i].DrawPoint(value);
            }
        }



        /// <summary>
        /// Ne pas utiliser
        /// </summary>
        /// <param name="value"></param>
        public void DrawPoint(int value)
        {
            textResearchPoint.text = value.ToString();
        }   

        #endregion

    } // MenuRessourceResearch class
	
}// #PROJECTNAME# namespace
