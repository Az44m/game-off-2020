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

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.RequestNewLetters), this, nameof(OnNewLettersRequested));
            signalService.Connect(nameof(SignalService.LetterWordFound), this, nameof(OnLetterWordFound));
            signalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));

        }

        private void OnNewLettersRequested() => UpdateLetters();
        private void OnLetterWordFound() => UpdateLetters();
        private void OnGameStarted() => UpdateLetters();
        private void UpdateLetters() => Text = string.Join(" ", _wordService.GetRandomLetters(10));
        private void OnGamePaused(bool isPaused) => Visible = !isPaused;
    }
}
