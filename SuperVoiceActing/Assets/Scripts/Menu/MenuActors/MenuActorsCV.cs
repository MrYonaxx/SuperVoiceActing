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
    public class MenuActorsCV : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        GameObject panelActorCV;

        [SerializeField]
        ImageDictionnary emotionDictionnary;
        [SerializeField]
        Image[] imageBestGrowthEmotion;

        [SerializeField]
        CharacterDialogueController characterDialogueController;
        [SerializeField]
        TextAppearManager textAppearManager;

        [SerializeField]
        GameObject buttonVoiceReel;
        [SerializeField]
        GameObject panelVoiceReel;

        [SerializeField]
        GameObject panelStat;
        [SerializeField]
        GameObject panelSkill;

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
        public void ShowCVPanel(bool b)
        {
            this.gameObject.SetActive(b);
            panelActorCV.gameObject.SetActive(b);
            panelStat.gameObject.SetActive(!b);
            panelSkill.gameObject.SetActive(!b);
            characterDialogueController.StopMouth();
        }

        public void DrawActorCV(VoiceActor va)
        {
            // Select Best Voxography


            // Select Best Actor Growth
            imageBestGrowthEmotion[0].sprite = emotionDictionnary.GetSprite(va.BestGrowthFirst);
            imageBestGrowthEmotion[1].sprite = emotionDictionnary.GetSprite(va.BestGrowthSecond);
            imageBestGrowthEmotion[2].sprite = emotionDictionnary.GetSprite(va.BestGrowthThird);


            //

            buttonVoiceReel.gameObject.SetActive(true);
            panelVoiceReel.gameObject.SetActive(false);
        }


        public void PlayVoiceReel()
        {
            buttonVoiceReel.gameObject.SetActive(false);
            panelVoiceReel.gameObject.SetActive(true);
            StoryCharacterData storyCharacterData = characterDialogueController.GetStoryCharacterData();
            if (storyCharacterData.VoiceReel == "")
            {
                textAppearManager.NewPhrase("Bonjour.", Emotion.Neutre);
            }
            else
            {
                textAppearManager.NewPhrase(storyCharacterData.VoiceReel, storyCharacterData.EmotionVoiceReel);
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace