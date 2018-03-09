using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public int X;
    public int Y;

    public void MoveTile()
    {
        transform.position = new Vector3(X, 0, Y);
    }
}
