using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Grid.Samples
{
    public class GridScreenSampleManager : MonoBehaviour
    {
        [SerializeField] int width = 3;
        [SerializeField] int height = 3;

        [SerializeField] SampleValueSelectionView selectionView;
        [SerializeField] SampleValueGridScreenView inventoryView;

        private GridScreenManager<SampleValue> gridManager;

        public void Start()
        {
            gridManager = new GridScreenManager<SampleValue>(
                 new(width, height), inventoryView
             );
            gridManager.Start();

            FillInventoryGrid();

            selectionView.Setup(gridManager.ValueSelection);
            selectionView.Display();

            gridManager.Display();
        }

        private void FillInventoryGrid()
        {
            var grid = gridManager.Grid;

            grid.SetValue(
                new(0, 0),
                new SampleValue() { Name = $"Item 0", Color = ColorGenerator.Random() }
            );

            grid.SetValue(
                new(2, 3),
                new SampleValue() { Name = $"Item 1", Color = ColorGenerator.Random() }
            );

            grid.SetValue(
                new(1, 1),
                new SampleValue() { Name = $"Item 2", Color = ColorGenerator.Random() }
            );
        }
    }
}
