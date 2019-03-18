/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{

    public class SoundEngineerManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Animator animatorMixingTable;

        [SerializeField]
        InputController inputMixingTable;

        [SerializeField]
        GameObject vignettageNormal;
        [SerializeField]
        GameObject vignettageIngeSon;

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

        public void SwitchToMixingTable()
        {
            animatorMixingTable.SetBool("Active", true);
            inputMixingTable.gameObject.SetActive(true);
            vignettageNormal.SetActive(false);
            vignettageIngeSon.SetActive(true);
        }

        public void SwitchToEmotion()
        {
            animatorMixingTable.SetBool("Active", false);
            inputMixingTable.gameObject.SetActive(false);
            vignettageNormal.SetActive(true);
            vignettageIngeSon.SetActive(false);
        }

        #endregion

    } // SoundEngineerManager class
	
}// #PROJECTNAME# namespace
