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
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.BackToMainMenu), this, nameof(OnBackToMainMenu));
        }

        protected override void OnPressed()
        {
            if (_gameStateService.IsFirstTime)
            {
                _tutorial.Visible = true;
            }
            else
            {
                if (IsInstanceValid(_tutorial))
                    _tutorial.QueueFree();

                _gameStateService.ShowLevelSelector();
            }

            Visible = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (_gameStateService.GameState != GameState.Started
                && _gameStateService.GameState != GameState.Paused
                && @event is InputEventKey eventKey
                && eventKey.Pressed && eventKey.Scancode == (int) KeyList.Escape)
            {
                if (IsInstanceValid(_tutorial))
                {
                    _tutorial.Visible = false;
                    _tutorial.TutorialState = TutorialState.Prologue;
                }

                Visible = true;
            }
        }

        private void OnBackToMainMenu() => Visible = true;
    }
}
