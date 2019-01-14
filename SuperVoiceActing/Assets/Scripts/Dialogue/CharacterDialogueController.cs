/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the CharacterDialogueController class
    /// </summary>
    public class CharacterDialogueController : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        StoryCharacterData storyCharacterData;

        [SerializeField]
        SpriteRenderer spriteRenderer;

        [SerializeField]
        Yeux eyesScript;

        [SerializeField]
        MouthAnimation mouth;


        [Header("Phase d'Acting")]

        [SerializeField]
        private TextPerformanceAppear textActing;
        [SerializeField]
        private CameraController camera;
        /*public TextPerformanceAppear TextActing
        {
            get { return textActing; }
        }*/


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetPhraseTextacting(string newText, int cameraID)
        {
            textActing.NewMouthAnim(mouth);
            textActing.NewPhrase(newText);
            if(cameraID != 0)
                camera.CinematicCamera(cameraID);
        }

        public StoryCharacterData GetStoryCharacterData()
        {
            return storyCharacterData;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public void ActivateMouth(float speed = -1)
        {
            if(speed == -1)
                mouth.ActivateMouth();
            else
                mouth.ActivateMouth(speed);
        }

        public void StopMouth()
        {
            mouth.DesactivateMouth();
        }

        public void FadeIn(float time)
        {
            if(eyesScript != null)
                eyesScript.StartBlink();
            if(mouth != null)
                mouth.ShowMouth();
            StartCoroutine(Fade(true, time));
        }

        public void FadeOut(float time)
        {
            if (eyesScript != null)
                eyesScript.StopBlink();
            if (mouth != null)
                mouth.HideMouth();
            StartCoroutine(Fade(false, time));
        }

        // Peut etre a déplacer dans StoryEventMoveCharacter
        private IEnumerator Fade(bool appear, float time)
        {
            float alphaSpeed = 1 / time;
            if (appear == false)
                alphaSpeed = -alphaSpeed;
            while (time != 0)
            {
                time -= 1;
                spriteRenderer.color += new Color(0, 0, 0, alphaSpeed);
                yield return null;
            }
        }

        public void ChangeTint(Color newColor)
        {
            spriteRenderer.color = newColor;
            eyesScript.ChangeTint(newColor);
        }



        public void ChangeOrderInLayer(int newOrder)
        {
            spriteRenderer.sortingOrder = newOrder;
            mouth.ChangeOrderInLayer(newOrder + 1);
        }

        #endregion

    } // CharacterDialogueController class

} // #PROJECTNAME# namespace