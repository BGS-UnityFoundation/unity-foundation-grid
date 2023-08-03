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

    public class InventoryItemSelection : GenericSelection<SelectedValue<XY, InventoryItem>>
    {
        public void Set(XY coord, InventoryItem inventoryItem)
        {
            Set(new(coord, inventoryItem));
        }
    }
}
