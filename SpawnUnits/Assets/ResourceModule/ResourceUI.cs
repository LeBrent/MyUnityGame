using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Resource))]
public class ResourceUI : MonoBehaviour
{
    public Text Value;
    public Text Label;

    private Resource resource;

    private Coroutine valueTicker;
    private int cachedValue; 
    
    
    private void Awake()
    {
        resource = GetComponent<Resource>();
        Label.text = resource.name;
        
        // Let the method 'updateUI()' run when the OnValueChanged UnityEvent is invoked.
        // See the resource class for more info.
        resource.OnValueChanged.AddListener(UpdateUI); 
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // We could just set Value.text to resource.Quantity and be done, but in this
        // version of the UI we made the visual number to increase until it reaches
        // the actual number of the model. To do this we use a Coroutine.
        if (valueTicker != null)
        {
            StopCoroutine(valueTicker);
        }
        //   valueTicker = StartCoroutine(TickVisualValue(resource.GetQuantity()));
        Value.text = resource.GetQuantity().ToString();
    }
    
    //IEnumerator TickVisualValue(int target)
    //{        
    //    while (cachedValue != target)
    //    {
    //        if (cachedValue < target)
    //        {
    //            cachedValue++;
    //        }
    //        else
    //        {
    //            cachedValue--;
    //        }
    //        Value.text = cachedValue.ToString();
    //        // The coroutine will run through this code until it returns a yield.
    //        // Then, it will wait until the next frame and continue from where it stopped.
    //        // In this case it will change the value by one every frame until current and target are the same.
    //        yield return null; 
    //    }
    //}
}
