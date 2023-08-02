using System;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public readonly struct SelectedValue<TKey, TValue>
    {
        public SelectedValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; }
        public TValue Value { get; }
    }

    public class InventoryItemSelection
    {
        public Optional<SelectedValue<XY, InventoryItem>> CurrentItem { get; private set; }
        public event Action<Optional<SelectedValue<XY, InventoryItem>>> OnInventoryItemChanged;

        public InventoryItemSelection()
        {
            CurrentItem = Optional<SelectedValue<XY, InventoryItem>>.None();
        }

        public void Set(XY coord, InventoryItem inventoryItem)
        {
            CurrentItem = Optional<SelectedValue<XY, InventoryItem>>
                .Some(new(coord, inventoryItem));
            OnInventoryItemChanged?.Invoke(CurrentItem);
        }
    }
}
