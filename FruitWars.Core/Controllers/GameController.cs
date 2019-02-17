using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using FruitWars.Core.Factory;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;
using FruitWars.Core.Models.GameState;
using FruitWars.Core.Models.Warriors;
using System.Collections.Generic;

namespace FruitWars.Core.Controllers
{
    public class GameController
    {
        private const int PlayerNumberToStart = 1;
        private const int NumberOfPlayers = 2;
        private const string ChooseWarriorMessage = "Player{0}, please choose a warrior.\nInsert 1 for turtle / 2 for monkey / 3 for pigeon";
        private const string StartNewGameMessage = "Do you want to start a rematch? (y/n)";
        private const string ConfirmAnswer = "y";
        private const string DenyAnswer = "n";
        private const string InvalidOption = "Invalid option";
        private const string SelectedWarriorMessage = "Warrior {0} was selected";

        private readonly BoardController _boardController;
        private readonly GameStateController _gameStateController;
        private readonly WarriorFactory _warriorFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly IInputReceiver _inputReceiver;
        private readonly IRenderer _renderer;
        private readonly IFrameCreator _frameCreator;

        public GameController(BoardController boardController,
            GameStateController gameStateController,
            WarriorFactory warriorFactory,
            PlayerFactory playerFactory,
            IInputReceiver inputReceiver,
            IRenderer renderer,
            IFrameCreator frameCreator)
        {
            _boardController = boardController;
            _gameStateController = gameStateController;
            _warriorFactory = warriorFactory;
            _playerFactory = playerFactory;
            _inputReceiver = inputReceiver;
            _renderer = renderer;
            _frameCreator = frameCreator;
        }

        public void RunGameLoop()
        {
            bool playNewGame = true;

            // loop for the different games
            while (playNewGame)
            {
                List<Player> players = _playerFactory.Create(NumberOfPlayers);
                CreateNewGame(players);
                RunGame(players);
                playNewGame = AskForRematch();
            }
        }

        private void CreateNewGame(List<Player> players)
        {
            _gameStateController.CreateNewGameState();
            Dictionary<int, Warrior> warriorsByPlayerNumber = CreateWarriorsForPlayerNumbers(players);
            _gameStateController.CreateInProgressGameState();
            _boardController.CreateNewBoard(warriorsByPlayerNumber);
            _gameStateController.AddPlayersToGameState(players);
            _gameStateController.AssignCurrentPlayer(PlayerNumberToStart);
        }

        private void RunGame(List<Player> players)
        {
            // loop for a single game
            while (true)
            {
                Render();

                if (_gameStateController.IsGameFinished())
                {
                    break;
                }

                RunPlayerMoves(players);
            }
        }

        private void RunPlayerMoves(List<Player> players)
        {
            foreach (var player in players)
            {
                if (_gameStateController.IsGameFinished())
                {
                    break;
                }

                _gameStateController.AssignCurrentPlayer(player.Number);
                Render();

                int numberOfMoves = player.Warrior.Speed;
                while (numberOfMoves > 0)
                {
                    Direction direction = _inputReceiver.ReceiveDirectionInput();
                    bool successfulMove = _boardController.MovePlayerWarrior(player.Number, direction);
                    if (successfulMove)
                    {
                        if (_gameStateController.IsGameFinished())
                        {
                            break;
                        }

                        numberOfMoves--;
                        Render();
                    }
                }
            }
        }

        private Dictionary<int, Warrior> CreateWarriorsForPlayerNumbers(List<Player> players)
        {
            Dictionary<int, Warrior> warriorTypesByPlayerNumber = new Dictionary<int, Warrior>();
            foreach (var player in players)
            {
                string message = string.Format(ChooseWarriorMessage, player.Number);
                _gameStateController.AddScreenMessageToWarriorSelectScreen(message);
                Render();
                Warrior warrior = CreateWarriorForPlayer();
                player.Warrior = warrior;
                warriorTypesByPlayerNumber.Add(player.Number, warrior);
            }

            return warriorTypesByPlayerNumber;
        }

        private Warrior CreateWarriorForPlayer()
        {
            int warriorType = -1;
            Warrior warrior = null;
            while (warrior == null)
            {
                bool warriorTypeParsed = int.TryParse(_inputReceiver.ReceiveStringInput(), out warriorType);
                warrior = _warriorFactory.Create(warriorType);
                if (!warriorTypeParsed || warrior == null)
                {
                    _gameStateController.AddScreenMessageToWarriorSelectScreen(InvalidOption);
                }

                Render();
            }

            _gameStateController.AddScreenMessageToWarriorSelectScreen(string.Format(SelectedWarriorMessage, warrior.GetType().Name));

            return warrior;
        }

        private bool AskForRematch()
        {
            Render();
            string answer = _inputReceiver.ReceiveStringInput().ToLower();
            while (answer != ConfirmAnswer && answer != DenyAnswer)
            {
                Render();
                answer = _inputReceiver.ReceiveStringInput().ToLower();
            }

            return answer == ConfirmAnswer;
        }

        private void Render()
        {
            GameStateBase gameState = _gameStateController.GameState;
            IFrame frame = _frameCreator.CreateFrame(gameState);
            _renderer.RenderFrame(frame);
        }
    }
}
