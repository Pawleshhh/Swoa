using CelestialObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa
{
    public class CelestialObjectList : ICelestialObjectCollection, IList<CelestialObject>
    {

        #region Constructors
        public CelestialObjectList()
        {
            celestialObjects = new List<CelestialObject>();
        }

        public CelestialObjectList(List<CelestialObject> celestialObjects)
        {
            this.celestialObjects = celestialObjects ?? throw new ArgumentNullException(nameof(celestialObjects));
        }
        #endregion

        #region Fields

        private List<CelestialObject> celestialObjects;

        #endregion

        #region Properties

        public CelestialObject this[int index]
        {
            get => celestialObjects[index];
            set
            {
                var previous = celestialObjects[index];
                celestialObjects[index] = value;
                OnRemoved(index, previous);
                OnAdded(index, value);
            }
        }

        public int Count => celestialObjects.Count;

        public bool IsReadOnly => ((ICollection<CelestialObject>)celestialObjects).IsReadOnly;

        #endregion

        #region Events
        public event EventHandler<CelestialObjectCollectionChangedEventArgs>? Added;
        public event EventHandler<CelestialObjectCollectionChangedEventArgs>? Removed;
        public event EventHandler<CelestialObjectCollectionChangedEventArgs>? Cleared;

        protected virtual void OnAdded(int index, params CelestialObject[] itemsChanged)
            => InvokeEvent(Added, index, itemsChanged);
        protected virtual void OnRemoved(int index, params CelestialObject[] itemsChanged)
            => InvokeEvent(Removed, index, itemsChanged);
        protected virtual void OnCleared(params CelestialObject[] itemsChanged)
            => InvokeEvent(Cleared, -1, itemsChanged);

        private void InvokeEvent(EventHandler<CelestialObjectCollectionChangedEventArgs>? eventHandler, int index,
            params CelestialObject[] itemsChanged)
            => eventHandler?.Invoke(this, new CelestialObjectCollectionChangedEventArgs(itemsChanged, new int[] { index }));
        #endregion

        #region Methods

        public void Add(CelestialObject item)
        {
            if (item == null)
                return;

            celestialObjects.Add(item);

            OnAdded(celestialObjects.Count - 1, item);
        }

        public void Insert(int index, CelestialObject item)
        {
            if (item == null)
                return;

            celestialObjects.Insert(index, item);

            OnAdded(index, item);
        }

        public bool Remove(CelestialObject item)
        {
            int index = celestialObjects.IndexOf(item);
            if (index < 0)
                return false;

            celestialObjects.RemoveAt(index);

            OnRemoved(index, item);

            return true;
        }

        public void RemoveAt(int index)
        {
            var removed = celestialObjects[index];

            celestialObjects.RemoveAt(index);

            OnRemoved(index, removed);
        }

        public void Clear()
        {
            var removed = celestialObjects;

            celestialObjects = new List<CelestialObject>();

            OnCleared(removed.ToArray());
        }

        public bool Contains(CelestialObject item)
        {
            return celestialObjects.Contains(item);
        }

        public int IndexOf(CelestialObject item)
        {
            return celestialObjects.IndexOf(item);
        }

        public void CopyTo(CelestialObject[] array, int arrayIndex)
        {
            celestialObjects.CopyTo(array, arrayIndex);
        }

        public IEnumerator<CelestialObject> GetEnumerator()
        {
            return celestialObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
