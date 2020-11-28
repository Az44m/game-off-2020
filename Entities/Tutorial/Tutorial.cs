using GameOff2020.Entities.Services;
using Godot;

namespace GameOff2020.Entities.Tutorial
{
    public enum TutorialState
    {
        Prologue = 0,
        PlayerRocket,
        AIRocket,
        SpaceCenter,
        WordInput,
        Spy,
        Epilogue,
        TutorialEnd
    }

    public class Tutorial : Node2D
    {
        private Button _nextButton;
        private GameStateService _gameStateService;
        private Node2D _prologue;
        private CanvasItem _playerRocketMask;
        private CanvasItem _playerRocketText;
        private AnimatedSprite _playerProgressBar;
        private CanvasItem _aiRocketMask;
        private CanvasItem _aiRocketText;
        private CanvasItem _spaceCenterMask;
        private CanvasItem _spaceCenterText;
        private CanvasItem _wordInputMask;
        private CanvasItem _wordInputText;
        private CanvasItem _tutorialSpyMask;
        private CanvasItem _tutorialSpyText;
        private CanvasItem _epilogueMask;
        private CanvasItem _epilogueText;

        private TutorialState _tutorialState = TutorialState.Prologue;

        public TutorialState TutorialState
        {
            get => _tutorialState;
            set
            {
                _tutorialState = value;
                OnTutorialStateChanged(_tutorialState);
            }
        }

        public override void _Ready()
        {
            _prologue = GetNode<Node2D>("Prologue");
            _playerRocketMask = GetNode<CanvasItem>("PlayerRocketMask");
            _playerRocketText = GetNode<CanvasItem>("PlayerRocketText");
            _playerProgressBar = GetNode<AnimatedSprite>("PlayerProgressBar");
            _aiRocketMask = GetNode<CanvasItem>("AIRocketMask");
            _aiRocketText = GetNode<CanvasItem>("AIRocketText");
            _spaceCenterMask = GetNode<CanvasItem>("SpaceCenterMask");
            _spaceCenterText = GetNode<CanvasItem>("SpaceCenterText");
            _wordInputMask = GetNode<CanvasItem>("WordInputMask");
            _wordInputText = GetNode<CanvasItem>("WordInputText");
            _tutorialSpyMask = GetNode<CanvasItem>("TutorialSpyMask");
            _tutorialSpyText = GetNode<CanvasItem>("TutorialSpyText");
            _epilogueMask = GetNode<CanvasItem>("EpilogueMask");
            _epilogueText = GetNode<CanvasItem>("EpilogueText");

            _nextButton = GetNode<Button>("NextButton");
            _nextButton.Connect("button_up", this, nameof(OnNextPressed));

            _gameStateService = GetNode<GameStateService>("/root/GameStateService");
        }

        private void OnNextPressed() => TutorialState++;

        private void OnTutorialStateChanged(TutorialState newState)
        {
            switch (newState)
            {
                case TutorialState.Prologue:
                    _prologue.Visible = true;
                    _playerRocketMask.Visible = false;
                    _playerRocketText.Visible = false;
                    _playerProgressBar.Stop();
                    _aiRocketText.Visible = false;
                    _aiRocketMask.Visible = false;
                    _spaceCenterText.Visible = false;
                    _spaceCenterMask.Visible = false;
                    _wordInputText.Visible = false;
                    _wordInputMask.Visible = false;
                    _tutorialSpyText.Visible = false;
                    _tutorialSpyMask.Visible = false;
                    _epilogueMask.Visible = false;
                    _epilogueText.Visible = false;
                    _nextButton.Text = "Next";
                    break;
                case TutorialState.PlayerRocket:
                    _prologue.Visible = false;
                    _playerRocketMask.Visible = true;
                    _playerRocketText.Visible = true;
                    _playerProgressBar.Play();
                    _aiRocketText.Visible = false;
                    _aiRocketMask.Visible = false;
                    _spaceCenterText.Visible = false;
                    _spaceCenterMask.Visible = false;
                    _wordInputText.Visible = false;
                    _wordInputMask.Visible = false;
                    _tutorialSpyText.Visible = false;
                    _tutorialSpyMask.Visible = false;
                    _epilogueMask.Visible = false;
                    _epilogueText.Visible = false;
                    _nextButton.Text = "Next";
                    break;
                case TutorialState.AIRocket:
                    _playerRocketMask.Visible = false;
                    _aiRocketMask.Visible = true;
                    _aiRocketText.Visible = true;
                    _playerProgressBar.Stop();
                    break;
                case TutorialState.SpaceCenter:
                    _aiRocketMask.Visible = false;
                    _spaceCenterMask.Visible = true;
                    _spaceCenterText.Visible = true;
                    break;
                case TutorialState.WordInput:
                    _spaceCenterMask.Visible = false;
                    _wordInputMask.Visible = true;
                    _wordInputText.Visible = true;
                    break;
                case TutorialState.Spy:
                    _wordInputMask.Visible = false;
                    _tutorialSpyMask.Visible = true;
                    _tutorialSpyText.Visible = true;
                    break;
                case TutorialState.Epilogue:
                    _tutorialSpyMask.Visible = false;
                    _epilogueMask.Visible = true;
                    _epilogueText.Visible = true;
                    _nextButton.Text = "Start game";
                    break;
                case TutorialState.TutorialEnd:
                    _gameStateService.IsFirstTime = false;
                    _gameStateService.ShowLevelSelector();
                    QueueFree();
                    break;
            }
        }
    }
}
