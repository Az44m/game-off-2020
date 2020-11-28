using System;
using System.Linq;
using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.SpaceCenters
{
    public class AISpaceCenter : SpaceCenterBase<PlayerSpy>
    {
        private Random _random;
        private Timer _exposureTimer;
        private Timer _spyTimer;
        private LevelService _levelService;

        protected override string EnterSignalName { get; } = nameof(SignalService.PlayerSpyEntered);

        public override void _Ready()
        {
            base._Ready();
            _random = new Random();
            _levelService = GetNode<LevelService>("/root/LevelService");

            _exposureTimer = GetNode<Timer>("ExposureTimer");
            _exposureTimer.Connect("timeout", this, nameof(TryToExposePlayerSpy));

            _spyTimer = GetNode<Timer>("SpyTimer");
            _spyTimer.WaitTime = 7;
            _spyTimer.Connect("timeout", this, nameof(SpawnSpy));
        }

        private void TryToExposePlayerSpy()
        {
            var playerSpies = SpawnService.PlayerSpies.Where(spy => !string.Equals(spy.Word, "!!!")).ToList();
            if (playerSpies.Count == 0)
                return;

            var selectedSpy = playerSpies.Count == 1 ? playerSpies[0] : playerSpies[_random.Next(0, playerSpies.Count)];
            var exposureChance = CalculateExposureChance(selectedSpy.Word.Length);
            if (!(_random.NextDouble() < exposureChance))
                return;

            SignalService.EmitSignal(nameof(SignalService.PlayerSpyExposed), selectedSpy);
        }

        private void SpawnSpy()
        {
            var spy = SpawnService.SpawnAISpy();
            SpyContainer.AddChild(spy);
            spy.Position = Vector2.Zero;
            OpenDoor();
        }

        private float CalculateExposureChance(int length)
        {
            if (length <= 4)
                return _levelService.LevelData.AIExposureChances[0];
            if (length <= 5)
                return _levelService.LevelData.AIExposureChances[1];
            if (length <= 6)
                return _levelService.LevelData.AIExposureChances[2];
            if (length <= 7)
                return _levelService.LevelData.AIExposureChances[3];
            if (length <= 8)
                return _levelService.LevelData.AIExposureChances[4];

            return _levelService.LevelData.AIExposureChances[5];
        }

        protected override void OnGameStarted()
        {
            base.OnGameStarted();
            _exposureTimer.Start(0);
            _spyTimer.Start(0);
        }
    }
}
