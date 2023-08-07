using NUnit.Framework;
using UnityFoundation.Code;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Grid.Samples.Tests
{
    public class SampleValueSelectionTests
    {
        [Test]
        public void Should_have_no_selected_item_when_not_selected()
        {
            var selection = new SampleValueSelection();
            Assert.That(selection.Current.IsPresent, Is.False);
        }

        [Test]
        public void Should_permit_to_set_item()
        {
            var selection = new SampleValueSelection();
            var item = new SampleValue();
            var selectedCoord = new XY(0, 0);

            var changedEvent = new EventTest<Optional<GridScreenValueSelected<SampleValue>>>(
                selection, nameof(selection.OnValueChanged)
            );

            selection.Set(selectedCoord, item);

            Assert.That(selection.Current.IsPresent, Is.True);
            Assert.That(selection.Current.Get().Coord, Is.EqualTo(selectedCoord));
            Assert.That(selection.Current.Get().Value, Is.EqualTo(item));
            Assert.That(changedEvent.WasTriggered, Is.True);
        }
    }
}