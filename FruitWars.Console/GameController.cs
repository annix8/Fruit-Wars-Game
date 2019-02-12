using FruitWars.Contracts.IO;
using FruitWars.Models;
using System.Collections.Generic;

namespace NETFramework.TestConsoleApp
{
    public class GameController
    {
        private const int NumberOfPlayers = 2;
        private const string ChooseWarriorMessage = "Player{0}, please choose a warrior.\nInsert 1 for turtle / 2 for monkey / 3 for pigeon";

        private readonly BoardController _boardController;
        private readonly IInputReceiver _playerInputReceiver;
        private readonly IRenderer _playerOutputSender;

        public GameController(BoardController boardController,
            IInputReceiver playerInputReceiver,
            IRenderer playerOutputSender)
        {
            _boardController = boardController;
            _playerInputReceiver = playerInputReceiver;
            _playerOutputSender = playerOutputSender;
        }

        public void RunGameLoop()
        {
            List<Player> players = CreatePlayers();

            bool playNewGame = true;

            // different games loop
            while (playNewGame)
            {
                // single game loop
                while (true)
                {

                }

                playNewGame = false;
            }
            _boardController.InitializeBoard(null, null);
        }

        private List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();
            for (int playerNumber = 0; playerNumber <= NumberOfPlayers; playerNumber++)
            {
                var player = new Player(playerNumber);
            }

            return players;
        }

        private void ChooseWarriorsForPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                string message = string.Format(ChooseWarriorMessage, player.Number);
                _playerOutputSender.RenderMessage(message);
                int warriorType = int.Parse(_playerInputReceiver.ReceiveStringInput());
            }
        }
    }
}
