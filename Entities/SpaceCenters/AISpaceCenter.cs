using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;

namespace GameOff2020.Entities.SpaceCenters
{
    public class AISpaceCenter : SpaceCenterBase<PlayerSpy>
    {
        protected override string EnterSignalName { get; } = nameof(SignalService.PlayerSpyEntered);
    }
}
