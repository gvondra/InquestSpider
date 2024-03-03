using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Core
{
    internal sealed class AsyncDataEnumerable<TData, TResult> : IAsyncEnumerable<TResult>
    {
        private readonly Func<Task<IEnumerable<TData>>> _getDataEnumerable;
        private readonly Func<TData, TResult> _map;

        public AsyncDataEnumerable(Func<Task<IEnumerable<TData>>> getDataEnumerable, Func<TData, TResult> map)
        {
            _getDataEnumerable = getDataEnumerable;
            _map = map;
        }

        public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => new AsyncDataEnumerator<TData, TResult>(_getDataEnumerable, _map);
    }
}
