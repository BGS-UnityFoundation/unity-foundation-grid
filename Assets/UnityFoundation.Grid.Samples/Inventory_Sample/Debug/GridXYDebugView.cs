using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class GridXYDebugView
    {
        private readonly InventoryGrid grid;
        private readonly GridLimitXY limits;
        private Vector2 cellSize;

        public GridXYDebugView(InventoryGrid grid, GridLimitXY limits)
        {
            this.grid = grid;
            this.limits = limits;
        }

        public void Display(RectTransform parent)
        {
            cellSize = new Vector2(
                parent.sizeDelta.x / limits.Width,
                parent.sizeDelta.y / limits.Height
            );

            var debugGrid = new GridXYDebug<InventoryItem>(limits);

            foreach(var coord in limits.GetAllCoordinates())
            {
                debugGrid.SetValue(
                    coord,
                    new GridDebugValue<InventoryItem>(
                        grid.GetValue(coord),
                        parent,
                        ConvertToScreenPosition(coord),
                        cellSize
                    )
                );
            }
        }

        private Vector3 ConvertToScreenPosition(XY coord)
        {
            return new Vector3(
                coord.X * cellSize.x,
                coord.Y * cellSize.y
            );
        }
    }
}
