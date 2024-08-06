using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;

    public void Start()
    {
        InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach(InventoryItem item in InventorySystem.current.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(transform, false);

        Slots slot = obj.GetComponent<Slots>();
        slot.Set(item);
    }

    public void RemoveInventorySlot(GameObject slotObject)
    {
        Destroy(slotObject);
    }

    private void OnDestroy()
    {
        if (InventorySystem.current != null)
        {
            InventorySystem.current.onInventoryChangedEvent -= OnUpdateInventory;
        }
    }
}
