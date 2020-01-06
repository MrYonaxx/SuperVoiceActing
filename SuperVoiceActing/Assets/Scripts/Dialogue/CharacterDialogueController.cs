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
        protected StoryCharacterData storyCharacterData;
        [SerializeField]
        SpriteRenderer spriteRenderer;
        [SerializeField]
        protected Yeux eyesScript;


        [Header("Mouth")]
        [SerializeField]
        protected bool mouthEmitSound = true;
        [SerializeField]
        protected float speedMouth = 5;
        [SerializeField]
        protected AudioSource voice = null;

        [Header("Autres")]
        [SerializeField]
        protected Animator animatorBalloon;
        [SerializeField]
        protected Animator animatorAura;
        [SerializeField]
        protected SpriteRenderer auraRenderer;


        protected bool speak = false;
        protected Sprite[] currentSprites;

        protected IEnumerator mouthCoroutine = null;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public virtual Sprite GetSprite()
        {
            return spriteRenderer.sprite;
        }
        public StoryCharacterData GetStoryCharacterData()
        {
            return storyCharacterData;
        }

        public virtual Vector3 GetPosition()
        {
            return this.transform.localPosition;
        }
        public virtual void SetPosition(Vector3 transform)
        {
            this.transform.localPosition = transform;
        }


        public void ChangeEmotion(Emotion emotionBattle)
        {
            switch (emotionBattle)
            {
                case Emotion.Joie:
                    currentSprites = storyCharacterData.SpriteJoy;
                    break;
                default:
                    currentSprites = storyCharacterData.SpriteNormal;
                    break;
            }
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



        public virtual void SetStoryCharacterData(StoryCharacterData sprites)
        {
            if(sprites.CharacterVoice != null && voice != null)
                voice.clip = sprites.CharacterVoice;
            storyCharacterData = sprites;
            currentSprites = storyCharacterData.SpriteNormal;
            if (spriteRenderer != null)
                spriteRenderer.sprite = currentSprites[0];
            if (sprites.SpriteEye != null)
                eyesScript.SetSprite(sprites.SpriteEye);
        }

        public virtual void LoadCharacterDataSprites()
        {
            currentSprites = storyCharacterData.SpriteNormal;
            if (spriteRenderer != null)
                spriteRenderer.sprite = currentSprites[0];
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
            if (this.gameObject.activeInHierarchy == false)
                return;
            speak = noSpeak;
            if (speed != -1)
                speedMouth = speed;
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);
            mouthCoroutine = MouthAnim();
            StartCoroutine(mouthCoroutine);
        }

        public virtual void StopMouth()
        {
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);

            if (spriteRenderer != null && currentSprites.Length != 0)
                spriteRenderer.sprite = currentSprites[0];

        }

        public void DesactivateMouth()
        {
            StopMouth();
        }


        private IEnumerator MouthAnim()
        {
            int i = 0;
            float t = 0f;
            while (true)
            {
                t += Time.deltaTime;
                if (t >= speedMouth / 60f)
                {
                    i += Random.Range(1, 2);
                    if (currentSprites != null)
                    {
                        if (i >= currentSprites.Length)
                            i = 0;
                    }
                    ChangeMouthSprite(i);
                    t -= (speedMouth / 60f);
                }
                yield return null;
            }
        }

        public virtual void ChangeMouthSprite(int index)
        {
            if (speak == true)
                return;
            if (mouthEmitSound == true)
                PlayVoice();
            if (index < currentSprites.Length)
                spriteRenderer.sprite = currentSprites[index];
        }


        public void PlayVoice()
        {
            if (voice != null)
            {
                voice.pitch = Random.Range(0.95f, 1.05f);
                if(storyCharacterData.CharacterVoice != null)
                    voice.PlayOneShot(storyCharacterData.CharacterVoice);
            }
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
        protected virtual IEnumerator Fade(bool appear, float time)
        {
            float alphaSpeed = 1 / time;
            if (appear == false)
                alphaSpeed = -alphaSpeed;
            while (time != 0)
            {
                time -= 1;
                spriteRenderer.color += new Color(0, 0, 0, alphaSpeed);
                if(eyesScript != null)
                    eyesScript.ChangeTint(spriteRenderer.color);
                yield return null;
            }
            if(appear == false)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            else
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            if (eyesScript != null)
                eyesScript.ChangeTint(spriteRenderer.color);
        }




        public void ChangeTint(Color newColor)
        {
            spriteRenderer.color = newColor;
            eyesScript.ChangeTint(newColor);
        }

        public void ChangeOrderInLayer(int newOrder)
        {
            spriteRenderer.sortingOrder = newOrder;
            eyesScript.ChangeOrderInLayer(newOrder+1);
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

            time /= 60f;
            float t = 0f;
            float currentIntensity = intensity;
            float blend = 0;
            while (t < 1f)
            {
                t += Time.deltaTime / time;
                this.transform.position = origin + new Vector3(Random.Range(-currentIntensity, currentIntensity), Random.Range(-currentIntensity, currentIntensity), Random.Range(-currentIntensity, currentIntensity));
                blend = 1f - Mathf.Pow(1f - 0.1f, Time.deltaTime * 60);
                currentIntensity = Mathf.Lerp(currentIntensity, 0, blend);
                yield return null;
            }
            /*while (time != 0)
            {
                time -= 1;
                this.transform.position = origin + new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), Random.Range(-intensity, intensity));
                intensity *= 0.9f;
                yield return null;
            }*/
            this.transform.position = origin;
        }


        public void PlayAnimBalloon(int emotion)
        {
            if(animatorBalloon != null)
                animatorBalloon.SetInteger("Emotion", emotion);
        }


        public void ActivateAura(bool b)
        {
            if (auraRenderer != null)
            {
                auraRenderer.sprite = spriteRenderer.sprite;
                animatorAura.SetBool("Aura", b);
            }
        }

        public void FeedbackAura()
        {
            if (auraRenderer != null)
            {
                auraRenderer.sprite = spriteRenderer.sprite;
                animatorAura.SetTrigger("Feedback");
                animatorAura.SetBool("Aura", false);
            }
        }

        #endregion

    } // CharacterDialogueController class

} // #PROJECTNAME# namespace