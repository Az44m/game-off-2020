using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.SpaceCenters
{
    public abstract class SpaceCenterBase<TEnemySpyType> : Area2D where TEnemySpyType : SpyBase
    {
        private Sprite _staticDoor;
        private AnimatedSprite _animatedDoor;
        private TEnemySpyType _enemySpy;
        protected SignalService SignalService { get; private set; }
        protected SpawnService SpawnService { get; private set; }
        protected Node2D SpyContainer { get; private set; }

        protected abstract string EnterSignalName { get; }

        public override void _Ready()
        {
            SpyContainer = GetNode<Node2D>("SpyContainer");
            _staticDoor = GetNode<Sprite>("StaticDoor");
            _animatedDoor = GetNode<AnimatedSprite>("AnimatedDoor");
            Connect("area_entered", this, nameof(OnAreaEntered));
            SignalService = GetNode<SignalService>("/root/SignalService");
            SpawnService = GetNode<SpawnService>("/root/SpawnService");
            _animatedDoor.Connect("animation_finished", this, nameof(OnDoorAnimationFinished));
            SignalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
            SignalService.Connect(nameof(SignalService.LevelOver), this, nameof(OnLevelOver));
            SignalService.Connect(nameof(SignalService.GamePaused), this, nameof(OnGamePaused));
        }

        protected virtual void OnGamePaused(bool isPaused)
        {
            _animatedDoor.ZIndex = isPaused ? 0 : 2;
            SpyContainer.ZIndex = isPaused ? 0 : 1;
        }

        protected virtual void OnGameStarted()
        {
            _animatedDoor.Visible = true;
            _animatedDoor.ZIndex = 2;
            _staticDoor.Visible = true;
            SpyContainer.ZIndex = 1;
        }

        protected void OpenDoor() => _animatedDoor.Play("open");
        private void CloseDoor() => _animatedDoor.Play("close");

        private void OnDoorAnimationFinished()
        {
            if (string.Equals(_animatedDoor.Animation, "open"))
            {
                if (IsInstanceValid(_enemySpy))
                {
                    SignalService.EmitSignal(EnterSignalName, _enemySpy.Word);
                    SpawnService.Destroy(_enemySpy);
                }

                CloseDoor();
            }
        }

        private void OnAreaEntered(Area2D target)
        {
            if (target is TEnemySpyType spy)
            {
                _enemySpy = spy;
                OpenDoor();
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnLevelOver(bool isWin)
        {
            _animatedDoor.ZIndex = 0;
            SpyContainer.ZIndex = 0;
            _animatedDoor.Stop();
            OpenDoor();
            _animatedDoor.Stop();
            _animatedDoor.Frame = 0;
        }
    }
}
