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
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuActorsManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        // Pour les tests

        private List<VoiceActor> actorsList = new List<VoiceActor>();


        [Header("Prefab")]
        [SerializeField]
        private RectTransform buttonListTransform;
        [SerializeField]
        private ButtonVoiceActor prefabButtonVoiceActor;
        [SerializeField]
        private Animator animatorSelection;
        [SerializeField]
        private TextMeshProUGUI textMeshSelection;
        [SerializeField]
        private Image imageButtonSelection;

        [Header("Info Panel")]
        [SerializeField]
        private Animator statImageActorAnimator;
        [SerializeField]
        private Image statImageActor;
        [SerializeField]
        private Image statOutlineImageActor;
        [SerializeField]
        private TextMeshProUGUI statNameStylishActor;
        [SerializeField]
        private TextMeshProUGUI statNameActor;
        [SerializeField]
        private TextMeshProUGUI statLevelActor;
        [SerializeField]
        private TextMeshProUGUI statLevelNext;
        [SerializeField]
        private TextMeshProUGUI statLevelActorSelection;
        [SerializeField]
        private TextMeshProUGUI statFanActor;
        [SerializeField]
        private TextMeshProUGUI statPriceActor;
        [SerializeField]
        private TextMeshProUGUI statCurrentHpActor;
        [SerializeField]
        private TextMeshProUGUI statMaxHpActor;
        [SerializeField]
        private TextMeshProUGUI textUnavailable;
        [SerializeField]
        private TextMeshProUGUI textUnavailableReason;
        [SerializeField]
        private RectTransform statHpGauge;
        [SerializeField]
        private RectTransform statTimbre;
        [SerializeField]
        private TextMeshProUGUI[] statSkillsActor;
        [SerializeField]
        private TextMeshProUGUI statSkillActorDescription;

        [Header("Stats Panel")]
        [SerializeField]
        Image[] imageStatIcon;
        [SerializeField]
        Image[] feedbackBestStat;

        [Header("Stats Panel 1")]
        [InfoBox("Joie > Tristesse > Dégoût > Colère > Surprise > Douceur > Peur > Confiance")]
        [SerializeField]
        GameObject representation1;
        [SerializeField]
        TextMeshProUGUI[] textStatsActor;
        [SerializeField]
        RectTransform[] jaugeStatsActor;
        [SerializeField]
        TextMeshProUGUI[] textStatsRole;
        [SerializeField]
        RectTransform[] jaugeStatsRole;

        [Header("Stats Panel 2")]
        [InfoBox("Joie > Tristesse > Dégoût > Colère > Surprise > Douceur > Peur > Confiance")]
        [SerializeField]
        GameObject representation2;
        [SerializeField]
        TextMeshProUGUI[] textStatsActor2;
        [SerializeField]
        RectTransform[] jaugeStatsActor2;
        [SerializeField]
        TextMeshProUGUI[] textStatsRole2;
        [SerializeField]
        RectTransform[] jaugeStatsRole2;
        [SerializeField]
        Image[] jaugeEmpty;

        [Header("Colors")]
        float thresholdHealth = 0.5f;
        [SerializeField]
        Color colorNameNormal;
        [SerializeField]
        Color colorNameTired;
        [SerializeField]
        Color colorNameUnavailable;

        [Header("Audition")]
        [SerializeField]
        GameObject auditionIndication;
        [SerializeField]
        TextMeshProUGUI textContractName;
        [SerializeField]
        TextMeshProUGUI textRoleName;
        [SerializeField]
        TextMeshProUGUI textRoleFan;
        [SerializeField]
        TextMeshProUGUI textRoleLine;
        [SerializeField]
        TextMeshProUGUI textRoleCostEstimate;
        [SerializeField]
        Image imageContractIcon;
        [SerializeField]
        MenuContractMoney menuContractMoney;

        [Header("Feedbacks")]
        [SerializeField]
        Animator animatorFeedbackButtonAudition;

        // ===========================
        // Menu Input
        [Header("Menu Input")]
        [SerializeField]
        private InputController inputController;
        [SerializeField]
        private int scrollSize = 11;
        [SerializeField]
        private int buttonSize = 85; // à voir si on peut pas automatiser
        [SerializeField]
        private int timeBeforeRepeat = 10;
        [SerializeField]
        private int repeatInterval = 3;

        private int currentTimeBeforeRepeat = -1;
        private int currentRepeatInterval = -1;
        private int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private int indexActorLimit = 0;
        private int indexActorSelected = 0;
        private IEnumerator coroutineScroll = null;
        // Menu Input
        // ===========================


        [Header("MenuManagers")]
        [SerializeField]
        CameraBureau cameraManager;
        [SerializeField]
        MenuContractPreparation menuContractPreparation;
        [SerializeField]
        MenuActorsAudition menuActorsAudition;
        [SerializeField]
        SortManager sortManager;

        private List<ButtonVoiceActor> buttonsActors = new List<ButtonVoiceActor>();


        private bool auditionMode = false;
        private Role auditionRole = null;

        private bool gaugeCursorMode = false;

        private bool firstDraw = true;

        int actorUnavailable = 0;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetActorUnavailable()
        {
            return actorUnavailable;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */



        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            //rectTransformSelection = animatorSelection.GetComponent<RectTransform>();
            indexActorLimit = scrollSize;
        }

        public void SetListActors(List<VoiceActor> list)
        {
            actorsList = list;
            CreateActorsButtonList();
        }

        [ContextMenu("LevelUpCheat")]
        public void DebugLevelUp()
        {
            actorsList[indexActorSelected].LevelUp();
            DrawActorStat(actorsList[indexActorSelected]);
        }

        private void OnEnable()
        {
            if (auditionMode == true)
            {
                auditionIndication.SetActive(true);
                imageButtonSelection.gameObject.SetActive(true);
                statLevelActorSelection.gameObject.SetActive(false);
                menuContractMoney.ActivateEstimate();
                DrawGaugeRoleInfo();
            }
            else
            {
                menuContractMoney.DesactivateEstimate();
                statLevelActorSelection.gameObject.SetActive(true);
                imageButtonSelection.gameObject.SetActive(false);
                auditionIndication.SetActive(false);
            }



            if (indexActorSelected >= actorsList.Count || indexActorSelected == -1)
            {
                indexActorSelected = 0;
            }
            //buttonsActors[indexActorSelected].SelectButton();
            DrawActorStat(actorsList[indexActorSelected]);
            inputController.gameObject.SetActive(true);

        }

        private void OnDisable()
        {
            auditionIndication.SetActive(false);
        }


        private void CreateActorsButtonList()
        {
            for(int i = 0; i < actorsList.Count; i++)
            {
                buttonsActors.Add(Instantiate(prefabButtonVoiceActor, buttonListTransform));
                buttonsActors[i].DrawActor(actorsList[i].Name, actorsList[i].Level, (float) actorsList[i].Hp / actorsList[i].HpMax, actorsList[i].Availability, actorsList[i].SpriteSheets.SpriteIcon);
                if(actorsList[i].Availability == false)
                {
                    actorUnavailable += 1;
                }
            }
            if (indexActorSelected < actorsList.Count)
            {
                DrawActorStat(actorsList[indexActorSelected]);
            }
            buttonListTransform.anchoredPosition = new Vector2(0, 0);
        }

        public void CreateAuditionButton()
        {
            buttonsActors.Add(Instantiate(prefabButtonVoiceActor, buttonListTransform));
            buttonsActors[buttonsActors.Count - 1].DrawAudition();
        }

        public void DestroyAuditionButton()
        {
            Destroy(buttonsActors[buttonsActors.Count-1].gameObject);
            buttonsActors.RemoveAt(buttonsActors.Count - 1);
        }


        public void DestroyButtonList()
        {
            firstDraw = true;
            for (int i = 0; i < buttonsActors.Count; i++)
            {
                Destroy(buttonsActors[i].gameObject);
            }
            buttonsActors.Clear();
            CreateActorsButtonList();
        }

        public void RedrawActorsButton()
        {
            for (int i = 0; i < actorsList.Count; i++)
            {
                buttonsActors[i].DrawActor(actorsList[i].Name, actorsList[i].Level, (float)actorsList[i].Hp / actorsList[i].HpMax, actorsList[i].Availability, actorsList[i].SpriteSheets.SpriteIcon);
            }
        }

        private void DrawActorStat(VoiceActor actor)
        {
            animatorSelection.transform.position = buttonsActors[indexActorSelected].transform.position;
            animatorSelection.SetTrigger("Active");

            if (actor == null)
            {
                SetActorGaugeToZero();
                textMeshSelection.text = "Audition";
                textMeshSelection.color = colorNameNormal;
                menuActorsAudition.DrawAuditionPanel(true);
                imageButtonSelection.gameObject.SetActive(true);
                statLevelActorSelection.gameObject.SetActive(false);
                textUnavailable.gameObject.SetActive(false);
                menuContractMoney.ActivateEstimate();
                menuContractMoney.DrawEstimate(-menuActorsAudition.GetBudget());
                return;
            }
            else
            {
                menuActorsAudition.DrawAuditionPanel(false);
            }

            textMeshSelection.text = actor.Name;

            if (actor.Availability == true)
            {
                statImageActorAnimator.SetTrigger("AppearAnim");
                textUnavailable.gameObject.SetActive(false);
                if(auditionMode == true)
                {
                    imageButtonSelection.gameObject.SetActive(true);
                    statLevelActorSelection.gameObject.SetActive(false);
                    CalculateAudtionEstimate();
                }
            }
            else
            {
                statImageActorAnimator.SetTrigger("Unavailable");
                textUnavailable.gameObject.SetActive(true);
                textUnavailableReason.text = SetActorAbsentReason(actor.ActorMentalState);
                imageButtonSelection.gameObject.SetActive(false);
                statLevelActorSelection.gameObject.SetActive(true);
                menuContractMoney.DesactivateEstimate();
            }







            statImageActor.sprite = actor.ActorSprite;
            statOutlineImageActor.sprite = actor.ActorSprite;
            statImageActor.SetNativeSize();
            statOutlineImageActor.SetNativeSize();

            statTimbre.anchorMin = new Vector2((actor.Timbre.x + 10) / 20f, 0);
            statTimbre.anchorMax = new Vector2((actor.Timbre.y + 10) / 20f, 1);
            statTimbre.anchoredPosition = Vector3.zero;

            statNameStylishActor.text = actor.Name;
            statNameActor.text = actor.Name;
            statNameActor.transform.eulerAngles = new Vector3(0, 0, Random.Range(-2, 2));
            statNameStylishActor.transform.eulerAngles = statNameActor.transform.eulerAngles;

            statLevelActor.text = actor.Level.ToString();
            statLevelNext.text = actor.NextEXP.ToString();
            statLevelActorSelection.text = "Lv. " + actor.Level.ToString();
            statFanActor.text = actor.Fan.ToString();
            statPriceActor.text = actor.Price.ToString();

            statCurrentHpActor.text = actor.Hp.ToString();
            statMaxHpActor.text = actor.HpMax.ToString();
            statHpGauge.transform.localScale = new Vector3((actor.Hp / (float)actor.HpMax), statHpGauge.transform.localScale.y, statHpGauge.transform.localScale.z);


            if (actor.Availability == false)
            {
                statNameActor.color = colorNameUnavailable;
                textMeshSelection.color = colorNameUnavailable;
            }
            else if (statHpGauge.transform.localScale.x <= thresholdHealth)
            {
                statNameActor.color = colorNameTired;
                textMeshSelection.color = colorNameTired;
            }
            else
            {
                statNameActor.color = colorNameNormal;
                textMeshSelection.color = colorNameNormal;
            }

            for (int i = 0; i < statSkillsActor.Length; i++)
            {
                if (i < actor.Potentials.Length)
                    statSkillsActor[i].text = actor.Potentials[i].SkillName;
                else
                    statSkillsActor[i].text = " ";
            }

            if (actor.Potentials.Length != 0)
                statSkillActorDescription.text = actor.Potentials[0].Description;

            if (firstDraw == true)
                firstDraw = false;
            else
                DrawGaugeInfo(actor);

        }


        private string SetActorAbsentReason(VoiceActorState voiceActorState)
        {
            switch(voiceActorState)
            {
                case VoiceActorState.Absent:
                    return "Absent";
                case VoiceActorState.Dead:
                    return "Hopital";
                case VoiceActorState.Tired:
                    return "Repos";
            }
            return null;
        }



        private void DrawGaugeInfo(VoiceActor actor)
        {
            actor.RoleDefense = 8;
            StopAllCoroutines(); // sauf le scroll 
            for (int i = 0; i < textStatsActor.Length; i++)
            {
                int currentStatActor = actor.Statistique.GetEmotion(i+1);
                /*switch (i)
                {
                    case 0:
                        currentStatActor = actor.Statistique.Joy;
                        break;
                    case 1:
                        currentStatActor = actor.Statistique.Sadness;
                        break;
                    case 2:
                        currentStatActor = actor.Statistique.Disgust;
                        break;
                    case 3:
                        currentStatActor = actor.Statistique.Anger;
                        break;
                    case 4:
                        currentStatActor = actor.Statistique.Surprise;
                        break;
                    case 5:
                        currentStatActor = actor.Statistique.Sweetness;
                        break;
                    case 6:
                        currentStatActor = actor.Statistique.Fear;
                        break;
                    case 7:
                        currentStatActor = actor.Statistique.Trust;
                        break;
                }*/


                if (gaugeCursorMode == false)
                {

                    textStatsActor[i].text = currentStatActor.ToString();
                    StartCoroutine(GaugeCoroutine(jaugeStatsActor[i], currentStatActor / 100f));
                }
                else
                {

                    textStatsActor2[i].text = currentStatActor.ToString();
                    StartCoroutine(GaugeCoroutine(jaugeStatsActor2[i], currentStatActor / 100f));
                }




                if (auditionMode == true)
                {
                    int currentStatRole = 0;
                    switch (i)
                    {
                        case 0:
                            currentStatRole = auditionRole.CharacterStat.Joy;
                            break;
                        case 1:
                            currentStatRole = auditionRole.CharacterStat.Sadness;
                            break;
                        case 2:
                            currentStatRole = auditionRole.CharacterStat.Disgust;
                            break;
                        case 3:
                            currentStatRole = auditionRole.CharacterStat.Anger;
                            break;
                        case 4:
                            currentStatRole = auditionRole.CharacterStat.Surprise;
                            break;
                        case 5:
                            currentStatRole = auditionRole.CharacterStat.Sweetness;
                            break;
                        case 6:
                            currentStatRole = auditionRole.CharacterStat.Fear;
                            break;
                        case 7:
                            currentStatRole = auditionRole.CharacterStat.Trust;
                            break;
                    }

                    if (currentStatActor >= currentStatRole)
                    {
                        feedbackBestStat[i].color = new Color(1, 1, 0, 0.5f);
                        imageStatIcon[i].color = new Color(1, 1, 0);
                        jaugeEmpty[i].color = new Color(0, 0, 0, 0.7f);
                    }
                    else
                    {
                        actor.RoleDefense -= 1;
                        feedbackBestStat[i].color = new Color(1, 1, 1, 0.5f);
                        imageStatIcon[i].color = new Color(1, 1, 1);
                        jaugeEmpty[i].color = new Color(0, 0, 0, 0.4f);
                    }
                }

            }
        }

        private void DrawGaugeRoleInfo()
        {
            int currentStatRole = 0;
            for (int i = 0; i < textStatsActor.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        currentStatRole = auditionRole.CharacterStat.Joy;
                        break;
                    case 1:
                        currentStatRole = auditionRole.CharacterStat.Sadness;
                        break;
                    case 2:
                        currentStatRole = auditionRole.CharacterStat.Disgust;
                        break;
                    case 3:
                        currentStatRole = auditionRole.CharacterStat.Anger;
                        break;
                    case 4:
                        currentStatRole = auditionRole.CharacterStat.Surprise;
                        break;
                    case 5:
                        currentStatRole = auditionRole.CharacterStat.Sweetness;
                        break;
                    case 6:
                        currentStatRole = auditionRole.CharacterStat.Fear;
                        break;
                    case 7:
                        currentStatRole = auditionRole.CharacterStat.Trust;
                        break;
                }

                if (gaugeCursorMode == false)
                {
                    textStatsRole[i].text = currentStatRole.ToString();
                    jaugeStatsRole[i].transform.localScale = new Vector3(currentStatRole / 100f, 1, 1);
                }
                else
                {
                    jaugeStatsRole2[i].sizeDelta = new Vector2((currentStatRole / 100f) * 500, 0);
                }

                if(currentStatRole == auditionRole.BestStat || currentStatRole == auditionRole.SecondBestStat)
                {
                    feedbackBestStat[i].gameObject.SetActive(true);
                }
                else
                {
                    feedbackBestStat[i].gameObject.SetActive(false);
                }
            }
        }


        private IEnumerator GaugeCoroutine(RectTransform jaugeStat, float target, int time = 10)
        {
            if (jaugeStat.transform.localScale.x == target)
            {
                yield break;
            }

            Vector3 speed = new Vector3((target - jaugeStat.transform.localScale.x) / time, 0, 0);
            while (time != 0)
            {
                time -= 1;
                jaugeStat.transform.localScale += speed;
                yield return null;
            }
        }




        public IEnumerator GachaEffect()
        {
            int time = 10; // l même time que gaugeCourtine sinon bug
            while (true)
            {
                if (gaugeCursorMode == false)
                {
                    for (int i = 0; i < textStatsActor.Length; i++)
                    {
                        textStatsActor[i].text = Random.Range(1, 99).ToString();
                        if (time == 10)
                            StartCoroutine(GaugeCoroutine(jaugeStatsActor[i], Random.Range(1, 99) / 100f));
                    }
                }
                else
                {
                    for (int i = 0; i < textStatsActor.Length; i++)
                    {
                        textStatsActor2[i].text = Random.Range(1, 99).ToString();
                        if (time == 10)
                            StartCoroutine(GaugeCoroutine(jaugeStatsActor2[i], Random.Range(1, 99) / 100f));
                    }
                }
                time -= 1;
                if (time == 0)
                    time = 10;
                yield return null;
            }
        }

        public void StopGachaEffect(VoiceActor va)
        {
            StopAllCoroutines();
            DrawGaugeInfo(va);
        }


        private void SetActorGaugeToZero()
        {
            if (gaugeCursorMode == false)
            {
                for (int i = 0; i < textStatsActor.Length; i++)
                {
                    textStatsActor[i].text = "0";
                    StartCoroutine(GaugeCoroutine(jaugeStatsActor[i], 0 / 100f));
                    feedbackBestStat[i].color = new Color(1, 1, 1, 0.5f);
                    imageStatIcon[i].color = new Color(1, 1, 1);
                    jaugeEmpty[i].color = new Color(0, 0, 0, 0.4f);
                }

            }
            else
            {
                for (int i = 0; i < textStatsActor.Length; i++)
                {
                    textStatsActor2[i].text = "0";
                    StartCoroutine(GaugeCoroutine(jaugeStatsActor2[i], 0 / 100f));
                    feedbackBestStat[i].color = new Color(1, 1, 1, 0.5f);
                    imageStatIcon[i].color = new Color(1, 1, 1);
                    jaugeEmpty[i].color = new Color(0, 0, 0, 0.4f);
                }
            }
        }

        public void RedrawButtonAfterAudition()
        {
            DestroyAuditionButton();
            DestroyButtonList();
            CreateAuditionButton();
            string newActorName = menuActorsAudition.GetAuditionName();
            for(int i = 0; i < actorsList.Count; i++)
            {
                if(actorsList[i].Name == newActorName)
                {
                    indexActorSelected = i;
                    textMeshSelection.text = newActorName;
                    StartCoroutine(SelectionCoroutine());
                    return;
                }
            }
        }

        private IEnumerator SelectionCoroutine()
        {
            yield return new WaitForEndOfFrame();
            DrawActorStat(actorsList[indexActorSelected]);
        }




        // =======================================================================
        // Audition

        public void AuditionMode(bool b, Role roleToAudition)
        {
            if (auditionMode == false && b == true)
            {
                ChangeStatForAudition(false);
                CreateAuditionButton();
            }
            else if (auditionMode == true && b == false)
            {
                ChangeStatForAudition(true);
                DestroyAuditionButton();
            }

            auditionMode = b;

            if (b == true)
            {
                auditionRole = roleToAudition;
                DrawAuditionInfo(roleToAudition);
            }
        }

        private void ChangeStatForAudition(bool hideRole)
        {
            if (hideRole == false)
            {
                for (int i = 0; i < textStatsActor.Length; i++)
                {
                    textStatsActor[i].rectTransform.anchoredPosition = new Vector2(textStatsActor[i].rectTransform.anchoredPosition.x, 10);
                    textStatsRole[i].transform.localScale = new Vector3(1, 1, 1);
                    jaugeStatsRole2[i].transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                for (int i = 0; i < textStatsActor.Length; i++)
                {
                    feedbackBestStat[i].gameObject.SetActive(false);
                    textStatsActor[i].rectTransform.anchoredPosition = new Vector2(textStatsActor[i].rectTransform.anchoredPosition.x, -10);
                    textStatsRole[i].transform.localScale = new Vector3(1, 0, 1);
                    jaugeStatsRole2[i].transform.localScale = new Vector3(1, 0, 1);
                }
            }         
        }

        private void DrawAuditionInfo(Role role)
        {
            textRoleName.text = role.Name;
            textRoleFan.text = role.Fan.ToString();
            textRoleLine.text = role.Line.ToString();
            if(indexActorSelected != buttonsActors.Count - 1)
                CalculateAudtionEstimate();
        }

        public void DrawAuditionTitle(string title)
        {
            textContractName.text = title;
        }
        public void DrawAuditionIcon(Sprite icon)
        {
            imageContractIcon.sprite = icon;
        }

        private void CalculateAudtionEstimate()
        {
            menuContractMoney.ActivateEstimate();
            menuContractMoney.DrawEstimate(auditionRole.Line * actorsList[indexActorSelected].Price);
        }

        public void DrawBudgetAudition()
        {
            menuContractMoney.DrawEstimate(-menuActorsAudition.GetBudget());
        }

        public void Audition()
        {
            menuActorsAudition.Audition(auditionRole);
        }







        // ==================================================================================
        // Commandes

        public void Validate()
        {
            if (auditionMode == true && indexActorSelected == buttonsActors.Count - 1)
            {
                if(menuActorsAudition.CheckCost() == true)
                {
                    StartCoroutine(GachaEffect());
                    menuActorsAudition.AnimAudition(auditionRole);
                    menuContractMoney.AddSalaryFast(-menuActorsAudition.GetBudget());
                    menuContractMoney.AuditionFeedback();
                }
                return;
            }

            if (auditionMode == false)
                return;

            if (actorsList[indexActorSelected].Availability == false)
                return;

            animatorFeedbackButtonAudition.SetTrigger("Feedback");
            statImageActorAnimator.SetTrigger("Feedback");
            animatorSelection.SetTrigger("Validate");
            StartCoroutine(WaitFeedback());

        }

        private IEnumerator WaitFeedback()
        {
            inputController.gameObject.SetActive(false);
            int time = 20;
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            cameraManager.MoveToCamera(2);
            menuContractPreparation.SetActor(actorsList[indexActorSelected]);
        }

        public void Return()
        {
            cameraManager.MoveToCamera(2);
        }

        public void ChangeRepresentation()
        {
            gaugeCursorMode = !gaugeCursorMode;
            if (gaugeCursorMode == true)
            {
                representation1.SetActive(false);
                representation2.SetActive(true);
            }
            else
            {
                representation1.SetActive(true);
                representation2.SetActive(false);
            }
            DrawActorStat(actorsList[indexActorSelected]);
            if (auditionMode == true)
                DrawGaugeRoleInfo();
        }

        public void IncreaseAuditionBudget()
        {
            if (auditionMode == true && indexActorSelected == buttonsActors.Count - 1)
            {
                menuActorsAudition.IncreaseBudget();
            }
        }

        public void ReduceAuditionBudget()
        {
            if (auditionMode == true && indexActorSelected == buttonsActors.Count - 1)
            {
                menuActorsAudition.ReduceBudget();
            }
        }


        public void ShowSortMenu()
        {
            sortManager.ShowSortPanel();
            inputController.gameObject.SetActive(false);
            animatorSelection.gameObject.SetActive(false);
        }

        public void SortActorsList()
        {
            actorsList = sortManager.SortActorsList(actorsList);
            RedrawActorsButton();
            indexActorSelected = 0;
            MoveScrollRect();
            DrawActorStat(actorsList[indexActorSelected]);
            inputController.gameObject.SetActive(true);
            animatorSelection.gameObject.SetActive(true);
        }
        public void SortActorsListFast()
        {
            actorsList = sortManager.SortActorsList(actorsList);
            RedrawActorsButton();
            indexActorSelected = 0;
            DrawActorStat(actorsList[indexActorSelected]);

            indexActorLimit = scrollSize;
            MoveScrollRect();
        }



        public void SelectActorUp()
        {
            if(lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            indexActorSelected -= 1;
            if(indexActorSelected <= -1)
            {
                indexActorSelected = buttonsActors.Count-1;
            }
            if (auditionMode == true && indexActorSelected == buttonsActors.Count - 1)
            {
                DrawActorStat(null);
            }
            else
            {
                DrawActorStat(actorsList[indexActorSelected]);
            }

            MoveScrollRect();
        }

        public void SelectActorDown()
        {
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            indexActorSelected += 1;
            if (indexActorSelected >= buttonsActors.Count)
            {
                indexActorSelected = 0;
            }
            if (auditionMode == true && indexActorSelected == buttonsActors.Count - 1)
            {
                DrawActorStat(null);
            }
            else
            {
                DrawActorStat(actorsList[indexActorSelected]);
            }
            

            MoveScrollRect();

        }

        // ==================================================================================
        // Check si on peut repeter l'input
        private bool CheckRepeat()
        {
            if (currentRepeatInterval == -1)
            {
                if (currentTimeBeforeRepeat == -1)
                {
                    currentTimeBeforeRepeat = timeBeforeRepeat;
                    return true;
                }
                else if (currentTimeBeforeRepeat == 0)
                {
                    currentRepeatInterval = repeatInterval;
                }
                else
                {
                    currentTimeBeforeRepeat -= 1;
                }
            }
            else if(currentRepeatInterval == 0)
            {
                currentRepeatInterval = repeatInterval;
                return true;
            }
            else
            {
                currentRepeatInterval -= 1;
            }
            return false;
        }

        public void StopRepeat()
        {
            currentRepeatInterval = -1;
            currentTimeBeforeRepeat = -1;
        }


        private void MoveScrollRect()
        {

            if (indexActorSelected > indexActorLimit)
            {
                indexActorLimit = indexActorSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                if (coroutineScroll != null)
                {
                    StopCoroutine(coroutineScroll);
                }
                StartCoroutine(coroutineScroll);
            }
            else if (indexActorSelected < indexActorLimit - scrollSize+1)
            {
                indexActorLimit = indexActorSelected + scrollSize-1;
                coroutineScroll = MoveScrollRectCoroutine();
                if (coroutineScroll != null)
                {
                    StopCoroutine(coroutineScroll);
                }
                StartCoroutine(coroutineScroll);
            }
            
        }

        private IEnumerator MoveScrollRectCoroutine()
        {
            float time = 10f;
            int ratio = indexActorLimit - scrollSize;
            float speed = (buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time;
            while (time != 0)
            {
                animatorSelection.transform.position = buttonsActors[indexActorSelected].transform.position;
                buttonListTransform.anchoredPosition -= new Vector2(0, speed);
                time -= 1;
                yield return null;
            }
            animatorSelection.transform.position = buttonsActors[indexActorSelected].transform.position;

        }
        
        #endregion
		
	} // MenuActorsManager class
	
}// #PROJECTNAME# namespace
