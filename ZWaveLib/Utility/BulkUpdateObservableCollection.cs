using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
namespace ZWaveLib
{
    public class BulkUpdateObservableCollection<T> : ObservableCollection<T>
    {
        bool isBulkUpdating;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!isBulkUpdating)
                base.OnCollectionChanged(e);
        }

        public void Replace(IEnumerable<T> items)
        {
            if (!items?.Any() ?? false)
            {
                Clear();
                return;
            }
            isBulkUpdating = true;
            Clear();

            foreach (var item in items)
                Add(item);
            isBulkUpdating = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public void AddRange(IEnumerable<T> items)
        {
            if (!items?.Any() ?? false)
            {
                return;
            }

            isBulkUpdating = true;
            var startingIndex = items.Count();
            foreach (var item in items)
                Add(item);
            isBulkUpdating = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToList()));
        }
        public void Remove(IEnumerable<T> items)
        {
            if (!items?.Any() ?? false)
            {
                return;
            }

            isBulkUpdating = true;
            var startingIndex = items.Count();
            foreach (var item in items)
                Remove(item);
            isBulkUpdating = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items.ToList()));
        }
    }
}
