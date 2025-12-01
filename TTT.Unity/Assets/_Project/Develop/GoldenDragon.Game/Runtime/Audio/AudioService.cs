using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    public class AudioService : MonoBehaviour,ILoadUnit
    {
        [Header("Mixer")]
        [SerializeField] private AudioMixer _mixer;
        
        [Header("Audio Source")]
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _backgroundSource;
        
        private AudioClip _backgroundMeta;
        private AudioClip _buttonClick;
        private AudioClip _sfx2;
        private AssetService _assetService;
        private ConfigSounds _config;
        private float _currentCountSound;
        private float _currentCountMusic;
        private float _dbSound;
        private float _dbMusic;
        private bool _isMuteSfx;
        private bool _isMuteMusic;

        public ConfigSounds ConfigAudio => _config;
        public bool IsMuteSound => _isMuteSfx;
        public bool IsMuteMusic => _isMuteMusic;

        private void Awake() => DontDestroyOnLoad(this);

        [Inject]
        public void Resolve(AssetService assetService) => _assetService = assetService;

        public void InitializedDefaultAudioSetting()
        {
            _currentCountSound = GetValueMixerAudio(RuntimeConstants.Audio.SoundMixerExposeName);
            _currentCountMusic = GetValueMixerAudio(RuntimeConstants.Audio.MusicMixerExposeName);
        }
        
        public void ChangeValue(float value, TypeValueChange type)
        {
            switch (type)
            {
                case TypeValueChange.Sound:
                    if (_currentCountSound != value)
                        SetValue(RuntimeConstants.Audio.SoundMixerExposeName, value, ref _currentCountSound, ref _dbSound);
                    break;
                
                case TypeValueChange.Music:
                    if (_currentCountMusic != value)
                        SetValue(RuntimeConstants.Audio.MusicMixerExposeName, value, ref _currentCountMusic, ref _dbMusic);
                    break;
            }
        }

        public void SetMute(TypeValueChange type)
        {
            switch (type)
            {
                case TypeValueChange.Sound:
                    ChangeMute(RuntimeConstants.Audio.SoundMixerExposeName,ref _dbSound, ref _isMuteSfx);
                    break;
                
                case TypeValueChange.Music:
                    ChangeMute(RuntimeConstants.Audio.MusicMixerExposeName, ref _dbMusic, ref _isMuteMusic);
                    break;
            }
        }

        public UniTask Load()
        {
            _config = _assetService.Load.GetAsset<ConfigSounds>(TypeAsset.Audio,RuntimeConstants.Audio.AudioConfig);
            
            if (_config == null) Log.Default.W($"Not Load config:{RuntimeConstants.Audio.AudioConfig}");

            return UniTask.CompletedTask;
        }

        private float GetValueMixerAudio(string musicMixerExposeName)
        {
            if (_mixer.GetFloat(musicMixerExposeName, out float valueMusic))
                return valueMusic;

            return 0;
        }

        public float GetSliderValue(TypeValueChange type)
        {
            switch (type)
            {
                case TypeValueChange.Sound:
                    return Mathf.Pow(10f, GetValueMixerAudio(RuntimeConstants.Audio.SoundMixerExposeName) / 20f);

                case TypeValueChange.Music:
                    return Mathf.Pow(10f, GetValueMixerAudio(RuntimeConstants.Audio.MusicMixerExposeName) / 20f);
            }

            return 0;
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
                _sfxSource.PlayOneShot(clip);
            else
                Log.Default.W($"Not yet sfx:{clip}");
        }

        public void PlayBackground(AudioClip clip)
        {
            if (clip != null)
            {
                _backgroundSource.clip = clip;
                _backgroundSource.Play();
            }
            else
            {
                Log.Default.W($"Not yet bacground:{clip}");
            }
        }

        private void ChangeMute(string soundMixerExposeName, ref float db, ref bool isMute)
        {
            if (isMute == false)
            {
                isMute = true;
                db = GetValueMixerAudio(soundMixerExposeName);
                _mixer.SetFloat(soundMixerExposeName, -80f);
            }
            else
            {
                isMute = false;
                _mixer.SetFloat(soundMixerExposeName, db);
            }
        }

        private void SetValue(string soundMixerExposeName, float value,ref float currentAudio, ref float dbAudio)
        {
            currentAudio = value;
            dbAudio = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
            _mixer.SetFloat(soundMixerExposeName, dbAudio);
        }
    }
}
