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
        [Header("CharacterData")]

        [SerializeField]
        StoryCharacterData storyCharacterData;
        [SerializeField]
        SpriteRenderer spriteRenderer;
        [SerializeField]
        Yeux eyesScript;


        [Header("Mouth")]
        /*[SerializeField]
        MouthAnimation mouth;*/
        [SerializeField]
        float speedMouth = 5;
        [SerializeField]
        AudioSource voice = null;
        [SerializeField]
        FeedbackSon soundVisualizer;


        [Header("Phase d'Acting")]

        [SerializeField]
        private TextPerformanceAppear textActing;
        [SerializeField]
        private CameraController camera;




        IEnumerator mouthCoroutine = null;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetPhraseTextacting(string newText, int cameraID)
        {
            textActing.NewMouthAnim(this);
            textActing.NewPhrase(newText);
            /*if (cameraID != 0)
                camera.CinematicCamera(cameraID);*/
        }

        public void SetStoryCharacterData(StoryCharacterData sprites)
        {
            storyCharacterData = sprites;
            if (spriteRenderer != null)
                spriteRenderer.sprite = sprites.SpriteNormal[0];
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
            if (speed != -1)
                speedMouth = speed;
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);
            mouthCoroutine = MouthAnim();
            StartCoroutine(mouthCoroutine);
        }

        public void StopMouth()
        {
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);

            if (spriteRenderer != null)
                spriteRenderer.sprite = storyCharacterData.SpriteNormal[0];

            if (soundVisualizer != null)
                soundVisualizer.StopVisualizer();
        }

        public void DesactivateMouth()
        {
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);

            if (spriteRenderer != null)
                spriteRenderer.sprite = storyCharacterData.SpriteNormal[0];

            if (soundVisualizer != null)
                soundVisualizer.StopVisualizer();
        }


        private IEnumerator MouthAnim()
        {
            int i = 0;
            float speed = speedMouth;
            while (speed > 0)
            {
                speed -= 1;
                yield return null;
                if (speed == 0)
                {
                    i += Random.Range(1, 2);
                    if (i >= storyCharacterData.SpriteNormal.Length)
                        i = 0;
                    changeMouthSprite(i);
                    speed = speedMouth;
                    if (soundVisualizer != null)
                        soundVisualizer.SetVisualizer();
                }
            }
        }

        public void changeMouthSprite(int index)
        {
            if (voice != null)
            {
                voice.pitch = Random.Range(0.95f, 1.05f);
                voice.Play();
            }
            if (index < storyCharacterData.SpriteNormal.Length)
                spriteRenderer.sprite = storyCharacterData.SpriteNormal[index];
        }







        public void FadeIn(float time)
        {
            if(eyesScript != null)
                eyesScript.StartBlink();
            StartCoroutine(Fade(true, time));
        }

        public void FadeOut(float time)
        {
            if (eyesScript != null)
                eyesScript.StopBlink();
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
            //mouth.ChangeOrderInLayer(newOrder + 1);
        }

        [ContextMenu("Shake")]
        public void Shake()
        {
            StartCoroutine(ShakeCoroutine(0.06f, 60));
        }

        private IEnumerator ShakeCoroutine(float intensity, float time)
        {
            Vector3 origin = this.transform.position;
            while (time != 0)
            {
                time -= 1;
                this.transform.position = origin + new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), Random.Range(-intensity, intensity));
                intensity *= 0.9f;
                yield return null;
            }
            this.transform.position = origin;
        }

        #endregion

        } // CharacterDialogueController class

} // #PROJECTNAME# namespace