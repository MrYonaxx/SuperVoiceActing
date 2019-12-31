/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageManagerPrologue class
    /// </summary>
    public class DoublageManagerPrologue : DoublageManager
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [Header("-------------------------------------------------------------")]
        [Header("Changement d'ambiance")]
        [SerializeField]
        Light lightAmbient;
        [SerializeField]
        SpriteRenderer spriteRendererCharacterDoremi;
        [SerializeField]
        SpriteRenderer spriteRendererEye;
        [SerializeField]
        RectTransform blackRectUpper;
        [SerializeField]
        RectTransform blackRectLower;

        [SerializeField]
        UnityEvent newAmbiance;
        [SerializeField]
        UnityEvent newAmbiance2;


        [SerializeField]
        AudioClip battleThemePrologue;
        bool eventCustom = false;

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

        public override void SetPhrase()
        {

            //base.SetPhrase();


           /* if (indexPhrase == 0)
            {
                ShiftAmbiance();
                //ChangeEventPhase();
            }
            else if (indexPhrase == 1)
            {
                textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Joie);
                textAppearManager.ApplyDamage(100);
                inputController.gameObject.SetActive(true);
                ShowUIButton(buttonUIY);
            }
            else if (indexPhrase == 2)
            {
                if (eventCustom == false)
                {
                    textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Tristesse);
                    enemyManager.SetHp(10);
                    textAppearManager.ApplyDamage(50);
                    eventManager.CheckEvent(indexPhrase, true, enemyManager.GetHpPercentage());
                    //ChangeEventPhase();
                    eventCustom = true;
                }
                else
                {
                    inputController.gameObject.SetActive(true);
                    ShowUIButton(buttonUIA);
                    ShowUIButton(buttonUIB);
                }
            }
            else if (indexPhrase == 3)
            {
                textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Tristesse);
                textAppearManager.ApplyDamage(100);
                inputController.gameObject.SetActive(true);
                ShowUIButton(buttonUIY);
            }
            else if (indexPhrase == 11)
            {
                textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text);
                inputController.gameObject.SetActive(true);
                skillManager.ActorSkillFeedback();
                //emotionAttackManager.SelectCard(Emotion.Confiance);
                base.SetPhrase();

            }
            else
            {
                base.SetPhrase();
            }*/

        }

        /*private IEnumerator EventPhrase2()
        {
            eventManager.CheckEvent(contrat, indexPhrase, startLine, enemyManager.GetHpPercentage());
        }*/

        private void ShiftAmbiance()
        {
            StartCoroutine(ShiftAmbianceCoroutine());
        }

        private IEnumerator ShiftAmbianceCoroutine()
        {
            AudioManager.Instance.StopMusic();
            float time = 600;
            float speedLight = 1 / time;
            float speedColor = 0.75f / time;
            Color color = new Color(1, 1, 1, 1);
            while (time != 0)
            {
                if(time == 300)
                {
                    cameraController.SetNoCameraEffect(true);
                    cameraController.CameraMovement(-6.5f,2.9f,0,0,90,0,400);
                }
                if(time < 300)
                {
                    blackRectUpper.anchoredPosition += new Vector2(0,0.25f);
                    blackRectLower.anchoredPosition -= new Vector2(0,0.25f);
                }
                color -= new Color(speedColor, speedColor, speedColor, 0);
                spriteRendererCharacterDoremi.color = color;
                spriteRendererEye.color = color;
                lightAmbient.intensity -= speedLight;
                time -= 1;
                yield return null;

            }
            yield return new WaitForSeconds(2);
            color = new Color(1, 1, 1, 1);
            spriteRendererCharacterDoremi.color = color;
            spriteRendererEye.color = color;
            newAmbiance.Invoke();
            AudioManager.Instance.PlaySound(audioClipSpotlight);
            AudioManager.Instance.PlayMusic(battleThemePrologue);
            yield return new WaitForSeconds(3);
            newAmbiance2.Invoke();
            emotionAttackManager.SwitchCardTransformIntro();
            /*enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Joie);*/
            textAppearManager.ApplyDamage(100);
            inputController.gameObject.SetActive(true);
            cameraController.enabled = true;
            cameraController.SetNoCameraEffect(false);
            cameraController.MoveToInitialPosition();
            ShowUIButton(buttonUIY);
            //SwitchToDoublage();
        }


        #endregion

    } // DoublageManagerPrologue class

} // #PROJECTNAME# namespace