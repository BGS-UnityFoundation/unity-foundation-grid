using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{

    public readonly struct InventoryItemSelected
    {
        public InventoryItemSelected(XY key, InventoryItem value)
        {
            Coord = key;
            Value = value;
        }

        public XY Coord { get; }
        public InventoryItem Value { get; }
    }

    public class InventoryItemSelection : GenericSelection<InventoryItemSelected>
    {
        public void Set(XY coord, InventoryItem inventoryItem)
        {
            Set(new(coord, inventoryItem));
        }
    }
}
