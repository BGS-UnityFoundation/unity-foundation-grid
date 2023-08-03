using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject selectedFramePrefab;
        [SerializeField] private GameObject cursorPrefab;

        private GridLimitXY limits;

        private Vector2 sizeDelta;
        private Vector2 cellSize;

        private GameObject cursorView;
        private GameObject selectedMark;

        public void Setup(
            GridLimitXY limits,
            InventoryCursorSelection cursorPosition,
            InventoryItemSelection itemSelection
        )
        {
            this.limits = limits;

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
            selectedItem.Some(i => InstantiateSelectedFrame(i.Coord));
        }

        private void UpdateCursorView(XY coord)
        {
            if(cursorView == null)
                cursorView = Instantiate(cursorPrefab, transform);

            var rect = cursorView.GetComponent<RectTransform>();
            SetCellSize(rect);
            UpdatePosition(coord, rect);
        }

        private void InstantiateSelectedFrame(XY coord)
        {
            if(selectedMark == null)
                selectedMark = Instantiate(selectedFramePrefab, transform);

            var rect = selectedMark.GetComponent<RectTransform>();
            SetCellSize(rect);
            UpdatePosition(coord, rect);
        }

        private void InstantiateItem(XY coord)
        {
            var item = Instantiate(itemPrefab, transform);

            var rect = item.GetComponent<RectTransform>();
            SetCellSize(rect);
            UpdatePosition(coord, rect);

            var image = item.GetComponent<Image>();
            image.color = ColorGenerator.Random();
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
    }
}
