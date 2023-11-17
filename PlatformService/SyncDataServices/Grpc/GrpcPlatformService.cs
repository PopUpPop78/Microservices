using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Recieved request {context.AuthContext}");
            var response = new PlatformResponse();
            var platforms = _repository.GetAll();

            foreach(var platform in platforms)
                response.Platforms.Add(_mapper.Map<GrpcPlatformModel>(platform));

            return Task.FromResult(response);
        }
    }
}