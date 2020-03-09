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
        MenuAutoContract menuAutoContract;
        [SerializeField]
        MenuContractEnd menuContractEnd;

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



        public void VoiceActorsWork(List<VoiceActor> voiceActors, List<Contract> contracts)
        {
            UpdateActorsState(voiceActors);

            //!\ Gérer les exceptions (les acteurs en contrat pour le joueur ne peuvent pas être utilisés, un acteur ne peut pas être auditionné plus de 2 fois)

            for (int i = 0; i < contracts.Count; i++)
            {
                if (contracts[i].WeekRemaining <= 1)
                {
                    List<VoiceActor> actorAuditionned = Audition(voiceActors, contracts[i]);
                    // Calculate Score Manually
                    menuAutoContract.AutoContract(contracts[i], actorAuditionned);
                    menuContractEnd.CalculTotalScore(contracts[i], actorAuditionned);
                }
            }

            //!\ Les acteurs qui ont trouvés des contrats ne peuvent pas Work For week
            // Si un acteur passe un certain level il ne travaille plus ?

            VoiceActorsWorkForWeek(voiceActors);
        }




        private void UpdateActorsState(List<VoiceActor> voiceActors)
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                UpdateActorState(voiceActors[i]);
            }
        }

        private void UpdateActorState(VoiceActor voiceActor)
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
        }









        private List<VoiceActor> Audition(List<VoiceActor> voiceActors, Contract contract)
        {
            int[] randomTable;
            int randomTotal;
            List<VoiceActor> res = new List<VoiceActor>(contract.Characters.Count);
            for (int i = 0; i < contract.Characters.Count; i++)
            {
                randomTable = new int[voiceActors.Count];
                randomTotal = 0;
                // Calculate Draw Table
                for (int j = 0; j < voiceActors.Count; j++)
                {
                    randomTotal += CalculateVoiceActorGachaScore(contract.Characters[i], voiceActors[j]);
                    randomTable[j] = randomTotal;
                }

                // Draw an actor with the Draw Table
                int draw = Random.Range(0, randomTotal);
                for (int k = 0; k < randomTable.Length; k++)
                {
                    if (draw < randomTable[k])
                    {
                        res.Add(voiceActors[k]);
                        break;
                    }
                }
                contract.VoiceActorsID[i] = res[res.Count-1].VoiceActorID;            
            }
            return res;
        }

        private int CalculateVoiceActorGachaScore(Role role, VoiceActor va, float finalMultiplier = 1)
        {
            int finalScore = 1;


            finalScore += va.Level;
            finalScore += role.CheckTimbre(va.Timbre);
            int statRole = 0;
            int statActor = 0;
            int multiplier = 1;

            int statGain = 0;

            for (int i = 1; i < 9; i++)
            {
                multiplier = 1;
                statGain = 0;
                statRole = role.CharacterStat.GetEmotion(i);
                statActor = va.Statistique.GetEmotion(i);

                if (i + 1 == role.BestStatEmotion)
                {
                    multiplier = 3;
                }
                else if (i + 1 == role.SecondBestStatEmotion)
                {
                    multiplier = 2;
                }

                if (statRole == statActor) // Si stat équivalente c'est la folie
                {
                    statGain = 20;
                }
                else if (statRole < statActor)
                {
                    statGain = 20 - Mathf.Abs(statActor - statRole);
                    if (statGain < 0)
                        statGain = 0;
                    statGain += 1;
                    if (statGain == 1 && multiplier != 1)
                        multiplier /= 2;
                }
                else if (statRole > statActor)
                {
                    statGain = 5 - Mathf.Abs(statRole - statActor);
                    if (statGain < 0)
                        statGain = 0;
                    if (multiplier != 1)
                        multiplier /= 2;
                }
                statGain *= multiplier;
                finalScore += statGain;
            }
            Debug.Log(va.VoiceActorName + " | " + finalScore);
            return (int)(finalScore * finalMultiplier);
        }








        private void VoiceActorsWorkForWeek(List<VoiceActor> voiceActors)
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

            //Chance de trouver du travail dépendant du nombre de fan
            int chanceWork = 15;
            int random = Random.Range(0, 100);

            // Le comédien travaille
            if (random <= chanceWork)
            {
                voiceActor.Hp -= (int)(voiceActor.HpMax * Random.Range(0.1f, 0.2f));
                voiceActor.GainExp((int)(voiceActor.ExperienceCurve[voiceActor.Level] * Random.Range(0.1f, 0.3f)));
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