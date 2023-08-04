using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

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
            Bind();

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

        private void Bind()
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
        }

        private void FillInventoryGrid()
        {
            var grid = container.Resolve<InventoryGrid>();

            grid.SetValue(
                new(0, 0),
                new InventoryItem() { Name = $"Item 0", Color = ColorGenerator.Random() }
            );

            grid.SetValue(
                new(2, 3),
                new InventoryItem() { Name = $"Item 1", Color = ColorGenerator.Random() }
            );

            grid.SetValue(
                new(1, 1),
                new InventoryItem() { Name = $"Item 2", Color = ColorGenerator.Random() }
            );
        }
    }
}
