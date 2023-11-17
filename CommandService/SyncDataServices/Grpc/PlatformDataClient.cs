using AutoMapper;
using CommandService.Models;
using Grpc.Net.Client;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"Calling Grpc service {_config["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_config["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var response = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(response.Platforms);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed on Grpc call {ex.Message}");
                return null;
            }
        }
    }
}