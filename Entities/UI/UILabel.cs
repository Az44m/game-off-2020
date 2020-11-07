using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class UILabel : Label
    {
        public override void _Ready()
        {
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GameOver), this, nameof(OnGameOver));
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        private void OnGameOver(bool isWin) => Text = isWin ? "You won" : "You lost";
        private void OnGamePaused(bool isPaused) => Text = isPaused ? "Game paused" : string.Empty;
    }
}
