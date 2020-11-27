using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class CreditsBack : ButtonBase
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            base._Ready();
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
        }

        protected override void OnPressed()
        {
            _gameStateService.BackToMainMenu();
            GetNode<Control>("/root/Main/Credits").Visible = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int) KeyList.Escape)
                OnPressed();
        }
    }
}
