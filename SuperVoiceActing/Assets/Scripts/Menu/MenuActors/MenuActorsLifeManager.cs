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
    public class MenuActorsLifeManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        ExperienceCurveData experienceCurve;

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

        public void VoiceActorWork(List<VoiceActor> voiceActors)
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                WorkForWeek(voiceActors[i]);
            }
        }

        private void WorkForWeek(VoiceActor voiceActor)
        {
            if (voiceActor.ActorMentalState == VoiceActorState.Absent)
            {
                return;
            }

            // Si le comédien était mort il passe en fatigué
            if (voiceActor.ActorMentalState == VoiceActorState.Dead)
            {
                voiceActor.Hp = 1;
                voiceActor.ActorMentalState = VoiceActorState.Tired;
                voiceActor.Availability = false;
                return;
            }

            // Si le comédien a 0 hp il est mort
            if (voiceActor.Hp == 0)
            {
                voiceActor.ActorMentalState = VoiceActorState.Dead;
                voiceActor.Availability = false;
                return;
            }

            // Si le comédien était indisponible il revient full
            if (voiceActor.ActorMentalState == VoiceActorState.Tired)
            {
                voiceActor.Hp = voiceActor.HpMax;
                voiceActor.Availability = true;
                voiceActor.ActorMentalState = VoiceActorState.Fine;
                return;
            }

            //Chance de trouver du travail dépendant du nombre de fan
            int chanceWork = 15;
            int random = Random.Range(0, 100);

            // Le comédien travaille
            if (random <= chanceWork)
            {
                voiceActor.Hp -= (int)(voiceActor.HpMax * Random.Range(0.1f, 0.2f));
                //experience = experience + (int) (experienceCurve.ExperienceCurve[level] * Random.Range(0.1f, 0.3f));
                voiceActor.NextEXP -= (int)(experienceCurve.ExperienceCurve[voiceActor.Level] * Random.Range(0.1f, 0.3f));//experienceCurve.ExperienceCurve[level] - experience;
                if (voiceActor.NextEXP <= 0)
                {
                    voiceActor.LevelUp();
                    voiceActor.NextEXP += experienceCurve.ExperienceCurve[voiceActor.Level];
                }
            }
            else // le comédien fais des trucs
            {
                voiceActor.Hp += (int)(voiceActor.HpMax * Random.Range(0.05f, 0.2f));
            }
            voiceActor.Hp = Mathf.Clamp(voiceActor.Hp, 0, voiceActor.HpMax);

            // Si le comédien est dans le mal il passe indisponible
            if (voiceActor.Hp <= (voiceActor.HpMax * 0.25f))
            {
                voiceActor.ActorMentalState = VoiceActorState.Tired;
                voiceActor.Availability = false;
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace