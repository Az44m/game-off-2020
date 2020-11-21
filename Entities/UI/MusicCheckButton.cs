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
        }

        private void OnToggled(bool isPressed)
        {
            _gameStateService.IsMusicOn = isPressed;
            _signalService.EmitSignal(nameof(SignalService.MusicSettingChanged), isPressed);
        }
    }
}
