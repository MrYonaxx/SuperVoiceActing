/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class MenuDecorationManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        GameObject windowAutumn;
        [SerializeField]
        GameObject windowSpring;
        [SerializeField]
        GameObject windowSummer;
        [SerializeField]
        GameObject windowWinter;

        [SerializeField]
        GameObject windowNight;
        [SerializeField]
        GameObject windowRain;
        #endregion


        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        private void Start()
        {
            CreateDecoration();
        }

        private void CreateDecoration()
        {
            if (Random.Range(0, 100) < 10)
                windowRain.gameObject.SetActive(true);
            else if (Random.Range(0, 100) < 1)
                windowNight.gameObject.SetActive(true);

            else if (playerData.Season == Season.Autumn)
                windowAutumn.gameObject.SetActive(true);
            else if (playerData.Season == Season.Spring)
                windowSpring.gameObject.SetActive(true);
            else if (playerData.Season == Season.Summer)
                windowSummer.gameObject.SetActive(true);
            else if (playerData.Season == Season.Winter)
                windowWinter.gameObject.SetActive(true);
        }


        #endregion

    } 

} // #PROJECTNAME# namespace