using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public class SelectedItemCommand<T> : IGridScreenCommand where T : new()
    {
        private readonly KeyboardInputs inputs;
        private readonly CursorSelection cursorPosition;
        private readonly GridScreenSelection<T> itemSelection;
        private readonly GridXY<T> grid;
        private readonly GridScreenView<T> gridView;

        public SelectedItemCommand(
            KeyboardInputs inputs,
            CursorSelection cursorPosition,
            GridScreenSelection<T> valueSelection,
            GridXY<T> grid,
            GridScreenView<T> gridView
        )
        {
            this.inputs = inputs;
            this.cursorPosition = cursorPosition;
            itemSelection = valueSelection;
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
            if(!itemSelection.Current.IsPresentAndGet(out GridScreenValueSelected<T> item))
            {
                itemSelection.Set(new(cursorCoord, cursorItem));
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
