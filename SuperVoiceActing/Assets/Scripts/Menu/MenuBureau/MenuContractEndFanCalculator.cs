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
        [Space]
        [Space]
        [Space]
        [Title("Exigence des fans sur un acteur")]
        [DetailedInfoBox("roleFanExpectationRatio", "Pourcentage du nombre de fan d'un rôle a ajouté au meilleur score théorique")]
        [DetailedInfoBox("actorFanExpectationRatio", "Pourcentage du nombre de fan d'un acteur a ajouté au meilleur score théorique")]
        [SerializeField]
        float roleFanExpectationRatio = 0.01f;
        [SerializeField]
        float actorFanExpectationRatio = 0.005f;


        [Space]
        [Space]
        [Space]
        [Title("Calcul du gain de fan")]
        [DetailedInfoBox("Score Threshold", "Le pourcentage de score a dépasser pour gagner des fans.")]
        [DetailedInfoBox("maxFanGainPercentage", "Le pourcentage max de fan que l'acteur peut obtenir du rôle")]
        [SerializeField]
        float scoreThreshold = 0.5f;
        [SerializeField]
        float maxFanGainPercentage = 0.4f;

        [Space]
        [Space]
        [Space]
        [Title("Gain de fan")]
        [DetailedInfoBox("Fan Random Gain", "Le pourcentage de random au moment où l'acteur gagne des fans")]
        [DetailedInfoBox("Fan Decrease Ratio", "Le nombre de fan est multiplié par cette valeure à la fin de chaque semaine.")]
        [SerializeField]
        float fanRandomGain = 0.1f;
        [SerializeField]
        float fanGainDecreaseRatio = 0.8f;




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

        // En fonction du nombre de fan de l'acteur et du nombre de fan du rôle, augmente le score max théorique du rôle.
        public void AddScoreToRole(VoiceActor voiceActor, Role role, int totalHype)
        {
            if(totalHype != 0)
                role.RoleBestScore += (int)((role.Fan * roleFanExpectationRatio) *  (role.Fan / totalHype));
            role.RoleBestScore += (int)(voiceActor.FanGain * actorFanExpectationRatio);
        }

        // En fonction du score obtenu, augmente le gain de fan de l'acteur.
        public void CalculateFanGain(VoiceActor voiceActor, Role role)
        {

            if (role.RoleBestScore != 0)
            {
                float ratio = ((float)role.RoleScore / role.RoleBestScore) - scoreThreshold;
                voiceActor.FanGain += (int)((role.Fan * maxFanGainPercentage) * (ratio / scoreThreshold));
            }
        }


        // Ajoute le gain de fan, au montant de fan de tout les acteurs, et diminue le gain de fan en fonction de divers paramètres
        public void AddFan(List<VoiceActor> voiceActors)
        {
            for(int i = 0; i < voiceActors.Count;i++)
            {
                voiceActors[i].Fan += (int)(voiceActors[i].FanGain + Random.Range(-voiceActors[i].FanGain * fanRandomGain, voiceActors[i].FanGain * fanRandomGain));
                voiceActors[i].FanGain = (int)(voiceActors[i].FanGain * fanGainDecreaseRatio);
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