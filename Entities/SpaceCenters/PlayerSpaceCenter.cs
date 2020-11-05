using GameOff2020.Entities.Services;
using GameOff2020.Entities.Spies;

namespace GameOff2020.Entities.SpaceCenters
{
    public class PlayerSpaceCenter : SpaceCenterBase<AISpy>
    {
        protected override string EnterSignalName { get; } = nameof(SignalService.AISpyEntered);
    }
}
