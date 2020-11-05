using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public class AIObjectiveProgressBar : ObjectiveProgressBarBase
    {
        private SpawnService _spawnService;
        private WordService _wordService;

        public override void _Ready()
        {
            base._Ready();
            _spawnService = GetNode<SpawnService>("/root/SpawnService");
            _wordService = GetNode<WordService>("/root/WordService");
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.PlayerSpyEntered), this, nameof(OnPlayerSpyEntered));
        }

        protected override void DoProgress()
        {
            base.DoProgress();
            // TODO Spawn Spies with Timer as Value can be manipulated in other ways besides timer which causes uneven spawning
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Value == 1 || Value % 6 == 0)
                AddChild(_spawnService.SpawnAISpy());
        }

        private void OnPlayerSpyEntered(string word) => Value -= _wordService.CalculateScore(word);
    }
}
