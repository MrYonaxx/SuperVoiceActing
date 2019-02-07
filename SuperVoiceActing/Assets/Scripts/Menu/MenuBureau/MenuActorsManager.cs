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
	public class MenuActorsManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        // Pour les tests
        [SerializeField]
        private List<VoiceActorData> debugList;


        private List<VoiceActor> actorsList = new List<VoiceActor>();

        private List<VoiceActor> actorsSortList = new List<VoiceActor>();

        [Header("Prefab")]
        [SerializeField]
        private RectTransform buttonListTransform;
        [SerializeField]
        private ButtonVoiceActor prefabButtonVoiceActor;

        // ===========================
        // Menu Input
        [Header("Menu Input")]
        [SerializeField]
        private int scrollSize = 11;
        [SerializeField]
        private int buttonSize = 85; // à voir si on peut pas automatiser
        [SerializeField]
        private int timeBeforeRepeat = 10;
        [SerializeField]
        private int repeatInterval = 3;

        private int currentTimeBeforeRepeat = -1;
        private int currentRepeatInterval = -1;
        private int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private int indexActorLimit = 0;
        private IEnumerator coroutineScroll = null;
        // Menu Input
        // ===========================


        private List<ButtonVoiceActor> buttonsActors = new List<ButtonVoiceActor>();



        int indexActorSelected = 0;

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



        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            indexActorLimit = scrollSize;
            CreateFromDebugList();
            CreateActorsButtonList();
        }
        
        private void CreateFromDebugList()
        {
            if(debugList != null)
            {
                for(int i = 0; i < debugList.Count; i++)
                {
                    actorsList.Add(new VoiceActor(debugList[i]));
                }
            }
        }

        private void CreateActorsButtonList()
        {
            for(int i = 0; i < actorsList.Count; i++)
            {
                buttonsActors.Add(Instantiate(prefabButtonVoiceActor, buttonListTransform));
                buttonsActors[i].DrawActor(actorsList[i].Name, actorsList[i].Level);
            }
            if(indexActorSelected < actorsList.Count)
                buttonsActors[indexActorSelected].SelectButton();

            buttonListTransform.anchoredPosition = new Vector2(0, 0);
        }

        private void DrawActorStat()
        {

        }

        public void SelectActorUp()
        {
            if(lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            buttonsActors[indexActorSelected].UnSelectButton();
            indexActorSelected -= 1;
            if(indexActorSelected <= -1)
            {
                indexActorSelected = buttonsActors.Count-1;
            }
            buttonsActors[indexActorSelected].SelectButton();

            MoveScrollRect();
        }

        public void SelectActorDown()
        {
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            buttonsActors[indexActorSelected].UnSelectButton();
            indexActorSelected += 1;
            if (indexActorSelected >= buttonsActors.Count)
            {
                indexActorSelected = 0;
            }
            buttonsActors[indexActorSelected].SelectButton();

            MoveScrollRect();

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
            else if(currentRepeatInterval == 0)
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


        private void MoveScrollRect()
        {
            if(coroutineScroll != null)
            {
                StopCoroutine(coroutineScroll);
            }
            if (indexActorSelected > indexActorLimit)
            {
                indexActorLimit = indexActorSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            else if (indexActorSelected < indexActorLimit - scrollSize)
            {
                indexActorLimit = indexActorSelected + scrollSize;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            
        }

        private IEnumerator MoveScrollRectCoroutine()
        {
            int time = 20;
            int ratio = indexActorSelected - scrollSize;
            if (ratio <= 0) {
                float speed = (buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time;
                while (time != 0)
                {
                    buttonListTransform.anchoredPosition -= new Vector2(0, speed);
                    time -= 1;
                    yield return null;
                }
            }
            
        }
        
        #endregion
		
	} // MenuActorsManager class
	
}// #PROJECTNAME# namespace
