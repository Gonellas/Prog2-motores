using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;

    public void Start()
    {
        InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
    }
    //public void Update()
    //{
    //    InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
    //}

    private void OnUpdateInventory()
    {
        //foreach(Transform t in transform)
        //{
        //    Destroy(t.gameObject);
        //}

        DrawInventory();
    }

    public void DrawInventory()
    {
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
}
