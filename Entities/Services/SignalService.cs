using Godot;

namespace GameOff2020.Entities.Services
{
    public class SignalService : Node
    {
        [Signal]
        public delegate void RequestNewLetters();

        [Signal]
        public delegate void SpaceWordFound();

        [Signal]
        public delegate void LetterWordFound();
    }
}
