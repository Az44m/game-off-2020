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
        private const string GameDataPath = "res://game_data.cfg";

        private SignalService _signalService;

        public GameState GameState { get; private set; } = GameState.UnStarted;

        //TODO Change ZIndex on gameover
        //TODO Hide sound buttons on game over
        //TODO Show quit on game over
        public bool IsMusicOn
        {
            get => getConfig("IsMusicOn");
            set => setConfig("IsMusicOn", value);
        }

        public bool IsSoundOn
        {
            get => getConfig("IsSoundOn");
            set => setConfig("IsSoundOn", value);
        }

        public override void _Ready()
        {
            var file = new File();
            if (!file.FileExists(GameDataPath))
            {
                file.Open(GameDataPath, File.ModeFlags.Write);
                file.Close();
            }

            _signalService = GetNode<SignalService>("/root/SignalService");
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int) KeyList.Escape)
                PauseGame();
        }

        public void StartGame()
        {
            GameState = GameState.Started;
            GetTree().Paused = false;
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

        public void GameOver(bool isWin)
        {
            GameState = GameState.GameOver;
            GetTree().Paused = true;
            _signalService.EmitSignal(nameof(SignalService.GameOver), isWin);
        }

        private bool getConfig(string configName)
        {
            var gameData = new ConfigFile();
            gameData.Load(GameDataPath);
            return (bool) gameData.GetValue("section", configName, true);
        }

        private void setConfig(string configName, bool isEnabled)
        {
            var gameData = new ConfigFile();
            gameData.Load(GameDataPath);
            gameData.SetValue("section", configName, isEnabled);
            gameData.Save(GameDataPath);
        }
    }
}
