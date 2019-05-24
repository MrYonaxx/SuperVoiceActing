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

namespace VoiceActing
{
	public class MenuActorsAudition : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        Animator animatorAudition;
        [SerializeField]
        Animator animatorAuditionInfo;
        [SerializeField]
        CameraBureau camBureau;


        [SerializeField]
        GameObject actorsSpriteOutline;
        [SerializeField]
        GameObject actorsInfo;
        [SerializeField]
        GameObject actorsSkills;

        [SerializeField]
        GameObject auditionInfo;
        [SerializeField]
        GameObject auditionMicro;

        [SerializeField]
        Image actorsSprite;

        [SerializeField]
        AudioClip auditionSpotlight;

        [SerializeField]
        MenuActorsManager menuActorsManager;

        [SerializeField]
        InputController inputActor;
        [SerializeField]
        InputController inputAudition;

        VoiceActor va;

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

        public void Audition(Role role)
        {
            va = playerData.GachaVoiceActors(role);
            actorsSprite.sprite = va.ActorSprite;
            actorsSprite.SetNativeSize();
        }

        public void DrawAuditionPanel(bool b)
        {
            actorsSpriteOutline.SetActive(!b);
            actorsInfo.SetActive(!b);
            actorsSkills.SetActive(!b);

            auditionMicro.SetActive(b);
            auditionInfo.SetActive(b);

            if (b == true)
                actorsSprite.enabled = false;
            else
                actorsSprite.enabled = true;

        }

        public void AnimAudition(Role roleToAudition)
        {
            AudioManager.Instance.StopMusic(120);
            camBureau.transform.eulerAngles = new Vector3(camBureau.transform.eulerAngles.x, camBureau.transform.eulerAngles.y, 0);
            camBureau.ChangeOrthographicSize(-30, 300);
            camBureau.HideHUD();
            actorsSprite.enabled = true;
            animatorAudition.enabled = true;
            animatorAudition.SetTrigger("Audition");
            animatorAuditionInfo.SetTrigger("Disappear");
            inputActor.gameObject.SetActive(false);


            Audition(roleToAudition);


        }


        public void EndAnimAudition()
        {
            animatorAuditionInfo.gameObject.SetActive(false);
            menuActorsManager.StopGachaEffect(va);
            inputAudition.gameObject.SetActive(true);
            AudioManager.Instance.PlaySound(auditionSpotlight);
        }



        #endregion

    } // MenuActorsAudition class
	
}// #PROJECTNAME# namespace
