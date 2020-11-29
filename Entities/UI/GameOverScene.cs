using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.UI
{
    public class GameOverScene : Control
    {
        private GameStateService _gameStateService;

        public override void _Ready()
        {
            _gameStateService = GetNode<GameStateService>("/root/GameStateService");

            GetNode<Button>("GameOverDialogContainer/PeaceButton").Connect("button_up", this, nameof(OnPeacePressed));
            GetNode<Button>("GameOverDialogContainer/WarButton").Connect("button_up", this, nameof(OnWarPressed));
            GetNode<Button>("QuitButton").Connect("button_up", this, nameof(OnQuitPressed));
        }

        private void OnPeacePressed()
        {
            GD.Print("PRESSED");
            GetNode<Button>("QuitButton").Visible = true;
            GetNode<Label>("PeaceOutComeLabel").Visible = true;
            GetNode<Label>("ThankYouLabel").Visible = true;
            GetNode<Control>("GameOverDialogContainer").Visible = false;
        }

        private void OnWarPressed()
        {
            GetNode<Button>("QuitButton").Visible = true;
            GetNode<Label>("WarOutComeLabel").Visible = true;
            GetNode<Label>("ThankYouLabel").Visible = true;
            GetNode<Control>("GameOverDialogContainer").Visible = false;
        }

        private void OnQuitPressed() => GetTree().Quit();
    }
}
