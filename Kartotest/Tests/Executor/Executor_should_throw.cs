using Kartotrezor.Executor;
using Kartotrezor.Model;
using System;
using Xunit;

namespace Kartotest.Tests.Parser
{
    public class Executor_should_throw
    {
        [Fact]
        public void Executor_should_InitOnce()
        {
            var command = new[]
            {
               new InitMapCommand(1, 1),
               new InitMapCommand(1, 1)
            };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_ThrowsNull()
        {
            var command = new Command[] { null };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_InitOnce_1()
        {
            var command = new[]
            {
               new InitMapCommand(1, 1)
            };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command, new Map(1, 1)));
        }

        [Fact]
        public void Executor_should_NeedCommands()
        {
            var command = new Command[0];

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_SetLevelOnMap()
        {
            var command = new[] { new LevelCommand(Level.Mountain, 1, 1) };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_SeTreasureOnMap()
        {
            var command = new[] { new SetTreasureCommand(1, 1, 5) };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_SeTreasureOnMapAfterLevel()
        {
            var command = new Command[] 
            { 
                new SetTreasureCommand(1, 1, 5),
                new LevelCommand(Level.Plain, 1, 5)
            };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command, new Map(1, 1)));
        }

        [Fact]
        public void Executor_should_SetPlayerOnMap()
        {
            var command = new[] { new InitPlayerCommand("Bernard", Kartotrezor.Model.Entities.Direction.S, 1, 1) };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_SetPlayerOnMapAfterLevel()
        {
            var command = new Command[] 
            {
                new InitPlayerCommand("Bernard", Kartotrezor.Model.Entities.Direction.S, 1, 1), 
                new LevelCommand(Level.Mountain, 1, 5) 
            };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Executor_should_SetPlayerOnMapAfterTreasure()
        {
            var command = new Command[]
            {
                new InitPlayerCommand("Bernard", Kartotrezor.Model.Entities.Direction.S, 1, 1),
                new SetTreasureCommand(1, 1, 5)
            };

            Assert.Throws<Exception>(() => new MapExecutor().ExecuteMap(command));
        }
    }
}
