using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    //Generics
    public List<Power> list = new List<Power>();

    private void Start()
    {
        CountPower<ShieldBoost>();
        CountPower<SpeedBoost>();
        CountPower<LifeBoost>();
    }

    public int CountPower<T>()
    {
        int count = 0;

        foreach (var p in list)
        {
            if (p is T)
                count++;
        }
        return count;
    }

}
