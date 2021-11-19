using CelestialObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public class CelestialObjectDictionary : ICelestialObjectCollection, IDictionary<long, CelestialObject>
    {

        #region Constructors

        public CelestialObjectDictionary()
        {
            _dictionary = new Dictionary<long, CelestialObject>();
        }

        public CelestialObjectDictionary(Dictionary<long, CelestialObject> dict)
        {
            _dictionary = dict ?? throw new ArgumentNullException(nameof(dict));
        }

        #endregion

        #region Fields

        private Dictionary<long, CelestialObject> _dictionary;

        #endregion

        #region Properties

        public CelestialObject this[long key]
        {
            get => _dictionary[key];
            set
            {
                var previous = _dictionary[key];
                _dictionary[key] = value;
                OnRemoved(previous);
                OnAdded(value);
            }
        }

        public ICollection<long> Keys => _dictionary.Keys;

        public ICollection<CelestialObject> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => ((ICollection<CelestialObject>)_dictionary).IsReadOnly;

        #endregion

        #region Events

        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Added;
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Removed;
        public event EventHandler<CelestialObjectCollectionChangedEventArgs> Cleared;

        protected virtual void OnAdded(params CelestialObject[] itemsChanged)
            => InvokeEvent(Added, -1, itemsChanged);
        protected virtual void OnRemoved(params CelestialObject[] itemsChanged)
            => InvokeEvent(Removed, -1, itemsChanged);
        protected virtual void OnCleared(params CelestialObject[] itemsChanged)
            => InvokeEvent(Cleared, -1, itemsChanged);

        private void InvokeEvent(EventHandler<CelestialObjectCollectionChangedEventArgs>? eventHandler, int index,
            params CelestialObject[] itemsChanged)
            => eventHandler?.Invoke(this, new CelestialObjectCollectionChangedEventArgs(itemsChanged, new int[] { index }));

        #endregion

        #region Methods

        public void Add(long key, CelestialObject value)
        {
            _dictionary.Add(key, value);

            OnAdded(value);
        }

        public void Add(KeyValuePair<long, CelestialObject> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(CelestialObject item)
        {
            Add(item.Id, item);
        }

        public void Clear()
        {
            var removed = _dictionary.Values.ToArray();

            _dictionary.Clear();

            OnCleared(removed);
        }

        public bool Contains(KeyValuePair<long, CelestialObject> item)
        {
            return _dictionary.ContainsKey(item.Key);
        }

        public bool Contains(CelestialObject item)
        {
            return _dictionary.ContainsKey(item.Id);
        }

        public bool ContainsKey(long key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<long, CelestialObject>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(CelestialObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(long key)
        {
            if (_dictionary.Remove(key, out CelestialObject? celestialObj))
            {
                OnRemoved(celestialObj);
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<long, CelestialObject> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(CelestialObject item)
        {
            return Remove(item.Id);
        }

        public bool TryGetValue(long key, [MaybeNullWhen(false)] out CelestialObject value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<long, CelestialObject>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<CelestialObject> IEnumerable<CelestialObject>.GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        #endregion

    }
}
