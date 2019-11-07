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
    public class ResearchEvent : ScriptableObject
    {
        public bool canCollide;
        public Animator animatorPrefab;




        public virtual bool CanCollide()
        {
            return canCollide;
        }

        public virtual bool CanAddToPlayerDataSave()
        {
            return false;
        }


        public virtual bool CanInstantiate()
        {
            return animatorPrefab != null;
        }




        public virtual Animator InstantiateEvent(Transform transformEvent, float cellSize, int positionX, int positionY)
        {

            Animator res = Instantiate(animatorPrefab, transformEvent);
            res.transform.localPosition += new Vector3(cellSize * positionX + (cellSize / 2f), cellSize * (positionY + 1), 0);
            return res;
        }

        public virtual void ApplyEvent(PlayerData playerData, int eventID)
        {

        }





        protected bool CheckResearchEventInPlayerData(List<ResearchEventSave> playerResearch, int eventID)
        {
            for (int i = 0; i < playerResearch.Count; i++)
            {
                if (playerResearch[i].EventID == eventID)
                {
                    return playerResearch[i].EventActive;
                }
            }
            return false;
        }

        protected void SetResearchEventInPlayerData(List<ResearchEventSave> playerResearch, int eventID)
        {
            for (int i = 0; i < playerResearch.Count; i++)
            {
                if (playerResearch[i].EventID == eventID)
                {
                    playerResearch[i].EventActive = !playerResearch[i].EventActive;
                }
            }
        }

    } 

} // #PROJECTNAME# namespace