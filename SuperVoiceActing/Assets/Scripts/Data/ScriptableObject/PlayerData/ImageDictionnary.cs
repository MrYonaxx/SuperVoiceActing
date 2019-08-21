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
    [CreateAssetMenu(fileName = "ImageDictionnary", menuName = "PlayerData/ImageDictionnary", order = 1)]
    public class ImageDictionnary : ScriptableObject
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [HorizontalGroup]
        [SerializeField]
        string[] textDict;

        [HorizontalGroup]
        [SerializeField]
        Sprite[] spriteDict;

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

        public string GetName(int i)
        {
            if (i > textDict.Length)
                return "";
            return textDict[i];
        }

        public Sprite GetSprite(int i)
        {
            if (i > spriteDict.Length)
                return null;
            return spriteDict[i];
        }

        #endregion

    } // ImageDictionnary class
	
}// #PROJECTNAME# namespace
