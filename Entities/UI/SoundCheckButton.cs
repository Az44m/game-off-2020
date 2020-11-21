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
        }

        private void OnToggled(bool isPressed) => _gameStateService.IsSoundOn = isPressed;
    }
}
