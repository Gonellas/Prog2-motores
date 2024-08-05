using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IMessage
{
    [Header("Values")]
    public float _speed;
    [SerializeField] float _gravity;
    [SerializeField] float _jumpForce;
    [SerializeField] float _groundDistance;
    [SerializeField] bool _isGrounded;

    Rigidbody _rb;
    Vector3 _newDir;
    Vector3 _dir;
    PlayerController _controller;
    PlayerMovement _movement;
    PlayerView _view;
    PlayerHealth playerHealth;

    public InventoryItemData healthItem;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform _camTransform;

    [SerializeField] GameObject _interactionMessage;
    [SerializeField] GameObject scrollMessage;
    Interact interact;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();

        _movement = new PlayerMovement(transform, _camTransform, groundMask, _rb, _newDir, _view, _speed, _gravity, _jumpForce, _groundDistance, _isGrounded);
        _controller = new PlayerController(_movement, _dir);

        _movement.ArtificialAwake();
    }

    private void FixedUpdate()
    {
        _controller.ListenFixedKeys();
        _movement.ArtificialFixedUpdate();
        _movement.SetMovementSpeed(_speed);
        playerHealth.UpdateHealth(playerHealth.currentHealth);
    }

    private void Update()
    {
        _controller.ArtificialUpdate();

        if(Input.GetKeyDown(KeyCode.E) && interact != null)
        {
            interact.InteractionAction();
        }
    }

    public void ActiveUI(GameObject message)
    {
        MessageType messageType = message.GetComponent<MessageType>();
        if (messageType != null)
        {
            if (messageType.messageType == MessageType.MessageTypeEnum.Scroll)
            {
                Scroll scroll = message.GetComponent<Scroll>();
                if (scroll != null) scroll.ActiveScroll();

            }
            else if (messageType.messageType == MessageType.MessageTypeEnum.Interact)
            {
                if (_interactionMessage != null)
                {
                    _interactionMessage.SetActive(false);
                }

                _interactionMessage = message;
                _interactionMessage.SetActive(true);
            }
        }
    }

    public void DeactivateUI()
    {
        if (_interactionMessage != null)
        {
            MessageType messageType = _interactionMessage.GetComponent<MessageType>();
            if (messageType != null)
            {
                Debug.Log("MessageType detected: " + messageType.messageType);
                if (messageType.messageType == MessageType.MessageTypeEnum.Scroll)
                {
                    Scroll scroll = _interactionMessage.GetComponent<Scroll>();
                    if (scroll != null)
                    {
                        Debug.Log("Deactivating Scroll");
                        scroll.DeactivateScroll();
                    }
                }
                else if (messageType.messageType == MessageType.MessageTypeEnum.Interact)
                {
                    Debug.Log("Deactivating Interact Message");
                    _interactionMessage.SetActive(false);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Interact interactable = other.GetComponent<Interact>();
        if (interactable != null)
        {
            interact = interactable;
            ActiveUI(other.gameObject);
            Debug.Log("Toy cerca de un interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interact interactable = other.GetComponent<Interact>();
        if (interactable != null)
        {
            interact = null;
            DeactivateUI();
        }
    }

}
