using TMPro;
using UnityEngine.UI;
using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.Grid.Samples
{
    public class SampleValueGridScreenView : GridScreenView<SampleValue>
    {
        protected override void OnInstantiateCell(GameObject cell, SampleValue value)
        {
            if(value.Name != null)
            {
                cell.GetComponent<Image>().color = value.Color;
                cell.transform.FindComponent<TextMeshProUGUI>("name").text = value.Name;
            }
            else
            {
                cell.GetComponent<Image>().color = Color.gray;
                cell.transform.FindComponent<TextMeshProUGUI>("name").text = "";
            }
        }
    }
}
