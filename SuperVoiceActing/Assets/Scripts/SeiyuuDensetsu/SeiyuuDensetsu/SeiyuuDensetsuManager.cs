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
    public class SeiyuuDensetsuManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        SeiyuuData seiyuuData;

        [SerializeField]
        SeiyuuActorManager seiyuuActorManager;

        [SerializeField]
        Animator animatorBackground;

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

        public void NewWeek()
        {
            //DrawDate();
            seiyuuActorManager.DrawActorStat(seiyuuData.VoiceActor);
        }

        public void StartWeek()
        {

        }

        public void MoveMenuCenter(bool b)
        {
            animatorBackground.SetBool("Center", b);
        }

        public void MoveMenuLeft(bool b)
        {
            animatorBackground.SetBool("Left", b);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace