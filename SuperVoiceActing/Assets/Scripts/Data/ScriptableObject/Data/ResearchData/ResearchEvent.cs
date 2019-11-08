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
    public class ResearchEvent : MonoBehaviour
    {
        [SerializeField]
        protected bool isActive;
        public bool IsActive()
        {
            return isActive;
        }
        [SerializeField]
        protected bool canCollide;
        [SerializeField]
        protected Animator animator;



        public virtual bool CanCollide()
        {
            return canCollide;
        }

        public virtual bool CanAddToPlayerDataSave()
        {
            return false;
        }




        public virtual void SetActive(bool b)
        {
            animator.SetBool("Active", b);
            isActive = b;
        }

        // Trouver un moyen de ne pas etre dependant de menuresearchManager genre que ça soit relou
        public virtual IEnumerator ApplyEvent(MenuResearchMovementManager menuResearch, int eventID)
        {
            yield return null;
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