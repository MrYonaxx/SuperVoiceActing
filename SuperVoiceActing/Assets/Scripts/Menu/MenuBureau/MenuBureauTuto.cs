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
	public class MenuBureauTuto : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        StoryEventData[] tutoDatabase;
        [SerializeField]
        StoryEventManager eventManager;
        [SerializeField]
        GameObject eventBox;
        [SerializeField]
        GameObject menuManagers;


        [Header("------------------------------")]

        [SerializeField]
        GameObject menuContratAvailable;
        [SerializeField]
        GameObject menuContratPreparation;
        [SerializeField]
        GameObject panelAudition;
        [SerializeField]
        GameObject panelInSession;


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
            if(playerData.TutoEvent.Count == 0)
            {
                this.gameObject.SetActive(false);
            }



            if(menuContratAvailable.activeInHierarchy == true)
            {
                StartTuto(2);
            }


            if (menuContratPreparation.activeInHierarchy == true)
            {
                StartTuto(4);
            }

            if (panelAudition.activeInHierarchy == true)
            {
                StartTuto(5);
            }
            if (panelInSession.activeInHierarchy == true)
            {
                StartTuto(6);
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
            }
        }
        
        #endregion
		
	} // MenuBureauTuto class
	
}// #PROJECTNAME# namespace
