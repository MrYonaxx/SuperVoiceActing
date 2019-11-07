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
    [CreateAssetMenu(fileName = "ResearchEventSwitchObstacle", menuName = "Research/ResearchEvent/ResearchEventSwitchObstacle", order = 1)]
    public class ResearchEventSwitchObstacle: ResearchEvent
    {
        public int eventSwitchBind;

        public override bool CanCollide()
        {
            /*if(CheckResearchEventInPlayerData(playerData, eventSwitchBind) == canCollide)
            {
                return true;
            }*/
            return false;
        }

    } 

} // #PROJECTNAME# namespace