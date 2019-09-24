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
	public class RandomEventManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        PlayerData playerData;
        
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

        /*public void SelectNextRandomEvent()
        {
            int rand = Random.Range(-playerData.RandomEventsAvailable.Count, playerData.RandomEventsAvailable.Count);
            if (rand < 0)
                return;
            else
            {
                playerData.NextStoryEvents.Add(playerData.RandomEventsAvailable[rand]);
            }
        }*/

        /*public void AddEventAvailable()
        {
            
            
        }*/
        
        #endregion
		
	} // RandomEventManager class
	
}// #PROJECTNAME# namespace
