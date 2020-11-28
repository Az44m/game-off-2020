using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.Spies
{
    public class AISpy : SpyBase
    {
        private SignalService _signalService;

        protected override int MovementSpeed { get; set; }

        public override void _Ready()
        {
            base._Ready();

            Velocity = Vector2.Left;
            Word = WordService.GetRandomAISpyWord();
            GlobalPosition = new Vector2(1550, 850);

            var levelService = GetNode<LevelService>("/root/LevelService");
            MovementSpeed = levelService.LevelData.AISpyMovementSpeed;
            _signalService = GetNode<SignalService>("/root/SignalService");
            _signalService.Connect(nameof(SignalService.AISpyWordFound), this, nameof(OnAISpyWordFound));
        }

        private void OnAISpyWordFound(string word)
        {
            if (!string.Equals(word, Word))
                return;

            _signalService.EmitSignal(nameof(SignalService.AISpyExposed), this);
        }
    }
}
