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
    public class RectCharacterDialogueController : CharacterDialogueController
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Image imageRenderer;
        [SerializeField]
        RectTransform rectTransform;

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

        public override Sprite GetSprite()
        {
            return imageRenderer.sprite;
        }
        public override Vector3 GetPosition()
        {
            return new Vector3((rectTransform.anchorMin.x - 0.5f) * 2, rectTransform.anchorMin.y, 0);
        }
        public override void SetPosition(Vector3 transform)
        {
            rectTransform.anchorMin = new Vector3((transform.x * 0.5f) + 0.5f, transform.y, 0);
            rectTransform.anchorMax = new Vector3((transform.x * 0.5f) + 0.5f, transform.y, 0);
            rectTransform.anchoredPosition = Vector2.zero;
        }


        public override void SetStoryCharacterData(StoryCharacterData sprites)
        {
            if (sprites.CharacterVoice != null && voice != null)
                voice.clip = sprites.CharacterVoice;
            storyCharacterData = sprites;
            currentSprites = storyCharacterData.SpriteNormal;
            if (imageRenderer != null)
                imageRenderer.sprite = currentSprites[0];
            if (sprites.SpriteEye != null)
                eyesScript.SetSprite(sprites.SpriteEye);
            imageRenderer.SetNativeSize();
        }

        public override void LoadCharacterDataSprites()
        {
            currentSprites = storyCharacterData.SpriteNormal;
            if (imageRenderer != null)
                imageRenderer.sprite = currentSprites[0];
        }






        public override void StopMouth()
        {
            if (mouthCoroutine != null)
                StopCoroutine(mouthCoroutine);

            if (imageRenderer != null && currentSprites.Length != 0)
                imageRenderer.sprite = currentSprites[0];
        }

        public override void ChangeMouthSprite(int index)
        {
            if (speak == true)
                return;
            if (mouthEmitSound == true)
                PlayVoice();
            if (index < currentSprites.Length)
                imageRenderer.sprite = currentSprites[index];
        }






        // Peut etre a déplacer dans StoryEventMoveCharacter
        protected override IEnumerator Fade(bool appear, float time)
        {
            float alphaSpeed = 1 / time;
            if (appear == false)
                alphaSpeed = -alphaSpeed;
            while (time != 0)
            {
                time -= 1;
                imageRenderer.color += new Color(0, 0, 0, alphaSpeed);
                if (eyesScript != null)
                    eyesScript.ChangeTint(imageRenderer.color);
                yield return null;
            }
            if (appear == false)
                imageRenderer.color = new Color(imageRenderer.color.r, imageRenderer.color.g, imageRenderer.color.b, 0);
            else
                imageRenderer.color = new Color(imageRenderer.color.r, imageRenderer.color.g, imageRenderer.color.b, 1);
            if (eyesScript != null)
                eyesScript.ChangeTint(imageRenderer.color);
        }






        #endregion

    } 

} // #PROJECTNAME# namespace