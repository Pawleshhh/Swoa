using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public class ObservableDictionary<TKey, TValue> : ICollection<TValue>, IEnumerable<TValue>, IReadOnlyCollection<TValue>, IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged, INotifyPropertyChanging
    {

        #region Constructors

        public ObservableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(Dictionary<TKey, TValue> dict)
        {
            _dictionary = dict ?? throw new ArgumentNullException(nameof(dict));
        }

        #endregion

        #region Fields

        private readonly Dictionary<TKey, TValue> _dictionary;

        #endregion

        #region Properties

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary[key] = value;

                OnCollectionChanged(NotifyCollectionChangedAction.Replace);
            }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;

        public int Count => _dictionary.Count;
        public bool IsReadOnly => ((ICollection<TValue>)_dictionary).IsReadOnly;

        #endregion

        #region Events

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction n)
            => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(n));

        #endregion

        #region Methods

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);

            OnCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(TValue item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _dictionary.Clear();

            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public bool Contains(TValue item)
        {
            return _dictionary.Values.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            if (_dictionary.Remove(key))
            {
                OnCollectionChanged(NotifyCollectionChangedAction.Remove);
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(TValue item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        #endregion
    }
}
