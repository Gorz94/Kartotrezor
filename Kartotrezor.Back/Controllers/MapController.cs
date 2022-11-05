using Kartotrezor.Back.Model;
using Kartotrezor.Back.Services;
using Kartotrezor.Back.Utils;
using Kartotrezor.Executor;
using Microsoft.AspNetCore.Mvc;

namespace Kartotrezor.Back.Controllers
{
    [ApiController]
    [Route("map")]
    public class MapController : Controller
    {
        private readonly MapExecutor _executor;
        private readonly CommandParser _parser;
        private readonly ICacheMap _cacher;

        public MapController(ICacheMap cacher)
        {
            _executor = new MapExecutor();
            _parser = new CommandParser();
            _cacher = cacher;
        }

        [HttpPost("compute")]
        public ComputeResponse ComputeOnceMap([FromBody] ComputeOnceRequest request)
        {
            try
            {
                var commands = _parser.ParseCommands(request.Command);
                var map = _executor.ExecuteMap(commands);

                return new ComputeResponse { Map = map.ToConcrete() };
            } catch (Exception e)
            {
                return new ComputeResponse { Error = e.Message };
            }
        }

        [HttpPost("init")]
        public InitMapResponse InitMap([FromBody] InitMapRequest request)
        {
            try
            {
                var commands = _parser.ParseCommands(request.Command);

                var id = _cacher.Add(new MapExecution { Commands = commands, Done = 0, Map = null });

                return new InitMapResponse { Id = id, Success = true };
            }
            catch (Exception e)
            {
                return new InitMapResponse { Success = false, Error = e.Message };
            }
        }

        [HttpPost("continue")]
        public ContiuneMapResponse ContinueMap([FromBody] ContinueMapRequest request)
        {
            try
            {
                var execution = _cacher.Get(request.Id);

                var newMap = _executor.ExecuteMap(execution.Commands.Skip(execution.Done).Take(1).ToArray(), execution.Map);
                execution.Map = newMap;
                execution.Done++;
                var finished = execution.Commands.Length == execution.Done;

                if (finished)
                {
                    _cacher.Remove(request.Id);
                }

                return new ContiuneMapResponse { Finished = finished, Map = newMap.ToConcrete(), Success = true };
            }
            catch (Exception e)
            {
                return new ContiuneMapResponse { Error = e.Message };
            }
        }
    }
}
