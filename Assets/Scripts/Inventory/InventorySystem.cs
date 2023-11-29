using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedEvent;

    public Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory { get; private set; }

    public static InventorySystem current;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

        if(current == null)
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
        if(_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }
    public void Add(InventoryItemData item)
    {
        if (item == null)
        {
            Debug.LogError("El objeto item es nulo"); 
            return;
        }
        if (_itemDictionary.TryGetValue(item, out InventoryItem value))
        {
            Debug.Log("se agrego el item");
                value.AddToStack();
        }
        else
        {
            Debug.Log("Agregando nuevo item al diccionario");
            InventoryItem newItem = new InventoryItem(item);
            inventory.Add(newItem);
            _itemDictionary.Add(item, newItem);
        }

        onInventoryChangedEvent?.Invoke();
    }

    public void Remove(InventoryItemData item)
    {
        if (_itemDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.RemoveFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                _itemDictionary.Remove(item);
            }
        }

        onInventoryChangedEvent?.Invoke();
    }

    //public void DebugPrintDictionary()
    //{
    //    foreach (var item in _itemDictionary)
    //    {
    //        Debug.Log("Key: " + item.Key + " - Value: " + item.Value);
    //    }
    //}
}
