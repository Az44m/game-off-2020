using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.Spies
{
    public class AISpy : SpyBase
    {
        public override void _Ready()
        {
            base._Ready();

            Velocity = Vector2.Left;
            Word = WordService.GetRandomAISpyWord();
            GlobalPosition = new Vector2(1550, 850);

            var signalService = GetNode<SignalService>("/root/SignalService");
            signalService.Connect(nameof(SignalService.AISpyWordFound), this, nameof(OnAISpyWordFound));
        }

        private void OnAISpyWordFound(string word)
        {
            if (string.Equals(word, Word))
                QueueFree();
        }
    }
}
