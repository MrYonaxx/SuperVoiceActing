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
	public class CharacterDoublageManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private List<CharacterDialogueController> characters = new List<CharacterDialogueController>();

        [SerializeField]
        private int characterLayerForeground = 3;
        [SerializeField]
        private int characterLayerBackground = 7;
        [SerializeField]
        private Color characterColorForeground = Color.white;
        [SerializeField]
        private Color characterColorBackground;

        #endregion

        public CharacterDialogueController GetCharacter(int indexCharacter)
        {
            return characters[indexCharacter];
        }

        public void SetCharacterForeground(int indexCharacter)
        {
            for(int i = 0; i < characters.Count; i++)
            {
                if(i == indexCharacter)
                {
                    characters[i].ChangeOrderInLayer(characterLayerForeground);
                    characters[i].ChangeTint(characterColorForeground);
                }
                else
                {
                    characters[i].ChangeOrderInLayer(characterLayerBackground);
                    characters[i].ChangeTint(characterColorBackground);
                }
            }
        }

        public void ShakeCharacter(int indexCharacter)
        {
            characters[indexCharacter].Shake();
        }
    } // CharacterDoublageManager class
	
}// #PROJECTNAME# namespace
