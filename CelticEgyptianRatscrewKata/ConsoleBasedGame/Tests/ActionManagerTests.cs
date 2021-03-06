﻿using CelticEgyptianRatscrewKata.Game;
using NSubstitute;
using NUnit.Framework;

namespace ConsoleBasedGame.Tests
{
    [TestFixture]
    public class ActionManagerTests
    {
        private IGameController _gameController;
        private ActionManager _actionManager;

        [SetUp]
        public void SetUp()
        {
            _gameController = Substitute.For<IGameController>();
            _actionManager = new ActionManager(_gameController);
        }

        [Test]
        public void DoesNothingWhenNoKeysBound()
        {
            // ACT
            _actionManager.Process('a');

            // ASSERT
            _gameController.DidNotReceive().AttemptSnap(Arg.Any<IPlayer>());
            _gameController.DidNotReceive().PlayCard(Arg.Any<IPlayer>());
        }

        [Test]
        public void DoesNothingWhenKeyNotBound()
        {
            // ARRANGE
            _actionManager.Bind(new PlayerInfo("playerName", 'a', 'b'));

            // ACT
            _actionManager.Process('c');

            // ASSERT
            _gameController.DidNotReceive().AttemptSnap(Arg.Any<IPlayer>());
            _gameController.DidNotReceive().PlayCard(Arg.Any<IPlayer>());
        }

        [Test]
        public void AttemptsSnapWhenSnapKeyPressed()
        {
            // ARRANGE
            var playerInfo = new PlayerInfo("playerName", 'a', 'b');
            _actionManager.Bind(playerInfo);
            
            // ACT
            _actionManager.Process(playerInfo.SnapKey);

            // ASSERT
            _gameController.Received(1).AttemptSnap(playerInfo);
        }

        [Test]
        public void AttemptsToPlayCardWhenPlayCardKeyPressed()
        {
            // ARRANGE
            var playerInfo = new PlayerInfo("playerName", 'a', 'b');
            _actionManager.Bind(playerInfo);

            // ACT
            _actionManager.Process(playerInfo.PlayCardKey);

            // ASSERT
            _gameController.Received(1).PlayCard(playerInfo);
        }
    }
}
