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
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class ButtonResearchDungeon: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        TextMeshProUGUI textDungeonName;
        [SerializeField]
        TextMeshProUGUI textDungeonCompletion;

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
        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }

        public void DrawDungeonName(string dungeonName, int completionPercentage)
        {
            textDungeonName.text = dungeonName;
            textDungeonCompletion.text = completionPercentage + "%";
        }

        #endregion

    } 

} // #PROJECTNAME# namespace