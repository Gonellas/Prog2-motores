using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour, IInteractable
{


    public virtual void ApplyPowerUp()
    {
        Debug.Log("Apply");
    }

    public virtual void Interact()
    {
        Debug.Log("Apply");

    }
}
