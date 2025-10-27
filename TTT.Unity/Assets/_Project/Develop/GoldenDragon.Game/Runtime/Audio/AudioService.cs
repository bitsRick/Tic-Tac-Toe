using Cysharp.Threading.Tasks;
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

        public ConfigSounds ConfigAudio => _config;

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

        public UniTask Load()
        {
            _config = _assetLoad.GetAsset<ConfigSounds>(TypeAsset.Audio,Constant.B.Audio.AudioConfig);

            if (_config == null) Log.Default.W($"Not Load config:{Constant.B.Audio.AudioConfig}");

            return UniTask.CompletedTask;
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
    }
}
