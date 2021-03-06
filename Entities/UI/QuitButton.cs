using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.UI
{
    public class QuitButton : ButtonBase
    {
        private SignalService _signalService;

        public override void _Ready()
        {
            base._Ready();

            _signalService = GetNode<SignalService>("/root/SignalService");
            _signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
            _signalService.Connect(nameof(SignalService.LevelOver), this, nameof(OnLevelOver));
        }

        protected override void OnPressed() => GetTree().Quit();

        private void OnGamePaused(bool isPaused) => Visible = isPaused;
        private void OnLevelOver(bool isWin) => Visible = true;
    }
}
