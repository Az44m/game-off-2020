using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class UI : Control
    {
        public override void _Ready()
        {
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
            signalService.Connect(nameof(SignalService.GameOver), this, nameof(OnGameOver));
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        private void OnGameStarted() => Visible = false;

        // ReSharper disable once UnusedParameter.Local
        private void OnGameOver(bool isWin) => Visible = true;
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}
