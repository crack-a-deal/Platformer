using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour,ICollectible
{
    public static Action OnOrbCollected;
    public void Collect()
    {
        OnOrbCollected?.Invoke();
        Destroy(gameObject);
    }
}
