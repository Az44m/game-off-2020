using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public class AIObjectiveProgressBar : ObjectiveProgressBarBase
    {
        private WordService _wordService;
        protected override string ResourcePrefix => "AI";
        protected override bool GameOverState => false;

        public override void _Ready()
        {
            base._Ready();
            _wordService = GetNode<WordService>("/root/WordService");
            SignalService.Connect(nameof(SignalService.PlayerSpyEntered), this, nameof(OnPlayerSpyEntered));
        }

        private void OnPlayerSpyEntered(string word) => Value -= _wordService.CalculateScore(word);
    }
}
