using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public abstract class GridScreenView<T> : MonoBehaviour
        where T : new()
    {
        public class GridCellView
        {
            public GameObject Obj { get; set; }
        }

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject selectedFramePrefab;
        [SerializeField] private GameObject cursorPrefab;

        private GridXY<GridCellView> gridView;
        private GridLimitXY limits;
        private GridXY<T> grid;
        private Vector2 sizeDelta;
        private Vector2 cellSize;

        private GameObject cursorView;
        private GameObject selectedMark;

        public void Setup(
            GridLimitXY limits,
            GridXY<T> grid,
            CursorSelection cursorPosition,
            GridScreenSelection<T> valueSelection
        )
        {
            this.limits = limits;
            this.grid = grid;
            gridView = new(limits);

            cursorPosition.OnValueChanged += HandleCursorPositionChange;
            valueSelection.OnValueChanged += UpdateSelectedItemView;

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
                InstantiateCell(coord);

            UpdateCursorView(new(0, 0));
        }

        private void UpdateSelectedItemView(Optional<GridScreenValueSelected<T>> selectedItem)
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

            cursorView.transform.SetAsLastSibling();
            UpdateCell(cursorView, coord);
        }

        private void InstantiateSelectedFrame(XY coord)
        {
            if(selectedMark == null)
                selectedMark = Instantiate(selectedFramePrefab, transform);

            selectedMark.SetActive(true);
            UpdateCell(selectedMark, coord);
        }

        private void InstantiateCell(XY coord)
        {
            var item = Instantiate(itemPrefab, transform);
            UpdateCell(item, coord);

            var value = grid.GetValue(coord);
            OnInstantiateCell(item, value);

            gridView.UpdateValue(coord, c => c.Obj = item);
        }

        private void UpdateCell(GameObject obj, XY coord)
        {
            var rect = obj.GetComponent<RectTransform>();
            rect.sizeDelta = cellSize;
            rect.localPosition = ConvertToScreenPosition(coord);
        }

        private Vector3 ConvertToScreenPosition(XY coord)
        {
            return new Vector3(coord.X * cellSize.x, coord.Y * cellSize.y);
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

        protected abstract void OnInstantiateCell(GameObject cellObj, T value);
    }
}
