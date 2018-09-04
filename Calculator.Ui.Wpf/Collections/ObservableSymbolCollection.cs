using Calculator.Core.Symbols;
using Calculator.Ui.Wpf.Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Calculator.Ui.Wpf.Collections
{
    public class ObservableSymbolCollection : INotifyCollectionChanged, IEnumerable<SymbolItem>
    {
        private readonly IReadOnlyDictionary<string, double> _symbolDictionary;
        private List<SymbolItem> Items { get; set; } = new List<SymbolItem>();
        public ObservableSymbolCollection(SymbolHolder holder)
        {
            _symbolDictionary = holder.SymbolDictionary;
            Update();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Update()
        {
            Items.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            foreach (var kv in _symbolDictionary)
            {
                Items.Add(new SymbolItem
                {
                    Name = kv.Key,
                    Value = kv.Value.ToString()
                });
            }

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Items));
        }

        public IEnumerator<SymbolItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
