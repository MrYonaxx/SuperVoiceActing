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
    // Ce truc est pas opti il ajoute une entrée dans la list du playerData alors que techniquement c'est tout le temps la meme chose que l'interrupteur
    public class ResearchEventSwitchObstacle: ResearchEvent
    {

        [SerializeField]
        bool reverseObstacle = false;

        public override void SetActive(bool b)
        {
            if (reverseObstacle == false)
            {
                animator.gameObject.SetActive(!b);
                isActive = b;
                canCollide = !b;
            }
            else
            {
                animator.gameObject.SetActive(b);
                isActive = !b;
                canCollide = b;
            }
        }

        public override bool CanAddToPlayerDataSave()
        {
            return true;
        }

    } 

} // #PROJECTNAME# namespace