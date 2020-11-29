using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.UI
{
    public class NextLevelButton : ButtonBase
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            base._Ready();

            _gameStateService = GetNode<GameStateService>("/root/GameStateService");

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.LevelOver), this, nameof(OnLevelOver));
        }

        protected override void OnPressed()
        {
            _gameStateService.GoToNextLevel();
            Visible = false;
        }

        private void OnLevelOver(bool isWin) => Visible = isWin && _gameStateService.MaxAvailableLevel < 3;
    }
}
