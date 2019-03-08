﻿/*****************************************************************
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
        [SerializeField]
        private List<VoiceActorData> debugList;


        private List<VoiceActor> actorsList = new List<VoiceActor>();

        private List<VoiceActor> actorsSortList = new List<VoiceActor>();

        [Header("Prefab")]
        [SerializeField]
        private RectTransform buttonListTransform;
        [SerializeField]
        private ButtonVoiceActor prefabButtonVoiceActor;
        [SerializeField]
        private Animator animatorSelection;
        [SerializeField]
        private TextMeshProUGUI textMeshSelection;

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
        private TextMeshProUGUI statFanActor;
        [SerializeField]
        private TextMeshProUGUI statPriceActor;
        [SerializeField]
        private RectTransform statTimbre;

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

        [Header("Tri")]
        [SerializeField]
        private bool croissant = true;

        // ===========================
        // Menu Input
        [Header("Menu Input")]
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
        public int indexActorLimit = 0;
        public int indexActorSelected = 0;
        private IEnumerator coroutineScroll = null;
        // Menu Input
        // ===========================


        [Header("MenuManagers")]
        [SerializeField]
        CameraBureau cameraManager;
        [SerializeField]
        MenuContractPreparation menuContractPreparation;

        private List<ButtonVoiceActor> buttonsActors = new List<ButtonVoiceActor>();

        private bool auditionMode = false;
        private Role auditionRole = null;

        private bool gaugeCursorMode = false;





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



        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            indexActorLimit = scrollSize;
            //CreateFromDebugList();
            //CreateActorsButtonList();
        }

        public void SetListActors(List<VoiceActor> list)
        {
            actorsList = list;
            CreateActorsButtonList();
        }


        private void OnEnable()
        {
            if (auditionMode == true)
            {
                auditionIndication.SetActive(true);
                DrawGaugeRoleInfo();
            }
            else
            {
                auditionIndication.SetActive(false);
            }
                
        }

        private void OnDisable()
        {
            auditionIndication.SetActive(false);
        }

        private void CreateFromDebugList()
        {
            if(debugList != null)
            {
                for(int i = 0; i < debugList.Count; i++)
                {
                    actorsList.Add(new VoiceActor(debugList[i]));
                }
            }
        }

        private void CreateActorsButtonList()
        {
            for(int i = 0; i < actorsList.Count; i++)
            {
                buttonsActors.Add(Instantiate(prefabButtonVoiceActor, buttonListTransform));
                buttonsActors[i].DrawActor(actorsList[i].Name, actorsList[i].Level);
            }
            if (indexActorSelected < actorsList.Count)
                StartCoroutine(WaitEndOfFrame());

            buttonListTransform.anchoredPosition = new Vector2(0, 0);
        }

        private IEnumerator WaitEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            DrawActorStat(actorsList[indexActorSelected]);
            buttonsActors[indexActorSelected].SelectButton();
            this.gameObject.SetActive(false);
        }

        private void DrawActorStat(VoiceActor actor)
        {
            animatorSelection.transform.position = buttonsActors[indexActorSelected].transform.position;
            textMeshSelection.text = actor.Name;
            animatorSelection.SetTrigger("Active");

            statImageActorAnimator.SetTrigger("AppearAnim");          
            statImageActor.sprite = actor.ActorSprite;
            statOutlineImageActor.sprite = actor.ActorSprite;

            statTimbre.anchorMin = new Vector2((actor.Timbre.x + 10) / 20f, 0);
            statTimbre.anchorMax = new Vector2((actor.Timbre.y + 10) / 20f, 1);
            statTimbre.anchoredPosition = Vector3.zero;

            statNameStylishActor.text = actor.Name;
            statNameActor.text = actor.Name;
            statNameActor.transform.eulerAngles = new Vector3(0, 0, Random.Range(-3, 3));
            statNameStylishActor.transform.eulerAngles = statNameActor.transform.eulerAngles;

            statLevelActor.text = actor.Level.ToString();
            statFanActor.text = actor.Fan.ToString();
            statPriceActor.text = actor.Price.ToString();

            DrawGaugeInfo(actor);

            if(auditionMode == true)
                CalculateAudtionEstimate();
        }



        private void DrawGaugeInfo(VoiceActor actor)
        {
            StopAllCoroutines();
            for (int i = 0; i < textStatsActor.Length; i++)
            {
                int currentStatActor = 0;
                switch (i)
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
                }


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
            /*int time2 = time/2;
            Vector3 speed = new Vector3((1 - jaugeStat.transform.localScale.x) / Random.Range(time/2,time*1.5f), 0, 0);
            while (time2 != 0)
            {
                time2 -= 1;
                jaugeStat.transform.localScale += speed;
                yield return null;
            }*/

            Vector3 speed = new Vector3((target - jaugeStat.transform.localScale.x) / time, 0, 0);
            while (time != 0)
            {
                time -= 1;
                jaugeStat.transform.localScale += speed;
                yield return null;
            }
        }






        // =======================================================================
        // Audition

        public void AuditionMode(bool b, Role roleToAudition)
        {
            if (auditionMode == false && b == true)
                ChangeStatForAudition(false);
            else if (auditionMode == true && b == false)
                ChangeStatForAudition(true);

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
            CalculateAudtionEstimate();
        }

        public void DrawAuditionTitle(string title)
        {
            textContractName.text = title;
        }

        private void CalculateAudtionEstimate()
        {
            textRoleCostEstimate.text = (auditionRole.Line * actorsList[indexActorSelected].Price).ToString();
        }






        // ==================================================================================
        // Commandes

        public void Validate()
        {
            if (auditionMode == false)
                return;

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

        public void SelectActorUp()
        {
            if(lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            buttonsActors[indexActorSelected].UnSelectButton();
            indexActorSelected -= 1;
            if(indexActorSelected <= -1)
            {
                indexActorSelected = buttonsActors.Count-1;
            }

            buttonsActors[indexActorSelected].SelectButton();
            DrawActorStat(actorsList[indexActorSelected]);
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

            buttonsActors[indexActorSelected].UnSelectButton();
            indexActorSelected += 1;
            if (indexActorSelected >= buttonsActors.Count)
            {
                indexActorSelected = 0;
            }

            buttonsActors[indexActorSelected].SelectButton();
            DrawActorStat(actorsList[indexActorSelected]);
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
            if(coroutineScroll != null)
            {
                StopCoroutine(coroutineScroll);
            }
            if (indexActorSelected > indexActorLimit)
            {
                indexActorLimit = indexActorSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            else if (indexActorSelected < indexActorLimit - scrollSize)
            {
                indexActorLimit = indexActorSelected + scrollSize;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            
        }

        private IEnumerator MoveScrollRectCoroutine()
        {
            int time = 10;
            int ratio = indexActorLimit - scrollSize;
            //if (ratio != 0) {
            float speed = (buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time;
            while (time != 0)
            {
                buttonListTransform.anchoredPosition -= new Vector2(0, speed);
                time -= 1;
                yield return null;
            }
            //}
            /*if(indexActorLimit == scrollSize)
            {

            }*/
            
        }
        
        #endregion
		
	} // MenuActorsManager class
	
}// #PROJECTNAME# namespace
