using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public static class XYExtensions
    {
        public static XY Move(this XY value, int x, int y)
        {
            return new(value.X + x, value.Y + y);
        }
    }
}
