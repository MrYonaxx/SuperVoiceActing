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
    public class MenuManagementContract: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        ContractDatabase contractDatabase;

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

        public void GachaContract(PlayerData playerData)
        {
            int nbContract = 0;
            if (playerData.ContractAvailable.Count == 0)
            {
                nbContract = Random.Range(2, 4);
            }
            else if (playerData.ContractAvailable.Count <= 3)
            {
                nbContract = Random.Range(1, 4);
            }
            else if (playerData.ContractAvailable.Count <= 6)
            {
                nbContract = Random.Range(0, 3);
            }
            else if (playerData.ContractAvailable.Count > 6)
            {
                nbContract = Random.Range(0, 2);
            }


            if (playerData.ContractGacha.Count != 0)
            {
                nbContract = Mathf.Clamp(nbContract, 0, playerData.ContractGacha.Count);
                for (int i = 0; i < nbContract; i++)
                {
                    int rand = Random.Range(0, playerData.ContractGacha.Count);
                    ContractData cd = contractDatabase.GetContractData(playerData.ContractGacha[rand]);
                    Contract contractSelected = new Contract(cd);
                    playerData.ContractAvailable.Add(contractSelected);
                    playerData.ContractGachaCooldown.Add(new ContractCooldown(cd.name, cd.WeekMax));
                    playerData.ContractGacha.RemoveAt(rand);
                }
            }
        }

        public void CheckContractCooldown(PlayerData playerData)
        {
            for (int i = 0; i < playerData.ContractGachaCooldown.Count; i++)
            {
                playerData.ContractGachaCooldown[i].cooldown -= 1;
                if (playerData.ContractGachaCooldown[i].cooldown <= 0)
                {
                    playerData.ContractGacha.Add(playerData.ContractGachaCooldown[i].contract);
                    playerData.ContractGachaCooldown.RemoveAt(i);
                }
            }
        }


        public void ContractNextWeek(PlayerData playerData)
        {
            for (int i = 0; i < playerData.ContractAccepted.Count; i++)
            {
                if (playerData.ContractAccepted[i] != null)
                {
                    playerData.ContractAccepted[i].WeekRemaining -= 1;
                    if (playerData.ContractAccepted[i].CurrentLine == playerData.ContractAccepted[i].TotalLine)
                    {
                        continue;
                    }
                    if (playerData.ContractAccepted[i].WeekRemaining <= 0)
                    {
                        playerData.ContractAccepted.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i < playerData.ContractAvailable.Count; i++)
            {
                playerData.ContractAvailable[i].WeekRemaining -= 1;
                if (playerData.ContractAvailable[i].WeekRemaining <= 0)
                {
                    playerData.ContractAvailable.RemoveAt(i);
                }
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace