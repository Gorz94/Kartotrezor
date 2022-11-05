using Kartotrezor.Model;

namespace Kartotrezor.Back.Model
{
    public class ComputeOnceRequest
    {
        public string[] Command { get; set; }
    }

    public class ComputeResponse
    {
        public object Map { get; set; }

        public string Error { get; set; }
    }

    public class InitMapRequest
    {
        public string[] Command { get; set; }
    }

    public class InitMapResponse
    {
        public bool Success { get; set; }

        public string Id { get; set; }

        public string Error { get; set; }
    }

    public class ContinueMapRequest
    {
        public string Id { get; set; }
    }

    public class ContiuneMapResponse
    {
        public bool Finished { get; set; }

        public string Error { get; set; }

        public object Map { get; set; }

        public bool Success { get; set; }
    }
}
