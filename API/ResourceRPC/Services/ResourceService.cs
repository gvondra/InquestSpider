using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InquestSpider.Interface.Resource.Protos;
using InquestSpider.Resource.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceRPC.Services
{
    public class ResourceService : InquestSpider.Interface.Resource.Protos.ResourceService.ResourceServiceBase
    {
        private readonly SettingsFactory _settingsFactory;
        private readonly ILogger<ResourceService> _logger;
        private readonly IResourceFactory _resourceFactory;

        public ResourceService(SettingsFactory settingsFactory, ILogger<ResourceService> logger, IResourceFactory resourceFactory)
        {
            _settingsFactory = settingsFactory;
            _logger = logger;
            _resourceFactory = resourceFactory;
        }

        [Authorize(Constants.POLICY_RESOURCE_READ)]
        public override async Task GetAll(Empty request, IServerStreamWriter<Resource> responseStream, ServerCallContext context)
        {
            try
            {
                CoreSettings settings = _settingsFactory.CreateCore();
                await foreach (IResource resource in await _resourceFactory.GetAll(settings))
                {
                    await responseStream.WriteAsync(Map(resource));
                }
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

        [Authorize(Constants.POLICY_RESOURCE_READ)]
        public override async Task<Resource> GetByUrl(GetResourceByUrlRequest request, ServerCallContext context)
        {
            try
            {
                CoreSettings settings = _settingsFactory.CreateCore();
                IResource resource = await _resourceFactory.GetByUrl(settings, request.Url);
                return Map(resource);
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

        [Authorize(Constants.POLICY_RESOURCE_EDIT)]
        public async override Task<Resource> Save(Resource request, ServerCallContext context)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Url))
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Bad Request"), "Missing url property value");
                CoreSettings settings = _settingsFactory.CreateCore();
                IResource innerResource = (await _resourceFactory.GetByUrl(settings, request.Url))
                    ?? _resourceFactory.Create(request.Url);
                Map(request, innerResource);
                await innerResource.Save(settings);
                return Map(innerResource);
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

        private static void Map(Resource resource, IResource innerResource)
        {
            innerResource.Status = resource.Status;
            if (resource.Headers != null)
            {
                innerResource.Headers = resource.Headers.ToDictionary(
                    (KeyValuePair<string, string> pair) => pair.Key,
                    (KeyValuePair<string, string> pair) => pair.Value);
            }
        }

        private static Resource Map(IResource resource)
        {
            Resource result = new Resource
            {
                ResourceId = resource.ResourceId,
                Url = resource.Url,
                Status = resource.Status ?? string.Empty,
                CreateTimestamp = Timestamp.FromDateTime(resource.CreateTimestamp),
                UpdateTimestamp = Timestamp.FromDateTime(resource.UpdateTimestamp)
            };
            if (resource.Headers != null)
            {
                foreach (KeyValuePair<string, string> pair in resource.Headers)
                {
                    result.Headers.Add(pair.Key, pair.Value);
                }
            }
            return result;
        }
    }
}
