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

        public void SelectButton()
        {
            actorImage.enabled = false;
        }

        public void UnSelectButton()
        {
            actorImage.enabled = true;
        }

        #endregion

    } // ButtonVoiceActor class
	
}// #PROJECTNAME# namespace
