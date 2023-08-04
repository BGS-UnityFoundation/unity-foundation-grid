using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryCell
    {
        public GameObject Obj { get; set; }
    }

    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject selectedFramePrefab;
        [SerializeField] private GameObject cursorPrefab;

        private GridXY<InventoryCell> gridView;
        private GridLimitXY limits;
        private InventoryGrid inventoryGrid;
        private Vector2 sizeDelta;
        private Vector2 cellSize;

        private GameObject cursorView;
        private GameObject selectedMark;

        public void Setup(
            GridLimitXY limits,
            InventoryGrid inventoryGrid,
            InventoryCursorSelection cursorPosition,
            InventoryItemSelection itemSelection
        )
        {
            this.limits = limits;
            this.inventoryGrid = inventoryGrid;
            gridView = new(limits);

            cursorPosition.OnValueChanged += HandleCursorPositionChange;
            itemSelection.OnValueChanged += UpdateSelectedItemView;

            sizeDelta = GetComponent<RectTransform>().sizeDelta;
        }

        private void HandleCursorPositionChange(Optional<XY> cursorPosition)
        {
            cursorPosition.Some(coord => UpdateCursorView(coord));
        }

        public void Display()
        {
            cellSize = new Vector2(
                sizeDelta.x / limits.Width,
                sizeDelta.y / limits.Height
            );

            foreach(var coord in limits.GetAllCoordinates())
                InstantiateItem(coord);

            UpdateCursorView(new(0, 0));
        }

        private void UpdateSelectedItemView(Optional<InventoryItemSelected> selectedItem)
        {
            selectedItem.Some(i => InstantiateSelectedFrame(i.Coord))
                .OrElse(() => {
                    selectedMark.SetActive(false);
                });
        }

        private void UpdateCursorView(XY coord)
        {
            if(cursorView == null)
                cursorView = Instantiate(cursorPrefab, transform);

            UpdateCell(cursorView, coord);
        }

        private void InstantiateSelectedFrame(XY coord)
        {
            if(selectedMark == null)
                selectedMark = Instantiate(selectedFramePrefab, transform);

            selectedMark.SetActive(true);
            UpdateCell(selectedMark, coord);
        }

        private void InstantiateItem(XY coord)
        {
            var item = Instantiate(itemPrefab, transform);
            UpdateCell(item, coord);

            var inventoryItem = inventoryGrid.GetValue(coord);

            if(inventoryItem.Name != null)
            {
                item.GetComponent<Image>().color = inventoryItem.Color;
                item.transform.FindComponent<TextMeshProUGUI>("name").text = inventoryItem.Name;
            }
            else
            {
                item.GetComponent<Image>().color = Color.gray;
                item.transform.FindComponent<TextMeshProUGUI>("name").text = "";
            }

            gridView.UpdateValue(coord, c => c.Obj = item);
        }

        private void UpdateCell(GameObject obj, XY coord)
        {
            var rect = obj.GetComponent<RectTransform>();
            SetCellSize(rect);
            UpdatePosition(coord, rect);
        }

        private void SetCellSize(RectTransform rect)
        {
            rect.sizeDelta = cellSize;
        }

        private void UpdatePosition(XY coord, RectTransform rect)
        {
            rect.localPosition = ConvertToScreenPosition(coord);
        }

        private Vector3 ConvertToScreenPosition(XY coord)
        {
            return new Vector3(
                coord.X * cellSize.x,
                coord.Y * cellSize.y
            );
        }

        public void ChangeCells(XY first, XY second)
        {
            var firstCell = gridView.GetValue(first);
            var secondCell = gridView.GetValue(second);

            gridView.Clear(second);
            gridView.SetValue(second, firstCell);
            UpdateCell(firstCell.Obj, second);

            gridView.Clear(first);
            gridView.SetValue(first, secondCell);
            UpdateCell(secondCell.Obj, first);
        }
    }
}
