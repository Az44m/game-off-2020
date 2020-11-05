using GameOff2020.Entities.Services;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public class AIObjectiveProgressBar : ObjectiveProgressBarBase
    {
        private SpawnService _spawnService;

        public override void _Ready()
        {
            base._Ready();
            _spawnService = GetNode<SpawnService>("/root/SpawnService");
        }

        protected override void DoProgress()
        {
            base.DoProgress();
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Value == 1 || Value % 6 == 0)
                AddChild(_spawnService.SpawnAISpy());
        }
    }
}
