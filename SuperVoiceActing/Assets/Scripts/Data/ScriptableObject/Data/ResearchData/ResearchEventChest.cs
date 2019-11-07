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
    [CreateAssetMenu(fileName = "ResearchEventChest", menuName = "Research/ResearchEvent/ResearchEventChest", order = 1)]
    public class ResearchEventChest: ResearchEvent
    {

        public ResearchData researchData;



        public override bool CanAddToPlayerDataSave()
        {
            return true;
        }


        public override void ApplyEvent(PlayerData playerData, int eventID)
        {
            if(CheckResearchEventInPlayerData(playerData.ResearchEventSaves, eventID) == false)
            {
                SetResearchEventInPlayerData(playerData.ResearchEventSaves, eventID);
                researchData.ApplyResearchEffect(playerData);
            }

        }



    } 

} // #PROJECTNAME# namespace