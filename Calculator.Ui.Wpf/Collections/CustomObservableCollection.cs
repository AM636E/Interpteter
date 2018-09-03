using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Calculator.Ui.Wpf.Collections
{
    public class CustomObservableCollection<T> : IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private readonly bool _isReversed;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<T> _underlyingCollection;

        public CustomObservableCollection(bool isReversed = false)
        {
            _isReversed = isReversed;
            _underlyingCollection = new List<T>();
        }

        public void AddAndNotify(T item)
        {
            if (_isReversed)
            {
                var copy = _underlyingCollection.ToList();
                _underlyingCollection.Clear();
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                _underlyingCollection.Add(item);
                _underlyingCollection.AddRange(copy);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _underlyingCollection));
            }
            else
            {
                _underlyingCollection.Add(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _underlyingCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
