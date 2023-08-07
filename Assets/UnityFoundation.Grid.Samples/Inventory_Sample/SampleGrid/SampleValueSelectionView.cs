using TMPro;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class SampleValueSelectionView : MonoBehaviour
    {
        private TextMeshProUGUI text;

        public TextMeshProUGUI Text {
            get {
                if(text == null)
                    text = transform.FindComponent<TextMeshProUGUI>("item");

                return text;
            }
            set => text = value;
        }

        public void Start()
        {
            Text = transform.FindComponent<TextMeshProUGUI>("item");
        }

        public void Setup(GridScreenSelection<SampleValue> selection)
        {
            selection.OnValueChanged += HandleInventoryItemChanged;
        }

        private void HandleInventoryItemChanged(
            Optional<GridScreenValueSelected<SampleValue>> currentItem
        )
        {
            currentItem
                .Some(item => {
                    Text.text = item.Value.Name;
                })
                .OrElse(() => Text.text = "");
        }

        public void Display()
        {
            Text.text = "";
        }
    }
}
