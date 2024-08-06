using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedEvent;

    public Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory { get; private set; }
    private Dictionary<string, GameObject> _itemIcons;

    public static InventorySystem current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        _itemIcons = new Dictionary<string, GameObject>();
    }

    public void ResetInventory()
    {
        Debug.Log("Resetting inventory...");
        inventory.Clear();
        _itemDictionary.Clear();
        onInventoryChangedEvent?.Invoke();
        Debug.Log("Inventory reset completed. Item count: " + inventory.Count);
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData item)
    {
        if (item == null)
        {
            Debug.LogError("Item data is null");
            return;
        }

        if (_itemDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            Debug.Log("Adding new item to dictionary");
            InventoryItem newItem = new InventoryItem(item);
            inventory.Add(newItem);
            _itemDictionary.Add(item, newItem);
        }

        int numberOfItems = _itemDictionary.Count;
        Debug.Log("Number of items in dictionary: " + numberOfItems);

        onInventoryChangedEvent?.Invoke();
    }

    public void RemoveItemFromInventory(string itemID, string itemName)
    {
        InventoryItemData itemToRemove = null;

        foreach (var pair in _itemDictionary)
        {
            InventoryItemData itemData = pair.Key;
            if (itemData.id == itemID && itemData.displayName == itemName)
            {
                itemToRemove = itemData;
                break;
            }
        }

        if (itemToRemove != null)
        {
            Remove(itemToRemove);
        }
        else
        {
            Debug.Log("Item not found in inventory");
        }
    }

    public void Remove(InventoryItemData item)
    {
        if (_itemDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                _itemDictionary.Remove(item);
            }
        }

        int numberOfItems = _itemDictionary.Count;
        Debug.Log("Number of items in dictionary: " + numberOfItems);
        onInventoryChangedEvent?.Invoke();
    }

    public bool HasItemWithDetails(string itemID, string itemName)
    {
        foreach (var pair in _itemDictionary)
        {
            InventoryItemData itemData = pair.Key;
            if (itemData.id == itemID && itemData.displayName == itemName)
            {
                return true;
            }
        }
        return false;
    }
}