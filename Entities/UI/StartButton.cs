using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.UI
{
    public class StartButton : ButtonBase
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            base._Ready();
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
        }

        protected override void OnPressed()
        {
            _gameStateService.StartGame();
            Visible = false;
        }
    }
}
