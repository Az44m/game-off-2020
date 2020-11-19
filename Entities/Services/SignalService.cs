using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.Services
{
    public class SignalService : Node
    {
        [Signal]
        public delegate void RequestNewLetters();

        [Signal]
        public delegate void AISpyWordFound(string word);

        [Signal]
        public delegate void LetterWordFound(string word);

        [Signal]
        public delegate void AISpyEntered(string word);

        [Signal]
        public delegate void PlayerSpyEntered(string word);

        [Signal]
        public delegate void GameStarted();

        [Signal]
        public delegate void GameOver(bool isWin);

        [Signal]
        public delegate void GamePaused(bool isPaused);

        [Signal]
        public delegate void AISpyExposed(SpyBase spy);

        [Signal]
        public delegate void PlayerSpyExposed(SpyBase spy);
    }
}
