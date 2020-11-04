using Godot;

namespace GameOff2020.Entities
{
    public class ProgressBar : Godot.ProgressBar
    {
        public override void _Ready()
        {
            var timer = GetNode<Timer>("Timer");
            timer.Connect("timeout", this, nameof(DoProgress));
        }

        private void DoProgress() => Value++;
    }
}
