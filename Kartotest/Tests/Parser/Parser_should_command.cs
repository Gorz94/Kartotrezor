using FluentAssertions;
using Kartotrezor.Executor;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
using Xunit;

namespace Kartotest.Tests.Parser
{
    public class Parser_should_command
    {
        [Fact]
        public void Parse_Init()
        {
            var command = new[]
            {
               new InitMapCommand(1, 1)
            };

            var lines = new[]
            {
                "C - 1 - 1"
            };

            new CommandParser().ParseCommands(lines).Should().BeEquivalentTo(command);
        }

        [Theory]
        [InlineData("M - 1 - 5", Level.Mountain, 1, 5)]
        [InlineData("M - 1 - 55", Level.Mountain, 1, 55)]
        [InlineData("M - 15 - 50", Level.Mountain, 15, 50)]
        public void Parse_Level(string line, Level level, int x, int y)
        {
            var command = new[]
            {
               new LevelCommand(level, x, y)
            };

            var lines = new[] { line };

            new CommandParser().ParseCommands(lines).Should().BeEquivalentTo(command);
        }

        [Theory]
        [InlineData("T - 1 - 5 - 4", 1, 5, 4)]
        [InlineData("T - 1 - 55 - 99", 1, 55, 99)]
        [InlineData("T - 15 - 50 - 56", 15, 50, 56)]
        public void Parse_Treasure(string line, int x, int y, int value)
        {
            var command = new[]
            {
               new SetTreasureCommand(x, y, value)
            };

            var lines = new[] { line };

            new CommandParser().ParseCommands(lines).Should().BeEquivalentTo(command);
        }

        [Theory]
        [InlineData("A - Louis - 1 - 2 - S - AAGDA", "Louis", 1, 2, Direction.S, 5)]
        [InlineData("A - Bastien - 1 - 3 - W - AAGDA", "Bastien", 1, 3, Direction.W, 5)]
        [InlineData("A - PrimoKilo - 1 - 5 - E - AAGDAA", "PrimoKilo", 1, 5, Direction.E, 6)]
        public void Parse_Player(string line, string name, int x, int y, Direction dir, int count)
        {
            var command = new InitPlayerCommand(name, dir, x, y);

            var lines = new[] { line };

            var commands = new CommandParser().ParseCommands(lines);

            commands[0].Should().BeEquivalentTo(command);

            commands.Length.Should().Be(count + 1);
        }

        [Fact]
        public void Parse_Player_1()
        {
            var lines = new[]
            {
                "A - Mehdi - 5 - 6 - W - AADAAG"
            };

            var commands = new Command[]
            {
                new InitPlayerCommand("Mehdi", Direction.W, 5, 6),
                new MovePlayerForwardCommand("Mehdi"),
                new MovePlayerForwardCommand("Mehdi"),
                new ChangePlayerDirectionCommand("Mehdi", Turn.D),
                new MovePlayerForwardCommand("Mehdi"),
                new MovePlayerForwardCommand("Mehdi"),
                new ChangePlayerDirectionCommand("Mehdi", Turn.G)
            };

            new CommandParser().ParseCommands(lines).Should().BeEquivalentTo(commands);
        }
    }
}
