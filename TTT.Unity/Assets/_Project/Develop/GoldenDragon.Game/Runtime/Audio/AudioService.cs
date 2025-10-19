using UnityEngine;
using UnityEngine.Audio;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    //Test
    public class AudioService : MonoBehaviour
    {
        private const string MusicMixerExposeName = "MusicValue"; 
        private const string SoundMixerExposeName = "SFXValue"; 
        
        [Header("Mixer")]
        [SerializeField] private AudioMixer _mixer;
        [Header("Audio Source")]
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _backgroundSource;
        [Header("Audio Clip")]
        [SerializeField] private AudioClip _background;
        [SerializeField] private AudioClip _sfx1;
        [SerializeField] private AudioClip _sfx2;

        [Header("Настройки шромкости")]
        [Range(0,1f)]
        [SerializeField] private float _musicSlider;
        [Range(0,1f)]
        [SerializeField] private float _sfxSlider;
        [SerializeField] private bool _isMuteSfx;
        [SerializeField] private bool _isMuteBacground;

        [Header("Выбор звуков")]
        [SerializeField] private bool _isPlayBackground;
        [SerializeField] private bool _isPlaySfx1;
        [SerializeField] private bool _isPlaySfx2;

        private float _currentCountSound;
        private float _currentCountMusic;
        private float _dbSound;
        private float _dbMusic;

        private void Awake()
        {
            if (_mixer.GetFloat(MusicMixerExposeName, out float valueMusic))
            {
                _musicSlider = Mathf.Pow(10f,valueMusic / 20f);
                _dbMusic = valueMusic;
            }

            if (_mixer.GetFloat(SoundMixerExposeName, out float valueSound))
            {
                _sfxSlider = Mathf.Pow(10f,valueSound / 20f);
                _dbSound = valueSound;
            }

            _currentCountMusic = _musicSlider;
            _currentCountSound = _sfxSlider;
        }


        private void Update()
        {
            if (_isPlayBackground)
            {
                _isPlayBackground = false;
                _backgroundSource.clip = _background;
                _backgroundSource.Play();
            }

            if (_isPlaySfx1)
            {
                _isPlaySfx1 = false;
                _sfxSource.PlayOneShot(_sfx1);
            }
            
            if (_isPlaySfx2)
            {
                _isPlaySfx2 = false;
                _sfxSource.PlayOneShot(_sfx2);
            }
            
            if (_currentCountSound != _sfxSlider)
            {
                _currentCountSound = _sfxSlider;
                _dbSound = _sfxSlider > 0.0001f ? Mathf.Log10(_sfxSlider) * 20f : -80f;
                _mixer.SetFloat(SoundMixerExposeName, _dbSound);
            }
            
            if (_currentCountMusic != _musicSlider)
            {
                _currentCountMusic = _musicSlider;
                _dbMusic = _musicSlider > 0.0001f ? Mathf.Log10(_musicSlider) * 20f : -80f;
                _mixer.SetFloat(MusicMixerExposeName, _dbMusic);
            }
            
            if (_isMuteSfx)
            {
                _mixer.SetFloat(SoundMixerExposeName, -80f);
            }
            else
            {
                _mixer.SetFloat(SoundMixerExposeName, _dbSound);
            }
            
            if (_isMuteBacground)
            {
                _mixer.SetFloat(MusicMixerExposeName, -80f);
            }
            else
            {
                _mixer.SetFloat(MusicMixerExposeName, _dbMusic);
            }
        }
    }
}
