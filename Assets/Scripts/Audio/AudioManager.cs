using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        [Space(20)]
        public AudioMixer audioMixer;
        [Space]
        [SerializeField] private bool masterMuted;
        [SerializeField] private bool sfxMuted;
        [SerializeField] private bool musicMuted;
        [SerializeField] private bool uiMuted;
    
        public static AudioManager Instance;

        private bool _coroutineIsRunning;
    
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.audioClip;

                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
            }
        }

        private void Start()
        {
            Play("MainTheme");
        }

        public void ToggleAudioMaster()
        {
            masterMuted = !masterMuted;
            audioMixer.SetFloat("MasterVolume", -80f * Convert.ToInt16(masterMuted));
        }

        public void ToggleAudioMusic()
        {
            musicMuted = !musicMuted;
            audioMixer.SetFloat("MusicVolume", -80f * Convert.ToInt16(musicMuted));
        }

        public void ToggleAudioSfx()
        {
            sfxMuted = !sfxMuted;
            audioMixer.SetFloat("SfxVolume", -80f * Convert.ToInt16(sfxMuted));
        }

        public void ToggleAudioUi()
        {
            uiMuted = !uiMuted;
            audioMixer.SetFloat("UiVolume", -80f * Convert.ToInt16(uiMuted));
        }

        public void Play(string soundName)
        {
            Sound sound = FindSound(soundName);
            sound.source.volume = sound.volume;
            sound.source.Play();
        }
    
        public void Stop(string soundName)
        {
            Sound sound = FindSound(soundName);
            sound.source.Stop();
        }
    
        public void SetVolume(string soundName, float volume)
        {
            Sound sound = FindSound(soundName);
            sound.volume = volume;
            sound.source.volume = volume;
        }

        public void MuteSoundUntilOtherIsFinish(string muteSoundName, string otherSoundName)
        {
            StartCoroutine(MuteSoundCoroutine(muteSoundName, otherSoundName));
        }

        IEnumerator MuteSoundCoroutine(string muteSoundName, string otherSoundName)
        {
            if (_coroutineIsRunning) yield break;
            _coroutineIsRunning = true;
            float lastVolume = FindSound(muteSoundName).volume;
            Sound otherSound = FindSound(otherSoundName);
            float duration = otherSound.audioClip.length / otherSound.pitch;
            SetVolume(muteSoundName, 0f);
            yield return new WaitForSecondsRealtime(duration);
            SetVolume(muteSoundName, lastVolume);
            _coroutineIsRunning = false;
        }

        private Sound FindSound(string soundName)
        {
            Sound sound = Array.Find(sounds, s => s.name == soundName);
            if (sound != null) return sound;
            Debug.LogError("Sound " + name + " not found...");
            return null;
        }
    }
}
