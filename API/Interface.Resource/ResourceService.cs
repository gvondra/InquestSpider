using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Interface.Resource
{
    public class ResourceService : IResourceService
    {
        public Task<IAsyncEnumerable<Models.Resource>> GetAll(ISettings settings)
        {
            return Task.FromResult<IAsyncEnumerable<Models.Resource>>(
                new StreamEnumerable<Protos.Resource, Models.Resource>(
                    settings,
                    async channel =>
                    {
                        Protos.ResourceService.ResourceServiceClient client = new Protos.ResourceService.ResourceServiceClient(channel);
                        return client.GetAll(new Empty(), await RpcUtil.CreateMetaDataWithAuthHeader(settings));
                    },
                    Models.Resource.Create));
        }

        public async Task<Models.Resource> GetByUrl(ISettings settings, string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            Protos.GetByUrlRequest request = new Protos.GetByUrlRequest
            {
                Url = url
            };
            using (GrpcChannel channel = GrpcChannel.ForAddress(settings.BaseAddress))
            {
                Protos.ResourceService.ResourceServiceClient client = new Protos.ResourceService.ResourceServiceClient(channel);
                Protos.Resource response = await client.GetByUrlAsync(request, await RpcUtil.CreateMetaDataWithAuthHeader(settings));
                return Models.Resource.Create(response);
            }
        }

        public async Task<Models.Resource> Save(ISettings settings, Models.Resource resource)
        {
            ArgumentNullException.ThrowIfNull(resource);
            Protos.Resource request = resource.ToProto();
            using (GrpcChannel channel = GrpcChannel.ForAddress(settings.BaseAddress))
            {
                Protos.ResourceService.ResourceServiceClient client = new Protos.ResourceService.ResourceServiceClient(channel);
                Protos.Resource response = await client.SaveAsync(request, await RpcUtil.CreateMetaDataWithAuthHeader(settings));
                return Models.Resource.Create(response);
            }
        }
    }
}
