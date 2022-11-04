using FluentAssertions;
using Kartotrezor.Executor;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
using System;
using System.Linq;
using Xunit;

namespace Kartotest.Tests.Parser
{
    public class Executor_should
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 2)]
        [InlineData(5, 2)]
        [InlineData(1, Map.MAX_SIZE)]
        public void Executor_should_InitMap(int w, int h)
        {
            var command = new[]
            {
               new InitMapCommand(w, h)
            };

            var map = new MapExecutor().ExecuteMap(command);

            map.Should().BeEquivalentTo(new Map(w, h) { Slots = Enumerable.Range(0, w * h).Select(_ => new MapSlot()).ToArray() });
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(-3, 2)]
        [InlineData(5, -2)]
        [InlineData(1, Map.MAX_SIZE + 1)]
        public void Executor_should_InitMap_Throws(int w, int h)
        {
            var command = new[]
            {
               new InitMapCommand(w, h)
            };

            Assert.Throws<InvalidOperationException>(() => new MapExecutor().ExecuteMap(command));
        }

        [Fact]
        public void Execute_should_Execute_WalkOnLimit()
        {
            var commands = new[]
            {
                "C - 2 - 2",
                "M - 1 - 1",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - S - AA"
            };

            var map = new Map(2, 2);

            SetLevel(map, 1, 1, Level.Mountain);
            AddTreasure(map, 0, 1, 2);
            AddPlayer(map, 1, 0, "Bernard", Direction.S);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_Move()
        {
            var commands = new[]
            {
                "C - 2 - 2",
                "M - 1 - 1",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - S - ADDA"
            };

            var map = new Map(2, 2);

            SetLevel(map, 1, 1, Level.Mountain);
            AddTreasure(map, 0, 1, 2);
            AddPlayer(map, 0, 0, "Bernard", Direction.N);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_WalkOnMountain()
        {
            var commands = new[]
            {
                "C - 2 - 2",
                "M - 1 - 1",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - S - AAAAAAAAAAAAAGAAAAAAAAAAA"
            };

            var map = new Map(2, 2);

            SetLevel(map, 1, 1, Level.Mountain);
            AddTreasure(map, 0, 1, 2);
            AddPlayer(map, 1, 0, "Bernard", Direction.S);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_GetTreasure()
        {
            var commands = new[]
            {
                "C - 1 - 2",
                "T - 0 - 1 - 1",
                "A - Bernard - 0 - 0 - E - A"
            };

            var map = new Map(1, 2);

            AddPlayer(map, 0, 1, "Bernard", Direction.E);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_GetTreasure_NotEmpty()
        {
            var commands = new[]
            {
                "C - 1 - 2",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - E - A"
            };

            var map = new Map(1, 2);

            AddTreasure(map, 0, 1, 1);
            AddPlayer(map, 0, 1, "Bernard", Direction.E);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_GetTreasure_Twice()
        {
            var commands = new[]
            {
                "C - 1 - 2",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - E - ADDAGGA"
            };

            var map = new Map(1, 2);

            AddPlayer(map, 0, 1, "Bernard", Direction.E);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        private void AddPlayer(Map map, int x, int y, string name, Direction dir)
        {
            map[x, y].Entities = map[x, y].Entities.Concat(new Entity[] { new Adventurer(name, dir) });
        }

        private void AddTreasure(Map map, int x, int y, int count)
        {
            map[x, y].Entities = map[x, y].Entities.Concat(new Entity[] { new Treasure(count) });
        }

        private void SetLevel(Map map, int x, int y, Level level)
        {
            map[x, y].Level = level;
        }
    }
}
