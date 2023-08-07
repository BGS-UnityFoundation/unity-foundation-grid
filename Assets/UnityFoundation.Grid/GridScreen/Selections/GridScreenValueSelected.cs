using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public class GridScreenValueSelected<T>
    {
        public GridScreenValueSelected(XY key, T value)
        {
            Coord = key;
            Value = value;
        }

        public XY Coord { get; }
        public T Value { get; }
    }

    public class GridScreenSelection<T> : GenericSelection<GridScreenValueSelected<T>>
    {
    }
}
