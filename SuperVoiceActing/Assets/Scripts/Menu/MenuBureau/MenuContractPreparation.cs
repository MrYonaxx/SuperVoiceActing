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
        Image imageContractIcon;
        [SerializeField]
        TextMeshProUGUI textContractSalaire;
        [SerializeField]
        TextMeshProUGUI textContractTotalCost;

        [SerializeField]
        MenuContractPreparationAnnexe[] contractInfoAnnex;


        [Header("InfoRole")]
        [SerializeField]
        TextMeshProUGUI textRoleName;
        [SerializeField]
        TextMeshProUGUI textRoleFan;
        [SerializeField]
        TextMeshProUGUI textRoleLine;
        [SerializeField]
        TextMeshProUGUI textRoleCadence;
        [SerializeField]
        RectTransform transformRoleTimbre;

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
        RectTransform transformActorTimbre;
        [SerializeField]
        TextMeshProUGUI textActorLevel;
        [SerializeField]
        TextMeshProUGUI textActorPV;
        [SerializeField]
        TextMeshProUGUI textActorPVMax;
        [SerializeField]
        RectTransform transformGaugeHP;
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
        Animator inSessionObject;
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
        [SerializeField]
        MenuContractMoney menuContractMoney;

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

        List<MenuContractPreparationAnnexe> listAnnexTeamTech = new List<MenuContractPreparationAnnexe>();
        private int indexSelectedTeamTech = 0;

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
        }

        public void SwitchToMenuContractManager()
        {
            menuActorsManager.AuditionMode(false, null);
            StopAllCoroutines();
            menuContractManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            animatorMenu.SetBool("Appear", false);
            inSessionObject.SetTrigger("Disappear");
        }

        public void SetContract(Contract contract)
        {
            currentContract = contract;
            menuActorsManager.DrawAuditionTitle(contract.Name);
            menuActorsManager.DrawAuditionIcon(contract.IconSprite);
            DrawContractInfo();
            DrawButtonsTeamTech();
            DrawButtons();
            CheckButtonInSession();
        }

        private void DrawContractInfo()
        {
            textContractTitle.text = currentContract.Name;
            imageContractIcon.sprite = currentContract.IconSprite;
            textContractSalaire.text = currentContract.Money.ToString();
            textContractTotalCost.text = GetTotalCost().ToString();
        }

        private void DrawButtonsTeamTech()
        {
            listAnnexTeamTech.Clear();
            for(int i = 0; i < contractInfoAnnex.Length; i++)
            {
                if (contractInfoAnnex[i].CanAdd(currentContract) == true)
                {
                    listAnnexTeamTech.Add(contractInfoAnnex[i]);
                }
                contractInfoAnnex[i].UnselectButton();
            }
            indexSelectedTeamTech = -1;
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
            indexSelected = 0;
            if(currentContract.Characters[indexSelected].CharacterLock != null)
                menuActorsManager.AuditionMode(false, currentContract.Characters[indexSelected]);
            else
                menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);
            listButtonRoles[0].SelectButton();
            indexSelected = 0;
            DrawRoleInfo();

        }

        private IEnumerator WaitEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            listButtonRoles[0].SelectButton();
            indexSelected = 0;
            DrawRoleInfo();
        }

        private int GetTotalCost()
        {
            int sum = 0;
            for(int i = 0; i < currentContract.VoiceActors.Count; i++)
            {
                if(currentContract.VoiceActors[i] != null) 
                    sum += (currentContract.VoiceActors[i].Price * currentContract.Characters[i].Line);
            }
            return sum;
        }

        private void DrawRoleInfo()
        {
            // bug si appelé dans OnEnable(), les buttons sont encore considéré inactive
            if (currentContract.Characters[indexSelected].CharacterLock != null)
                menuActorsManager.AuditionMode(false, currentContract.Characters[indexSelected]);
            else
                menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);
            //menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);

            textRoleName.text = currentContract.Characters[indexSelected].Name;
            textRoleFan.text = currentContract.Characters[indexSelected].Fan.ToString();
            textRoleLine.text = currentContract.Characters[indexSelected].Line.ToString();
            textRoleCadence.text = currentContract.Characters[indexSelected].Attack.ToString();

            transformRoleTimbre.anchorMin = new Vector2((currentContract.Characters[indexSelected].Timbre.x + 10) / 20f, 0);
            transformRoleTimbre.anchorMax = new Vector2((currentContract.Characters[indexSelected].Timbre.y + 10) / 20f, 1);
            transformRoleTimbre.anchoredPosition = Vector3.zero;

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
            //panelActor.SetActive(hasActor);
            if (hasActor == true)
            {
                textActorName.text = currentContract.VoiceActors[indexSelected].Name;
                textActorFan.text = currentContract.VoiceActors[indexSelected].Fan.ToString();
                textActorCost.text = currentContract.VoiceActors[indexSelected].Price.ToString();
                textActorCadence.text = "-" + (currentContract.VoiceActors[indexSelected].RoleDefense * 5) + " %";
                transformActorTimbre.anchorMin = new Vector2((currentContract.VoiceActors[indexSelected].Timbre.x + 10) / 20f, 0);
                transformActorTimbre.anchorMax = new Vector2((currentContract.VoiceActors[indexSelected].Timbre.y + 10) / 20f, 1);
                transformActorTimbre.anchoredPosition = Vector3.zero;
                textActorLevel.text = currentContract.VoiceActors[indexSelected].Level.ToString();
                textActorPV.text = currentContract.VoiceActors[indexSelected].Hp.ToString();
                textActorPVMax.text = currentContract.VoiceActors[indexSelected].HpMax.ToString();
                transformGaugeHP.localScale = new Vector3((float)currentContract.VoiceActors[indexSelected].Hp / currentContract.VoiceActors[indexSelected].HpMax, 1, 1);
                imageActorSprite.enabled = true;
                imageActorSprite.sprite = currentContract.VoiceActors[indexSelected].ActorSprite;
                imageActorSprite.SetNativeSize();
            }
            else
            {
                textActorName.text = " - ";
                textActorFan.text = " - ";
                textActorCost.text = " - ";
                textActorCadence.text = " - ";
                textActorLevel.text = " - ";
                textActorPV.text = " - ";
                textActorPVMax.text = " - ";
                transformActorTimbre.anchorMin = new Vector2(0, 0);
                transformActorTimbre.anchorMax = new Vector2(0, 1);
                transformActorTimbre.anchoredPosition = Vector3.zero;
                transformGaugeHP.localScale = new Vector3(0, 1, 1);
                imageActorSprite.enabled = false;

            }
        }

        private void DrawGaugeInfo(bool hasActor)
        {
            StopAllCoroutines();
            for (int i = 0; i < textStatsRole.Length; i++)
            {
                int currentStatRole = currentContract.Characters[indexSelected].CharacterStat.GetEmotion(i+1);
                textStatsRole[i].text = currentStatRole.ToString();
                StartCoroutine(GaugeCoroutine(jaugeStatsRole[i], currentStatRole / 100f));

                if (hasActor == true)
                {
                    int currentStatActor = currentContract.VoiceActors[indexSelected].Statistique.GetEmotion(i+1);
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
            if (indexSelectedTeamTech != -1)
            {
                listAnnexTeamTech[indexSelectedTeamTech].ValidateButton(currentContract);
            }
            else
            {
                if (currentContract.Characters[indexSelected].CharacterLock != null)
                {
                    return;
                }
                menuActorsManager.AuditionMode(true, currentContract.Characters[indexSelected]);
                cameraManager.MoveToCamera(1);
            }
        }

        public void SetActor(VoiceActor actor)
        {
            currentContract.VoiceActors[indexSelected] = actor;
            DrawRoleInfo();
            animatorFeedbackSpriteAudition.SetTrigger("Feedback");
            listButtonRoles[indexSelected].DrawActor(currentContract.Characters[indexSelected], actor);
            textContractTotalCost.text = "-" + GetTotalCost().ToString();
            CheckButtonInSession();
        }

        public void SetSoundEngi(SoundEngineer soundEngineer)
        {
            currentContract.SoundEngineer = soundEngineer;
            contractInfoAnnex[indexSelectedTeamTech].DrawContract(currentContract);
        }

        private void CheckButtonInSession()
        {
            if (currentContract.SessionLock == true)
            {
                inSessionPossible = false;
                inSessionObject.gameObject.SetActive(false);
                return;
            }
            for (int i = 0; i < currentContract.VoiceActors.Count; i++)
            {
                if (currentContract.VoiceActors[i] == null)
                {
                    inSessionPossible = false;
                    inSessionObject.gameObject.SetActive(false);
                    return;
                }
            }
            inSessionPossible = true;
            inSessionObject.gameObject.SetActive(true);
            inSessionObject.SetTrigger("Appear");
        }

        public void InSession()
        {
            if (inSessionPossible == false)
                return;

            menuContractMoney.AddSalaryDatas("Acteur(s)", -GetTotalCost());
            menuContractMoney.DrawMoneyGain();
            playerData.Money -= GetTotalCost();

            playerData.CurrentContract = currentContract;
            menuTransitionDoublage.StartTransition();
            inSessionObject.SetTrigger("Selection");
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

            if(indexSelected == -1)
            {
                listAnnexTeamTech[indexSelectedTeamTech].UnselectButton();
                indexSelectedTeamTech -= 1;
                if (indexSelectedTeamTech <= -1)
                {
                    indexSelectedTeamTech = -1;
                    indexSelected = currentContract.Characters.Count - 1;
                    listButtonRoles[indexSelected].SelectButton();
                    DrawRoleInfo();
                    return;
                }
                listAnnexTeamTech[indexSelectedTeamTech].SelectButton();
            }
            else
            {
                listButtonRoles[indexSelected].UnselectButton();
                indexSelected -= 1;
                if (indexSelected <= -1)
                {
                    indexSelected = -1;
                    indexSelectedTeamTech = listAnnexTeamTech.Count - 1;
                    listAnnexTeamTech[indexSelectedTeamTech].SelectButton();
                    menuActorsManager.AuditionMode(false, null);
                    return;
                }
                listButtonRoles[indexSelected].SelectButton();
                DrawRoleInfo();
            }
            //MoveScrollRect();
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


            if (indexSelected == -1)
            {
                listAnnexTeamTech[indexSelectedTeamTech].UnselectButton();
                indexSelectedTeamTech += 1;
                if (indexSelectedTeamTech >= listAnnexTeamTech.Count)
                {
                    indexSelectedTeamTech = -1;
                    indexSelected = 0;
                    listButtonRoles[indexSelected].SelectButton();
                    DrawRoleInfo();
                    return;
                }
                listAnnexTeamTech[indexSelectedTeamTech].SelectButton();
            }
            else
            {
                listButtonRoles[indexSelected].UnselectButton();
                indexSelected += 1;
                if (indexSelected >= currentContract.Characters.Count)
                {
                    indexSelected = -1;
                    indexSelectedTeamTech = 0;
                    listAnnexTeamTech[indexSelectedTeamTech].SelectButton();
                    menuActorsManager.AuditionMode(false, null);
                    return;
                }
                listButtonRoles[indexSelected].SelectButton();
                DrawRoleInfo();
            }
            //MoveScrollRect();
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
