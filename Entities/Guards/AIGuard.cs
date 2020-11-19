using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.Guards
{
    public class AIGuard : GuardBase
    {
        public override void _Ready()
        {
            base._Ready();
            SignalService.Connect(nameof(SignalService.PlayerSpyExposed), this, nameof(OnSpyExposed));
        }
    }
}
