using UnityEngine;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public override string ToString() => Name;
    }
}
