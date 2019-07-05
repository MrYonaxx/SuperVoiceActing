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
        private CharacterDialogueController[] characters;
        //0 = centre ; 1 = gauche ; 2 = droite
        [SerializeField]
        private Transform[] charactersPosition;

        [SerializeField]
        private int characterLayerForeground = 3;
        [SerializeField]
        private int characterLayerBackground = 7;
        [SerializeField]
        private Color characterColorForeground = Color.white;
        [SerializeField]
        private Color characterColorBackground;


        [Header("SwitchActorFeedback")]
        [SerializeField]
        Transform switchPositionLeft;
        [SerializeField]
        Animator animatorSwitchActors;
        [SerializeField]
        Transform spriteSwitchPanel;
        [SerializeField]
        SpriteRenderer spriteCurrentActor;
        [SerializeField]
        SpriteRenderer spriteNextActor;


        private int characterIndexCenter = 0;
        private int characterIndexLeft = 1;
        private int characterIndexRight = 2;

        #endregion

        public CharacterDialogueController GetCharacter(int indexCharacter)
        {
            return characters[indexCharacter];
        }

        public void SetCharacterForeground(int indexCharacter)
        {
            for(int i = 0; i < characters.Length; i++)
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


        public void ShowCharacters(bool b)
        {
            for (int i = 0; i < charactersPosition.Length; i++)
            {
                charactersPosition[i].gameObject.SetActive(b);
            }
        }


        public bool SwitchActors(int nextActor)
        {
            bool result = false;
            spriteSwitchPanel.gameObject.SetActive(true);
            spriteCurrentActor.sprite = characters[characterIndexCenter].GetSprite();
            if(nextActor == characterIndexLeft) // switchLeft
            {
                characters[characterIndexLeft].transform.SetParent(charactersPosition[0]);
                characters[characterIndexRight].transform.SetParent(charactersPosition[1]);
                characters[characterIndexCenter].transform.SetParent(charactersPosition[2]);
                characterIndexLeft = characterIndexRight;
                characterIndexRight = characterIndexCenter;
                characterIndexCenter = nextActor;
                spriteSwitchPanel.transform.localPosition = new Vector3(switchPositionLeft.transform.localPosition.x, switchPositionLeft.transform.localPosition.y, switchPositionLeft.transform.localPosition.z);
                spriteSwitchPanel.transform.localEulerAngles = new Vector3(switchPositionLeft.transform.localEulerAngles.x, switchPositionLeft.transform.localEulerAngles.y, switchPositionLeft.transform.localEulerAngles.z);
                spriteSwitchPanel.transform.localScale = new Vector3(switchPositionLeft.transform.localScale.x, switchPositionLeft.transform.localScale.y, switchPositionLeft.transform.localScale.z);
                animatorSwitchActors.SetTrigger("Left");
                result = true; // switchLeft

            }
            else if(nextActor == characterIndexRight) // switchRight
            {
                characters[characterIndexRight].transform.SetParent(charactersPosition[0]);
                characters[characterIndexCenter].transform.SetParent(charactersPosition[1]);
                characters[characterIndexLeft].transform.SetParent(charactersPosition[2]);
                characterIndexRight = characterIndexLeft;
                characterIndexLeft = characterIndexCenter;
                characterIndexCenter = nextActor;
                spriteSwitchPanel.transform.localPosition = new Vector3(switchPositionLeft.transform.localPosition.x, switchPositionLeft.transform.localPosition.y, -switchPositionLeft.transform.localPosition.z);
                spriteSwitchPanel.transform.localEulerAngles = new Vector3(switchPositionLeft.transform.localEulerAngles.x, switchPositionLeft.transform.localEulerAngles.y - 90, switchPositionLeft.transform.localEulerAngles.z);
                spriteSwitchPanel.transform.localScale = new Vector3(-switchPositionLeft.transform.localScale.x, switchPositionLeft.transform.localScale.y, switchPositionLeft.transform.localScale.z);
                animatorSwitchActors.SetTrigger("Right");
                result = false; // switchRight
            }
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].transform.localPosition = Vector3.zero;
            }
            spriteNextActor.sprite = characters[characterIndexCenter].GetSprite();
            SetCharacterForeground(characterIndexCenter);
            ShowCharacters(false);
            return result;
        }

    } // CharacterDoublageManager class
	
}// #PROJECTNAME# namespace
