using Godot;

namespace GameOff2020.Entities.Services
{
    public enum GameState
    {
        UnStarted,
        Started,
        Paused,
        Credits,
        LevelSelector,
        GameOver
    }

    public class GameStateService : Node
    {
        private const string GameDataPath = "res://game_data.cfg";

        private SignalService _signalService;

        public GameState GameState { get; private set; } = GameState.UnStarted;

        public bool IsMusicOn
        {
            get => getConfig("IsMusicOn", true);
            set => setConfig("IsMusicOn", value);
        }

        public bool IsSoundOn
        {
            get => getConfig("IsSoundOn", true);
            set => setConfig("IsSoundOn", value);
        }

        public bool IsFirstTime
        {
            get => getConfig("IsFirstTime", true);
            set => setConfig("IsFirstTime", value);
        }

        public int CurrentLevel
        {
            get => getConfig("CurrentLevel", 1);
            set => setConfig("CurrentLevel", value);
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
            if ((GameState == GameState.Started || GameState == GameState.Paused)
                && @event is InputEventKey eventKey
                && eventKey.Pressed
                && eventKey.Scancode == (int) KeyList.Escape)
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

        public void ShowCredits()
        {
            GameState = GameState.Credits;
            GetNode<Control>("/root/Main/Credits").Visible = true;
        }

        public void ShowLevelSelector()
        {
            GameState = GameState.LevelSelector;
            GetNode<Control>("/root/Main/LevelSelector").Visible = true;
        }

        public void BackToMainMenu()
        {
            GameState = GameState.UnStarted;
            GetNode<Control>("/root/Main/Credits").Visible = false;
            GetNode<Control>("/root/Main/LevelSelector").Visible = false;
            _signalService.EmitSignal(nameof(SignalService.BackToMainMenu));
        }

        public void GameOver(bool isWin)
        {
            GameState = GameState.GameOver;
            GetTree().Paused = true;
            _signalService.EmitSignal(nameof(SignalService.GameOver), isWin);
        }

        private T getConfig<T>(string configName, T defaultValue)
        {
            var gameData = new ConfigFile();
            gameData.Load(GameDataPath);
            return (T) gameData.GetValue("section", configName, defaultValue);
        }

        private void setConfig<T>(string configName, T value)
        {
            var gameData = new ConfigFile();
            gameData.Load(GameDataPath);
            gameData.SetValue("section", configName, value);
            gameData.Save(GameDataPath);
        }
    }
}
