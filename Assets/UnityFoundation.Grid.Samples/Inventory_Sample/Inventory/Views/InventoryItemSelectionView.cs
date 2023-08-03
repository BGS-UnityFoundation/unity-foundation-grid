using TMPro;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryItemSelectionView : MonoBehaviour
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

        public void Setup(InventoryItemSelection selection)
        {
            selection.OnValueChanged += HandleInventoryItemChanged;
        }

        private void HandleInventoryItemChanged(Optional<InventoryItemSelected> currentItem)
        {
            currentItem
                .Some(item => {
                    Debug.Log(item.Value);
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
