using FluentAssertions;
using Kartotrezor.Executor;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
using System;
using System.Linq;
using Xunit;

namespace Kartotest.Tests.Parser
{
    public class Parser_should
    {
        [Theory]
        [InlineData("C - 1 - 2", "C - 5 - 8", "M - 5 - 7")]
        public void Parser_should_return_values(params string[] values)
        {
            var parser = new CommandParser();

            var result = parser.ParseCommands(values);

            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(Enumerable.Empty<string>());
        }

        [Theory]
        [InlineData("M - 1 - 2", Level.Plain, 1, 2)]
        [InlineData("M - 2 - 2", Level.Mountain, 2, 2)]
        [InlineData("", Level.Mountain, 2, 2, true)]
        public void Parser_parse_level(string line, Level level, int x, int y, bool error = false)
        {
            var command = new LevelCommand(level, x, y);

            var parser = new CommandParser();

            if (error)
            {
                Assert.Throws<ArgumentException>(() => parser.ParseCommands(new[] { line }));
                return;
            }

            var commands = parser.ParseCommands(new[] { line });
            commands.Should().HaveCount(1);

            commands.First().Should().BeEquivalentTo(command);
        }

        [Theory]
        [InlineData("M - 1 - 2", 1, 1, 2)]
        [InlineData("M - 2 - 2", 2, 2, 2)]
        [InlineData("M - 2 - 3", 99, 2, 3, true)]
        [InlineData("", 2, 2, 2, true)]
        public void Parser_parse_setTreasure(string line, int value, int x, int y, bool error = false)
        {
            if (value == 0)
            {
                Assert.Throws<ArgumentException>(() => new SetTreasureCommand(x, y, value));
                return;
            }

            var command = new SetTreasureCommand(x, y, value);

            var parser = new CommandParser();

            if (error)
            {
                Assert.Throws<ArgumentException>(() => parser.ParseCommands(new[] { line }));
                return;
            }

            var commands = parser.ParseCommands(new[] { line });
            commands.Should().HaveCount(1);

            commands.First().Should().BeEquivalentTo(command);
        }

        [Theory]
        [InlineData("T - 1 - 2", "Bernard", Direction.W, 1, 2)]
        [InlineData("T - 1 - 2", "Bernard", Direction.E, 1, -5)]
        [InlineData("T - 1 - 2", "Bernard", Direction.W, 0, 2)]
        [InlineData("T - 2 - 2", "", Direction.W, 2, 2)]
        [InlineData("T - 2 - 2", null, Direction.W, 2, 2, true)]
        [InlineData("", "Mehdi", Direction.W, 2, 2, true)]
        public void Parser_parse_setTreasureCommand(string line, string name, Direction direction, int x, int y, bool error = false)
        {
            if (string.IsNullOrEmpty(name) || x <= 0 || y <= 0)
            {
                Assert.Throws<ArgumentException>(() => new InitPlayerCommand(name, direction, x, y));
                return;
            }

            var command = new InitPlayerCommand(name, direction, x, y);

            var parser = new CommandParser();

            if (error)
            {
                Assert.Throws<ArgumentException>(() => parser.ParseCommands(new[] { line }));
                return;
            }

            var commands = parser.ParseCommands(new[] { line });
            commands.Should().HaveCount(1);

            commands.First().Should().BeEquivalentTo(command);
        }
    }
}
