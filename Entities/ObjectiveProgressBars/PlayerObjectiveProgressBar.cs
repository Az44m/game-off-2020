using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public class PlayerObjectiveProgressBar : ObjectiveProgressBarBase
    {
        private WordService _wordService;

        public override void _Ready()
        {
            base._Ready();

            _wordService = GetNode<WordService>("/root/WordService");
            SignalService.Connect(nameof(SignalService.AISpyEntered), this, nameof(OnAISpyEntered));
            SignalService.Connect(nameof(SignalService.RequestNewLetters), this, nameof(OnNewLettersRequested));
        }

        protected override void Progress()
        {
            base.Progress();

            if (Value >= 100)
                GameStateService.GameOver(true);
        }

        private void OnAISpyEntered(string word) => Value -= _wordService.CalculateScore(word);

        private void OnNewLettersRequested() => Value -= 3;
    }
}
