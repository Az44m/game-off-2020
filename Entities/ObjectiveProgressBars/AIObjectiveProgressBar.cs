using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public class AIObjectiveProgressBar : ObjectiveProgressBarBase
    {
        private SpawnService _spawnService;
        private WordService _wordService;
        private int _timeoutCounter;

        public override void _Ready()
        {
            base._Ready();
            _spawnService = GetNode<SpawnService>("/root/SpawnService");
            _wordService = GetNode<WordService>("/root/WordService");
            SignalService.Connect(nameof(SignalService.PlayerSpyEntered), this, nameof(OnPlayerSpyEntered));
        }

        protected override void Progress()
        {
            base.Progress();
            _timeoutCounter++;
            if (_timeoutCounter % 7 == 0)
                AddChild(_spawnService.SpawnAISpy());
        }

        private void OnPlayerSpyEntered(string word) => Value -= _wordService.CalculateScore(word);
    }
}
