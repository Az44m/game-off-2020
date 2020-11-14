using System.Linq;
using GameOff2020.Entities.Services;
using Godot;
using ResourceLoader = GameOff2020.Entities.Services.ResourceLoader;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public abstract class ObjectiveProgressBarBase : Sprite
    {
        private Timer _progressTimer;
        private ResourceLoader _resourceLoader;
        protected SignalService SignalService;
        protected GameStateService GameStateService;

        private int _value;

        protected int Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateTexture(_value);
            }
        }

        public override void _Ready()
        {
            _resourceLoader = GetNode<ResourceLoader>("/root/ResourceLoader");
            GameStateService = GetNode<GameStateService>("/root/GameStateService");
            SignalService = GetNode<SignalService>("/root/SignalService");

            _progressTimer = GetNode<Timer>("Timer");
            _progressTimer.Connect("timeout", this, nameof(Progress));

            SignalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
        }

        protected virtual void Progress() => Value++;

        protected virtual void OnGameStarted()
        {
            Value = 0;
            Progress();
            _progressTimer.Start(0);
        }

        private void UpdateTexture(int value)
        {
            if (_resourceLoader.RocketTextures.Any())
            {
                if (value < 8)
                    Texture = _resourceLoader.RocketTextures[0];
                else if (value < 100)
                    Texture = _resourceLoader.RocketTextures[(value - 7) / 4];
                else
                    Texture = _resourceLoader.RocketTextures[_resourceLoader.RocketTextures.Count - 1];
            }
        }
    }
}
