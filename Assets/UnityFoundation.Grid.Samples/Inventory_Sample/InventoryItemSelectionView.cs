using TMPro;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryItemSelectionView : MonoBehaviour
    {
        private InventoryItemSelection selection;
        private TextMeshProUGUI text;

        public void Start()
        {
            text = transform.FindComponent<TextMeshProUGUI>("item");
        }

        public void Setup(InventoryItemSelection selection)
        {
            this.selection = selection;

            selection.OnInventoryItemChanged += HandleInventoryItemChanged;
            HandleInventoryItemChanged(selection.CurrentItem);
        }

        private void HandleInventoryItemChanged(
            Optional<SelectedValue<XY, InventoryItem>> currentItem
        )
        {
            currentItem
                .Some(item => text.text = item.Value.Name)
                .OrElse(() => text.text = "");
        }
    }
}
