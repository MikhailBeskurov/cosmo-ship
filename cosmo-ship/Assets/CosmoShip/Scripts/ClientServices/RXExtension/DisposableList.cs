using System;
using System.Collections;
using System.Collections.Generic;

namespace CosmoShip.Scripts.ClientServices.RXExtension
{
    public class DisposableList : ICollection<IDisposable>, IDisposable
    {
        private List<IDisposable> _disposables = new List<IDisposable>();

        public IEnumerator<IDisposable> GetEnumerator()
        {
            return _disposables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _disposables.GetEnumerator();
        }

        public void Add(IDisposable item)
        {
            _disposables.Add(item);
        }

        public void Clear()
        {
            _disposables.Clear();
        }

        public bool Contains(IDisposable item)
        {
            return _disposables.Contains(item);
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            _disposables.CopyTo(array, arrayIndex);
        }

        public bool Remove(IDisposable item)
        {
            return _disposables.Remove(item);
        }

        public int Count => _disposables.Count;
        public bool IsReadOnly => false;


        public void Dispose()
        {
            foreach (var item in _disposables)
            {
                item.Dispose();
            }
            _disposables.Clear();
        }

        public void AddRange(List<IDisposable> disposables)
        {
            _disposables.AddRange(disposables);
        }
    }
}
