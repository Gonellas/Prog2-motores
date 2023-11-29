using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    ShieldBoost,
    SpeedBoost,
    LifeBoost
}
public abstract class Power : MonoBehaviour
{
    public PowerUpType Type { get; }
    public abstract void ApplyPowerUp();
}
