using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour {
    public int Quantity;
    public UnityEvent OnQuantityChange = new UnityEvent();
	// Use this for initialization

    public void Add( int value )
    {
        Quantity += value;
        OnQuantityChange.Invoke();
    }
}
