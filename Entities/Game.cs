using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class Game : Node
    {
        private AudioStreamPlayer _backgroundMusicPlayer;

        public override void _Ready()
        {
            _backgroundMusicPlayer = GetNode<AudioStreamPlayer>("BackgroundMusicPlayer");
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.MusicSettingChanged), this, nameof(OnMusicSettingChanged));
        }

        private void OnMusicSettingChanged(bool isEnabled) => _backgroundMusicPlayer.Playing = isEnabled;
    }
}
