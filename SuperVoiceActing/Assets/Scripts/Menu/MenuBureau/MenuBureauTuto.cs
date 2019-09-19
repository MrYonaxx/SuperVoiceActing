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
	public class MenuBureauTuto : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;

        [HorizontalGroup]
        [SerializeField]
        StoryEventData[] tutoDatabase;
        [HorizontalGroup]
        [SerializeField]
        GameObject[] tutoTrigger;
        [HorizontalGroup("Input")]
        [SerializeField]
        InputController[] inputTutoActivate;
        [HorizontalGroup("Input")]
        [SerializeField]
        InputController[] inputTutoDesactivate;

        [Space]
        [Space]
        [SerializeField]
        StoryEventManager eventManager;
        [SerializeField]
        GameObject eventBox;
        [SerializeField]
        GameObject menuManagers;




        int currentTuto = -1;


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
            if(playerData.TutoEvent.Count == 0)
            {
                this.gameObject.SetActive(false);
            }
            //StartTuto(0);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            // Optimisable avec les inputController mais après faut chercher partout c'est chiant
            // A optimiser quand on sera sur pour les tutos
            if (currentTuto == -1)
            {
                for (int i = 0; i < tutoDatabase.Length; i++)
                {
                    if (tutoTrigger[i] != null)
                    {
                        if (tutoTrigger[i].activeInHierarchy == true)
                        {
                            StartTuto(i);
                            if (currentTuto != -1)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void StartTuto(int i)
        {
            if(playerData.TutoEvent.Contains(tutoDatabase[i]))
            {
                playerData.TutoEvent.Remove(tutoDatabase[i]);
                eventManager.StartStoryEventData(tutoDatabase[i]);
                eventBox.SetActive(true);
                menuManagers.SetActive(false);
                currentTuto = i;
                if (inputTutoActivate[i] != null)
                    currentTuto = i;
            }
        }

        // appelé à la fin d'un tuto
        public void ActivateInput()
        {
            if (inputTutoActivate[currentTuto] != null)
            {
                inputTutoActivate[currentTuto].gameObject.SetActive(true);
                inputTutoDesactivate[currentTuto].gameObject.SetActive(false);
                menuManagers.SetActive(false);
            }
            else
            {
                currentTuto = -1;
                menuManagers.SetActive(true);
            }
        }

        
        #endregion
		
	} // MenuBureauTuto class
	
}// #PROJECTNAME# namespace
