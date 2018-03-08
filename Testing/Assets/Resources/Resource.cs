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

    public void Substract ( int value )
    {
        if (Quantity - value < 0)
        {
            print("You do not have enough resources. Please gather some more");
        }
        else
        {
            Quantity -= value;
            OnQuantityChange.Invoke();
        }

    }
}
