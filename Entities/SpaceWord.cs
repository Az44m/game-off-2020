using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class SpaceWord : Label
    {
        private WordService _wordService;
        private SignalService _signalService;

        public override void _Ready()
        {
            _signalService = GetNode<SignalService>("/root/SignalService");
            _signalService.Connect(nameof(SignalService.AISpyWordFound), this, nameof(OnAISpyWordFound));

            _wordService = GetNode<WordService>("/root/WordService");
            Text = _wordService.GetRandomAISpyWord();
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnAISpyWordFound(string word) => Text = _wordService.GetRandomAISpyWord();
    }
}
