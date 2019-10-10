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
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuActionManager: MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Space]
        [Title("Seiyuu Action Manager")]
        [SerializeField]
        SeiyuuActionDatabase seiyuuDatabase;
        [SerializeField]
        SeiyuuButtonAction seiyuuButtonActionPrefab;


        [Title("Seiyuu Action Selected")]
        [SerializeField]
        Animator animatorSeiyuuActionDay;
        [SerializeField]
        Animator animatorSeiyuuActionNight;

        [SerializeField]
        TextMeshProUGUI textSeiyuuActionDay;
        [SerializeField]
        TextMeshProUGUI textSeiyuuActionNight;

        [SerializeField]
        Animator animatorButtonStartWeek;
        [SerializeField]
        InputController inputController;

        SeiyuuAction seiyuuActionDay;
        SeiyuuAction seiyuuActionNight;


        List<SeiyuuButtonAction> seiyuuActionButton = new List<SeiyuuButtonAction>();
        int currentTab = 0;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public SeiyuuAction GetSeiyuuActionDay()
        {
            return seiyuuActionDay;
        }

        public SeiyuuAction GetSeiyuuActionNight()
        {
            return seiyuuActionNight;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        private void CreateSeiyuuActionButton()
        {
            seiyuuActionButton.Add(Instantiate(seiyuuButtonActionPrefab, buttonScrollListTransform));
            seiyuuActionButton[seiyuuActionButton.Count - 1].gameObject.SetActive(true);
        }

        public void ShowSeiyuuActionMenu(int tab)
        {
            //indexSelected = -1;
            this.gameObject.SetActive(true);
            currentTab = tab;
            buttonsList.Clear();
            for (int i = 0; i < seiyuuDatabase.GetSeiyuuActions(tab).Length; i++)
            {
                if (i >= seiyuuActionButton.Count)
                    CreateSeiyuuActionButton();

                buttonsList.Add(seiyuuActionButton[seiyuuActionButton.Count - 1].RectTransform);
                seiyuuActionButton[i].DrawSeiyuuAction(seiyuuDatabase.GetSeiyuuActions(tab)[i], i);
                seiyuuActionButton[i].ButtonAppear(true, i * 0.05f);
            }
            for (int i = seiyuuDatabase.GetSeiyuuActions(tab).Length; i < seiyuuActionButton.Count; i++)
            {
                seiyuuActionButton[i].ButtonAppear(false, 0f);
            }
            animatorButtonStartWeek.SetBool("Appear", false);
        }

        public void QuitSeiyuuActionMenu()
        {
            for (int i = 0; i < seiyuuActionButton.Count; i++)
            {
                seiyuuActionButton[i].ButtonAppear(false, i * 0.05f);
            }
            this.gameObject.SetActive(false);
        }


        protected override void BeforeSelection()
        {
            base.BeforeSelection();
            seiyuuActionButton[indexSelected].ButtonSelected(false);
        }
        protected override void AfterSelection()
        {
            base.AfterSelection();
            seiyuuActionButton[indexSelected].ButtonSelected(true);
        }


        public void SelectAction(int index)
        {
            indexSelected = index;
            SelectAction();
        }

        public void SelectAction()
        {
            if (seiyuuActionDay == null)
            {
                seiyuuActionDay = seiyuuDatabase.GetSeiyuuActions(currentTab)[indexSelected];
                textSeiyuuActionDay.text = seiyuuDatabase.GetSeiyuuActions(currentTab)[indexSelected].ActionName;
                animatorSeiyuuActionDay.SetBool("Appear", true);
            }
            else if (seiyuuActionNight == null)
            {
                seiyuuActionNight = seiyuuDatabase.GetSeiyuuActions(currentTab)[indexSelected];
                textSeiyuuActionNight.text = seiyuuDatabase.GetSeiyuuActions(currentTab)[indexSelected].ActionName;
                animatorSeiyuuActionNight.SetBool("Appear", true);
                animatorButtonStartWeek.SetBool("Appear", true);
                inputController.EventButtonB();
            }
        }

        public void RemoveAction(bool action)
        {
            if (action == true)
            {
                seiyuuActionNight = null;
                animatorSeiyuuActionNight.SetBool("Appear", false);
                animatorButtonStartWeek.SetBool("Appear", false);
            }
            else if (action == false)
            {
                seiyuuActionDay = null;
                animatorSeiyuuActionDay.SetBool("Appear", false);
                animatorButtonStartWeek.SetBool("Appear", false);
            }
        }

        public void HideButtonStartWeek()
        {
            animatorButtonStartWeek.SetBool("Appear", false);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace