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
    [CreateAssetMenu(fileName = "ResearchDataCard", menuName = "Research/ResearchDataCard", order = 1)]
    public class ResearchDataCard : ResearchData
	{
        [Space]
        [SerializeField]
        protected Emotion emotionResearch;
        public Emotion EmotionResearch
        {
            get { return emotionResearch; }
        }

        public override void ApplyResearchEffect(PlayerData playerData, int researchLevel)
        {
            playerData.Deck.Add((int)EmotionResearch, researchValue[researchLevel]);
        }


    } // ResearchDataCard class
	
}// #PROJECTNAME# namespace
