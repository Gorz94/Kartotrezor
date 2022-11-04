using FluentAssertions;
using Kartotrezor.Executor;
using Kartotrezor.Model;
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

            var map = new MapExecutor().ExecuteMap(command);

            map.Should().BeEquivalentTo(new Map(w, h) { Slots = Enumerable.Range(0, w * h).Select(_ => new MapSlot()).ToArray() });
        }
    }
}
