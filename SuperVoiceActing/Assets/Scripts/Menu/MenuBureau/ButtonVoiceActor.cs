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
	public class ButtonVoiceActor : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI actorName;

        [SerializeField]
        TextMeshProUGUI actorLevel;

        [SerializeField]
        Image actorImage;

        [SerializeField]
        Image buttonImage;
        [SerializeField]
        Color colorSelected;
        [SerializeField]
        Color colorUnSelected;

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

        public void DrawActor(string name, int level)
        {
            actorName.text = name;
            actorLevel.text = "Lv." + level;
        }

        public void SelectButton()
        {
            actorImage.enabled = false;
            buttonImage.color = colorSelected;
        }

        public void UnSelectButton()
        {
            actorImage.enabled = true;
            buttonImage.color = colorUnSelected;
        }

        #endregion

    } // ButtonVoiceActor class
	
}// #PROJECTNAME# namespace
