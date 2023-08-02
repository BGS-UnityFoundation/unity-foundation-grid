using NUnit.Framework;
using UnityEngine.InputSystem;

namespace UnityFoundation.Grid.Samples.Tests
{
    public class KeyboardsInputsTests : InputTestFixture
    {
        [Test]
        public void Should_permit_to_press_arrow_keys()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            var inputs = new KeyboardInputs();

            Press(keyboard.upArrowKey);
            inputs.Update();
            Assert.That(inputs.UpKeyPressed, Is.True);
            Release(keyboard.upArrowKey);

            Press(keyboard.downArrowKey);
            inputs.Update();
            Assert.That(inputs.DownKeyPressed, Is.True);
            Release(keyboard.downArrowKey);

            Press(keyboard.leftArrowKey);
            inputs.Update();
            Assert.That(inputs.LeftKeyPressed, Is.True);
            Release(keyboard.leftArrowKey);

            Press(keyboard.rightArrowKey);
            inputs.Update();
            Assert.That(inputs.RightKeyPressed, Is.True);
            Release(keyboard.rightArrowKey);

            inputs.Update();
            Assert.That(inputs.UpKeyPressed, Is.False);
            Assert.That(inputs.DownKeyPressed, Is.False);
            Assert.That(inputs.LeftKeyPressed, Is.False);
            Assert.That(inputs.RightKeyPressed, Is.False);
        }
    }
}