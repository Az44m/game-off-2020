using Godot;

namespace GameOff2020.Entities.Services
{
    public enum GameState
    {
        UnStarted,
        Started,
        Paused,
        GameOver
    }

    public class GameStateService : Node
    {
        private SignalService _signalService;
        public GameState GameState { get; private set; } = GameState.UnStarted;

        public override void _Ready() => _signalService = GetNode<SignalService>("/root/SignalService");

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int) KeyList.Escape)
                PauseGame();
        }

        public void StartGame()
        {
            GameState = GameState.Started;
            _signalService.EmitSignal(nameof(SignalService.GameStarted));
        }

        public void PauseGame()
        {
            if (GameState != GameState.Paused && GameState != GameState.Started)
                return;

            GameState = GameState == GameState.Paused ? GameState.Started : GameState.Paused;
            GetTree().Paused = GameState == GameState.Paused;
            _signalService.EmitSignal(nameof(SignalService.GamePaused), GameState == GameState.Paused);
        }

        public void GameOver()
        {
            GameState = GameState.GameOver;
            _signalService.EmitSignal(nameof(SignalService.GameOver));
        }
    }
}
