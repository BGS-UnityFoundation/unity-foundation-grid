using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class MoveCursorCommand : IInventoryCommand
    {
        private readonly KeyboardInputs inputs;
        private readonly GridLimitXY limits;
        private readonly InventoryCursorSelection cursorPosition;

        public MoveCursorCommand(
            KeyboardInputs inputs,
            GridLimitXY limits,
            InventoryCursorSelection cursorPosition
        )
        {
            this.inputs = inputs;
            this.limits = limits;
            this.cursorPosition = cursorPosition;
        }

        public void Execute()
        {
            inputs.Update();

            var baseCoord = new XY(0, 0);
            cursorPosition.Current.Some(coord => baseCoord = coord);

            var x = inputs.RightKeyPressed ? 1 : 0;
            x += inputs.LeftKeyPressed ? -1 : 0;

            var y = inputs.UpKeyPressed ? 1 : 0;
            y += inputs.DownKeyPressed ? -1 : 0;

            if(x != 0 || y != 0)
            {
                var selectedCoord = baseCoord.Move(x, y);

                if(!limits.IsInside(selectedCoord))
                    return;

                cursorPosition.Set(selectedCoord);
            }
        }
    }
}
