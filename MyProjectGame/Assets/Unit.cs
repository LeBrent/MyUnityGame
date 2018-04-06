using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : TacticsMovement
{
    public int Health = 100;
    public int Damage = 10;

    void Start()
    {
        Init(this);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTarget(this, t);
                    }
                }

                if (hit.collider.tag == "Player")
                {
                    Tile t = hit.collider.GetComponentInParent<Tile>();
                    Unit unit = hit.collider.GetComponent<Unit>();

                    if (t.attackable && unit != this)
                    {
                        DealDamage(unit, Damage);
                    }
                }
            }
        }
    }

    public void DecreaseHealth(int amount)
    {
        Health -= amount;
        EndTurnUnit();
    }

    public void DealDamage(Unit unit, int amount)
    {
        unit.DecreaseHealth(amount);

        if (unit.Health < 1)
        {
            TurnManager.DeleteUnit(unit);
        }
        TurnManager.EndTurn();
        Update();
    }

}
