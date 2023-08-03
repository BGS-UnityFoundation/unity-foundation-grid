using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class SelectedItemCommand : IInventoryCommand
    {
        private readonly KeyboardInputs inputs;
        private readonly InventoryCursorSelection cursorPosition;
        private readonly InventoryItemSelection itemSelection;
        private readonly InventoryGrid grid;
        private readonly InventoryView gridView;

        public SelectedItemCommand(
            KeyboardInputs inputs,
            InventoryCursorSelection cursorPosition,
            InventoryItemSelection itemSelection,
            InventoryGrid grid,
            InventoryView gridView
        )
        {
            this.inputs = inputs;
            this.cursorPosition = cursorPosition;
            this.itemSelection = itemSelection;
            this.grid = grid;
            this.gridView = gridView;
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

            grid.Clear(cursorCoord);
            grid.SetValue(cursorCoord, selectedItem);

            grid.Clear(item.Coord);
            grid.SetValue(item.Coord, cursorItem);

            gridView.ChangeCells(cursorCoord, item.Coord);

            itemSelection.Clear();
        }
    }
}
