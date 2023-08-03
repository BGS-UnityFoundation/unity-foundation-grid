using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public sealed class GridXYDebug<T> : GridXY<GridDebugValue<T>>
    {
        public GridXYDebug(GridLimitXY limits) : base(limits)
        {
        }
    }
}
