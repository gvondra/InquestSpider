using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InquestSpider.Interface.Resource.Protos;
using InquestSpider.Resource.Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ResourceRPC.Services
{
    public class ResourceExclusionService : InquestSpider.Interface.Resource.Protos.ResourceExclusionService.ResourceExclusionServiceBase
    {
        private readonly SettingsFactory _settingsFactory;
        private readonly ILogger<ResourceService> _logger;
        private readonly IResourceExclusionFactory _resourceExclusionFactory;
        private readonly IResourceExclusionSaver _resourceExclusionSaver;

        public ResourceExclusionService(
            SettingsFactory settingsFactory,
            ILogger<ResourceService> logger,
            IResourceExclusionFactory resourceExclusionFactory,
            IResourceExclusionSaver resourceExclusionSaver)
        {
            _settingsFactory = settingsFactory;
            _logger = logger;
            _resourceExclusionFactory = resourceExclusionFactory;
            _resourceExclusionSaver = resourceExclusionSaver;
        }

        public async override Task<GetAllResourceExclusionResponse> GetAll(Empty request, ServerCallContext context)
        {
            try
            {
                CoreSettings settings = _settingsFactory.CreateCore();
                GetAllResourceExclusionResponse response = new GetAllResourceExclusionResponse();
                foreach (string expression in await _resourceExclusionFactory.GetAll(settings))
                {
                    response.Expressions.Add(expression);
                }
                return response;
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Internal, "Internal System Error"), ex.Message);
            }
        }

        public async override Task<Empty> Save(SaveResourceExclusionRequest request, ServerCallContext context)
        {
            try
            {
                CoreSettings settings = _settingsFactory.CreateCore();
                await _resourceExclusionSaver.Save(settings, request.Expressions);
                return new Empty();
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Internal, "Internal System Error"), ex.Message);
            }
        }
    }
}
