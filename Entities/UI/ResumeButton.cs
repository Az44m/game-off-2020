using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.UI
{
    public class ResumeButton : ButtonBase
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            base._Ready();

            _gameStateService = GetNode<GameStateService>("/root/GameStateService");

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        protected override void OnPressed() => _gameStateService.PauseGame();
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}
