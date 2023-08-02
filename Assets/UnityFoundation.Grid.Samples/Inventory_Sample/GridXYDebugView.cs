using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class GridXYDebugView
    {
        private Vector2 cellSize;

        public void Display<T>(GridXY<T> grid, GridLimitXY limits, RectTransform parent)
            where T : new()
        {
            cellSize = new Vector2(
                parent.sizeDelta.x / limits.Width, 
                parent.sizeDelta.y / limits.Height
            );

            var debugGrid = new GridXYDebug<T>(limits);

            foreach(var coord in limits.GetAllCoordinates())
            {
                debugGrid.SetValue(
                    coord,
                    new GridDebugValue<T>(
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
