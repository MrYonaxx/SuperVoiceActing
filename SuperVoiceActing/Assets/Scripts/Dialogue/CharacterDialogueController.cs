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
        [SerializeField]
        float speedMouth = 5;
        [SerializeField]
        AudioSource voice = null;
        [SerializeField]
        FeedbackSon soundVisualizer;


        [Header("Phase d'Acting")]
        [SerializeField]
        private TextPerformanceAppear textActing;


        bool speak = false;
        private Sprite[] currentSprites;

        IEnumerator mouthCoroutine = null;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public TextPerformanceAppear GetTextActing()
        {
            return textActing;
        }

        public void SetPhraseTextacting(string newText)
        {
            textActing.NewMouthAnim(this);
            textActing.NewPhrase(newText);
        }

        public void SetPhraseEventTextacting(string newText, EmotionNPC emotionNPC)
        {
            ChangeEmotion(emotionNPC);
            textActing.NewMouthAnim(this);
            textActing.NewPhrase(newText);
        }

        public void ChangeEmotion(EmotionNPC emotionNPC)
        {
            switch(emotionNPC)
            {
                case EmotionNPC.Normal:
                    currentSprites = storyCharacterData.SpriteNormal;
                    break;
                case EmotionNPC.Joyeux:
                    currentSprites = storyCharacterData.SpriteJoy;
                    break;
                case EmotionNPC.Special:
                    currentSprites = storyCharacterData.SpriteSpecial;
                    break;
            }
        }

        public void SetStoryCharacterData(StoryCharacterData sprites)
        {
            storyCharacterData = sprites;
            currentSprites = storyCharacterData.SpriteNormal;
            if (spriteRenderer != null)
                spriteRenderer.sprite = currentSprites[0];
        }

        public void LoadCharacterDataSprites()
        {
            currentSprites = storyCharacterData.SpriteNormal;
            if (spriteRenderer != null)
                spriteRenderer.sprite = currentSprites[0];
        }

        public void SetTextActing(TextPerformanceAppear textMeshPro)
        {
            textActing = textMeshPro;
        }

        public StoryCharacterData GetStoryCharacterData()
        {
            return storyCharacterData;
        }


        public void SetFlip(bool flip)
        {
            spriteRenderer.flipX = flip;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public void ActivateMouth(float speed = -1, bool noSpeak = false)
        {
            speak = noSpeak;
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
                spriteRenderer.sprite = currentSprites[0];

            if (soundVisualizer != null)
                soundVisualizer.StopVisualizer();
        }

        public void DesactivateMouth()
        {
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);

            if (spriteRenderer != null)
                spriteRenderer.sprite = currentSprites[0];

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
                    if (i >= currentSprites.Length)
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
            if (speak == true)
                return;
            if (index < currentSprites.Length)
                spriteRenderer.sprite = currentSprites[index];
        }





        public void FadeCharacter(bool fade, float time)
        {
            if (eyesScript != null)
                eyesScript.StartBlink();
            StartCoroutine(Fade(fade, time));
        }

        // Apparaitre
        public void FadeIn(float time)
        {
            if(eyesScript != null)
                eyesScript.StartBlink();
            StartCoroutine(Fade(true, time));
        }

        // Disparaitre
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
            if(appear == false)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            else
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        }

        public void ModifyCharacter(DoublageEventMoveCharacter data)
        {
            ChangeOrderInLayer(data.OrderLayer);
            ChangeTint(data.Color);
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