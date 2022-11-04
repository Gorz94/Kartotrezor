using Kartotrezor.Executor;
using Kartotrezor.Model;
using System;
using Xunit;

namespace Kartotest.Tests.Parser
{
    public class Executor_should
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
    }
}
