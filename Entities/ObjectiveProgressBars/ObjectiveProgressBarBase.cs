using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public abstract class ObjectiveProgressBarBase : ProgressBar
    {
        private Timer _progressTimer;
        protected SignalService SignalService;

        public override void _Ready()
        {
            SignalService = GetNode<SignalService>("/root/SignalService");

            _progressTimer = GetNode<Timer>("Timer");
            _progressTimer.Connect("timeout", this, nameof(Progress));

            SignalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
        }

        protected virtual void Progress() => Value++;

        private void OnGameStarted()
        {
            if (_progressTimer.IsStopped())
                _progressTimer.Start(0);
        }
    }
}
