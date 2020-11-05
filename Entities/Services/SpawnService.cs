using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.Services
{
    public class SpawnService : Node
    {
        public SpyBase SpawnPlayerSpy(string word)
        {
            var spy = (SpyBase) ResourceLoader.Load<PackedScene>("res://Entities/Spies/PlayerSpy.tscn").Instance();
            spy.Word = word;
            return spy;
        }

        public SpyBase SpawnAISpy() => (SpyBase) ResourceLoader.Load<PackedScene>("res://Entities/Spies/AISpy.tscn").Instance();
    }
}
