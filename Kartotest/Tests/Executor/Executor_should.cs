using FluentAssertions;
using Kartotrezor.Executor;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
using Kartotrezor.Utils;
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

            map.Should().BeEquivalentTo(new Map(w, h) { Slots = Enumerable.Range(0, w * h).Select(i => new MapSlot(i % w, i / w)).ToArray() });
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
                "T - 1 - 0 - 2",
                "A - Bernard - 0 - 0 - S - AA"
            };

            var map = new Map(2, 2);

            map.SetLevel(1, 1, Level.Mountain);
            map.AddTreasure(1, 0, 2);
            map.AddPlayer(0, 1, "Bernard", Direction.S);

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

            map.SetLevel(1, 1, Level.Mountain);
            map.AddTreasure(0, 1, 1);
            map.AddPlayer(0, 0, "Bernard", Direction.N);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_WalkOnMountain()
        {
            var commands = new[]
            {
                "C - 2 - 2",
                "M - 1 - 1",
                "T - 1 - 0 - 2",
                "A - Bernard - 0 - 0 - S - AAAAAAAAAAAAAGAAAAAAAAAAA"
            };

            var map = new Map(2, 2);

            map.SetLevel(1, 1, Level.Mountain);
            map.AddTreasure(1, 0, 2);
            map.AddPlayer(0, 1, "Bernard", Direction.E);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_GetTreasure()
        {
            var commands = new[]
            {
                "C - 1 - 2",
                "T - 0 - 1 - 1",
                "A - Bernard - 0 - 0 - S - A"
            };

            var map = new Map(1, 2);

            map.AddPlayer(0, 1, "Bernard", Direction.S, 1);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_GetTreasure_NotEmpty()
        {
            var commands = new[]
            {
                "C - 1 - 2",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - S - A"
            };

            var map = new Map(1, 2);

            map.AddTreasure(0, 1, 1);
            map.AddPlayer(0, 1, "Bernard", Direction.S, 1);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_GetTreasure_Twice()
        {
            var commands = new[]
            {
                "C - 1 - 2",
                "T - 0 - 1 - 2",
                "A - Bernard - 0 - 0 - S - ADDAGGA"
            };

            var map = new Map(1, 2);

            map.AddPlayer(0, 1, "Bernard", Direction.S, 2);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_TwoAdv()
        {
            var commands = new[]
            {
                "C - 2 - 2",
                "A - Bernard - 0 - 0 - E - AA",
                "A - Michou - 1 - 1 - S - AA"
            };

            var map = new Map(2, 2);

            map.AddPlayer(1, 0, "Bernard", Direction.E);

            map.AddPlayer(1, 1, "Michou", Direction.S);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }

        [Fact]
        public void Execute_should_Execute_TwoAdvWalk()
        {
            var commands = new[]
            {
                "C - 2 - 1",
                "A - Bernard - 0 - 0 - S - AA",
                "A - Michou - 1 - 0 - S - AA"
            };

            var map = new Map(2, 1);

            map.AddPlayer(0, 0, "Bernard", Direction.S);

            map.AddPlayer(1, 0, "Michou", Direction.S);

            map.Should().BeEquivalentTo(new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(commands)));
        }
    }
}
