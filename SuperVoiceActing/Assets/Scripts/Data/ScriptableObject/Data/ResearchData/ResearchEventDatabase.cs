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
    [System.Serializable]
    public class ResearchEventData
    {
        [HorizontalGroup]
        public int eventID;

        [HorizontalGroup]
        [HideLabel]
        public ResearchEvent researchEvent;

        public ResearchEventData(int id, ResearchEvent eventR)
        {
            eventID = id;
            researchEvent = eventR;
        }
    }










    [CreateAssetMenu(fileName = "ResearchEventDatabase", menuName = "Research/ResearchEvent/ResearchEventDatabase", order = 1)]
    public class ResearchEventDatabase: ScriptableObject
    {

        [SerializeField]
        private ResearchEventData[] researchEvents;
        public ResearchEventData[] ResearchEvents
        {
            get { return researchEvents; }
            set { researchEvents = value; }
        }




        public ResearchEvent GetResearchEvent(int id)
        {
            for(int i = 0; i < researchEvents.Length; i++)
            {
                if(researchEvents[i].eventID == id)
                {
                    return researchEvents[i].researchEvent;
                }
            }
            return null;
        }


    } 

} // #PROJECTNAME# namespace