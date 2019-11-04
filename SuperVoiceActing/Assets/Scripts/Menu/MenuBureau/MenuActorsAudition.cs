/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VoiceActing
{
	public class MenuActorsAudition : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        CharacterSpriteDatabase characterSpriteDatabase;

        [SerializeField]
        Animator animatorAudition;
        [SerializeField]
        Animator animatorAuditionInfo;
        [SerializeField]
        CameraBureau camBureau;


        [SerializeField]
        GameObject actorsSpriteOutline;
        [SerializeField]
        GameObject actorsInfo;
        [SerializeField]
        GameObject actorsSkills;

        [SerializeField]
        GameObject auditionInfo;
        [SerializeField]
        GameObject auditionMicro;


        [SerializeField]
        Slider sliderBudget;
        [SerializeField]
        TextMeshProUGUI textBudget;
        [SerializeField]
        int budgetInitialValue;

        [SerializeField]
        Image actorsSprite;

        [SerializeField]
        AudioClip auditionSpotlight;

        [SerializeField]
        MenuActorsManager menuActorsManager;

        [SerializeField]
        InputController inputActor;
        [SerializeField]
        InputController inputAudition;

        VoiceActor va;
        int budget;

        bool actorSpriteLocked = false;

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

        public int GetBudget()
        {
            return budget;
        }

        public string GetAuditionName()
        {
            return va.VoiceActorName;
        }


        public void Audition(Role role)
        {
            va = GachaVoiceActors(role, playerData.VoiceActorsGacha, playerData.VoiceActors);
            actorsSprite.sprite = characterSpriteDatabase.GetCharacterData(va.SpriteSheets).SpriteNormal[0];
            actorsSprite.SetNativeSize();
        }

        public void DrawAuditionPanel(bool b)
        {
            actorsSpriteOutline.SetActive(!b);
            actorsInfo.SetActive(!b);
            actorsSkills.SetActive(!b);

            auditionMicro.SetActive(b);
            auditionInfo.SetActive(b);
            budget = budgetInitialValue * (int)sliderBudget.value;
            textBudget.text = budget.ToString();
            menuActorsManager.DrawBudgetAudition();
            if(actorSpriteLocked == true)
            {
                return;
            }
            if (b == true)
                actorsSprite.enabled = false;
            else
                actorsSprite.enabled = true;

        }


















        public VoiceActor GachaVoiceActors(Role role, List<VoiceActor> voiceActorsGacha, List<VoiceActor> voiceActors)
        {
            int playerRank = 20;
            int[] randomDraw = new int[voiceActorsGacha.Count + voiceActors.Count];
            int currentMaxValue = 0;

            // Calculate Actor Draw Chances
            for (int i = 0; i < voiceActorsGacha.Count; i++)
            {
                if (voiceActorsGacha[i].Level > playerRank + 3)
                {
                    randomDraw[i] = -1;
                }
                else
                {
                    currentMaxValue += CalculateVoiceActorGachaScore(role, voiceActorsGacha[i]);
                    randomDraw[i] = currentMaxValue;
                }
            }

            // Calculate Actor Draw Chances
            for (int i = 0; i < voiceActors.Count; i++)
            {
                if (voiceActors[i].Availability == false)
                    currentMaxValue = -1;
                else
                {
                    currentMaxValue += CalculateVoiceActorGachaScore(role, voiceActors[i], 0.5f);
                    randomDraw[voiceActorsGacha.Count + i] = currentMaxValue;
                }
            }

            // Draw
            int draw = Random.Range(0, currentMaxValue);
            VoiceActor result;
            Debug.Log(draw);
            for (int i = 0; i < randomDraw.Length; i++)
            {
                if (draw < randomDraw[i])
                {
                    if (i < voiceActorsGacha.Count)
                    {
                        //Debug.Log(voiceActorsGacha[i].Name);
                        voiceActors.Add(voiceActorsGacha[i]);
                        result = voiceActorsGacha[i];
                        voiceActorsGacha.RemoveAt(i);
                        return result;
                    }
                    else
                    {
                        result = voiceActors[i - voiceActorsGacha.Count];
                        return result;
                    }
                }
            }
            return null;
        }

        private int CalculateVoiceActorGachaScore(Role role, VoiceActor va, float finalMultiplier = 1)
        {
            int finalScore = 1;

            int statRole = 0;
            int statActor = 0;
            int multiplier = 1;

            int statGain = 0;

            for (int i = 0; i < 8; i++)
            {
                multiplier = 1;
                statGain = 0;
                statRole = role.CharacterStat.GetEmotion(i + 1);
                statActor = va.Statistique.GetEmotion(i + 1);

                if (statRole == role.BestStat)
                {
                    multiplier = 100;
                }
                else if (statRole == role.SecondBestStat)
                {
                    multiplier = 80;
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





















        public void AnimAudition(Role roleToAudition)
        {
            AudioManager.Instance.StopMusic(120);
            camBureau.transform.eulerAngles = new Vector3(camBureau.transform.eulerAngles.x, camBureau.transform.eulerAngles.y, 0);
            camBureau.ChangeOrthographicSize(-30, 300);
            camBureau.HideHUD();
            actorsSprite.enabled = true;
            animatorAudition.enabled = true;
            animatorAudition.SetTrigger("Audition");
            animatorAuditionInfo.SetTrigger("Disappear");
            inputActor.gameObject.SetActive(false);
            actorSpriteLocked = true;

            Audition(roleToAudition);


        }


        public void EndAnimAudition()
        {
            animatorAuditionInfo.gameObject.SetActive(false);
            menuActorsManager.StopGachaEffect(va);
            inputAudition.gameObject.SetActive(true);
            AudioManager.Instance.PlaySound(auditionSpotlight);
            actorSpriteLocked = false;
        }



        public void IncreaseBudget()
        {
            sliderBudget.value += 1;
            budget = budgetInitialValue * (int)sliderBudget.value;
            textBudget.text = budget.ToString();
            menuActorsManager.DrawBudgetAudition();
        }

        public void ReduceBudget()
        {
            sliderBudget.value -= 1;
            budget = budgetInitialValue * (int)sliderBudget.value;
            textBudget.text = budget.ToString();
            menuActorsManager.DrawBudgetAudition();
        }

        public bool CheckCost()
        {
            if(playerData.Money >= budget)
            {
                playerData.Money -= budget;
                return true;
            }
            return false;

        }


        #endregion

    } // MenuActorsAudition class
	
}// #PROJECTNAME# namespace
