using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwned : MonoBehaviour
{
    public Player owner;

    public void Initialize(Player Owner)
    {
        Owner = owner;
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            rend.material.color = owner.color;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
