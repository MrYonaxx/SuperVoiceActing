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
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class ResultScreen : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("Database")]
        [SerializeField]
        private CharacterSpriteDatabase characterSpriteDatabase;
        [SerializeField]
        private SkillDatabase skillDatabase;

        [Title("NextScene")]
        [SerializeField]
        private string nextScene = "Bureau";


        [Title("Text")]
        [SerializeField]
        private TextMeshProUGUI textName;
        [SerializeField]
        private TextMeshProUGUI textSessionNumber;

        [SerializeField]
        private TextMeshProUGUI textCurrentLine;
        [SerializeField]
        private TextMeshProUGUI textLineDefeated;
        [SerializeField]
        private TextMeshProUGUI textMaxLine;

        [SerializeField]
        private TextMeshProUGUI textTurn;
        [SerializeField]
        private TextMeshProUGUI textEXP;
        [SerializeField]
        private TextMeshProUGUI textExpBonus;
        [SerializeField]
        private TextMeshProUGUI[] textEmotionUsed;

        [Title("UI")]
        [SerializeField]
        private RectTransform lineGauge;
        [SerializeField]
        private RectTransform lineNewGauge;


        private Contract contract;
        private List<VoiceActor> voiceActors;


        [Title("Actors")]
        [SerializeField]
        private ExperienceCurveData experience;

        [SerializeField]
        private Image[] actorsImage;
        [SerializeField]
        private GameObject[] actorsExpOutline;
        [SerializeField]
        private TextMeshProUGUI[] textsLevel;
        [SerializeField]
        private TextMeshProUGUI[] textsNext;
        [SerializeField]
        private RectTransform[] expGauge;

        [Space]
        [SerializeField]
        private TextMeshProUGUI[] textSkillRemaining;
        [SerializeField]
        private RectTransform[] skillGauge;



        [Title("LevelUp")]
        [SerializeField]
        private GameObject windowLevelUp;
        [SerializeField]
        private TextMeshProUGUI textOldLevel;
        [SerializeField]
        private TextMeshProUGUI textNewLevel;
        [SerializeField]
        private TextMeshProUGUI[] textsStats;
        [SerializeField]
        private RectTransform[] gaugeStats;
        [SerializeField]
        private RectTransform[] newGaugeStats;
        [SerializeField]
        private TextMeshProUGUI[] textsGain;
        [SerializeField]
        private TextMeshProUGUI textHPLevelUp;

        [Title("Skill")]
        [SerializeField]
        private GameObject windowSkillGet;
        [SerializeField]
        private TextMeshProUGUI textSkillGet;
        [SerializeField]
        private TextMeshProUGUI textSkillGetDescription;


        private bool[] actorsLevelUp;
        private int[] actorsOldLevel;

        private int[] actorsOldSkillLevel;
        private int[] actorsNewSkillLevel;

        private int[] actorsOldHP;

        private EmotionStat[] actorsOldStats;

        [Title("Feedback")]
        [SerializeField]
        private Animator resultScreen;
        [SerializeField]
        private Animator[] animatorLevelUpFeedback;
        [SerializeField]
        private Animator[] animatorStats;
        [SerializeField]
        private Animator animatorTextLevelUp;
        [SerializeField]
        private Animator[] animatorSkillGet;
        [SerializeField]
        private Animator animatorLevelUpHP;
        [SerializeField]
        private Animator animatorEndStatScreen;

        [Space]
        [SerializeField]
        private SimpleSpectrum spectrum;
        [SerializeField]
        private InputController inputController;

        private bool inAnimation = true;
        private int lineDefeated = 0;

        private int previousActorLevelUp = -1;

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

        public void ChangeEndScene(string newEndScene)
        {
            nextScene = newEndScene;
        }

        public void SetContract(Contract con, List<VoiceActor> va)
        {
            contract = con;
            voiceActors = va;
            spectrum.audioSource = AudioManager.Instance.GetAudioSourceMusic();
            spectrum.enabled = true;
            actorsLevelUp = new bool[contract.VoiceActorsID.Count];
            for (int i = 0; i < actorsLevelUp.Length; i++)
            {
                actorsLevelUp[i] = false;
            }
            actorsOldLevel = new int[contract.VoiceActorsID.Count];
            actorsOldStats = new EmotionStat[contract.VoiceActorsID.Count];

            actorsOldHP = new int[contract.VoiceActorsID.Count];
            actorsOldSkillLevel = new int[contract.VoiceActorsID.Count];
            actorsNewSkillLevel = new int[contract.VoiceActorsID.Count];

            DrawEmotionUsed(con.EmotionsUsed);
        }

        private void DrawEmotionUsed(List<EmotionUsed> emotionUseds)
        {
            int[] res = new int[9];
            for (int i = 0; i < emotionUseds.Count; i++)
            {
                for (int j = 0; j < emotionUseds[i].emotions.Length; j++)
                {
                    res[(int)emotionUseds[i].emotions[j]] += 1;
                }
            }
            for (int i = 1; i < res.Length; i++)
            {
                textEmotionUsed[i - 1].text = res[i].ToString();
            }
        }

        public void DrawResult(int numberTurn, int lineDefeated)
        {
            this.lineDefeated = lineDefeated;
            // Draw
            textName.text = contract.Name;
            textSessionNumber.text = contract.SessionNumber.ToString();
            textCurrentLine.text = contract.CurrentLine.ToString();
            textLineDefeated.text = lineDefeated.ToString();
            textMaxLine.text = contract.TotalLine.ToString();

            textTurn.text = (15-numberTurn).ToString();

            textEXP.text = "0";
            textExpBonus.text = "0";

            lineGauge.localScale = new Vector3((contract.CurrentLine / (float)contract.TotalLine), lineGauge.localScale.y, lineGauge.localScale.z);
            lineNewGauge.localScale = new Vector3((contract.CurrentLine / (float)contract.TotalLine), lineNewGauge.localScale.y, lineNewGauge.localScale.z);

            // Update Contract
            contract.CurrentLine += lineDefeated;

            int expBonus = 0;
            if (contract.CurrentLine == contract.TotalLine)
            {
                expBonus = contract.ExpBonus;
                StartCoroutine(ExpGainCoroutine(textExpBonus, expBonus, 1));
                AddVoxography();
            }

            // Coroutine
            StartCoroutine(LineGainCoroutine());
            StartCoroutine(ExpGainCoroutine(textEXP, contract.ExpGain, lineDefeated));

            DrawActors((contract.ExpGain * lineDefeated) + expBonus);

        }



        private IEnumerator LineGainCoroutine()
        {
            float ratio = contract.CurrentLine / (float)contract.TotalLine;
            int time = 180;
            Vector3 speed = new Vector3((ratio - lineNewGauge.localScale.x) / time, 0, 0);
            while (time != 0)
            {
                time -= 1;
                lineNewGauge.localScale += speed;
                yield return null;
            }
            textCurrentLine.text = contract.CurrentLine.ToString();
        }

        private IEnumerator ExpGainCoroutine(TextMeshProUGUI tmp, int exp, int lineDefeated)
        {
            float time = 180f;
            float speed = (exp * lineDefeated) / time;
            float digit = 0;
            while (time != 0)
            {
                time -= 1;
                digit += speed;
                tmp.text = ((int)digit).ToString();
                yield return null;
            }
            tmp.text = (exp * lineDefeated).ToString();
        }







        // ====================================================================================================================
        // D R A W    A C T O R S
        // ====================================================================================================================

        private void DrawActors(int expGain)
        {
            for(int i = 0; i < voiceActors.Count; i++)
            {
                actorsImage[i].gameObject.SetActive(true);
                actorsExpOutline[i].gameObject.SetActive(true);

                actorsImage[i].sprite = characterSpriteDatabase.GetCharacterData(voiceActors[i].VoiceActorID).SpriteNormal[0];
                actorsImage[i].SetNativeSize();
                textsLevel[i].text = voiceActors[i].Level.ToString();
                textsNext[i].text = (voiceActors[i].NextEXP).ToString();
                expGauge[i].transform.localScale = new Vector3((1-(voiceActors[i].NextEXP / (float)experience.ExperienceCurve[voiceActors[i].Level])),
                                                                expGauge[i].transform.localScale.y,
                                                                expGauge[i].transform.localScale.z);

                int actorGainDeduction = voiceActors[i].NextEXP;
                voiceActors[i].NextEXP -= expGain;
                StartCoroutine(CalculateExp(voiceActors[i], expGauge[i], i, expGain, actorGainDeduction));
                StartCoroutine(DrawRelationGauge(skillGauge[i], textSkillRemaining[i], i));
            }
        }

        private IEnumerator CalculateExp(VoiceActor va, RectTransform expGauge, int i, int expGain, int actorGainDeduction)
        {
            yield return new WaitForSeconds(3);
            yield return DrawExpGauge(expGauge, textsNext[i], experience.ExperienceCurve[va.Level], expGain);
            while (va.NextEXP <= 0)
            {
                // Level Up
                if (actorsLevelUp[i] == false)
                {
                    actorsOldLevel[i] = va.Level;
                    actorsOldStats[i] = new EmotionStat(va.Statistique);
                    actorsOldHP[i] = va.HpMax;
                }
                actorsLevelUp[i] = true;
                animatorTextLevelUp.gameObject.SetActive(true);

                va.LevelUp();
                va.NextEXP += experience.ExperienceCurve[va.Level];
                expGain -= actorGainDeduction;
                actorGainDeduction = experience.ExperienceCurve[va.Level];

                textsLevel[i].text = va.Level.ToString();
                textsNext[i].text = experience.ExperienceCurve[va.Level].ToString();

                animatorLevelUpFeedback[i].SetTrigger("LevelUp");
                expGauge.transform.localScale = new Vector3(0, expGauge.transform.localScale.y, expGauge.transform.localScale.z);
                yield return DrawExpGauge(expGauge, textsNext[i], experience.ExperienceCurve[va.Level], expGain);
            }

            textsLevel[i].text = va.Level.ToString();
            textsNext[i].text = va.NextEXP.ToString();
            inAnimation = false;
        }

        private IEnumerator DrawExpGauge(RectTransform expGauge, TextMeshProUGUI textNext, float exp, int expGain)
        {
            int time = 100;
            Vector3 speed = new Vector3((expGain / exp)/ (float)time, 0, 0);
            while(time != 0)
            {
                time -= 1;
                textNext.text = ((int)(exp * (1 - expGauge.transform.localScale.x))).ToString();
                expGauge.transform.localScale += speed;
                if (expGauge.transform.localScale.x >= 1)
                {
                    expGauge.transform.localScale = new Vector3(0, expGauge.transform.localScale.y, expGauge.transform.localScale.z);
                    yield break;
                }
                yield return null;
            }
        }

        private IEnumerator DrawRelationGauge(RectTransform skillGauge, TextMeshProUGUI textNext, int i)
        {
            int currentRelation = voiceActors[i].Relation;
            int currentRelationMax = voiceActors[i].FriendshipLevel[0];
            actorsOldSkillLevel[i] = -1;
            for (int j = 0; j < voiceActors[i].FriendshipLevel.Length; j++)
            {
                currentRelationMax = voiceActors[i].FriendshipLevel[j] + 1; // Pour pas avoir de 0 NaN
                if (currentRelation >= voiceActors[i].FriendshipLevel[j])
                {
                    currentRelation -= voiceActors[i].FriendshipLevel[j];
                    actorsOldSkillLevel[i] = j;
                }
                else
                {
                    break;
                }
            }
            int relationGain = voiceActors[i].AddRelation(contract);

            Vector3 startPosition = new Vector3(((float)currentRelation+1 / currentRelationMax),1,1);
            Vector3 finalPosition = new Vector3(((float)((currentRelation + relationGain)+1) / currentRelationMax), 1, 1);
            if(finalPosition.x > 1)
                finalPosition = new Vector3(1, 1, 1);

            int startRelation = (currentRelationMax-1) - currentRelation;
            int finalRelation = (currentRelationMax-1) - (currentRelation + relationGain);
            if (finalRelation < 0)
                finalRelation = 0;

            textNext.text = startRelation.ToString();
            skillGauge.transform.localScale = startPosition;
            yield return new WaitForSeconds(3);
            float t = 0f;
            float rate = (60f / 120);
            while (t < 1f)
            {
                t += Time.deltaTime * rate;
                textNext.text = ((int)Mathf.Lerp(startRelation, finalRelation, t)).ToString();
                skillGauge.transform.localScale = Vector3.Lerp(startPosition, finalPosition, t);
                yield return null;
            }
            if (finalPosition.x == 1)
            {
                animatorSkillGet[i].gameObject.SetActive(true);
            }
        }















        public void ValidateNext()
        {
            if(inAnimation == true)
            {
                SkipAnimation();
                return;
            }
            else
            {
                for(int i = 0; i < actorsLevelUp.Length; i++)
                {
                    SkipAnimRelation(i);
                    if (actorsLevelUp[i] == true || actorsOldSkillLevel[i] != actorsNewSkillLevel[i])
                    {
                        ActivateLevelUp(i);
                        return;
                    }
                }
            }
            EndScreenResult();
        }

        private void SkipAnimation()
        {
            StopAllCoroutines();
            VoiceActor va;
            for (int i = 0; i < voiceActors.Count; i++)
            {
                va = voiceActors[i];
                while (va.NextEXP <= 0)
                {
                    // Level Up
                    if (actorsLevelUp[i] == false)
                    {
                        actorsOldLevel[i] = va.Level;
                        actorsOldStats[i] = new EmotionStat(va.Statistique);
                    }
                    actorsLevelUp[i] = true;
                    animatorTextLevelUp.gameObject.SetActive(true);
                    va.LevelUp();
                    va.NextEXP += experience.ExperienceCurve[va.Level];

                    animatorLevelUpFeedback[i].SetTrigger("LevelUp");
                }
                textsLevel[i].text = va.Level.ToString();
                textsNext[i].text = va.NextEXP.ToString();
                expGauge[i].transform.localScale = new Vector3((1 - (va.NextEXP / (float)experience.ExperienceCurve[va.Level])), 1, 1);
                SkipAnimRelation(i);
            }
            textCurrentLine.text = contract.CurrentLine.ToString();
            textEXP.text = (contract.ExpGain * lineDefeated).ToString();
            lineGauge.localScale = new Vector3((contract.CurrentLine / (float)contract.TotalLine), lineGauge.localScale.y, lineGauge.localScale.z);
            lineNewGauge.localScale = new Vector3((contract.CurrentLine / (float)contract.TotalLine), lineNewGauge.localScale.y, lineNewGauge.localScale.z);
            inAnimation = false;
        }



        private void SkipAnimRelation(int i)
        {
            int currentRelation = voiceActors[i].Relation;
            int currentRelationMax = voiceActors[i].FriendshipLevel[0];
            actorsNewSkillLevel[i] = -1;
            for (int j = 0; j < voiceActors[i].FriendshipLevel.Length; j++)
            {
                currentRelationMax = voiceActors[i].FriendshipLevel[j]+1;
                if (currentRelation > voiceActors[i].FriendshipLevel[j])
                {
                    currentRelation -= voiceActors[i].FriendshipLevel[j];
                    actorsNewSkillLevel[i] = j;
                }
                else
                {
                    break;
                }
            }

            textSkillRemaining[i].text = (currentRelationMax - currentRelation).ToString();
            skillGauge[i].transform.localScale = new Vector3(((currentRelation+1) / (float)currentRelationMax), 1, 1);

            if(actorsNewSkillLevel[i] != actorsOldSkillLevel[i])
                animatorSkillGet[i].gameObject.SetActive(true);
        }


        private void ActivateLevelUp(int id)
        {
            if (previousActorLevelUp == -1)
            {
                resultScreen.SetInteger("LevelUp", id);
                DrawLevelUpPanel(id, actorsLevelUp[id], actorsOldSkillLevel[id] != actorsNewSkillLevel[id]);
            }
            else
            {
                resultScreen.SetInteger("LevelUp", -1);
                resultScreen.SetTrigger("NextLevelUp");
                actorsImage[0].sprite = characterSpriteDatabase.GetCharacterData(voiceActors[previousActorLevelUp].VoiceActorID).SpriteNormal[0];
                actorsImage[1].sprite = characterSpriteDatabase.GetCharacterData(voiceActors[id].VoiceActorID).SpriteNormal[0];
                StartCoroutine(OldstatCoroutine(id, actorsLevelUp[id], actorsOldSkillLevel[id] != actorsNewSkillLevel[id]));
            }
            actorsLevelUp[id] = false;
            actorsOldSkillLevel[id] = actorsNewSkillLevel[id];
            previousActorLevelUp = id;         
        }


        private IEnumerator NewstatCoroutine(int id)
        {
            yield return new WaitForSeconds(1.2f);
            DrawNewLevelStat(id);
        }

        private IEnumerator OldstatCoroutine(int id, bool levelUp, bool skillGet)
        {
            yield return new WaitForSeconds(0.2f);
            DrawLevelUpPanel(id, levelUp, skillGet);
        }

        private void DrawLevelUpPanel(int id, bool levelUp, bool skillGet)
        {
            if (levelUp == true)
            {
                windowLevelUp.gameObject.SetActive(true);
                textOldLevel.text = actorsOldLevel[id].ToString();
                textNewLevel.text = "";
                int stat = 0;
                for (int i = 0; i < gaugeStats.Length; i++)
                {
                    stat = actorsOldStats[id].GetEmotion(i + 1);
                    gaugeStats[i].transform.localScale = new Vector3(stat / 100f, gaugeStats[i].transform.localScale.y, gaugeStats[i].transform.localScale.z);
                    newGaugeStats[i].sizeDelta = new Vector2((stat / 100f) * 500, newGaugeStats[i].sizeDelta.y);
                    textsStats[i].text = stat.ToString();
                    animatorStats[i].enabled = false;
                    textsStats[i].color = Color.white;
                    textsGain[i].transform.localScale = new Vector3(1, 0, 1);
                }
                textHPLevelUp.color = Color.white;
                textHPLevelUp.text = actorsOldHP[id].ToString();
                StartCoroutine(NewstatCoroutine(id));
            }
            else
            {
                windowLevelUp.gameObject.SetActive(false);
            }



            if (skillGet == true)
            {
                windowSkillGet.gameObject.SetActive(true);
                DrawNewSkill(id);
            }
            else
            {
                windowSkillGet.gameObject.SetActive(false);
            }
        }


        private void DrawNewLevelStat(int id)
        {
            VoiceActor va = voiceActors[id];
            textNewLevel.text = va.Level.ToString();

            int stat = 0;
            int oldStat = 0;
            for (int i = 0; i < gaugeStats.Length; i++)
            {
                oldStat = actorsOldStats[id].GetEmotion(i + 1);
                stat = va.Statistique.GetEmotion(i + 1);
                if(oldStat != stat)
                    StartCoroutine(NewStatDrawCoroutine(stat, oldStat, i));
            }
            textHPLevelUp.text = va.HpMax.ToString();
            animatorLevelUpHP.SetTrigger("Feedback");
        }

        private IEnumerator NewStatDrawCoroutine(int stat, int oldStat, int i)
        {
            yield return new WaitForSeconds(0.1f * i);
            int time = 20;
            Vector2 speed = new Vector2((((stat / 100f) * 500) - newGaugeStats[i].sizeDelta.x) / time, 0);
            while (time != 0)
            {
                time -= 1;
                newGaugeStats[i].sizeDelta += speed;
                yield return null;
            }
            textsStats[i].text = stat.ToString();
            animatorStats[i].enabled = true;
            animatorStats[i].SetTrigger("Feedback");
            textsGain[i].transform.localScale = new Vector3(1, 1, 1);
            textsGain[i].text = (stat - oldStat).ToString();
        }



        private void DrawNewSkill(int id)
        {
            SkillData skill = skillDatabase.GetSkillData(voiceActors[id].Potentials[actorsNewSkillLevel[id]]);
            textSkillGet.text = skill.SkillName;
            textSkillGetDescription.text = skill.Description;
        }



        private void EndScreenResult()
        {
            inputController.gameObject.SetActive(false);
            animatorEndStatScreen.gameObject.SetActive(true);
            animatorEndStatScreen.SetTrigger("End");
            AudioManager.Instance.StopMusic(90);
            StartCoroutine(StopYokaiDisco());
        }

        private IEnumerator StopYokaiDisco()
        {
            int time = 90;
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            SceneManager.LoadScene(nextScene);

        }




        public void AddVoxography()
        {
            // Créer une voxography pour chaque perso
            for (int i = 0; i < voiceActors.Count; i++)
            {
                voiceActors[i].CreateVoxography(contract.Name);
            }
            // Assigne les roles à la voxography créait
            for (int i = 0; i < contract.Characters.Count; i++)
            {
                voiceActors[i].AddVoxography(contract.Characters[i], (Emotion)contract.Characters[i].BestStatEmotion);
            }
        }


        #endregion

    } // ResultScreen class
	
}// #PROJECTNAME# namespace
