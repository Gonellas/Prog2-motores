using UnityEngine;

//TPFinal Emi Cassarino
public class Scroll : Interact
{
    [SerializeField] GameObject _scrollMessage;
    [SerializeField] bool _scrollActive = false;

    public override void InteractionAction()
    {
        if (!_scrollActive)
        {
            ActiveScroll();
        }
    }

    public void ActiveScroll()
    {
        _scrollActive = true;
        if (_scrollMessage != null)
        {
            _scrollMessage.SetActive(true);
        }
    }

    public void DeactivateScroll()
    {
        _scrollActive = false;
        if (_scrollMessage != null)
        {
            _scrollMessage.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        DeactivateScroll();
    }
}



