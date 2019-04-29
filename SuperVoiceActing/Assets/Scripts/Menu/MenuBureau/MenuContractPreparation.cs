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
	public class MenuContractPreparation : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Save")]
        [SerializeField]
        PlayerData playerData;

        [Header("Info")]
        [SerializeField]
        TextMeshProUGUI textContractTitle;
        [SerializeField]
        TextMeshProUGUI textContractSalaire;
        [SerializeField]
        TextMeshProUGUI textContractTotalCost;
        [SerializeField]
        TextMeshProUGUI textContractLine;
        [SerializeField]
        TextMeshProUGUI textContractLineMax;
        [SerializeField]
        TextMeshProUGUI textContractMixage;
        [SerializeField]
        TextMeshProUGUI textContractMixageMax;

        [Header("InfoRole")]
        [SerializeField]
        TextMeshProUGUI textRoleName;
        [SerializeField]
        TextMeshProUGUI textRoleFan;
        [SerializeField]
        TextMeshProUGUI textRoleLine;
        [SerializeField]
        TextMeshProUGUI textRoleCadence;

        [Header("InfoActeur")]
        [SerializeField]
        GameObject panelActor;
        [SerializeField]
        TextMeshProUGUI textActorName;
        [SerializeField]
        TextMeshProUGUI textActorFan;
        [SerializeField]
        TextMeshProUGUI textActorCost;
        [SerializeField]
        TextMeshProUGUI textActorCadence;
        [SerializeField]
        TextMeshProUGUI textActorLevel;
        [SerializeField]
        Image imageActorSprite;

        [Header("PanelFinal")]
        [InfoBox("Joie > Tristesse > Dégoût > Colère > Surprise > Douceur > Peur > Confiance")]
        [SerializeField]
        TextMeshProUGUI[] textStatsActor;
        [SerializeField]
        RectTransform[] jaugeStatsActor;

        [SerializeField]
        TextMeshProUGUI[] textStatsRole;
        [SerializeField]
        RectTransform[] jaugeStatsRole;

        [Header("InSession")]
        [SerializeField]
        GameObject inSessionObject;
        [SerializeField]
        MenuTransitionDoublage menuTransitionDoublage;

        [Header("Prefabs")]
        [SerializeField]
        ButtonPreparationRole[] listButtonRoles;

        [Header("MenuManagers")]
        [SerializeField]
        CameraBureau cameraManager;
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContratManager menuContractManager;
        [SerializeField]
        MenuActorsManager menuActorsManager;

        [Header("Feedbacks")]
        [SerializeField]
        Animator animatorFeedbackSpriteAudition;



        // ===========================
        // Menu Input (Sans doute mettre dans une classe parent mais flemme)
        [Header("Menu Input")]
        [SerializeField]
        private int scrollSize = 8;
        [SerializeField]
        private int buttonSize = 85; // à voir si on peut pas automatiser
        [SerializeField]
        private int timeBeforeRepeat = 10;
        [SerializeField]
        private int repeatInterval = 3;

        private int currentTimeBeforeRepeat = -1;
        private int currentRepeatInterval = -1;
        private int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private int indexLimit = 0;

        private IEnumerator coroutineScroll = null;
        // ===========================


        private Contract currentContract = null;
        private int indexSelected = 0;
        private bool inSessionPossible = false;

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

        private void OnEnable()
        {
            animatorMenu.SetBool("Appear", true);
            animatorMenu.gameObject.SetActive(true);
            //listButtonRoles[0].SelectButton();
            //indexSelected = 0;
        }

        public void SwitchToMenuContractManager()
        {
            menuActorsManager.AuditionMode(false, null);
            StopAllCoroutines();
            menuContractManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            animatorMenu.SetBool("Appear", false);
        }

        public void SetContract(Contract contract)
        {
            currentContract = contract;
            menuActorsManager.DrawAuditionTitle(contract.Name);
            DrawContractInfo();
            DrawButtons();
            DrawTotalCost();
            CheckButtonInSession();
        }

        private void DrawContractInfo()
        {
            textContractTitle.text = currentContract.Name;
            textContractSalaire.text = currentContract.Money.ToString();
            textContractLine.text = currentContract.CurrentLine.ToString();
            textContractLineMax.text = currentContract.TotalLine.ToString();
            textContractMixage.text = currentContract.CurrentMixing.ToString();
            textContractMixageMax.text = currentContract.TotalMixing.ToString();
        }

        private void DrawButtons()
        {
            for(int i = 0; i < listButtonRoles.Length; i++)
            {
                if(i < currentContract.Characters.Count)
                {
                    listButtonRoles[i].gameObject.SetActive(true);
                    listButtonRoles[i].DrawButton(currentContract.Characters[i], currentContract.VoiceActors[i]);
                    listButtonRoles[i].UnselectButton();
                }
                else
                {
                    listButtonRoles[i].gameObject.SetActive(false);
                }
            }

            menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);
            //StartCoroutine(WaitEndOfFrame());

        }

        private IEnumerator WaitEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            listButtonRoles[0].SelectButton();
            indexSelected = 0;
            DrawRoleInfo();
        }

        private void DrawTotalCost()
        {
            int sum = 0;
            for(int i = 0; i < currentContract.VoiceActors.Count; i++)
            {
                if(currentContract.VoiceActors[i] != null) 
                    sum += (currentContract.VoiceActors[i].Price * currentContract.Characters[i].Line);
            }
            textContractTotalCost.text = sum.ToString();
        }

        private void DrawRoleInfo()
        {
            // bug si appelé dans OnEnable(), les buttons sont encore considéré inactive
            menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);

            textRoleName.text = currentContract.Characters[indexSelected].Name;
            textRoleFan.text = currentContract.Characters[indexSelected].Fan.ToString();
            textRoleLine.text = currentContract.Characters[indexSelected].Line.ToString();
            textRoleCadence.text = currentContract.Characters[indexSelected].Attack.ToString();

            if (currentContract.VoiceActors[indexSelected] == null)
            {
                DrawActorInfo(false);
                DrawGaugeInfo(false);
            }
            else
            {
                DrawActorInfo(true);
                DrawGaugeInfo(true);
            }
        }


        private void DrawActorInfo(bool hasActor)
        {
            panelActor.SetActive(hasActor);
            if (hasActor == true)
            {
                textActorName.text = currentContract.VoiceActors[indexSelected].Name;
                textActorFan.text = currentContract.VoiceActors[indexSelected].Fan.ToString();
                textActorCost.text = currentContract.VoiceActors[indexSelected].Price.ToString();
                textActorCadence.text = "-0 %";
                textActorLevel.text = currentContract.VoiceActors[indexSelected].Level.ToString();
                imageActorSprite.sprite = currentContract.VoiceActors[indexSelected].ActorSprite;
            }
        }

        private void DrawGaugeInfo(bool hasActor)
        {
            StopAllCoroutines();
            for (int i = 0; i < textStatsRole.Length; i++)
            {
                int currentStatRole = 0;
                switch (i)
                {
                    case 0:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Joy;
                        break;
                    case 1:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Sadness;
                        break;
                    case 2:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Disgust;
                        break;
                    case 3:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Anger;
                        break;
                    case 4:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Surprise;
                        break;
                    case 5:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Sweetness;
                        break;
                    case 6:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Fear;
                        break;
                    case 7:
                        currentStatRole = currentContract.Characters[indexSelected].CharacterStat.Trust;
                        break;
                }
                textStatsRole[i].text = currentStatRole.ToString();
                StartCoroutine(GaugeCoroutine(jaugeStatsRole[i], currentStatRole / 100f));

                if (hasActor == true)
                {
                    int currentStatActor = 0;
                    switch (i)
                    {
                        case 0:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Joy;
                            break;
                        case 1:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Sadness;
                            break;
                        case 2:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Disgust;
                            break;
                        case 3:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Anger;
                            break;
                        case 4:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Surprise;
                            break;
                        case 5:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Sweetness;
                            break;
                        case 6:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Fear;
                            break;
                        case 7:
                            currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.Trust;
                            break;
                    }
                    textStatsActor[i].text = currentStatActor.ToString();
                    StartCoroutine(GaugeCoroutine(jaugeStatsActor[i], currentStatActor / 100f));
                }
                else
                {
                    textStatsActor[i].text = "0";
                    jaugeStatsActor[i].transform.localScale = new Vector3(0, jaugeStatsRole[i].transform.localScale.y, jaugeStatsRole[i].transform.localScale.z);
                }
            }
        }

        private IEnumerator GaugeCoroutine(RectTransform jaugeStat, float target, int time = 10)
        {
            if(jaugeStat.transform.localScale.x == target)
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





        public void Validate()
        {
            menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);
            cameraManager.MoveToCamera(1);
        }

        public void SetActor(VoiceActor actor)
        {
            currentContract.VoiceActors[indexSelected] = actor;
            DrawRoleInfo();
            animatorFeedbackSpriteAudition.SetTrigger("Feedback");
            listButtonRoles[indexSelected].DrawActor(currentContract.Characters[indexSelected], actor);
            DrawTotalCost();
            CheckButtonInSession();
        }

        private void CheckButtonInSession()
        {
            for(int i = 0; i < currentContract.VoiceActors.Count; i++)
            {
                if (currentContract.VoiceActors[i] == null)
                {
                    inSessionPossible = false;
                    inSessionObject.SetActive(false);
                    return;
                }
            }
            inSessionPossible = true;
            /*if(currentContract.VoiceActors.Count == currentContract.Characters.Count)
            {
                inSessionPossible = true;
            }
            else
            {
                inSessionPossible = false;
            }*/
            inSessionObject.SetActive(true);
        }

        public void InSession()
        {
            if (inSessionPossible == false)
                return;
            playerData.CurrentContract = currentContract;
            menuTransitionDoublage.StartTransition();
        }


        // =================================================================
        public void SelectRoleUp()
        {
            if (currentContract.Characters.Count == 0)
            {
                return;
            }
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            listButtonRoles[indexSelected].UnselectButton();

            indexSelected -= 1;
            if (indexSelected <= -1)
            {
                indexSelected = currentContract.Characters.Count - 1;
            }
            MoveScrollRect();
            DrawRoleInfo();
            listButtonRoles[indexSelected].SelectButton();
        }

        public void SelectRoleDown()
        {
            if (currentContract.Characters.Count == 0)
            {
                return;
            }
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            listButtonRoles[indexSelected].UnselectButton();

            indexSelected += 1;
            if (indexSelected >= currentContract.Characters.Count)
            {
                indexSelected = 0;
            }
            MoveScrollRect();
            DrawRoleInfo();
            listButtonRoles[indexSelected].SelectButton();

        }


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
            else if (currentRepeatInterval == 0)
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
            /*if (coroutineScroll != null)
            {
                StopCoroutine(coroutineScroll);
            }
            if (indexSelected > indexLimit)
            {
                indexLimit = indexSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            else if (indexSelected < indexLimit - scrollSize)
            {
                indexLimit = indexSelected + scrollSize;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }*/

        }

        /*private IEnumerator MoveScrollRectCoroutine()
        {
            int time = 10;
            int ratio = indexLimit - scrollSize;
            float speed = (buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time;
            while (time != 0)
            {
                buttonListTransform.anchoredPosition -= new Vector2(0, speed);
                time -= 1;
                yield return null;
            }

        }*/




        #endregion

    } // MenuContractPreparation class
	
}// #PROJECTNAME# namespace
