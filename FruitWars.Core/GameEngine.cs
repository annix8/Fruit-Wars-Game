using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using FruitWars.Core.Factory;

namespace FruitWars.Core
{
    public class GameEngine
    {
        private readonly IInputReceiver _inputReceiver;
        private readonly IRenderer _renderer;
        private readonly IFrameCreator _frameCreator;

        public GameEngine(IInputReceiver inputReceiver,
            IRenderer renderer,
            IFrameCreator frameCreator)
        {
            _inputReceiver = inputReceiver;
            _renderer = renderer;
            _frameCreator = frameCreator;
        }

        public void RunGame()
        {
            GameStateController gameStateController = new GameStateController();
            BoardController boardController = new BoardController(gameStateController);
            WarriorFactory warriorFactory = new WarriorFactory();
            GameController gameController = new GameController(
                boardController,
                gameStateController,
                warriorFactory,
                _inputReceiver,
                _renderer,
                _frameCreator);

            gameController.RunGameLoop();
        }
    }
}
