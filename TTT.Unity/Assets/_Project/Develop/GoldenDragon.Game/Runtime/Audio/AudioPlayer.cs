namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio
{
    public class AudioPlayer
    {
        private readonly AudioService _audioService;

        public AudioPlayer(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void Click()
        {
            _audioService.PlaySFX(_audioService.ConfigAudio.Click);
        }

        public void MetaBackground()
        {
            _audioService.PlayBackground(_audioService.ConfigAudio.MetaBackground);
        }
    }
}