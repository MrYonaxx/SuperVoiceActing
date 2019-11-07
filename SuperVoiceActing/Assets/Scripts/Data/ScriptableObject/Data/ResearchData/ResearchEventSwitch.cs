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
    [CreateAssetMenu(fileName = "ResearchEventSwitch", menuName = "Research/ResearchEvent/ResearchEventSwitch", order = 1)]
    public class ResearchEventSwitch: ResearchEvent
    {

        public override bool CanAddToPlayerDataSave()
        {
            return true;
        }


        public override void ApplyEvent(PlayerData playerData, int eventID)
        {
            SetResearchEventInPlayerData(playerData.ResearchEventSaves, eventID);

        }

    } 

} // #PROJECTNAME# namespace