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
    /// Definition of the AudioManager class
    /// </summary>
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;
        private AudioSource audio;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                audio = GetComponent<AudioSource>();
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        // ===========================================================================================
        // Musique

        public void PlayMusic(AudioClip music)
        {
            audio.clip = music;
            audio.Play();
            StartCoroutine(PlayMusicCoroutine(audio));
        }

        private IEnumerator PlayMusicCoroutine(AudioSource audio)
        {
            while (audio.volume < 1)
            {
                audio.volume += 0.01f;
                yield return null;
            }
            audio.volume = 1;
        }

        public void StopMusic()
        {
            StartCoroutine(StopMusicCoroutine(audio));
        }

        private IEnumerator StopMusicCoroutine(AudioSource audio)
        {
            while(audio.volume > 0)
            {
                audio.volume -= 0.01f;
                yield return null;
            }
            audio.volume = 0;
        }

        // ===========================================================================================
        // Son


        public void PlaySound(AudioClip sound)
        {
            audio.PlayOneShot(sound);
        }


    } // AudioManager class

} // #PROJECTNAME# namespace