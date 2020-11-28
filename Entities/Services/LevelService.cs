using System.Collections.Generic;
using Godot;

namespace GameOff2020.Entities.Services
{
    public class LevelData
    {
        public int AISpyMovementSpeed { get; set; }

        public float AISpyRandomWordChance { get; set; }

        // 1 - (1 - exposureChance / 100) ^ 15 = overallChance / 100
        // 0.11881f -> 85% overall
        // 0.102f -> 80% overall
        // 0.0883f -> 75% overall
        // 0.0452f -> 50% overall
        // 0.019f -> 25% overall
        // 0.007f -> 10% overall
        // 0.0052f -> 7.5% overall
        // 0.0034f -> 5% overall
        public float[] AIExposureChances { get; set; }

        public LevelData(int aiSpyMovementSpeed, float aiSpyRandomWordChance, float[] aiExposureChances)
        {
            AISpyMovementSpeed = aiSpyMovementSpeed;
            AISpyRandomWordChance = aiSpyRandomWordChance;
            AIExposureChances = aiExposureChances;
        }
    }

    public class LevelService : Node
    {
        private GameStateService _gameStateService;

        private readonly List<LevelData> _levelDataList = new List<LevelData>
        {
            new LevelData(34, 0, new[] {0.0883f, 0.0452f, 0.019f, 0.007f, 0.0052f, 0.0034f}),
            new LevelData(70, 0.25f, new[] {0.102f, 0.0883f, 0.0452f, 0.019f, 0.007f, 0.0052f}),
            new LevelData(100, 0.5f, new[] {0.11881f, 0.102f, 0.0883f, 0.0452f, 0.019f, 0.007f})
        };

        public LevelData LevelData => _levelDataList[_gameStateService.MaxAvailableLevel - 1];

        public override void _Ready() => _gameStateService = GetNode<GameStateService>("/root/GameStateService");
    }
}
