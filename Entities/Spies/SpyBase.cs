using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.Spies
{
    public abstract class SpyBase : Area2D
    {
        private int _movementSpeed = 34;
        private Label _wordLabel;
        private SpawnService _spawnService;

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

            _spawnService = GetNode<SpawnService>("/root/SpawnService");

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));

            var visibilityNotifier = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
            visibilityNotifier.Connect("screen_exited", this, nameof(OnScreenExited));
        }

        public override void _PhysicsProcess(float delta)
        {
            Velocity = Velocity.Normalized() * _movementSpeed;
            Position += Velocity * delta;
        }

        public void RunAway()
        {
            Word = "!!!";
            var sprite = GetNode<AnimatedSprite>("AnimatedSprite");
            sprite.FlipH = !sprite.FlipH;
            _movementSpeed *= 5;
            sprite.Play("runaway");
            ZIndex = 2;
            Velocity.x *= -1;
        }

        private void OnGamePaused(bool isPaused)
        {
            _wordLabel.Text = isPaused ? string.Empty : _word;
            ZIndex = string.Equals("!!!", Word) && !isPaused ? 2 : 0;
        }

        private void OnScreenExited() => _spawnService.Destroy(this);
    }
}
