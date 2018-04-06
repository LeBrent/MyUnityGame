using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool attackable = false;

    public List<Tile> adjecenttiles = new List<Tile>();

    //BFS
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (attackable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        adjecenttiles.Clear();
        current = false;
        target = false;
        selectable = false;
        attackable = false;

        //BFS
        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeightbors(float jumpHeight)
    {
        Reset();
        CheckTile(-Vector3.forward, jumpHeight);
        CheckTile(Vector3.forward, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
        CheckTile(-Vector3.right, jumpHeight);

    }

    public void CheckTile(Vector3 direction, float jumpheight)
    {
        Vector3 halfExtends = new Vector3(0.25f, (1 + jumpheight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile !=null && tile.walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjecenttiles.Add(tile);
                }
                //ELSE attackable = true
            }
        }
    }
}
