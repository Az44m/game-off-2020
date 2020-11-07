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

        private void OnGameOver() => Visible = true;
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}