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
using UnityEngine.PostProcessing;

namespace VoiceActing
{
	public class EventBackgroundManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Transform defaultEnvironnement;
        [SerializeField]
        Transform newEnvironnementParent;
        [SerializeField]
        Transform UIParent;

        [SerializeField]
        PostProcessingBehaviour cameraPostProcess;

        [SerializeField]
        Image imageDefaultVignettage;
        [SerializeField]
        Image imageTransition;

        [SerializeField]
        Color ambientDefaultLight;
        [SerializeField]
        Color ambientDefaultFog;
        [SerializeField]
        float ambiantDefaultFogDensity;



        [SerializeField]
        SimpleSpectrum simpleSpectrum;


        List<GameObject> listObjectInstantiate = new List<GameObject>();
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



        public void InstantiateNewObject(DoublageEventInstantiate data)
        {
            if (data.TransitionForNewBackground == true)
            {
                StartCoroutine(TransitionCoroutine(data));
            }
            else
            {
                if (data.ObjectToInstantiate != null)
                {
                    if (data.InstantiateUI == false)
                        listObjectInstantiate.Add(Instantiate(data.ObjectToInstantiate, newEnvironnementParent));
                    else
                        listObjectInstantiate.Add(Instantiate(data.ObjectToInstantiate, UIParent));
                }

                if (data.NewPostProcess != null)
                    ChangePostProcess(data.NewPostProcess);
                if (data.ChangeSceneRenderSetting == true)
                {
                    RenderSettings.skybox = data.NewSkyBox;
                    ChangeAmbientSettings(data.NewAmbientColor, data.NewFogColor, data.NewFogDensity, data.TimeTransitionRender);
                    if (data.NewSkyBox != null)
                    {
                        RenderSettings.skybox = data.NewSkyBox;
                    }
                }
                simpleSpectrum.gameObject.SetActive(data.ActiveBaseSpectrum);
            }
        }

        private void ChangeAmbientSettings(Color newAmbientColor, Color newFogColor, float newFogDensity, int changeTime)
        {
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, newAmbientColor, changeTime * Time.deltaTime);
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, newFogColor, changeTime * Time.deltaTime);
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, newFogDensity, changeTime * Time.deltaTime);
        }

        private void ChangePostProcess(PostProcessingProfile newPostProfile)
        {
            cameraPostProcess.profile = newPostProfile;
        }

        private IEnumerator TransitionCoroutine(DoublageEventInstantiate data)
        {
            int time = data.TimeTransitionForNewBackground;
            Color speed = new Color(0, 0, 0, 1f / time);
            while(time != 0)
            {
                imageTransition.color += speed;
                time -= 1;
                yield return null;
            }

            listObjectInstantiate.Add(Instantiate(data.ObjectToInstantiate, newEnvironnementParent));
            defaultEnvironnement.gameObject.SetActive(false);
            if (data.NewPostProcess != null)
                ChangePostProcess(data.NewPostProcess);
            if (data.ChangeSceneRenderSetting == true)
            {
                ChangeAmbientSettings(data.NewAmbientColor, data.NewFogColor, data.NewFogDensity, data.TimeTransitionRender);
                if (data.NewSkyBox != null)
                {
                    RenderSettings.skybox = data.NewSkyBox;
                }
            }
            simpleSpectrum.gameObject.SetActive(data.ActiveBaseSpectrum);

            while (time != data.TimeTransitionForNewBackground)
            {
                imageTransition.color -= speed;
                imageDefaultVignettage.color -= speed;
                time += 1;
                yield return null;
            }
        }

        #endregion

    } // EventBackgroundManager class
	
}// #PROJECTNAME# namespace
