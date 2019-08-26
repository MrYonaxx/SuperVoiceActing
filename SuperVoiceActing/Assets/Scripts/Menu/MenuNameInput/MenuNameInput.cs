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
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuNameInput : SerializedMonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        [TableMatrix]
        private string[,] characterTable = new string[18,4];

        [SerializeField]
        private TMPro.TextMeshProUGUI textMeshPrefab;
        [SerializeField]
        private RectTransform selectionTransform;
        [SerializeField]
        private RectTransform[] characterPositions;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        TMPro.TMP_InputField inputField;
        [SerializeField]
        StoryEventData storyEventData;
        [SerializeField]
        string nextScene;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        Animator animatorCharacterTable;
        [SerializeField]
        InputController inputController;
        [SerializeField]
        InputController inputCharacterTable;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        protected int timeBeforeRepeat = 10;
        [SerializeField]
        protected int repeatInterval = 3;



        protected int currentTimeBeforeRepeat = -1;
        protected int currentRepeatInterval = -1;
        protected int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        protected int indexLimit = 0;

        protected IEnumerator coroutineMove = null;
        protected int indexSelectedX = 0;
        protected int indexSelectedY = 0;



        private TMPro.TextMeshProUGUI[] textMeshTable;

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

        private void Start()
        {
            textMeshTable = new TMPro.TextMeshProUGUI[characterPositions.Length];
            for (int i = 0; i < characterPositions.Length; i++)
            {
                textMeshTable[i] = Instantiate(textMeshPrefab, characterPositions[i]);
                textMeshTable[i].text = characterTable[i - (characterTable.GetLength(0) * (i  / characterTable.GetLength(0))),
                                                           0 + (i / characterTable.GetLength(0))];
            }
        }



        public void RegisterPlayerName()
        {
            playerData.PlayerName = inputField.text;
            if(storyEventData != null)
                playerData.NextStoryEvents.Add(storyEventData);
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }

        public void RegisterStudioName()
        {
            playerData.StudioName = inputField.text;
            if (storyEventData != null)
                playerData.NextStoryEvents.Add(storyEventData);
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }




        public void SwitchToCharacterTable(bool b)
        {
            /*if(b == true)
            {

            }*/
            animatorCharacterTable.SetBool("Active", b);
            inputController.gameObject.SetActive(!b);
            inputCharacterTable.gameObject.SetActive(b);
        }




        public void Type()
        {
            inputField.text += characterTable[indexSelectedX, indexSelectedY];
        }

        public void TypeSpace()
        {
            inputField.text += " ";
        }

        public void EraseText()
        {
            inputField.text = inputField.text.Remove(inputField.text.Length-1);
        }

        public void SelectUp()
        {
            /*if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }*/

            if (CheckRepeat() == false)
                return;

            indexSelectedY -= 1;
            if (indexSelectedY <= -1)
            {
                indexSelectedY = characterTable.GetLength(1)-1;
            }
            MoveSelection();
        }

        public void SelectDown()
        {
            /*if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }*/
            if (CheckRepeat() == false)
                return;

            indexSelectedY += 1;
            if (indexSelectedY >= characterTable.GetLength(1))
            {
                indexSelectedY = 0;
            }
            MoveSelection();
        }

        public void SelectLeft()
        {
            /*if (lastDirection != 4)
            {
                StopRepeat();
                lastDirection = 4;
            }*/

            if (CheckRepeat() == false)
                return;

            indexSelectedX -= 1;
            if (indexSelectedX <= -1)
            {
                indexSelectedX = characterTable.GetLength(0)-1;
            }
            MoveSelection();
        }

        public void SelectRight()
        {
            /*if (lastDirection != 6)
            {
                StopRepeat();
                lastDirection = 6;
            }*/
            if (CheckRepeat() == false)
                return;

            indexSelectedX += 1;
            if (indexSelectedX >= characterTable.GetLength(0))
            {
                indexSelectedX = 0;
            }
            MoveSelection();
        }

        private void MoveSelection()
        {
            if (coroutineMove != null)
                StopCoroutine(coroutineMove);
            coroutineMove = MoveSelectionCursorCoroutine();
            StartCoroutine(coroutineMove);

        }

        private IEnumerator MoveSelectionCursorCoroutine(int time = 5)
        {
            Vector2 speed = new Vector2((selectionTransform.anchoredPosition.x - characterPositions[indexSelectedX + (characterTable.GetLength(0) * indexSelectedY)].anchoredPosition.x) / time,
                                        (selectionTransform.anchoredPosition.y - characterPositions[indexSelectedX + (characterTable.GetLength(0) * indexSelectedY)].anchoredPosition.y) / time);
            while (time != 0)
            {
                selectionTransform.anchoredPosition -= speed;
                time -= 1;
                yield return null;
            }
            coroutineMove = null;
        }


        // Check si on peut repeter l'input
        private bool CheckRepeat()
        {
            if (currentRepeatInterval == -1)
            {
                if (currentTimeBeforeRepeat == -1)
                {
                    currentTimeBeforeRepeat = timeBeforeRepeat;
                    return true;
                }
                else if (currentTimeBeforeRepeat == 0)
                {
                    currentRepeatInterval = repeatInterval;
                }
                else
                {
                    currentTimeBeforeRepeat -= 1;
                }
            }
            else if (currentRepeatInterval == 0)
            {
                currentRepeatInterval = repeatInterval;
                return true;
            }
            else
            {
                currentRepeatInterval -= 1;
            }
            return false;
        }

        public void StopRepeat()
        {
            currentRepeatInterval = -1;
            currentTimeBeforeRepeat = -1;
        }





        #endregion

    } // MenuNameInput class
	
}// #PROJECTNAME# namespace
