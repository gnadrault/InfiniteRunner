using Data;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private SfxDatabase sfxDatabase;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxOneShotSource;
        [SerializeField] private AudioSource sfxLettersSource;
        [SerializeField] private AudioSource sfxLoopSource;

        private SfxType? _currentLoopType;
        private static readonly float[] ScalePitchLetter = { 1f, 1.075f, 1.15f, 1.225f, 1.3f };
        private static readonly string VolumeMusicPlayerPref = "MusicVolume";
        private static readonly string VolumeSfxPlayerPref = "SfxVolume";

        public float MusicVolume { get; private set; } = 1f;
        public float SfxVolume { get; private set; } = 1f;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                MusicVolume = PlayerPrefs.GetFloat(VolumeMusicPlayerPref, 1f);
                SfxVolume = PlayerPrefs.GetFloat(VolumeSfxPlayerPref, 1f);
                ApplyVolumes();
            }
            else
                Destroy(this);
        }
        
        public void PlayMusic(AudioClip clip)
        {
            if (musicSource.clip == clip && musicSource.isPlaying)
                return;
            
            musicSource.clip = clip;
            musicSource.volume = MusicVolume;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            musicSource.volume = volume;
            PlayerPrefs.SetFloat(VolumeMusicPlayerPref, volume);
        }
        
        public void SetMusicPitch(float pitch)
        {
            musicSource.pitch = pitch;
        }

        public void SetSfxVolume(float volume)
        {
            SfxVolume = volume;
            sfxOneShotSource.volume = volume;
            sfxLoopSource.volume = volume;
            PlayerPrefs.SetFloat(VolumeSfxPlayerPref, volume);
        }

        private void ApplyVolumes()
        {
            musicSource.volume = MusicVolume;
            sfxOneShotSource.volume = SfxVolume;
            sfxLoopSource.volume = SfxVolume;
        }

        public void PlayOneShot(SfxType type, AudioSource source = null)
        {
            var entry = sfxDatabase.Get(type);
            if (entry == null) return;
            
            source ??= sfxOneShotSource;
            source.PlayOneShot(entry.clip, entry.volume);
        }

        public void PlayLetterSound(string letter)
        {
            int position = char.ToUpper(letter[0]) - 'A';
            int index = (ScalePitchLetter.Length - 1) - (position % ScalePitchLetter.Length);
            sfxLettersSource.pitch = ScalePitchLetter[index];
            PlayOneShot(SfxType.LetterCollect, sfxLettersSource);
        }

        public void PlayLoop(SfxType type)
        {
            var entry = sfxDatabase.Get(type);
            if (entry == null) return;

            _currentLoopType = type;
            sfxLoopSource.clip = entry.clip;
            sfxLoopSource.volume = entry.volume;
            sfxLoopSource.loop = true;
            sfxLoopSource.Play();
        }

        public void StopLoop(SfxType type)
        {
            if (_currentLoopType != type) return;
            sfxLoopSource.Stop();
            _currentLoopType = null;
        }

        public void StopAll()
        {
            musicSource.Stop();
            sfxLoopSource.Stop();
        }
    }
}