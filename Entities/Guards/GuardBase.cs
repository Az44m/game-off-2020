using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.Guards
{
    public abstract class GuardBase : AnimatedSprite
    {
        private AudioStreamPlayer2D _exposedSpyAudioPlayer;
        private GameStateService _gameStateService;
        protected SignalService SignalService { get; private set; }

        public override void _Ready()
        {
            SignalService = GetNode<SignalService>("/root/SignalService");
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");

            _exposedSpyAudioPlayer = GetNode<AudioStreamPlayer2D>("ExposedSpyAudioPlayer");
        }

        protected async void OnSpyExposed(SpyBase spy)
        {
            if (Frame == 1)
                return;

            if (_gameStateService.IsSoundOn)
                _exposedSpyAudioPlayer.Play();

            Frame = 1;
            spy.RunAway();
            await ToSignal(GetTree().CreateTimer(1, false), "timeout");
            Frame = 0;
        }
    }
}
