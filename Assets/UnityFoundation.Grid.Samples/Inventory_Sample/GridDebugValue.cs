using TMPro;
using UnityEngine;

namespace UnityFoundation.Grid.Samples
{
    public sealed class GridDebugValue<T>
    {
        public T Value { get; set; }
        private readonly TextMeshProUGUI text;
        private readonly Transform parent;
        private readonly Vector2 cellSize;

        public GridDebugValue()
        {
        }

        public GridDebugValue(T value, RectTransform parent, Vector3 position, Vector2 cellSize)
        {
            Value = value;
            this.parent = parent;
            this.cellSize = cellSize;

            text = InstantiateText(value.ToString(), position);
        }

        private TextMeshProUGUI InstantiateText(string value, Vector3 position)
        {
            var text = new GameObject("text");
            text.transform.SetParent(parent);

            var transform = text.AddComponent<RectTransform>();
            transform.sizeDelta = cellSize;
            transform.localPosition = position + new Vector3(cellSize.x / 2, cellSize.y / 2);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            var textMesh = text.AddComponent<TextMeshProUGUI>();
            textMesh.text = value;
            textMesh.color = Color.white;
            textMesh.fontSize = 12;
            textMesh.alignment = TextAlignmentOptions.Midline;

            return textMesh;
        }
    }
}
