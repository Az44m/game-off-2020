using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.SpaceCenters
{
    public abstract class SpaceCenterBase<TEnemySpyType> : Area2D where TEnemySpyType : SpyBase
    {
        protected SignalService SignalService { get; private set; }
        protected SpawnService SpawnService { get; private set; }

        protected abstract string EnterSignalName { get; }

        public override void _Ready()
        {
            Connect("area_entered", this, nameof(OnAreaEntered));
            SignalService = GetNode<SignalService>("/root/SignalService");
            SpawnService = GetNode<SpawnService>("/root/SpawnService");
        }

        private void OnAreaEntered(Area2D target)
        {
            if (target is TEnemySpyType spy)
            {
                SignalService.EmitSignal(EnterSignalName, spy.Word);
                SpawnService.Destroy(spy);
            }
        }
    }
}
