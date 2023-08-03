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

        private IDependencyContainer container;

        public void Start()
        {
            var binder = new DependencyBinder();
            binder.RegisterSingleton<KeyboardInputs, KeyboardInputs>();
            binder.RegisterSingleton<InventoryItemSelection, InventoryItemSelection>();
            binder.RegisterSingleton<InventoryCursorSelection, InventoryCursorSelection>();
            binder.Register(new GridLimitXY(width, height));
            binder.RegisterSingleton<InventoryGrid, InventoryGrid>();
            binder.RegisterSetup(selectionView);
            binder.RegisterSetup(inventoryView);

            binder.Register<InventoryCommands>();
            binder.Register<MoveCursorCommand>();
            binder.Register<SelectedItemCommand>();

            container = binder.Build();

            FillInventoryGrid();
            container.Resolve<InventoryCursorSelection>().Set(new(0, 0));

            var commands = container.Resolve<InventoryCommands>();
            commands.Register(container.Resolve<MoveCursorCommand>());
            commands.Register(container.Resolve<SelectedItemCommand>());

            var updateProcessor = UpdateProcessor.Create();
            updateProcessor.Register(container.Resolve<KeyboardInputs>());
            updateProcessor.Register(commands);

            container.Resolve<InventoryView>().Display();
            container.Resolve<InventoryItemSelectionView>().Display();
        }

        private void FillInventoryGrid()
        {
            var limits = container.Resolve<GridLimitXY>();
            var grid = container.Resolve<InventoryGrid>();

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
        }
    }
}
