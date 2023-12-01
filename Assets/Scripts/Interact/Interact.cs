using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    protected GameObject _player;
    protected bool _playerInRange;

    GameObject _item;
    ItemInteractable itemInteractable;
    Player player;

    public void Start()
    {
        itemInteractable = FindObjectOfType<ItemInteractable>();
        player = FindObjectOfType<Player>();

        if(itemInteractable != null)
        {
            _item = itemInteractable._itemPrefab; 
        }
        else
        {
            Debug.Log("Item interactuable no existe");
        }
    }

    private void Update()
    {
        if(_playerInRange && _player != null && Input.GetKeyDown(KeyCode.E))  
        { 
            InteractionAction();
        }
    }

    protected abstract void InteractionAction();

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            _playerInRange = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        _player = null;
        _playerInRange = false;
    }
}
