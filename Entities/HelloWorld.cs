using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities
{
    public class HelloWorld : Node
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            var wordService = new WordService();
            GD.Print($"Hello World - {string.Join(",", wordService.GetRandomLetters(10))}");
            GD.Print($"Not valid: {wordService.IsValidWord("gfdsgdfs")}");
            GD.Print($"Valid: {wordService.IsValidWord("Apple")}");
            GD.Print($"Score: {wordService.CalculateScore("Apple")}");
        }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    }
}
