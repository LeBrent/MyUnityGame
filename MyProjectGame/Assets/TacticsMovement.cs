using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMovement : MonoBehaviour
{
    public bool turn = false;

    List<Tile> selectables = new List<Tile>();
    List<Tile> attackables = new List<Tile>();
    public GameObject[] tiles;
    TurnManager turnManager;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;
    public int moveRange = 5;
    public int attackRange = 6;
    public float jumpheight;
    public float moveSpeed = 4;
    public float jumpVelocity = 4.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfheight = 0;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    protected void Init(Unit unit)
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfheight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.AddUnit(unit);

        GetCurrentTile();
        unit.transform.parent = currentTile.transform;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void ComputeAdjentList()
    {
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeightbors(jumpheight);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjentList();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        // currentTile.parent == ?? leave parent as null


        while (process.Count > 0)
        {
            Tile t = process.Dequeue();

            if (t.distance < moveRange)
            {
                foreach (Tile tile in t.adjecenttiles)
                {
                    if (!tile.visited)
                    {
                        selectables.Add(t);
                        t.selectable = true;
                        attackables.Add(t);
                        t.attackable = true;

                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }

            if (t.distance >= moveRange && t.distance < attackRange)
            {
                foreach (Tile tile in t.adjecenttiles)
                {
                    if (!tile.visited)
                    {
                        attackables.Add(t);
                        t.attackable = true;

                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            } 
        }
    }
    public void MoveToTarget(Unit unit, Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        unit.transform.parent = tile.transform;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile tile = path.Peek();
            Vector3 target = tile.transform.position;

            target.y += halfheight + tile.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;
                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                //locomotion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
	        {
                //reached tile
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;

            TurnManager.EndTurn();
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        
        foreach (Tile tile in attackables)
        {
            tile.Reset();
        }
        attackables.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownwards(target);
        }
        else if (jumpingUp)
        {
            JumpUpwards(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }

    void FallDownwards(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 position = transform.position;
            position.y = target.y;
            transform.position = position;

            velocity = new Vector3();
        }
    }

    void JumpUpwards(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurnUnit()
    {
        turn = false;
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeightbors(jumpheight);
            t.Reset();
        }
    }
}
