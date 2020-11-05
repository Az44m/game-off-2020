using System.Collections.Generic;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.Services
{
    public class SpawnService : Node
    {
        private readonly List<SpyBase> _playerSpies = new List<SpyBase>();
        public IReadOnlyList<SpyBase> PlayerSpies => _playerSpies;

        public SpyBase SpawnPlayerSpy(string word)
        {
            var spy = (SpyBase) ResourceLoader.Load<PackedScene>("res://Entities/Spies/PlayerSpy.tscn").Instance();
            spy.Word = word;
            _playerSpies.Add(spy);
            return spy;
        }

        public SpyBase SpawnAISpy() => (SpyBase) ResourceLoader.Load<PackedScene>("res://Entities/Spies/AISpy.tscn").Instance();


        public void Destroy(SpyBase spy)
        {
            if (_playerSpies.Contains(spy))
                _playerSpies.Remove(spy);

            spy.QueueFree();
        }
    }
}
