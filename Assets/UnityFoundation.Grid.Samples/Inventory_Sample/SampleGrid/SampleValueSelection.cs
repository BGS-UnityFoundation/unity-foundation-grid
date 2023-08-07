using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class SampleValueSelection : GridScreenSelection<SampleValue>
    {
        public void Set(XY coord, SampleValue inventoryItem)
        {
            Set(new(coord, inventoryItem));
        }
    }
}
