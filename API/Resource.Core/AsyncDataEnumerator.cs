using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Core
{
    internal sealed class AsyncDataEnumerator<TData, TResult> : IAsyncEnumerator<TResult>
    {
        private readonly Func<Task<IEnumerable<TData>>> _getDataEnumerable;
        private readonly Func<TData, TResult> _map;
        private IEnumerator<TData> _dataEnumerator;

        public AsyncDataEnumerator(Func<Task<IEnumerable<TData>>> getDataEnumerable, Func<TData, TResult> mapp)
        {
            _getDataEnumerable = getDataEnumerable;
            _map = mapp;
        }

        public TResult Current { get; private set; }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            if (_dataEnumerator == null)
                _dataEnumerator = (await _getDataEnumerable()).GetEnumerator();
            bool result = _dataEnumerator.MoveNext();
            if (result)
                Current = _map(_dataEnumerator.Current);
            return result;
        }
    }
}
