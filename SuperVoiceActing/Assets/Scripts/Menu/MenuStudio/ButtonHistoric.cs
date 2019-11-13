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
    public class ButtonHistoric: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        TextMeshProUGUI textContractTitle;
        [SerializeField]
        TextMeshProUGUI textContractMoney;
        [SerializeField]
        TextMeshProUGUI textContractNote;
        [SerializeField]
        Image imageContractType;

        [HorizontalGroup]
        [SerializeField]
        TextMeshProUGUI[] textContractRoles;
        [HorizontalGroup]
        [SerializeField]
        TextMeshProUGUI[] textContractNotes;
        [HorizontalGroup]
        [SerializeField]
        Image[] imageContractActors;

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

        public void DrawContractHistoric(Contract c, Sprite spriteIcon, List<Sprite> spritesActors)
        {
            textContractTitle.text = c.Name;
            textContractMoney.text = "+" + (c.Money + c.MoneyBonus);
            imageContractType.sprite = spriteIcon;

            for(int i = 0; i < imageContractActors.Length; i++)
            {
                if(i < c.Characters.Count)
                {
                    imageContractActors[i].gameObject.SetActive(true);
                    imageContractActors[i].sprite = spritesActors[i];
                    textContractRoles[i].text = c.Characters[i].Name;
                }
                else
                {
                    imageContractActors[i].gameObject.SetActive(false);
                }
            }
        }


        #endregion

    } 

} // #PROJECTNAME# namespace