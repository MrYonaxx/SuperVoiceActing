/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinigameMicrophone
{
	public class MinigameMicrophoneRecording : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


        [SerializeField]
        AudioSource audioSourceMicrophone;


        bool isRecording = true;
        //temporary audio vector we write to every second while recording is enabled..
        List<float> tempRecording = new List<float>();
        //list of recorded clips...
        List<float[]> recordedClips = new List<float[]>();

        string microphone;

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

        ////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected void Awake()
        {
            
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            //set up recording to last a max of 1 seconds and loop over and over
            for (int i = 0; i < Microphone.devices.Length; i++)
            {
                if (Microphone.devices[i] != null)
                {
                    microphone = Microphone.devices[i];
                    Debug.Log(Microphone.devices[i]);
                    break;
                }
            }

            audioSourceMicrophone.clip = Microphone.Start(microphone, true, 1, 44100);
            //audioSourceMicrophone.Play();
            //resize our temporary vector 
            Invoke("ResizeRecording", 1);
        }


       void ResizeRecording()
        {
            if (isRecording)
            {
                //add the next second of recorded audio to temp vector
                int length = 44100;
                float[] clipData = new float[length];
                audioSourceMicrophone.clip.GetData(clipData, 0);
                tempRecording.AddRange(clipData);
                Invoke("ResizeRecording", 1);
            }
        }


        void Update()
        {
            //space key triggers recording to start...
            if (Input.GetKeyDown("space"))
            {
                isRecording = !isRecording;
                Debug.Log(isRecording == true ? "Is Recording" : "Off");

                if (isRecording == false)
                {
                    //stop recording, get length, create a new array of samples
                    int length = Microphone.GetPosition(microphone);
                    Debug.Log(length);

                    Microphone.End(microphone);
                    audioSourceMicrophone.Play();
                    float[] clipData = new float[length];
                    if(length != 0)
                        audioSourceMicrophone.clip.GetData(clipData, 0);

                    //create a larger vector that will have enough space to hold our temporary
                    //recording, and the last section of the current recording
                    float[] fullClip = new float[clipData.Length + tempRecording.Count];
                    for (int i = 0; i < fullClip.Length; i++)
                    {
                        //write data all recorded data to fullCLip vector
                        if (i < tempRecording.Count)
                            fullClip[i] = tempRecording[i];
                        else
                            fullClip[i] = clipData[i - tempRecording.Count];
                    }

                    recordedClips.Add(fullClip);
                    audioSourceMicrophone.clip = AudioClip.Create("recorded samples", fullClip.Length, 1, 44100, false);
                    audioSourceMicrophone.clip.SetData(fullClip, 0);
                    audioSourceMicrophone.loop = true;

                }
                else
                {
                    //stop audio playback and start new recording...
                    audioSourceMicrophone.Stop();
                    tempRecording.Clear();
                    Microphone.End(null);
                    audioSourceMicrophone.clip = Microphone.Start(microphone, true, 1, 44100);
                    Invoke("ResizeRecording", 1);
                }

            }

            //use number keys to switch between recorded clips, start from 1!!
            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown("" + i))
                {
                    SwitchClips(i - 1);
                }
            }

        }

        //chooose which clip to play based on number key..
        void SwitchClips(int index)
        {
            if (index < recordedClips.Count)
            {
                Debug.Log("je joue" + index);
                audioSourceMicrophone.Stop();
                int length = recordedClips[index].Length;
                audioSourceMicrophone.clip = AudioClip.Create("recorded samples", length, 1, 44100, false);
                audioSourceMicrophone.clip.SetData(recordedClips[index], 0);
                audioSourceMicrophone.loop = true;
                audioSourceMicrophone.Play();
            }
        }

        #endregion

    } // MicrophoneRecording class
	
}// #PROJECTNAME# namespace
