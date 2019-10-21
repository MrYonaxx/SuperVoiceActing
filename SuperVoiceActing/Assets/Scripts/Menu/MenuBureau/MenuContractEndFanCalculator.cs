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
    public class MenuContractEndFanCalculator: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


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
        public void AddScoreToRole(VoiceActor voiceActor, Role role, int totalHype)
        {
            float roleFanExpectationRatio = 0.01f;
            float actorFanExpectationRatio = 0.005f;

            role.RoleBestScore += (int)((role.Fan * roleFanExpectationRatio) *  (role.Fan / totalHype));
            role.RoleBestScore += (int)(voiceActor.FanGain * actorFanExpectationRatio);
        }

        public void CalculateFanGain(VoiceActor voiceActor, Role role)
        {

            float scoreThreshold = 0.5f;
            float maxFanGainPercentage = 0.25f;

            float ratio = (role.RoleScore / role.RoleBestScore) - scoreThreshold;
            voiceActor.FanGain += (int)((role.Fan * maxFanGainPercentage) * (ratio / scoreThreshold));
        }



        public void AddFan(List<VoiceActor> voiceActors)
        {
            float fanRandomGain = 0.1f;
            float fanGainDecreaseRatio = 0.5f;

            //float fanMax = 0;

            for(int i = 0; i < voiceActors.Count;i++)
            {
                Debug.Log(voiceActors[i].Name + " " + voiceActors[i].FanGain);
                voiceActors[i].Fan += (int)(voiceActors[i].FanGain + Random.Range(-voiceActors[i].FanGain * fanRandomGain, voiceActors[i].FanGain * fanRandomGain));
                voiceActors[i].FanGain = (int)(voiceActors[i].FanGain * fanGainDecreaseRatio);
                Debug.Log(voiceActors[i].Name + " " + voiceActors[i].FanGain);
                if (voiceActors[i].FanGain <= 0)
                    voiceActors[i].Fan -= Random.Range(0, voiceActors[i].Level);
                else if (voiceActors[i].FanGain <= voiceActors[i].Level)
                    voiceActors[i].FanGain = 0;
                voiceActors[i].Fan = Mathf.Max(voiceActors[i].Fan, 0);
            }

        }

        #endregion

    } 

} // #PROJECTNAME# namespace