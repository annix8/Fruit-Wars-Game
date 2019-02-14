using FruitWars.Contracts;
using FruitWars.Contracts.IO;
using FruitWars.Core.BoardObjectCollisionHandlers.Factory;
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
            GameController gameController = CreateGameController();
            gameController.RunGameLoop();
        }

        private GameController CreateGameController()
        {
            GameStateController gameStateController = new GameStateController();
            FruitFactory fruitFactory = new FruitFactory();
            ObjectCollisionHandlerFactory objectCollisionHandlerFactory = new ObjectCollisionHandlerFactory(gameStateController);
            BoardController boardController = new BoardController(gameStateController, fruitFactory, objectCollisionHandlerFactory);
            WarriorFactory warriorFactory = new WarriorFactory();
            PlayerFactory playerFactory = new PlayerFactory();
            GameController gameController = new GameController(
                boardController,
                gameStateController,
                warriorFactory,
                playerFactory,
                _inputReceiver,
                _renderer,
                _frameCreator);
            return gameController;
        }
    }
}
