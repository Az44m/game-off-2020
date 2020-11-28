using System.Collections.Generic;
using System.Linq;
using GameOff2020.Entities.Spies;
using Godot;

namespace GameOff2020.Entities.Services
{
    public class SpawnService : Node
    {
        private SignalService _signalService;

        private readonly List<SpyBase> _aiSpies = new List<SpyBase>();
        private readonly List<SpyBase> _playerSpies = new List<SpyBase>();

        public IReadOnlyList<SpyBase> PlayerSpies => _playerSpies;
        public IReadOnlyList<SpyBase> AISpies => _aiSpies;

        public override void _Ready()
        {
            _signalService = GetNode<SignalService>("/root/SignalService");
            _signalService.Connect(nameof(SignalService.GameStarted), this, nameof(OnGameStarted));
        }

        public SpyBase SpawnPlayerSpy(string word)
        {
            var spy = (SpyBase) ResourceLoader.Load<PackedScene>("res://Entities/Spies/PlayerSpy.tscn").Instance();
            spy.Word = word;
            _playerSpies.Add(spy);
            return spy;
        }

        public SpyBase SpawnAISpy()
        {
            var spy = (SpyBase) ResourceLoader.Load<PackedScene>("res://Entities/Spies/AISpy.tscn").Instance();
            _aiSpies.Add(spy);
            return spy;
        }


        public void Destroy(SpyBase spy)
        {
            if (_playerSpies.Contains(spy))
                _playerSpies.Remove(spy);
            else if (_aiSpies.Contains(spy))
                _aiSpies.Remove(spy);

            if (IsInstanceValid(spy))
                spy.QueueFree();
        }

        private void OnGameStarted()
        {
            _playerSpies.ToList().ForEach(Destroy);
            _playerSpies.Clear();
            _aiSpies.ToList().ForEach(Destroy);
            _aiSpies.Clear();
        }
    }
}
