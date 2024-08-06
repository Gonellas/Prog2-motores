using UnityEngine;

//TPFinal Emi Cassarino

public abstract class Interact : MonoBehaviour
{
    protected GameObject _player;
    protected bool _playerInRange;

    private void Update()
    {
        if (_playerInRange && _player != null && Input.GetKeyDown(KeyCode.E))
        {
            InteractionAction();
        }
    }

    public abstract void InteractionAction();

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
        if (other.CompareTag("Player"))
        {
            _player = null;
            _playerInRange = false;
        }
    }
}
