/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VoiceActing
{
    public class MenuContractEnd : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        CharacterSpriteDatabase characterSpriteDatabase;

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

        [SerializeField]
        TextMeshProUGUI textContractResearch;

        [Space]
        [SerializeField]
        Image imageSoundEngi;
        [SerializeField]
        TextMeshProUGUI textSoundEngiOldLevel;
        [SerializeField]
        TextMeshProUGUI textSoundEngiNewLevel;
        [SerializeField]
        TextMeshProUGUI textSoundEngiExpGained;
        [SerializeField]
        RectTransform transformSoundEngiExpGauge;
        [SerializeField]
        Animator animatorLevelUp;

        [Space]
        [SerializeField]
        Image[] imageActors;
        [SerializeField]
        TextMeshProUGUI[] textActors;

        [SerializeField]
        private MenuContractMoney moneyManager;

        [SerializeField]
        private MenuContractEndFanCalculator fanCalculator;

        int totalRevenu = 0;
        int soundEngiOldLevel;
        int soundEngiExpGained;


        List<Contract> contractsEnd = new List<Contract>();

        #endregion


        #region Functions 



        // ========================================================================
        public void CalculTotalScore(Contract contract)
        {
            int finalScore = 0;
            int finalHighScore = 0;
            int totalFan = contract.GetHype();
            for (int i = 0; i < contract.Characters.Count; i++)
            {
                // Score Stat
                contract.Characters[i].RolePerformance /= contract.TotalLine;
                contract.Characters[i].RoleBestScore /= contract.TotalLine; // Vu que je divise ici, si j'ajoute du score d'une autre source que le combat les calculs sont faussés
                CalculateRoleScore(contract.Characters[i], contract.VoiceActors[i]);

                // Fan
                fanCalculator.AddScoreToRole(contract.VoiceActors[i], contract.Characters[i], totalFan);
                fanCalculator.CalculateFanGain(contract.VoiceActors[i], contract.Characters[i]);

                finalScore += contract.Characters[i].RoleScore + contract.Characters[i].RolePerformance;
                finalHighScore += contract.Characters[i].RoleBestScore;

            }
            contract.Score = finalScore;
            contract.HighScore = finalHighScore;
            CalculateContractReward(contract);
        }


        public void CalculateRoleScore(Role role, VoiceActor voiceActor)
        {

            int statRole = 0;
            int statActor = 0;
            int totalScore = 0;

            int statGain = 0;
            int multiplier;

            for(int i = 0; i < 8; i++)
            {
                statGain = 0;
                statRole = role.CharacterStat.GetEmotion(i + 1);
                statActor = voiceActor.Statistique.GetEmotion(i + 1);

                if (statRole == role.BestStat)
                    multiplier = 3;
                else if (statRole == role.SecondBestStat)
                    multiplier = 2;
                else
                    multiplier = 1;

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
                //Debug.Log(statGain * multiplier);
                totalScore += statGain * multiplier;

                // Calculate Theoric Max
                role.RoleBestScore += (statRole * 3);
            }
            Debug.Log("Score " + role.Name + " : " + totalScore);
            Debug.Log("Score Performance" + role.Name + " : " + role.RolePerformance);
            role.RoleScore = totalScore;


        }
      
        /*private void CalculateRoleTimbreScore(Role role, VoiceActor voiceActor)
        {
            int pitchRoleValue = role.Timbre.y - role.Timbre.x;
            if (pitchRoleValue == 20)
                pitchRoleValue = 0;
            pitchRoleValue = Mathf.Clamp(pitchRoleValue, 1, 5);
            int pitchActorValueX = Mathf.Max(role.Timbre.x, voiceActor.Timbre.x);
            int pitchActorValueY = Mathf.Min(role.Timbre.y, voiceActor.Timbre.y);
            int pitchActorValue = Mathf.Max(pitchActorValueY - pitchActorValueX, 0);

        }*/


        public void CalculateContractReward(Contract contract)
        {
            float bonusMoney = 0;
            if (contract.Score < contract.HighScore * 0.2f) // Catastrophique à réfaire
            {
                textScoreTotal.text = "Catastrophique ...";
                bonusMoney = -contract.Money * 0.5f;
            }
            else if (contract.Score < contract.HighScore * 0.5f) // Malus
            {
                textScoreTotal.text = "Mauvais.";
                bonusMoney = contract.Money * (0.3f - ((contract.Score / contract.HighScore) - 0.2f));
            }
            else if (contract.Score < contract.HighScore * 0.7f) // Rien de spécial
            {
                textScoreTotal.text = "Bon.";
                bonusMoney = 0;
            }
            else // Bonus
            {
                textScoreTotal.text = "SUPER !";
                bonusMoney = contract.Money * (((float)contract.Score / contract.HighScore) - 0.7f);
            }
            totalRevenu += contract.Money + (int)bonusMoney;
            contract.MoneyBonus = (int)bonusMoney;

        }

        public void SoundEngiLevelUp(SoundEngineer soundEngineer, int exp)
        {
            if (soundEngineer == null)
                return;
            soundEngiOldLevel = soundEngineer.Level;
            soundEngiExpGained = exp + Random.Range(80, 100);
            /*if (soundEngineer.GainExp(soundEngiExpGained))
            {

            }*/
        }
        // ========================================================================






        // ========================================================================

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

                            playerData.Money += (playerData.ContractAccepted[i].Money + playerData.ContractAccepted[i].MoneyBonus);
                            playerData.ResearchPoint += playerData.ContractAccepted[i].Level + 10;

                            SoundEngiLevelUp(playerData.ContractAccepted[i].SoundEngineer, playerData.ContractAccepted[i].TotalMixing);
                            playerData.ContractAccepted.RemoveAt(i);
                            i -= 1;
                        }
                    }
                }
            }
            if (b == true)
            {
                this.gameObject.SetActive(true);
                animatorEnd.gameObject.SetActive(true);
                DrawAllContracts();
            }
            fanCalculator.AddFan(playerData.VoiceActors);
            return b;
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
            textContractBonusMoney.text = contract.MoneyBonus.ToString();
            textContractTotalMoney.text = (contract.Money + contract.MoneyBonus).ToString();
            textScoreTotal.text = contract.Score.ToString() + " / " + contract.HighScore;
            textContractResearch.text = contract.Level.ToString();

            if (contract.SoundEngineer.IsNull != true)
            {
                imageSoundEngi.gameObject.SetActive(true);
                imageSoundEngi.sprite = contract.SoundEngineer.SpritesSheets.SpriteNormal[0];
                textSoundEngiOldLevel.text = soundEngiOldLevel.ToString();
                textSoundEngiExpGained.text = "+" + soundEngiExpGained.ToString();
                transformSoundEngiExpGauge.localScale = new Vector2((float)contract.SoundEngineer.Experience / contract.SoundEngineer.ExperienceCurve.ExperienceCurve[contract.SoundEngineer.Level], 1);
                if (soundEngiOldLevel != contract.SoundEngineer.Level)
                {
                    textSoundEngiNewLevel.gameObject.SetActive(true);
                    textSoundEngiNewLevel.text = contract.SoundEngineer.Level.ToString();
                    animatorLevelUp.gameObject.SetActive(true);
                    animatorLevelUp.SetTrigger("Feedback");
                }
                else
                {
                    textSoundEngiNewLevel.gameObject.SetActive(false);
                }

            }
            else
            {
                imageSoundEngi.gameObject.SetActive(false);
            }

            for (int i = 0; i < imageActors.Length; i++)
            {
                if(i < contract.VoiceActors.Count)
                {
                    imageActors[i].gameObject.SetActive(true);                 
                    imageActors[i].sprite = characterSpriteDatabase.GetCharacterData(contract.VoiceActors[i].SpriteSheets).SpriteIcon;
                    textActors[i].text = (contract.Characters[i].RoleScore + contract.Characters[i].RolePerformance) + " / " + contract.Characters[i].RoleBestScore;
                }
                else
                {
                    imageActors[i].gameObject.SetActive(false);
                }
            }
        }

        public void NextContract()
        {
            inputController.SetActive(false);
            contractsEnd.RemoveAt(0);
            if(contractsEnd.Count == 0)
            {
                animatorEnd.SetTrigger("Disappear");
                moneyManager.AddSalaryDatas("Revenu", totalRevenu);
            }
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
