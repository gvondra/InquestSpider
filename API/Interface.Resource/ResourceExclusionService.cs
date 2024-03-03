using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquestSpider.Interface.Resource
{
    public class ResourceExclusionService : IResourceExclusionService
    {
        public async Task<List<string>> GetAll(ISettings settings)
        {
            using (GrpcChannel channel = GrpcChannel.ForAddress(settings.BaseAddress))
            {
                Protos.ResourceExclusionService.ResourceExclusionServiceClient client = new Protos.ResourceExclusionService.ResourceExclusionServiceClient(channel);
                Protos.GetAllResourceExclusionResponse response = await client.GetAllAsync(new Empty(), await RpcUtil.CreateMetaDataWithAuthHeader(settings));
                return response.Expressions.ToList();
            }
        }

        public async Task Save(ISettings settings, IEnumerable<string> exclusions)
        {
            ArgumentNullException.ThrowIfNull(exclusions);
            Protos.SaveResourceExclusionRequest request = new Protos.SaveResourceExclusionRequest();
            foreach (string exclusion in exclusions)
            {
                request.Expressions.Add(exclusion);
            }
            using (GrpcChannel channel = GrpcChannel.ForAddress(settings.BaseAddress))
            {
                Protos.ResourceExclusionService.ResourceExclusionServiceClient client = new Protos.ResourceExclusionService.ResourceExclusionServiceClient(channel);
                _ = await client.SaveAsync(request, await RpcUtil.CreateMetaDataWithAuthHeader(settings));
            }
        }
    }
}
