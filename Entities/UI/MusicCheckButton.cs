using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class MusicCheckButton : CheckButton
    {
        private SignalService _signalService;
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            _signalService = GetNode<SignalService>("/root/SignalService");
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
            Pressed = _gameStateService.IsMusicOn;
            OnToggled(Pressed);
            Connect("toggled", this, nameof(OnToggled));
            _signalService.Connect(nameof(SignalService.GameOver), this, nameof(OnGameOver));
            _signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        private void OnToggled(bool isPressed)
        {
            _gameStateService.IsMusicOn = isPressed;
            _signalService.EmitSignal(nameof(SignalService.MusicSettingChanged), isPressed);
        }
        // ReSharper disable once UnusedParameter.Local
        private void OnGameOver(bool isWin) => Visible = false;
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}
