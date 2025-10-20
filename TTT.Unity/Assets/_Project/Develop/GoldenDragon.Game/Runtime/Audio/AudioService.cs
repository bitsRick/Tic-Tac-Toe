using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    public class AudioService : MonoBehaviour
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
        private List<AudioContainer> _audioContainers = new List<AudioContainer>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        // private void Awake()
        // {
        //     DontDestroyOnLoad(this);
        //     
        //     if (_mixer.GetFloat(Constant.U.Audio.MusicMixerExposeName, out float valueMusic))
        //     {
        //         _musicSlider = Mathf.Pow(10f,valueMusic / 20f);
        //         _dbMusic = valueMusic;
        //     }
        //
        //     if (_mixer.GetFloat(Constant.U.Audio.SoundMixerExposeName, out float valueSound))
        //     {
        //         _sfxSlider = Mathf.Pow(10f,valueSound / 20f);
        //         _dbSound = valueSound;
        //     }
        //
        //     _currentCountMusic = _musicSlider;
        //     _currentCountSound = _sfxSlider;
        // }
        //
        //
        // private void Update()
        // {
        //     if (_isPlayBackground)
        //     {
        //         _isPlayBackground = false;
        //         _backgroundSource.clip = _backgroundMeta;
        //         _backgroundSource.Play();
        //     }
        //
        //     if (_isPlaySfx1)
        //     {
        //         _isPlaySfx1 = false;
        //         _sfxSource.PlayOneShot(_buttonClick);
        //     }
        //     
        //     if (_isPlaySfx2)
        //     {
        //         _isPlaySfx2 = false;
        //         _sfxSource.PlayOneShot(_sfx2);
        //     }
        //     
        //     if (_currentCountSound != _sfxSlider)
        //     {
        //         _currentCountSound = _sfxSlider;
        //         _dbSound = _sfxSlider > 0.0001f ? Mathf.Log10(_sfxSlider) * 20f : -80f;
        //         _mixer.SetFloat(Constant.U.Audio.SoundMixerExposeName, _dbSound);
        //     }
        //     
        //     if (_currentCountMusic != _musicSlider)
        //     {
        //         _currentCountMusic = _musicSlider;
        //         _dbMusic = _musicSlider > 0.0001f ? Mathf.Log10(_musicSlider) * 20f : -80f;
        //         _mixer.SetFloat(Constant.U.Audio.MusicMixerExposeName, _dbMusic);
        //     }
        //     
        //     if (_isMuteSfx)
        //     {
        //         _mixer.SetFloat(Constant.U.Audio.SoundMixerExposeName, -80f);
        //     }
        //     else
        //     {
        //         _mixer.SetFloat(Constant.U.Audio.SoundMixerExposeName, _dbSound);
        //     }
        //     
        //     if (_isMuteBacground)
        //     {
        //         _mixer.SetFloat(Constant.U.Audio.MusicMixerExposeName, -80f);
        //     }
        //     else
        //     {
        //         _mixer.SetFloat(Constant.U.Audio.MusicMixerExposeName, _dbMusic);
        //     }
        // }

        [Inject]
        public void Contructor(AssetLoad assetLoad)
        {
            _assetLoad = assetLoad;
        }

        public UniTask LoadAudioBootstrap()
        {
            _audioContainers.Add(new AudioContainer(
                Constant.B.Audio.AudioClipButtonClick,
                _assetLoad.GetAsset<AudioClip>(TypeAsset.Audio, Constant.B.Audio.AudioClipButtonClick),
                TypeSceneAudio.Bootstrap));
            return UniTask.CompletedTask;
        }
        
        public UniTask LoadAudioMeta()
        {
            _audioContainers.Add(new AudioContainer(
                Constant.B.Audio.AudioClipBackgroundMeta,
                _assetLoad.GetAsset<AudioClip>(TypeAsset.Audio, Constant.B.Audio.AudioClipBackgroundMeta),
                TypeSceneAudio.Meta));
            return UniTask.CompletedTask;
        }

        public UniTask LoadAudioMatch()
        {
            return UniTask.CompletedTask;
        }
        
        public void PlaySFX(string path)
        {
            var audioContainer = _audioContainers.FirstOrDefault(container => container.Key == path);

            if (audioContainer != null)
                _sfxSource.PlayOneShot(audioContainer.AudioClip);
            else
                Log.Default.W($"Not yet sfx:{path}");
        }

        public void PlayBackground(string path)
        {
            var audioContainer = _audioContainers.FirstOrDefault(container => container.Key == path);

            if (audioContainer != null)
            {
                _backgroundSource.clip = audioContainer.AudioClip;
                _backgroundSource.Play();
            }
            else
            {
                Log.Default.W($"Not yet bacground:{path}");
            }
        }
        
        public void Release(TypeSceneAudio releaseSceneAudio)
        {
            switch (releaseSceneAudio)
            {
                case TypeSceneAudio.Bootstrap:
                    break;
                case TypeSceneAudio.Meta:
                    break;
                case TypeSceneAudio.Match:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(releaseSceneAudio), releaseSceneAudio, null);
            }
        }

        private void OnDestroy()
        {
            
        }
    }
}
