using FruitWars.Contracts.IO;
using FruitWars.Core.Factory;
using FruitWars.Models;
using FruitWars.Models.Warriors;
using System.Collections.Generic;

namespace FruitWars.Core
{
    public class GameController
    {
        private const int NumberOfPlayers = 2;
        private const string ChooseWarriorMessage = "Player{0}, please choose a warrior.\nInsert 1 for turtle / 2 for monkey / 3 for pigeon";

        private readonly BoardController _boardController;
        private readonly IInputReceiver _inputReceiver;
        private readonly IRenderer _renderer;
        private readonly WarriorFactory _warriorFactory;

        public GameController(BoardController boardController,
            IInputReceiver playerInputReceiver,
            IRenderer playerOutputSender,
            WarriorFactory warriorFactory)
        {
            _boardController = boardController;
            _inputReceiver = playerInputReceiver;
            _renderer = playerOutputSender;
            _warriorFactory = warriorFactory;
        }

        public void RunGameLoop()
        {
            List<Player> players = CreatePlayers();
            CreateWarriorsForPlayers(players);

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

        private void CreateWarriorsForPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                string message = string.Format(ChooseWarriorMessage, player.Number);
                _renderer.RenderMessage(message);
                int warriorType = int.Parse(_inputReceiver.ReceiveStringInput());
                Warrior warrior = _warriorFactory.Create(warriorType);
                player.Warrior = warrior;
            }
        }
    }
}
