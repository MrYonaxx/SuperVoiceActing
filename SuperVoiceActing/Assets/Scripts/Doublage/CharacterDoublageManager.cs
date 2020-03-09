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


using UnityEngine.UI;
using TMPro;

namespace VoiceActing
{
	public class CharacterDoublageManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        private CameraController cameraController;
        [SerializeField]
        private VoiceActorDatabase characterSpriteDatabase;
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


        [Title("HudActors")]
        [SerializeField]
        Image actorLeft;
        [SerializeField]
        Image actorRight;
        [SerializeField]
        TextMeshProUGUI textActorLeft;
        [SerializeField]
        TextMeshProUGUI textActorRight;


        private int characterIndexCenter = 0;
        private int characterIndexLeft = 1;
        private int characterIndexRight = 2;

        #endregion





        public void SetCharactersSprites(List<VoiceActor> actorsContract)
        {
            for (int i = 0; i < actorsContract.Count; i++)
            {
                if (actorsContract[i] != null)
                {
                    characters[i].SetStoryCharacterData(characterSpriteDatabase.GetCharacterData(actorsContract[i].VoiceActorID));
                    characters[i].gameObject.SetActive(true);
                }
            }
        }

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
            cameraController.SetCameraSwitchActor(result);
            return result;
        }


        public bool SwitchActorsSpeed(int nextActor)
        {
            bool result = false;
            if (nextActor == characterIndexLeft) // switchLeft
            {
                characters[characterIndexLeft].transform.SetParent(charactersPosition[0]);
                characters[characterIndexRight].transform.SetParent(charactersPosition[1]);
                characters[characterIndexCenter].transform.SetParent(charactersPosition[2]);
                characterIndexLeft = characterIndexRight;
                characterIndexRight = characterIndexCenter;
                characterIndexCenter = nextActor;
                animatorSwitchActors.SetTrigger("LeftSpeed");
                result = true; // switchLeft

            }
            else if (nextActor == characterIndexRight) // switchRight
            {
                characters[characterIndexRight].transform.SetParent(charactersPosition[0]);
                characters[characterIndexCenter].transform.SetParent(charactersPosition[1]);
                characters[characterIndexLeft].transform.SetParent(charactersPosition[2]);
                characterIndexRight = characterIndexLeft;
                characterIndexLeft = characterIndexCenter;
                characterIndexCenter = nextActor;
                animatorSwitchActors.SetTrigger("RightSpeed");
                result = false; // switchRight
            }
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].transform.localPosition = Vector3.zero;
            }
            SetCharacterForeground(characterIndexCenter);
            //cameraController.MoveToInitialPosition(1);
            return result;
        }


        /*public void DrawActorsOrder(List<TextData> textDatas, List<string> actorsContract, int currentLine)
        {
            if (actorsContract.Count >= 2)
                actorLeft.sprite = characterSpriteDatabase.GetCharacterData(actorsContract[characterIndexLeft]).SpriteIcon;

            if (actorsContract.Count >= 3)
                actorRight.sprite = characterSpriteDatabase.GetCharacterData(actorsContract[characterIndexRight]).SpriteIcon;

            actorLeft.gameObject.SetActive(false);
            actorRight.gameObject.SetActive(false);
            int leftTurn = 0;
            int rightTurn = 0;
            bool leftDraw = false;
            bool rightDraw = false;
            for (int i = currentLine; i < textDatas.Count; i++)
            {
                if (leftDraw == false)
                {
                    if (textDatas[i].Interlocuteur == characterIndexLeft)
                    {
                        DrawActorTurnOrder(actorLeft, textActorLeft, leftTurn);
                        leftDraw = true;
                    }
                    leftTurn += 1;
                }
                if (rightDraw == false)
                {
                    if (textDatas[i].Interlocuteur == characterIndexRight)
                    {
                        DrawActorTurnOrder(actorRight, textActorRight, rightTurn);
                        rightDraw = true;
                    }
                    rightTurn += 1;
                }
            }
        }*/

        public void DrawActorsOrder(List<TextData> textDatas, List<string> actorsContract, int currentLine)
        {
            if (characterIndexLeft < actorsContract.Count)
            {
                actorLeft.sprite = characterSpriteDatabase.GetCharacterData(actorsContract[characterIndexLeft]).SpriteIcon;
                actorLeft.enabled = (actorLeft.sprite != null);
            }

            if (characterIndexRight < actorsContract.Count)
            {
                actorRight.sprite = characterSpriteDatabase.GetCharacterData(actorsContract[characterIndexRight]).SpriteIcon;
                actorRight.enabled = (actorRight.sprite != null);
            }

            actorLeft.gameObject.SetActive(false);
            actorRight.gameObject.SetActive(false);
            int leftTurn = 0;
            int rightTurn = 0;
            for (int i = currentLine; i < textDatas.Count; i++)
            {
                if (textDatas[i].Interlocuteur == characterIndexLeft)
                {
                    DrawActorTurnOrder(actorLeft, textActorLeft, leftTurn);
                    return;
                }
                if (textDatas[i].Interlocuteur == characterIndexRight)
                {
                    DrawActorTurnOrder(actorRight, textActorRight, rightTurn);
                    return;
                }
                leftTurn += 1;
                rightTurn += 1;
            }
        }

        private void DrawActorTurnOrder(Image actorImage, TextMeshProUGUI textPro, int turn)
        {
            actorImage.gameObject.SetActive(true);
            if (turn == 1)
                textPro.text = "NEXT";
            else
                textPro.text = turn.ToString();
        }

    } // CharacterDoublageManager class
	
}// #PROJECTNAME# namespace
