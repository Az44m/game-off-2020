using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.UI
{
    public class RestartButton : StartButton
    {
        public override void _Ready()
        {
            base._Ready();

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GameOver), this, nameof(OnGameOver));
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnGameOver(bool isWin) => Visible = true;
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}
