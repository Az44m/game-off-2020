using GameOff2020.Entities.Services;
using GameOff2020.Entities.Tutorial;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class StartButton : ButtonBase
    {
        private GameStateService _gameStateService;
        private Tutorial.Tutorial _tutorial;

        public override void _Ready()
        {
            base._Ready();
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
            _tutorial = GetNode<Tutorial.Tutorial>("/root/Main/Tutorial");
        }

        protected override void OnPressed()
        {
            if (_gameStateService.IsFirstTime)
            {
                _tutorial.Visible = true;
            }
            else
            {
                _tutorial?.QueueFree();
                _gameStateService.StartGame();
            }

            Visible = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int) KeyList.Escape)
            {
                _tutorial.Visible = false;
                Visible = true;
                _tutorial.TutorialState = TutorialState.Prologue;
            }
        }
    }
}
