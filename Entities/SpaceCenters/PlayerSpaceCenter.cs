using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.SpaceCenters
{
    public class PlayerSpaceCenter : SpaceCenterBase<AISpy>
    {
        protected override string EnterSignalName { get; } = nameof(SignalService.AISpyEntered);

        public override void _Ready()
        {
            base._Ready();
            SignalService.Connect(nameof(SignalService.LetterWordFound), this, nameof(SpawnSpy));
        }

        private void SpawnSpy(string word)
        {
            var spy = SpawnService.SpawnPlayerSpy(word);
            SpyContainer.AddChild(spy);
            spy.Position = Vector2.Zero;
            OpenDoor();
        }

        protected override void OnGamePaused(bool isPaused)
        {
            base.OnGamePaused(isPaused);
            SpyContainer.ZIndex = isPaused ? 0 : 1;
        }
    }
}
