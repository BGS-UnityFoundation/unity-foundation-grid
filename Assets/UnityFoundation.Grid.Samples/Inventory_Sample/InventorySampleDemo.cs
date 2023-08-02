using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class InventorySampleDemo : MonoBehaviour
    {
        [SerializeField] int width = 3;
        [SerializeField] int height = 3;

        [SerializeField] InventoryItemSelectionView selectionView;
        [SerializeField] InventoryView inventoryView;
        [SerializeField] RectTransform debugPanel;

        InventoryItemSelection itemSelection;
        private GridLimitXY limits;
        KeyboardInputs inputs;
        private GridXY<InventoryItem> grid;

        public void Start()
        {
            inputs = new KeyboardInputs();
            itemSelection = new InventoryItemSelection();

            this.limits = new GridLimitXY(width, height);
            this.grid = new GridXY<InventoryItem>(limits);

            var index = 0;
            foreach(var coord in limits.GetAllCoordinates())
            {
                grid.SetValue(
                    coord,
                    new InventoryItem() {
                        Name = $"Item {index++}"
                    }
                );
            }
            var initialCoord = new XY(0, 0);
            itemSelection.Set(initialCoord, grid.GetValue(initialCoord));

            selectionView.Setup(itemSelection);
            inventoryView.Setup(grid, limits, itemSelection);
            new GridXYDebugView().Display(grid, limits, debugPanel);
        }

        public void Update()
        {
            inputs.Update();

            var baseCoord = new XY(0, 0);
            itemSelection.CurrentItem.Some(item => baseCoord = item.Key);

            var x = inputs.RightKeyPressed ? 1 : 0;
            x += inputs.LeftKeyPressed ? -1 : 0;

            var y = inputs.UpKeyPressed ? 1 : 0;
            y += inputs.DownKeyPressed ? -1 : 0;

            if(x != 0 || y != 0)
            {
                var selectedCoord = baseCoord.Move(x, y);

                if(!limits.IsInside(selectedCoord))
                    return;

                itemSelection.Set(selectedCoord, grid.GetValue(selectedCoord));
            }
        }
    }
}
