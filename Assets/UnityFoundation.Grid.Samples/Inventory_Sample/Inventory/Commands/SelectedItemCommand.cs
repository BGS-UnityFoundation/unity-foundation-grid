using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class SelectedItemCommand : IInventoryCommand
    {
        private readonly KeyboardInputs inputs;
        private readonly InventoryCursorSelection cursorPosition;
        private readonly InventoryItemSelection itemSelection;
        private readonly InventoryGrid grid;

        public SelectedItemCommand(
            KeyboardInputs inputs,
            InventoryCursorSelection cursorPosition,
            InventoryItemSelection itemSelection,
            InventoryGrid grid
        )
        {
            this.inputs = inputs;
            this.cursorPosition = cursorPosition;
            this.itemSelection = itemSelection;
            this.grid = grid;
        }

        public void Execute()
        {
            if(!inputs.SpaceKeyPressed)
                return;

            if(!cursorPosition.Current.IsPresentAndGet(out XY cursorCoord))
                return;

            var cursorItem = grid.GetValue(cursorCoord);
            if(!itemSelection.Current.IsPresentAndGet(out InventoryItemSelected item))
            {
                itemSelection.Set(cursorCoord, cursorItem);
                return;
            }

            var selectedItem = grid.GetValue(item.Coord);

            grid.SetValue(cursorCoord, selectedItem);
            grid.SetValue(item.Coord, cursorItem);
            itemSelection.Clear();
        }
    }
}
