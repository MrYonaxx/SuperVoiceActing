/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
	public class MenuContractEnd : MonoBehaviour
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

        public void CalculateRoleScore()
        {
            Role role = null;
            VoiceActor voiceActor = null;

            int statRole = 0;
            int statActor = 0;
            int totalScore = 0;

            int statGain = 0;

            for(int i = 0; i < 8; i++)
            {
                switch(i)
                {
                    case 0:
                        statRole = role.CharacterStat.Joy;
                        statActor = voiceActor.Statistique.Joy;
                        break;

                }

                if(statRole == statActor) // Si stat équivalente c'est la folie
                {
                    statGain = (statRole * 3);
                }
                else if (statRole < statActor) // Si actor supérieur a role c'est bien
                {
                    statGain = (statActor - statRole);
                    if (statGain > statRole)
                        statGain = statRole;
                    statGain = statRole + (statRole - statGain);
                }
                else if (statRole > statActor) // Si role supérieur à actor C'est nul
                {
                    statGain = (statRole - statActor);
                    if (statGain > statActor)
                        statGain = statActor;
                    statGain = statRole + (statRole - statGain);
                }
                totalScore += statGain;
            }
            role.RoleScore = totalScore;
        }
        
        #endregion
		
	} // MenuContractEnd class
	
}// #PROJECTNAME# namespace
