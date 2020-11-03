using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class Letters : Label
    {
        private WordService _wordService;

        public override void _Ready()
        {
            _wordService = GetNode<WordService>("/root/WordService");
            Text = string.Join(" ", _wordService.GetRandomLetters(10));

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.RequestNewLetters), this, nameof(OnNewLettersRequest));
        }

        private void OnNewLettersRequest() => Text = string.Join(" ", _wordService.GetRandomLetters(10));
    }
}
