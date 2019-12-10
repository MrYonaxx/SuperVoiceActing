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
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class MenuCrunch: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        Animator animatorCrunch;
        [SerializeField]
        TextMeshProUGUI textCrunch;


        private int crunchMoney = 0;

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

        public void Crunch()
        {
            animatorCrunch.gameObject.SetActive(true);
            animatorCrunch.SetTrigger("Feedback");
            int r = Random.Range(1, 5);
            crunchMoney += r;
            playerData.Money += r;
            textCrunch.text = "+ " + crunchMoney.ToString();
        }

        #endregion

    } 

} // #PROJECTNAME# namespace