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
    [CreateAssetMenu(fileName = "EmotionComboData", menuName = "PlayerData/EmotionCombo", order = 1)]
    public class EmotionComboData : SerializedScriptableObject
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        [TableMatrix(HorizontalTitle = "Emotions", VerticalTitle = "Emotions")]
        private string[,] emotionsCombo = new string[9, 9];

        #endregion

        public string GetName(int emotion1, int emotion2, int emotion3)
        {
            return "- " + emotionsCombo[emotion1, emotion2] + " -";
        }
        
		
	} // EmotionComboData class
	
}// #PROJECTNAME# namespace
