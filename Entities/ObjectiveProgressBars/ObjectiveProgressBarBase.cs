using Godot;

namespace GameOff2020.Entities.ObjectiveProgressBars
{
    public abstract class ObjectiveProgressBarBase : ProgressBar
    {
        public override void _Ready()
        {
            var timer = GetNode<Timer>("Timer");
            timer.Connect("timeout", this, nameof(DoProgress));
        }

        protected virtual void DoProgress() => Value++;
    }
}
