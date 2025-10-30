using System;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
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
        private float _currentCountSound;
        private float _currentCountMusic;
        private float _dbSound;
        private float _dbMusic;
        private AudioClip _backgroundMeta;
        private AudioClip _buttonClick;
        private AudioClip _sfx2;
        private AssetLoad _assetLoad;
        private ConfigSounds _config;
        private bool _isMuteSfx;
        private bool _isMuteMusic;

        public ConfigSounds ConfigAudio => _config;

        public bool IsMuteSound => _isMuteSfx;
        public bool IsMuteMusic => _isMuteMusic;
        public float CurrentVolumeSound => _currentCountSound;
        public float CurrentVolumeMusic => _currentCountMusic;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        [Inject]
        public void Contructor(AssetLoad assetLoad)
        {
            _assetLoad = assetLoad;
        }

        public void InitializedDefault()
        {
            _currentCountSound = GetValueMixerAudio(Constant.U.Audio.SoundMixerExposeName);
            _currentCountMusic = GetValueMixerAudio(Constant.U.Audio.MusicMixerExposeName);
        }
        
        public void ChangeValue(float value, TypeValueChange type)
        {
            switch (type)
            {
                case TypeValueChange.Sound:
                    if (_currentCountSound != value)
                    {
                        _currentCountSound = value;
                        _dbSound = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
                        _mixer.SetFloat(Constant.U.Audio.SoundMixerExposeName, _dbSound);
                    }
                    break;
                case TypeValueChange.Music:
                    if (_currentCountMusic != value)
                    {
                        _currentCountMusic = value;
                        _dbMusic = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
                        _mixer.SetFloat(Constant.U.Audio.MusicMixerExposeName, _dbMusic);
                    }
                    break;
            }
        }

        public void SetMute(TypeValueChange type)
        {
            switch (type)
            {
                case TypeValueChange.Sound:
                    ChangeMute(Constant.U.Audio.SoundMixerExposeName,ref _dbSound, ref _isMuteSfx);
                    break;
                
                case TypeValueChange.Music:
                    ChangeMute(Constant.U.Audio.MusicMixerExposeName, ref _dbMusic, ref _isMuteMusic);
                    break;
            }
        }

        public UniTask Load()
        {
            _config = _assetLoad.GetAsset<ConfigSounds>(TypeAsset.Audio,Constant.B.Audio.AudioConfig);
            
            if (_config == null) Log.Default.W($"Not Load config:{Constant.B.Audio.AudioConfig}");

            return UniTask.CompletedTask;
        }

        private float GetValueMixerAudio(string musicMixerExposeName)
        {
            if (_mixer.GetFloat(musicMixerExposeName, out float valueMusic))
            {
                return valueMusic;
            }

            return 0;
        }

        public float GetSliderValue(TypeValueChange type)
        {
            switch (type)
            {
                case TypeValueChange.Sound:
                    return Mathf.Pow(10f, GetValueMixerAudio(Constant.U.Audio.SoundMixerExposeName) / 20f);

                case TypeValueChange.Music:
                    return Mathf.Pow(10f, GetValueMixerAudio(Constant.U.Audio.MusicMixerExposeName) / 20f);
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
    }
}
