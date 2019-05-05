/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VoiceActing
{
	public class MenuContractEnd : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Animator animatorEnd;

        [SerializeField]
        GameObject inputController;

        [SerializeField]
        GameObject[] gameObjectContracts;

        [SerializeField]
        TextMeshProUGUI textContractTitle;
        [SerializeField]
        TextMeshProUGUI textContractBaseMoney;
        [SerializeField]
        TextMeshProUGUI textContractBonusMoney;
        [SerializeField]
        TextMeshProUGUI textContractTotalMoney;
        [SerializeField]
        TextMeshProUGUI textScoreTotal;


        List<Contract> contractsEnd = new List<Contract>();

        #endregion


        #region Functions 

        public void CalculateRoleScore(Role role, VoiceActor voiceActor)
        {

            int statRole = 0;
            int statActor = 0;
            int totalScore = 0;

            int statGain = 0;

            for(int i = 0; i < 8; i++)
            {
                statGain = 0;
                switch (i)
                {
                    case 0:
                        statRole = role.CharacterStat.Joy;
                        statActor = voiceActor.Statistique.Joy;
                        break;
                    case 1:
                        statRole = role.CharacterStat.Sadness;
                        statActor = voiceActor.Statistique.Sadness;
                        break;
                    case 2:
                        statRole = role.CharacterStat.Disgust;
                        statActor = voiceActor.Statistique.Disgust;
                        break;
                    case 3:
                        statRole = role.CharacterStat.Anger;
                        statActor = voiceActor.Statistique.Anger;
                        break;
                    case 4:
                        statRole = role.CharacterStat.Surprise;
                        statActor = voiceActor.Statistique.Surprise;
                        break;
                    case 5:
                        statRole = role.CharacterStat.Sweetness;
                        statActor = voiceActor.Statistique.Sweetness;
                        break;
                    case 6:
                        statRole = role.CharacterStat.Fear;
                        statActor = voiceActor.Statistique.Fear;
                        break;
                    case 7:
                        statRole = role.CharacterStat.Trust;
                        statActor = voiceActor.Statistique.Trust;
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
                Debug.Log(statGain);
                totalScore += statGain;
            }
            Debug.Log("Score " + role.Name + " : " + totalScore);
            Debug.Log("Score Performance" + role.Name + " : " + role.RolePerformance);
            role.RoleScore = totalScore;

        }
        

        public void CalculTotalScore(Contract contract)
        {
            int finalScore = 0;
            for(int i = 0; i < contract.Characters.Count; i++)
            {
                contract.Characters[i].RolePerformance /= contract.TotalLine;
                CalculateRoleScore(contract.Characters[i], contract.VoiceActors[i]);
                finalScore += contract.Characters[i].RoleScore + contract.Characters[i].RolePerformance;
            }
            contract.Score = finalScore;

        }

        public bool CheckContractDone(PlayerData playerData)
        {
            bool b = false;
            for (int i = 0; i < playerData.ContractAccepted.Count; i++)
            {
                if (playerData.ContractAccepted[i] != null)
                {
                    if (playerData.ContractAccepted[i].CurrentLine == playerData.ContractAccepted[i].TotalLine)
                    {
                        if (playerData.ContractAccepted[i].CurrentMixing == playerData.ContractAccepted[i].TotalMixing)
                        {
                            b = true;
                            contractsEnd.Add(playerData.ContractAccepted[i]);
                            CalculTotalScore(playerData.ContractAccepted[i]);
                            playerData.ContractAccepted.RemoveAt(i);
                            playerData.ContractAccepted.Add(null);
                        }
                    }
                }
            }
            if (b == true)
            {
                animatorEnd.gameObject.SetActive(true);
                DrawAllContracts();
            }
            return b;
        }

        // ========================================================================

        public void CalculateContractReward()
        {


        }


        public void DrawAllContracts()
        {
            for (int i = 0; i < gameObjectContracts.Length; i++)
            {
                if (i < contractsEnd.Count)
                {
                    if (contractsEnd[i] != null)
                        gameObjectContracts[i].SetActive(true);
                    else
                        gameObjectContracts[i].SetActive(false);
                }
                else
                {
                    gameObjectContracts[i].SetActive(false);
                }
            }
            inputController.SetActive(true);
            DrawContractReward(contractsEnd[0]);
        }

        public void DrawContractReward(Contract contract)
        {
            textContractTitle.text = contract.Name;
            textContractBaseMoney.text = contract.Money.ToString();
            textContractBonusMoney.text = "0";
            textContractTotalMoney.text = contract.Money.ToString();
            textScoreTotal.text = contract.Score.ToString();
        }

        public void NextContract()
        {
            inputController.SetActive(false);
            contractsEnd.RemoveAt(0);
            if(contractsEnd.Count == 0)
                animatorEnd.SetTrigger("Disappear");
            else
                animatorEnd.SetTrigger("Slide");

        }

        public void EndContractDone()
        {
            animatorEnd.gameObject.SetActive(false);
        }


        #endregion
		
	} // MenuContractEnd class
	
}// #PROJECTNAME# namespace
