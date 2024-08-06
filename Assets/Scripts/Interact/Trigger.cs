using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject _message;

    private void OnTriggerEnter(Collider other)
    {
        IMessage messageHandler = other.GetComponent<IMessage>();

        if (messageHandler != null)
        {
            messageHandler.ActiveUI(_message);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IMessage messageHandler = other.GetComponent<IMessage>();

        if (messageHandler != null)
        {
            messageHandler.DeactivateUI();
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
