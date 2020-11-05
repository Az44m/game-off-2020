using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.SpaceCenters
{
    public abstract class SpaceCenterBase<TEnemySpyType> : Area2D where TEnemySpyType : SpyBase
    {
        protected abstract string EnterSignalName { get; }
        protected SignalService SignalService { get; private set; }

        public override void _Ready()
        {
            Connect("area_entered", this, nameof(OnAreaEntered));
            SignalService = GetNode<SignalService>("/root/SignalService");
        }

        private void OnAreaEntered(Area2D target)
        {
            if (target is TEnemySpyType spy)
            {
                SignalService.EmitSignal(EnterSignalName, spy.Word);
                spy.QueueFree();
            }
        }
    }
}
