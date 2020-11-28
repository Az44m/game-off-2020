using Godot;

namespace GameOff2020.Entities.Spies
{
    public class PlayerSpy : SpyBase
    {
        protected override int MovementSpeed { get; set; } = 34;

        public override void _Ready()
        {
            base._Ready();
            Velocity = Vector2.Right;
            GlobalPosition = new Vector2(50, 850);
        }
    }
}
