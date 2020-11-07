using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.Spies
{
    public abstract class SpyBase : Area2D
    {
        private int _movementSpeed = 20;
        private Label _wordLabel;

        private string _word;

        public string Word
        {
            get => _word;
            set
            {
                _word = value;
                if (IsInstanceValid(_wordLabel))
                    _wordLabel.Text = _word;
            }
        }

        protected WordService WordService;

        protected Vector2 Velocity;

        public override void _Ready()
        {
            WordService = GetNode<WordService>("/root/WordService");
            _wordLabel = GetNode<Label>("Word");
            _wordLabel.Text = Word;

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        public override void _PhysicsProcess(float delta)
        {
            Velocity = Velocity.Normalized() * _movementSpeed;
            Position += Velocity * delta;
        }

        private void OnGamePaused(bool isPaused) => _wordLabel.Text = isPaused ? string.Empty : _word;
    }
}
