using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    [SerializeField] Transform _itemTransform;
    public GameObject _itemPrefab;

    private void Start()
    {
        if(_itemPrefab != null)
        {
            _itemTransform = _itemPrefab.transform;
        }
    }
}
