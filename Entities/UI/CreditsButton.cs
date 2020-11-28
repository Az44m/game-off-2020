using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class CreditsButton : ButtonBase
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            base._Ready();
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
        }

        protected override void OnPressed() => _gameStateService.ShowCredits();

        private void OnGameStarted() => Visible = false;
    }
}
