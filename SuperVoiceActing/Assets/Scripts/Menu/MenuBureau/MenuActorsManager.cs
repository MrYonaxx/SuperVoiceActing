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

        [SerializeField]
        private List<VoiceActor> actorsList;

        [SerializeField]
        private List<VoiceActor> actorsSortList;

        [Header("Prefab")]
        [SerializeField]
        private RectTransform buttonListTransform;

        [SerializeField]
        private ButtonVoiceActor prefabButtonVoiceActor;


        private List<ButtonVoiceActor> buttonsActors;

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
            DrawActorsList();
        }
        

        private void DrawActorsList()
        {
            for(int i = 0; i < actorsList.Count; i++)
            {
                ButtonVoiceActor button = Instantiate(prefabButtonVoiceActor, buttonListTransform);
                buttonsActors.Add(button);
            }
        }

        private void DrawActorStat()
        {

        }

        public void SelectActorUp()
        {

        }

        public void SelectActorDown()
        {

        }
        
        #endregion
		
	} // MenuActorsManager class
	
}// #PROJECTNAME# namespace
