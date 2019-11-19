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

        public void AddGachaContractsSameLevel(PlayerData playerData)
        {
            for (int i = 0; i < contractDatabase.Contracts.Count; i++)
            {
                if (contractDatabase.Contracts[i].Level == playerData.PlayerStudioLevel)
                    playerData.ContractGacha.Add(contractDatabase.Contracts[i].name);
            }
        }

        public void AddGachaContractsUnderLevel(PlayerData playerData)
        {
            playerData.ContractGacha.Clear();
            for (int i = 0; i < contractDatabase.Contracts.Count; i++)
            {
                if (contractDatabase.Contracts[i].Level <= playerData.PlayerStudioLevel)
                {
                    playerData.ContractGacha.Add(contractDatabase.Contracts[i].name);
                }
            }

        }









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
                    // Select level
                    int playerLevel = playerData.PlayerStudioLevel;

                    int rand = Random.Range(0, playerData.ContractGacha.Count);

                    // Select franchise
                    ContractData cd = contractDatabase.GetContractData(playerData.ContractGacha[rand]);
                    FranchiseSave franchiseSave = GetFranchiseSave(playerData.ContractFranchise, cd.name, cd.Franchise.contractDatas.Count+1);
                    Debug.Log(cd.name);
                    // Select contract from franchise and Check if suite and assign actor stat from the previous installment
                    Contract contractSelected = SelectContract(cd, franchiseSave, playerData.PlayerStudioLevel);

                    // Equilibrate 
                    Equilibrate(contractSelected, cd.Franchise, playerLevel);

                    playerData.ContractAvailable.Add(contractSelected);
                    playerData.ContractGachaCooldown.Add(new ContractCooldown(cd.name, cd.WeekMax*2));
                    playerData.ContractGacha.RemoveAt(rand);
                }
            }
        }



        private FranchiseSave GetFranchiseSave(List<FranchiseSave> franchiseSaves, string contractID, int size)
        {
            for(int i = 0; i < franchiseSaves.Count; i++)
            {
                if(franchiseSaves[i].FranchiseName == contractID)
                {
                    return franchiseSaves[i];
                }
            }
            franchiseSaves.Add(new FranchiseSave(contractID, size));
            return franchiseSaves[franchiseSaves.Count - 1];
        }



        private Contract SelectContract(ContractData cd, FranchiseSave franchiseSave, int playerLevel)
        {
            Franchise fra = cd.Franchise;
            Contract contractFinal = null;

            franchiseSave.CurrentFranchiseNumber += Random.Range(fra.franchiseCountIncreaseMin, fra.franchiseCountIncreaseMax);

            int selectIndex = cd.Franchise.GetSeriesIndex(franchiseSave.CurrentFranchiseNumber, playerLevel);


            if (fra.isSeries == true)
            {
                fra.FranchiseDirection(franchiseSave);
                contractFinal = new Contract(cd, franchiseSave, selectIndex);
                return contractFinal;
            }

            if (selectIndex == 0)
                contractFinal = new Contract(cd);
            else
                contractFinal = new Contract(fra.contractDatas[selectIndex - 1]);
            return contractFinal;
        }



        private void Equilibrate(Contract contract, Franchise fra, int playerLevel)
        {
            if (playerLevel <= contract.Level)
                return;
            if (fra.canScale == false)
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