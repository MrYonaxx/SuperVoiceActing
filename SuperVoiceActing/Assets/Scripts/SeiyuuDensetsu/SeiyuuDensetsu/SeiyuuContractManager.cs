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
    public class SeiyuuContractManager: MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        SeiyuuContractButton seiyuuContractButtonPrefab;

        List<Contract> listContract;
        List<SeiyuuContractButton> seiyuuContractButton;

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

        private void CreateContractButton()
        {
            seiyuuContractButton.Add(Instantiate(seiyuuContractButtonPrefab, buttonScrollListTransform));
            seiyuuContractButton[seiyuuContractButton.Count - 1].gameObject.SetActive(true);
        }

        public void ShowContractMenu()
        {
            //indexSelected = -1;
            this.gameObject.SetActive(true);
            buttonsList.Clear();
            for (int i = 0; i < listContract.Count; i++)
            {
                if (i >= seiyuuContractButton.Count)
                    CreateContractButton();

                buttonsList.Add(seiyuuContractButton[seiyuuContractButton.Count - 1].RectTransform);
                seiyuuContractButton[i].DrawContractButton(listContract[i], i);
                seiyuuContractButton[i].ButtonAppear(true, i * 0.05f);
            }
            for (int i = listContract.Count; i < seiyuuContractButton.Count; i++)
            {
                seiyuuContractButton[i].ButtonAppear(false, 0f);
            }
        }

        public void QuitContractMenu()
        {
            for (int i = 0; i < seiyuuContractButton.Count; i++)
            {
                seiyuuContractButton[i].ButtonAppear(false, i * 0.05f);
            }
            this.gameObject.SetActive(false);
        }


        protected override void BeforeSelection()
        {
            base.BeforeSelection();
        }
        protected override void AfterSelection()
        {
            base.AfterSelection();
        }


        public void SelectContract(int index)
        {
            indexSelected = index;
            SelectContract();
        }

        public void SelectContract()
        {

        }


        public void DrawContract(Contract contract)
        {

        }

        


        #endregion

    } 

} // #PROJECTNAME# namespace