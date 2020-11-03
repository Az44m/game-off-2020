using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class WordInputField : LineEdit
    {
        private RegEx _regex;
        private string _oldText = string.Empty;
        private SignalService _signalService;
        private WordService _wordService;

        public override void _Ready()
        {
            _signalService = GetNode<SignalService>("/root/SignalService");
            _wordService = GetNode<WordService>("/root/WordService");

            Connect("text_changed", this, nameof(OnTextChanged));
            Connect("text_entered", this, nameof(OnTextEntered));

            _regex = new RegEx();
            _regex.Compile(@"^[a-zA-Z]*$");
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
                if (_wordService.IsValidWord(newString))
                {
                    GD.Print("Valid word");
                    Text = string.Empty;
                    _oldText = string.Empty;
                }
                else
                {
                    GD.Print("NOT valid word");
                }
            }
        }
    }
}
