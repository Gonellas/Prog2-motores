using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slots : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _stackObject;
    [SerializeField] private TextMeshProUGUI _stackLabel;

    public void Set(InventoryItem item)
    {
        _icon.sprite = item.data.icon;
        
        if(item.stackSize <= 1)
        {
            _stackObject.SetActive(false);
            return;
        }

        _stackLabel.text = item.stackSize.ToString();
    }
}
