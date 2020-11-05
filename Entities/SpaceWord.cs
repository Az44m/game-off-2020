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
            _signalService.Connect(nameof(SignalService.SpaceWordFound), this, nameof(OnSpaceWordFound));

            _wordService = GetNode<WordService>("/root/WordService");
            Text = _wordService.GetRandomSpaceWord();
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnSpaceWordFound(string word) => Text = _wordService.GetRandomSpaceWord();
    }
}
