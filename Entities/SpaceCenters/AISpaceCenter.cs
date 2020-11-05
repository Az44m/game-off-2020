using System;
using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.SpaceCenters
{
    public class AISpaceCenter : SpaceCenterBase<PlayerSpy>
    {
        private Random _random;
        protected override string EnterSignalName { get; } = nameof(SignalService.PlayerSpyEntered);

        public override void _Ready()
        {
            base._Ready();

            _random = new Random();

            var timer = GetNode<Timer>("Timer");
            timer.Connect("timeout", this, nameof(TryToExposePlayerSpy));
            timer.Start();
            TryToExposePlayerSpy();
        }

        private void TryToExposePlayerSpy()
        {
            var playerSpies = SpawnService.PlayerSpies;
            if (playerSpies.Count == 0)
                return;

            var selectedSpy = playerSpies.Count == 1 ? playerSpies[0] : playerSpies[_random.Next(0, playerSpies.Count)];
            var exposureChance = CalculateExposureChance(selectedSpy.Word.Length);

            if (_random.NextDouble() < exposureChance)
                SpawnService.Destroy(selectedSpy);
        }

        private float CalculateExposureChance(int length)
        {
            // 1 - (1 - exposureChance / 100) ^ 15 = overallChange/100
            if (length <= 4)
                return 0.0883f; // 75% overall
            if (length <= 5)
                return 0.0452f; // 50% overall
            if (length <= 6)
                return 0.019f; // 25% overall
            if (length <= 7)
                return 0.007f; // 10% overall
            if (length <= 8)
                return 0.0052f; // 7.5% overall

            return 0.0034f; // 5% overall
        }
    }
}
