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
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class MenuStudioHistorique: MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("Menu Historique")]


        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        ScrollRect scrollRect;

        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        ImageDictionnary contractSpriteDictionnary;
        [SerializeField]
        VoiceActorDatabase characterSpriteDatabase;

        [SerializeField]
        ButtonHistoric buttonHistoric;

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

        protected override void Start()
        {
            base.Start();
            CreateButtonHistoric();
        }

        private void OnEnable()
        {
            animatorMenu.gameObject.SetActive(true);

        }

        private void OnDisable()
        {
            animatorMenu.gameObject.SetActive(false);

        }


        private void CreateButtonHistoric()
        {
            for (int i = 0; i < playerData.ContractHistoric.Count; i++)
            {
                ButtonHistoric b = Instantiate(buttonHistoric, buttonScrollListTransform);
                b.gameObject.SetActive(true);
                List<Sprite> sprites = new List<Sprite>(playerData.ContractHistoric[i].VoiceActorsID.Count);
                for(int j = 0; j < playerData.ContractHistoric[i].VoiceActorsID.Count; j++)
                {
                    sprites.Add(characterSpriteDatabase.GetCharacterData(playerData.ContractHistoric[i].VoiceActorsID[j]).SpriteIcon);
                }
                b.DrawContractHistoric(playerData.ContractHistoric[i], contractSpriteDictionnary.GetSprite((int)playerData.ContractHistoric[i].ContractType), sprites);
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace