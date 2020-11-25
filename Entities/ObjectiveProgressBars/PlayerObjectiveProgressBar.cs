using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public class PlayerObjectiveProgressBar : ObjectiveProgressBarBase
    {
        private WordService _wordService;
        protected override string ResourcePrefix => "Player";
        protected override bool GameOverState => true;

        public override void _Ready()
        {
            base._Ready();

            _wordService = GetNode<WordService>("/root/WordService");
            SignalService.Connect(nameof(SignalService.AISpyEntered), this, nameof(OnAISpyEntered));
            SignalService.Connect(nameof(SignalService.RequestNewLetters), this, nameof(OnNewLettersRequested));
        }

        private void OnAISpyEntered(string word)
        {
            Value -= _wordService.CalculateScore(word);
            if (GameStateService.IsSoundOn)
                SpySabotageAudioPlayer.Play();
        }

        private void OnNewLettersRequested() => Value -= 3;
    }
}
