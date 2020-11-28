using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class LevelButton : ButtonBase
    {
        private GameStateService _gameStateService;

        [Export] public int Level { get; set; }

        public override void _Ready()
        {
            base._Ready();
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
            Disabled = Level == 0 || Level > _gameStateService.MaxAvailableLevel;
        }

        protected override void OnPressed()
        {
            GetNode<Control>("/root/Main/LevelSelector").Visible = false;
            _gameStateService.StartGame();
            _gameStateService.CurrentLevel = Level;
        }
    }
}
