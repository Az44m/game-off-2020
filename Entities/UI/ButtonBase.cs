using Godot;

namespace GameOff2020.Entities.UI
{
    public abstract class ButtonBase : Button
    {
        public override void _Ready() => Connect("button_up", this, nameof(OnPressed));

        protected abstract void OnPressed();
    }
}
