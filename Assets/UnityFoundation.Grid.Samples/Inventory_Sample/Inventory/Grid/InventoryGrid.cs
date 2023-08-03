using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryGrid : GridXY<InventoryItem>
    {
        public InventoryGrid(GridLimitXY limits) : base(limits)
        {
        }
    }
}
