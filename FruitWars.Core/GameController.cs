﻿using System.Collections.Generic;
using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using FruitWars.Core.Factory;
using FruitWars.Core.Models;
using FruitWars.Core.Models.Enums;

namespace FruitWars.Core
{
    public class GameController
    {
        private const int NumberOfPlayers = 2;
        private const string ChooseWarriorMessage = "Player{0}, please choose a warrior.\nInsert 1 for turtle / 2 for monkey / 3 for pigeon";
        private const string StartNewGameMessage = "Do you want to start a rematch? (y/n)";

        private readonly BoardController _boardController;
        private readonly GameStateController _gameStateController;
        private readonly IInputReceiver _inputReceiver;
        private readonly IRenderer _renderer;
        private readonly WarriorFactory _warriorFactory;
        private readonly IFrameCreator _frameCreator;

        public GameController(BoardController boardController,
            GameStateController gameStateController,
            IInputReceiver playerInputReceiver,
            IRenderer playerOutputSender,
            WarriorFactory warriorFactory,
            IFrameCreator frameCreator)
        {
            _boardController = boardController;
            _gameStateController = gameStateController;
            _inputReceiver = playerInputReceiver;
            _renderer = playerOutputSender;
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
                Dictionary<int, int> warriorTypesByPlayerNumber = GetWarriorTypesForPlayers(players);
                _boardController.CreateNewBoard(warriorTypesByPlayerNumber);

                // loop for a single game
                while (true)
                {
                    GameState gameState = _gameStateController.GetGameState();
                    IFrame frame = _frameCreator.CreateFrame(gameState);
                    _renderer.RenderFrame(frame);

                    if (gameState.GameFinished)
                    {
                        break;
                    }

                    // ask for player input
                    foreach (var player in players)
                    {
                        _gameStateController.AssignCurrentPlayer(player.Number);
                        int numberOfMoves = player.Warrior.Speed;
                        while(numberOfMoves > 0)
                        {
                            // todo handle invalid input
                            Direction direction = _inputReceiver.ReceiveDirectionInput();
                            _boardController.MovePlayerWarrior(player.Number, direction);
                            // if input is valid lower number of moves
                            numberOfMoves--;
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
            }

            return players;
        }

        private Dictionary<int, int> GetWarriorTypesForPlayers(List<Player> players)
        {
            Dictionary<int, int> warriorTypesByPlayerNumber = new Dictionary<int, int>();
            foreach (var player in players)
            {
                string message = string.Format(ChooseWarriorMessage, player.Number);
                _renderer.RenderMessage(message);
                int warriorType = int.Parse(_inputReceiver.ReceiveStringInput());
                warriorTypesByPlayerNumber.Add(player.Number, warriorType);
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
    }
}
