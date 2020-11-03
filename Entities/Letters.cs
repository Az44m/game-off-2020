using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class Letters : Label
    {
        public override void _Ready()
        {
            var wordService = GetNode<WordService>("/root/WordService");
            Text = string.Join(" ", wordService.GetRandomLetters(10));
        }
    }
}
