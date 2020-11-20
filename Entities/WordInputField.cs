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
        private int _shakeIndex;
        private Vector2 _originalRectPosition;
        private AudioStreamPlayer _wrongWordAudioPlayer;

        public override void _Ready()
        {
            _originalRectPosition = RectGlobalPosition;
            _shakeIndex = _shakePositions.Length;

            _wrongWordAudioPlayer = GetNode<AudioStreamPlayer>("WrongWordAudioPlayer");

            _signalService = GetNode<SignalService>("/root/SignalService");
            _wordService = GetNode<WordService>("/root/WordService");

            Connect("text_changed", this, nameof(OnTextChanged));
            Connect("text_entered", this, nameof(OnTextEntered));

            _regex = new RegEx();
            _regex.Compile(@"^[a-zA-Z]*$");

            _signalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
            _signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
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
                if (_wordService.IsValidAISpyWord(newString))
                {
                    Text = string.Empty;
                    _oldText = string.Empty;
                    _signalService.EmitSignal(nameof(SignalService.AISpyWordFound), newString.ToUpper());
                }
                else if (_wordService.IsValidWord(newString))
                {
                    Text = string.Empty;
                    _oldText = string.Empty;
                    _signalService.EmitSignal(nameof(SignalService.LetterWordFound), newString.ToUpper());
                }
                else
                {
                    if (_shakeIndex == _shakePositions.Length && !_wrongWordAudioPlayer.Playing)
                    {
                        TriggerShake();
                        _wrongWordAudioPlayer.Play();
                    }
                }
            }
        }

        private void OnGameStarted()
        {
            Text = string.Empty;
            _oldText = string.Empty;

            GrabFocus();

            _shakeIndex = _shakePositions.Length;
        }

        private void OnGamePaused(bool isPaused)
        {
            Text = isPaused ? string.Empty : _oldText;
            CaretPosition = Text.Length;
            _shakeIndex = _shakePositions.Length;
            ResetPosition();
        }

        public override void _Process(float delta)
        {
            if (_shakeIndex < _shakePositions.Length)
                Shake();
            else
                ResetPosition();
        }

        private void ResetPosition()
        {
            RectGlobalPosition = _originalRectPosition;
            RectRotation = 0;
        }

        private void TriggerShake() => _shakeIndex = 0;

        private void Shake()
        {
            RectRotation = _shakeRotations[_shakeIndex];
            RectGlobalPosition = RectGlobalPosition + _shakePositions[_shakeIndex];
            _shakeIndex++;
        }

        private readonly Vector2[] _shakePositions =
        {
            new Vector2(-0.130f, 0.005f), new Vector2(0.107f, 0.383f), new Vector2(-0.004f, 0.259f), new Vector2(-0.064f, -0.125f),
            new Vector2(-0.091f, 0.237f), new Vector2(0.081f, 0.161f), new Vector2(-0.002f, -0.049f), new Vector2(0.111f, -0.061f),
            new Vector2(-0.019f, 0.086f), new Vector2(-0.034f, -0.041f), new Vector2(-0.152f, 0.041f), new Vector2(-0.136f, -0.106f),
            new Vector2(0.067f, 0.051f), new Vector2(0.094f, -0.094f), new Vector2(-0.066f, -0.051f), new Vector2(0.007f, 0.073f),
            new Vector2(-0.041f, 0.016f), new Vector2(0.071f, 0.012f), new Vector2(-0.046f, -0.014f), new Vector2(0.029f, 0.034f),
            new Vector2(-0.011f, 0.039f), new Vector2(-0.024f, 0.002f), new Vector2(-0.014f, -0.001f), new Vector2(0.011f, 0.001f),
            new Vector2(0.006f, 0.005f), new Vector2(0.007f, -0.008f), new Vector2(0.002f, -0.001f), new Vector2(0.001f, 0.001f),
            new Vector2(-0.001f, -0.001f), new Vector2(0f, 0f)
        };

        private readonly float[] _shakeRotations =
        {
            -0.0500309f, -0.1191529f, -0.06894816f, 0.1305884f, 0.04781348f, 0.1120516f, -0.1082932f, 0.1202639f, -0.0843064f, -0.02909492f,
            -0.06654023f, 0.03511398f, -0.06936035f, 0.05856201f, 0.04807435f, 0.02618374f, -0.0315142f, -0.02855696f, 0.01258381f, 0.01269334f,
            0.01046164f, 0.005594113f, -0.003524967f, -0.00332579f, -0.005547515f, -0.001953781f, -0.0007098448f, 0.0003350079f, -0.0002506397f, 0
        };
    }
}
