using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {
    public Resource resource;
    public Text text;
    public void Start()
    {
      
    }
    public  void UpdateUI()
    {
        text.text = resource.Quantity.ToString();
    }
}
