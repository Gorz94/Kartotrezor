using FluentAssertions;
using Kartotrezor.Model;
using Kartotrezor.Model.Entities;
using Kartotrezor.Utils;
using Xunit;

namespace Kartotest.Tests.Other
{
    public class Direction_should
    {
        [Theory]
        [InlineData(3, 3, 0, 0, Direction.E, 1, 0)]
        [InlineData(1, 1, 0, 0, Direction.E, 0, 0)]
        [InlineData(1, 1, 0, 0, Direction.N, 0, 0)]
        [InlineData(2, 2, 0, 0, Direction.E, 1, 0)]
        [InlineData(2, 2, 0, 0, Direction.S, 0, 1)]
        [InlineData(2, 2, 1, 1, Direction.S, 1, 1)]
        [InlineData(2, 2, 1, 1, Direction.N, 1, 0)]
        [InlineData(2, 2, 1, 1, Direction.E, 1, 1)]
        [InlineData(2, 2, 1, 1, Direction.W, 0, 1)]
        public void Direction_should_work(int w, int h, int x, int y, Direction dir, int tX, int tY)
        {
            var map = new Map(w, h);

            map.CalculateNextPos(x, y, dir).Should().BeEquivalentTo((X: tX, Y: tY));
        }

        [Fact]
        public void Direction_should_avoid_Mountain()
        {
            var map = new Map(2, 1);

            map.SetLevel(1, 0, Level.Mountain);

            map.CalculateNextPos(0, 0, Direction.E).Should().BeEquivalentTo((X: 0, Y: 0));
        }
    }
}
