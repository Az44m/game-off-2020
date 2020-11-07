using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class WordInputField : LineEdit
    {
        private RegEx _regex;
        private string _oldText;
        private SignalService _signalService;
        private WordService _wordService;
        private SpawnService _spawnService;

        public override void _Ready()
        {
            _signalService = GetNode<SignalService>("/root/SignalService");
            _wordService = GetNode<WordService>("/root/WordService");
            _spawnService = GetNode<SpawnService>("/root/SpawnService");

            Connect("text_changed", this, nameof(OnTextChanged));
            Connect("text_entered", this, nameof(OnTextEntered));

            _regex = new RegEx();
            _regex.Compile(@"^[a-zA-Z]*$");

            _signalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
        }

        private void OnTextChanged(string newString)
        {
            if (newString.Contains(" "))
            {
                Text = string.Empty;
                _oldText = string.Empty;
                _signalService.EmitSignal(nameof(SignalService.RequestNewLetters));
            }

            if (_regex.Search(newString) == null)
            {
                Text = _oldText;
            }
            else
            {
                _oldText = newString.ToUpper();
                Text = newString.ToUpper();
            }

            CaretPosition = Text.Length;
        }

        private void OnTextEntered(string newString)
        {
            if (newString.Length > 1)
            {
                if (_wordService.IsValidSpaceWord(newString))
                {
                    Text = string.Empty;
                    _oldText = string.Empty;
                    _signalService.EmitSignal(nameof(SignalService.SpaceWordFound), newString.ToUpper());
                }
                else if (_wordService.IsValidWord(newString))
                {
                    Text = string.Empty;
                    _oldText = string.Empty;
                    _signalService.EmitSignal(nameof(SignalService.LetterWordFound));
                    AddChild(_spawnService.SpawnPlayerSpy(newString.ToUpper()));
                }
                else
                {
                    // TODO Visual feedback eg text field shaking
                    GD.Print("NOT valid word");
                }
            }
        }

        private void OnGameStarted()
        {
            Text = string.Empty;
            _oldText = string.Empty;

            GrabFocus();
        }
    }
}
