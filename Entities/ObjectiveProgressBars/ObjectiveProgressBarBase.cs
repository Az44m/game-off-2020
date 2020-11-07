using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public abstract class ObjectiveProgressBarBase : ProgressBar
    {
        private Timer _progressTimer;
        protected SignalService SignalService;
        protected GameStateService GameStateService;

        public override void _Ready()
        {
            GameStateService = GetNode<GameStateService>("/root/GameStateService");
            SignalService = GetNode<SignalService>("/root/SignalService");

            _progressTimer = GetNode<Timer>("Timer");
            _progressTimer.Connect("timeout", this, nameof(Progress));

            SignalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
        }

        protected virtual void Progress() => Value++;

        protected virtual void OnGameStarted()
        {
            Value = 0;
            Progress();
            _progressTimer.Start(0);
        }
    }
}
