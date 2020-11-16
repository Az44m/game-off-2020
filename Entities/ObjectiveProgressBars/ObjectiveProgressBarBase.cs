using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GameOff2020.Entities.Services;
using Godot;
using Directory = Godot.Directory;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public abstract class ObjectiveProgressBarBase : Sprite
    {
        private List<Texture> _rocketTextures = new List<Texture>();
        private Timer _progressTimer;
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

        protected abstract string ResourcePrefix { get; }

        public override void _Ready()
        {
            GameStateService = GetNode<GameStateService>("/root/GameStateService");
            SignalService = GetNode<SignalService>("/root/SignalService");

            _progressTimer = GetNode<Timer>("Timer");
            _progressTimer.Connect("timeout", this, nameof(Progress));

            SignalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
            LoadRocketTextures();
        }

        protected virtual void Progress() => Value++;

        protected virtual void OnGameStarted()
        {
            Value = 0;
            Progress();
            _progressTimer.Start(0);
        }

        private void LoadRocketTextures()
        {
            var rocketTextureDirectory = new Directory();
            var rocketTextureDirectoryPath = "res://Resources/Rocket";
            if (rocketTextureDirectory.Open(rocketTextureDirectoryPath) != Error.Ok)
                throw new IOException("Could not open rocket resource folder");
            if (rocketTextureDirectory.ListDirBegin(true, true) != Error.Ok)
                throw new IOException("Could not list rocket textures");


            var textureName = rocketTextureDirectory.GetNext();
            while (!string.IsNullOrEmpty(textureName))
            {
                var match = Regex.Match(textureName, $@"^{ResourcePrefix}_Rocket_Progress_(\d+).png.import$");
                if (match.Success)
                {
                    textureName = textureName.Replace(".import", string.Empty);
                    var rocketTexture = ResourceLoader.Load<Texture>($"{rocketTextureDirectoryPath}/{textureName}");
                    rocketTexture.ResourceName = match.Groups[1].Value;
                    _rocketTextures.Add(rocketTexture);
                }

                textureName = rocketTextureDirectory.GetNext();
            }

            _rocketTextures = _rocketTextures.OrderBy(x => int.Parse(x.ResourceName)).ToList();
        }

        private void UpdateTexture(int value)
        {
            if (_rocketTextures.Any())
            {
                if (value < 8)
                    Texture = _rocketTextures[0];
                else if (value < 100)
                    Texture = _rocketTextures[(value - 7) / 4];
                else
                    Texture = _rocketTextures[_rocketTextures.Count - 1];
            }
        }
    }
}
