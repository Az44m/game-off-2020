using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class SoundCheckButton : CheckButton
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
            Pressed = _gameStateService.IsSoundOn;
            OnToggled(Pressed);
            Connect("toggled", this, nameof(OnToggled));

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.LevelOver), this, nameof(OnLevelOver));
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        private void OnToggled(bool isPressed) => _gameStateService.IsSoundOn = isPressed;
        // ReSharper disable once UnusedParameter.Local
        private void OnLevelOver(bool isWin) => Visible = false;
        private void OnGamePaused(bool isPaused) => Visible = isPaused;
    }
}
