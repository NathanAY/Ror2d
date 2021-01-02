using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventHandler(Vector3 position);
    public static event EventHandler OnDeath;
    // public static event EventHandler OnHit;
    
    public static void InvokeOnDeath(Vector3 deathPosition)
    {
        OnDeath?.Invoke(deathPosition);
    }
    
}
