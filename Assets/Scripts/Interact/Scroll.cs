using System.Collections;
using UnityEngine;

public class Scroll : Interact
{
    [SerializeField] GameObject _scrollMessage;
    [SerializeField] bool _scrollActive = false;

    new private void Start()
    {
        base.Start();
    }
    protected override void InteractionAction()
    {
        if (!_scrollActive)
        {
            ActiveScroll();
        }
        else
        {
            DeactivateScroll();
        }
    }
    

    private void ActiveScroll()
    {
        _scrollActive = true;
        _scrollMessage.SetActive(true);
    }

    private void DeactivateScroll()
    {
        _scrollActive = false;
        _scrollMessage.SetActive(false);
    }

}


