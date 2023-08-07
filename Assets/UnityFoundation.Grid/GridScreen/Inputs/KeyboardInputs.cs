using UnityEngine.InputSystem;
using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public class KeyboardInputs : IUpdatable
    {
        public bool UpKeyPressed { get; private set; }
        public bool DownKeyPressed { get; private set; }
        public bool LeftKeyPressed { get; private set; }
        public bool RightKeyPressed { get; private set; }
        public bool SpaceKeyPressed { get; private set; }
        public bool EnterKeyPressed { get; private set; }

        public void Update(float deltaTime = 0)
        {
            UpKeyPressed = Keyboard.current.upArrowKey.wasPressedThisFrame;
            DownKeyPressed = Keyboard.current.downArrowKey.wasPressedThisFrame;
            LeftKeyPressed = Keyboard.current.leftArrowKey.wasPressedThisFrame;
            RightKeyPressed = Keyboard.current.rightArrowKey.wasPressedThisFrame;
            SpaceKeyPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
            EnterKeyPressed = Keyboard.current.enterKey.wasPressedThisFrame;
        }
    }
}
