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
            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.AISpyEntered), this, nameof(OnAISpyEntered));
        }

        private void OnAISpyEntered(string word) => Value -= _wordService.CalculateScore(word);
    }
}
