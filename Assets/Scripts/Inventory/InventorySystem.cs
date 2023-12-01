using System.Collections;
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
        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        _itemIcons = new Dictionary<string, GameObject>();

        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        InventoryItemData itemdata = item;
        Debug.Log(itemdata);

        if (item == null)
        {
            return;
        }
        if (_itemDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            Debug.Log("Agregando nuevo item al diccionario");
            InventoryItem newItem = new InventoryItem(item);
            inventory.Add(newItem);
            _itemDictionary.Add(item, newItem);
        }

        int numberOfItems = _itemDictionary.Count;
        Debug.Log("Número de elementos en el diccionario: " + numberOfItems);
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
            Debug.Log("No se encontró el ítem en el inventario");
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
        Debug.Log("Número de elementos en el diccionario: " + numberOfItems);
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
