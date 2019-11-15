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

        [SerializeField]
        MenuContractDifficultyCurve menuContractDifficultyCurve;


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

            // Select level
            int playerLevel = playerData.PlayerStudioLevel;
            if(playerLevel < 3)
            {
                // Impossible de sélectionner des contrats d'un level différent du player level
            }


            if (playerData.ContractGacha.Count != 0)
            {
                nbContract = Mathf.Clamp(nbContract, 0, playerData.ContractGacha.Count);
                for (int i = 0; i < nbContract; i++)
                {
                    int rand = Random.Range(0, playerData.ContractGacha.Count);

                    // Select franchise
                    Franchise franchise = contractDatabase.GetFranchise(playerData.ContractGacha[rand]);
                    FranchiseSave franchiseSave = GetFranchiseSave(playerData.ContractFranchise, franchise);

                    // Select contract from franchise
                    Contract contractSelected = SelectContract(franchiseSave, franchise);

                    // Check if suite and assign actor stat from the previous installment
                    AssignOldActors(contractSelected, franchiseSave, franchise);

                    // Equilibrate 
                    Equilibrate(contractSelected, franchiseSave, franchise, playerLevel);

                    playerData.ContractAvailable.Add(contractSelected);
                    playerData.ContractGachaCooldown.Add(new ContractCooldown(franchiseSave.FranchiseName, contractSelected.WeekRemaining+1));
                    playerData.ContractGacha.RemoveAt(rand);
                }
            }
        }



        private FranchiseSave GetFranchiseSave(List<FranchiseSave> franchiseSaves, Franchise franchise)
        {
            for(int i = 0; i < franchiseSaves.Count; i++)
            {
                if(franchiseSaves[i].FranchiseName == franchise.contractDatas[0].name)
                {
                    franchiseSaves[i].CurrentFranchiseNumber += 1;
                    return franchiseSaves[i];
                }
            }
            franchiseSaves.Add(new FranchiseSave(franchise.contractDatas[0].name, franchise.contractDatas.Count));
            return franchiseSaves[franchiseSaves.Count - 1];
        }



        private Contract SelectContract(FranchiseSave franchiseSave, Franchise fra)
        {
            int franchiseCount = franchiseSave.CurrentFranchiseNumber;
            int playerLevel = 1;
            int selectIndex = 0;

            Contract contractSelected;

            if (fra.priorityOrder == true) // Select the next contract of the list
            {
                selectIndex = franchiseCount % fra.contractDatas.Count;
            }
            else if (fra.priorityScaling == true) // Select the hardest contract for the current playerLevel
            {
                int bestLevel = 0;
                for (int i = 0; i < fra.contractDatas.Count; i++)
                {
                    if (fra.contractDatas[i].Level > bestLevel && fra.contractDatas[i].Level < playerLevel)
                    {
                        bestLevel = fra.contractDatas[i].Level;
                        selectIndex = i;
                    }
                }
            }
            else if (fra.priorityRandom == true) // Select un contrat au pif
            {
                selectIndex = Random.Range(0, fra.contractDatas.Count);
            }

            contractSelected = new Contract(fra.contractDatas[selectIndex]);
            return contractSelected;
        }




        private void AssignOldActors(Contract contract, FranchiseSave franchiseSave, Franchise fra)
        {
            if (fra.isSeries == false)
                return;
        }

        private void Equilibrate(Contract contract, FranchiseSave franchiseSave, Franchise fra, int playerLevel)
        {
            if (playerLevel <= contract.Level)
                return;
            menuContractDifficultyCurve.EquilibrateContract(contract, playerLevel);
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