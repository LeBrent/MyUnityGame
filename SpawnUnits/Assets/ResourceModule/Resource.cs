using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    // We can make Quantity private so it can only be changed through the AddAmount() and RemoveAmount() methods
    // By adding [SerializeField] it will still get stored and displayed in the editor (normally this only happens
    // to fields marked Public).
    [SerializeField] private int quantity;
    public int InitialQuantity;
    
    // Unity Events are a Unity implementation of C#-like events that work with
    // Unity's serialization system (meaning you can save them in the editor)
    // Multiple methods can 'Subscribe' to a UnityEvent, and when it is invoked
    // each of these methods are executed.
    public UnityEvent OnValueChanged = new UnityEvent(); 

    void Awake()
    {
        quantity = InitialQuantity;
    }
    
    public void AddAmount(int value)
    {
        quantity += value;
        updateUI();
    }

    public void RemoveAmount(int value)
    {
        quantity -= value;
        updateUI();
    }

    public bool CanAfford(int cost)
    {
        return quantity >= cost;
    }

    // Since we made quantity private, provide access to reading the value here.
    public int GetQuantity()
    {
        return quantity;
    }
    

    void updateUI()
    {
        OnValueChanged.Invoke(); // Invoke the event.
    }
}
