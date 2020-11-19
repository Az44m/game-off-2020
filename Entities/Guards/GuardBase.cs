using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.Guards
{
    public abstract class GuardBase : AnimatedSprite
    {
        protected SignalService SignalService { get; private set; }

        public override void _Ready() => SignalService = GetNode<SignalService>("/root/SignalService");

        protected async void OnSpyExposed()
        {
            if (Frame == 1)
                return;

            Frame = 1;
            await ToSignal(GetTree().CreateTimer(1, false), "timeout");
            Frame = 0;
        }
    }
}
