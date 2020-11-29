using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class RestartButton : ButtonBase
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            base._Ready();

            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.LevelOver), this, nameof(OnLevelOver));
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        protected override void OnPressed()
        {
            _gameStateService.StartGame();
            Visible = false;
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnLevelOver(bool isWin) => Visible = true;
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}
