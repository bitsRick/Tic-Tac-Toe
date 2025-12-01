using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    public class AudioPlayer:Singleton<AudioPlayer>
    {
        private static AudioService _audioService;
        private static IPlayerProgress _playerProgress;
        
        public void Resolve(AudioService audioService, IPlayerProgress playerProgress)
        {
            _audioService = audioService;
            _playerProgress = playerProgress;
        }

        public void Initialized()
        {
            PlayerData data = _playerProgress.PlayerData;
            
            if (data == null)
            {
                _audioService.InitializedDefaultAudioSetting();
                return;
            }

            AudioSetting setting = data.AudioSetting ?? new AudioSetting(false, false, 1f, 1f);

            _audioService.ChangeValue(setting.VolumeMusic,TypeValueChange.Music);
            _audioService.ChangeValue(setting.VolumeSound,TypeValueChange.Sound);

            if (setting.IsMusicMute) _audioService.SetMute(TypeValueChange.Music);
            if (setting.IsSoundMute) _audioService.SetMute(TypeValueChange.Sound);
        }

        public void Click()
        {
            _audioService.PlaySFX(_audioService.ConfigAudio.Click);
        }

        public float LoadSliderValue(TypeValueChange typeValue)
        {
            PlayerData data = _playerProgress.PlayerData;

            switch (typeValue)
            {
                case TypeValueChange.Sound:
                    if (data.AudioSetting.IsSoundMute == false)
                        _audioService.ChangeValue(data.AudioSetting.VolumeSound,TypeValueChange.Sound);
                    break;
                
                case TypeValueChange.Music:
                    if (data.AudioSetting.IsMusicMute == false)
                        _audioService.ChangeValue(data.AudioSetting.VolumeMusic, TypeValueChange.Music);
                    break;
            }
            
            return GetSliderValue(typeValue);
        }

        public void MetaBackground()
        {
            _audioService.PlayBackground(_audioService.ConfigAudio.MetaBackground);
        }

        public void Mute(TypeValueChange type)
        {
            _audioService.SetMute(type);
            
            switch (type)
            {
                case TypeValueChange.Sound:
                    _playerProgress.PlayerData.AudioSetting.IsSoundMute = _audioService.IsMuteSound;
                    break;
                
                case TypeValueChange.Music:
                    _playerProgress.PlayerData.AudioSetting.IsMusicMute = _audioService.IsMuteMusic;
                    break;
            }
        }

        public void ChangeValue(float value, TypeValueChange type)
        {
            _audioService.ChangeValue(value, type);

            switch (type)
            {
                case TypeValueChange.Sound:
                    _playerProgress.PlayerData.AudioSetting.VolumeSound = value;
                    break;
                
                case TypeValueChange.Music:
                    _playerProgress.PlayerData.AudioSetting.VolumeMusic = value;
                    break;
            }
        }

        public float GetSliderValue(TypeValueChange music)
        {
            return _audioService.GetSliderValue(music);
        }
    }
}