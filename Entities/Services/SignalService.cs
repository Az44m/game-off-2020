using Godot;

namespace GameOff2020.Entities.Services
{
    public class SignalService : Node
    {
        [Signal]
        public delegate void RequestNewLetters();

        [Signal]
        public delegate void SpaceWordFound(string word);

        [Signal]
        public delegate void LetterWordFound();

        [Signal]
        public delegate void AISpyEntered(string word);

        [Signal]
        public delegate void PlayerSpyEntered(string word);
    }
}
