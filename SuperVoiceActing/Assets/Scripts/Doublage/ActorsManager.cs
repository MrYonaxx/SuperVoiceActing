/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VoiceActing
{
	public class ActorsManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        private List<VoiceActor> actors;

        [SerializeField]
        TextMeshProUGUI[] textCardStats;

        private int indexCurrentActor;
        
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

        public void SetActors(List<VoiceActor> actorsContract)
        {
            DrawActorStat();
        }

        private void DrawActorStat()
        {
            // Joie > Tristesse > Dégout > Colère > Surprise > Douceur > Peur > Confiance

            textCardStats[0].text = actors[indexCurrentActor].Statistique.Joy.ToString();
            textCardStats[1].text = actors[indexCurrentActor].Statistique.Sadness.ToString();
            textCardStats[2].text = actors[indexCurrentActor].Statistique.Disgust.ToString();
            textCardStats[3].text = actors[indexCurrentActor].Statistique.Anger.ToString();
            textCardStats[4].text = actors[indexCurrentActor].Statistique.Surprise.ToString();
            textCardStats[5].text = actors[indexCurrentActor].Statistique.Sweetness.ToString();
            textCardStats[6].text = actors[indexCurrentActor].Statistique.Fear.ToString();
            textCardStats[7].text = actors[indexCurrentActor].Statistique.Trust.ToString();
            textCardStats[8].text = actors[indexCurrentActor].Statistique.Neutral.ToString();

        }
        
        #endregion
		
	} // ActorsManager class
	
}// #PROJECTNAME# namespace
