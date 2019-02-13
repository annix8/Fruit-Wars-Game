using System.Collections.Generic;
using System.Linq;
using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using FruitWars.Core.Factory;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;
using FruitWars.Core.Models.Warriors;

namespace FruitWars.Core
{
    public class GameController
    {
        private const int NumberOfPlayers = 2;
        private const string ChooseWarriorMessage = "Player{0}, please choose a warrior.\nInsert 1 for turtle / 2 for monkey / 3 for pigeon";
        private const string StartNewGameMessage = "Do you want to start a rematch? (y/n)";

        private readonly BoardController _boardController;
        private readonly GameStateController _gameStateController;
        private readonly WarriorFactory _warriorFactory;
        private readonly IInputReceiver _inputReceiver;
        private readonly IRenderer _renderer;
        private readonly IFrameCreator _frameCreator;

        public GameController(BoardController boardController,
            GameStateController gameStateController,
            WarriorFactory warriorFactory,
            IInputReceiver inputReceiver,
            IRenderer renderer,
            IFrameCreator frameCreator)
        {
            _boardController = boardController;
            _gameStateController = gameStateController;
            _inputReceiver = inputReceiver;
            _renderer = renderer;
            _warriorFactory = warriorFactory;
            _frameCreator = frameCreator;
        }

        public void RunGameLoop()
        {
            List<Player> players = CreatePlayers();
            bool playNewGame = true;

            // loop for the different games
            while (playNewGame)
            {
                _gameStateController.CreateNewGameState();
                _boardController.CreateNewBoard(GetWarriorTypesForPlayers(players));
                _gameStateController.GameState.Players = players;
                _gameStateController.AssignCurrentPlayer(1);

                // loop for a single game
                while (true)
                {
                    Render();

                    if (_gameStateController.GameState.GameFinished)
                    {
                        break;
                    }
                    
                    foreach (var player in players)
                    {
                        if (_gameStateController.GameState.GameFinished)
                        {
                            break;
                        }

                        _gameStateController.AssignCurrentPlayer(player.Number);
                        Render();

                        int numberOfMoves = player.Warrior.Speed;
                        while (numberOfMoves > 0)
                        {
                            // todo handle invalid input
                            Direction direction = _inputReceiver.ReceiveDirectionInput();
                            _boardController.MovePlayerWarrior(player.Number, direction);
                            if (_gameStateController.GameState.GameFinished)
                            {
                                break;
                            }
                            // if input is valid lower number of moves
                            numberOfMoves--;
                            Render();
                        }
                    }
                }

                playNewGame = AskForRematch();
            }
        }

        private List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();
            for (int playerNumber = 1; playerNumber <= NumberOfPlayers; playerNumber++)
            {
                var player = new Player(playerNumber);
                players.Add(player);
            }

            return players;
        }

        private Dictionary<int, Warrior> GetWarriorTypesForPlayers(List<Player> players)
        {
            Dictionary<int, Warrior> warriorTypesByPlayerNumber = new Dictionary<int, Warrior>();
            foreach (var player in players)
            {
                // todo handle invalid input for warrior types
                string message = string.Format(ChooseWarriorMessage, player.Number);
                _renderer.RenderMessage(message);
                int warriorType = int.Parse(_inputReceiver.ReceiveStringInput());
                Warrior warrior = _warriorFactory.Create(warriorType);
                player.Warrior = warrior;
                warriorTypesByPlayerNumber.Add(player.Number, warrior);
            }

            return warriorTypesByPlayerNumber;
        }

        private bool AskForRematch()
        {
            // todo handle different inputs
            _renderer.RenderMessage(StartNewGameMessage);
            string answer = _inputReceiver.ReceiveStringInput();

            return answer.ToLower() == "y";
        }

        private void Render()
        {
            GameState gameState = _gameStateController.GameState;
            IFrame frame = _frameCreator.CreateFrame(gameState);
            _renderer.RenderFrame(frame);
        }
    }
}
