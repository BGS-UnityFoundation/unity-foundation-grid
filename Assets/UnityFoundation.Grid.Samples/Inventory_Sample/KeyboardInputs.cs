using UnityEngine.InputSystem;

namespace UnityFoundation.Grid.Samples
{
    public class KeyboardInputs
    {
        public bool UpKeyPressed { get; private set; }
        public bool DownKeyPressed { get; private set; }
        public bool LeftKeyPressed { get; private set; }
        public bool RightKeyPressed { get; private set; }
        public bool SpaceKeyPressed { get; private set; }

        public void Update()
        {
            UpKeyPressed = Keyboard.current.upArrowKey.wasPressedThisFrame;
            DownKeyPressed = Keyboard.current.downArrowKey.wasPressedThisFrame;
            LeftKeyPressed = Keyboard.current.leftArrowKey.wasPressedThisFrame;
            RightKeyPressed = Keyboard.current.rightArrowKey.wasPressedThisFrame;
            SpaceKeyPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        }
    }
}
